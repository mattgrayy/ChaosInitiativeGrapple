using UnityEngine;
using System.Collections;

public class Grapple : MonoBehaviour {

    Transform myCar;
    [SerializeField]
    Transform chainLink;

    Rigidbody rb;
	public bool hit = false;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity += transform.forward * 20;
        rb.velocity += Vector3.up * 3;
    }

    void Update()
    {
        if (myCar != null && hit)
        {
            if (Vector3.Distance(transform.position, myCar.position) > 10)
            {
                //rb.velocity += (myCar.position - transform.position) / 10;
            }
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
        if (col.transform.tag == "Draggable" && !hit)
        {
			hit = true;
            rb.isKinematic = true;
            transform.parent = col.transform;
            ConfigurableJoint hng = gameObject.AddComponent<ConfigurableJoint>();
            hng.connectedBody = col.transform.GetComponent<Rigidbody>();
            hng.xMotion = ConfigurableJointMotion.Locked;
            hng.yMotion = ConfigurableJointMotion.Locked;
            hng.zMotion = ConfigurableJointMotion.Locked;

            JointDrive drive = new JointDrive();
            drive.positionSpring = 20;
            hng.xDrive = drive;
            hng.yDrive = drive;
            hng.zDrive = drive;

            makeChain();
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
                hng.xMotion = ConfigurableJointMotion.Locked;
                hng.yMotion = ConfigurableJointMotion.Locked;
                hng.zMotion = ConfigurableJointMotion.Locked;
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
