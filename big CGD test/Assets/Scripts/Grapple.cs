using UnityEngine;
using System.Collections;

public class Grapple : MonoBehaviour {

    Transform myCar;
    [SerializeField]
    Transform chainLink;

    Rigidbody rb;
	public bool hit = false;
	public GrappleRopeManager myRopeManager;

	public float maxDistance;
    float distance = 0;

	void Start ()
    {

		myRopeManager.myCar = myCar.gameObject;
		this.GetComponent<NodeControler> ().target = myCar.gameObject;

        rb = GetComponent<Rigidbody>();

		rb.velocity += transform.forward * 8 + myCar.GetComponent<Rigidbody>().velocity;
        rb.velocity += Vector3.up * 3;
    }

    void FixedUpdate()
    {
        //drop if we reach max rope length
		if (hit == false && Vector3.Distance (transform.position, myCar.position) > maxDistance)
        {
			rb.velocity = Vector3.down * 2;
		}

        float ropeLength = myRopeManager.calculateRopeLength();
        

		if (distance != 0 && ropeLength > distance)
        {
            //Vector3 targToCar = myCar.position - transform.parent.position;
            //targToCar = new Vector3(targToCar.x / 2, targToCar.y / 2, targToCar.z / 2);
            //transform.parent.GetComponent<Rigidbody>().velocity += targToCar;

            // this is the car that launched the grapples' nearest node on the rope.
            GameObject nearestNode = myRopeManager.lastNode;

            Vector3 carToNearestNode = nearestNode.transform.position - myCar.position;
            carToNearestNode = new Vector3(carToNearestNode.x / 2, carToNearestNode.y / 2, carToNearestNode.z / 2);
            myCar.GetComponent<Rigidbody>().velocity += carToNearestNode * 100;
        }
    }

    public void addParent(Transform car)
    {
        myCar = car;
    }

	public void removeChildren()
	{
		foreach(Transform child in transform)
		{
			child.parent = null;
		}
	}

    void OnCollisionEnter(Collision col)
    {
        if (!hit)
        {
			hit = true;
            rb.isKinematic = true;
            GetComponent<Collider>().isTrigger = true;
            transform.parent = col.transform;

            distance = Vector3.Distance(transform.position, myCar.position);

            //makeChain();
        }
    }

    void makeChain()
    {
        int dist = Mathf.RoundToInt(Vector3.Distance(transform.position, myCar.position));

        Quaternion rot =  Quaternion.LookRotation(myCar.position - transform.position, transform.up);
        Vector3 dir = myCar.position - transform.position;
        dir.Normalize();

        Transform lastMade = null;

        for (int i = 0; i < dist; i++)
        {
            Vector3 thisDir = dir * i;

            Vector3 mod = dir / 4;

            Transform made = Instantiate(chainLink, transform.position + (thisDir), rot * chainLink.rotation) as Transform;
            made.parent = transform;
            made.name = i + " - 1";

            if (lastMade != null)
            {
                lastMade.GetComponent<ConfigurableJoint>().connectedBody = made.GetComponent<Rigidbody>();
            }
            else
            {
                ConfigurableJoint hng = gameObject.AddComponent<ConfigurableJoint>();
                hng.connectedBody = made.GetComponent<Rigidbody>();
                hng.anchor = Vector3.zero;
                hng.xMotion = ConfigurableJointMotion.Locked;
                hng.yMotion = ConfigurableJointMotion.Locked;
                hng.zMotion = ConfigurableJointMotion.Locked;
                hng.angularZMotion = ConfigurableJointMotion.Locked;
                hng.projectionMode = JointProjectionMode.PositionAndRotation;
            }
            lastMade = made;

            made = Instantiate(chainLink, transform.position + (thisDir + mod), rot * chainLink.rotation) as Transform;
            made.parent = transform;
            made.name = i + " - 2";

            lastMade.GetComponent<ConfigurableJoint>().connectedBody = made.GetComponent<Rigidbody>();
            lastMade = made;

            made = Instantiate(chainLink, transform.position + (thisDir + (mod * 2)), rot * chainLink.rotation) as Transform;
            made.parent = transform;
            made.name = i + " - 3";

            lastMade.GetComponent<ConfigurableJoint>().connectedBody = made.GetComponent<Rigidbody>();
            lastMade = made;

            made = Instantiate(chainLink, transform.position + (thisDir + (mod * 3)), rot * chainLink.rotation) as Transform;
            made.parent = transform;
            made.name = i + " - 4";

            lastMade.GetComponent<ConfigurableJoint>().connectedBody = made.GetComponent<Rigidbody>();
            lastMade = made;

            if (i == dist - 1)
            {
                lastMade.parent = myCar;
                lastMade.GetComponent<Rigidbody>().isKinematic = true;
                lastMade.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
