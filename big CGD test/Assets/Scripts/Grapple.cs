using UnityEngine;
using System.Collections;

public class Grapple : MonoBehaviour {

    Transform myCar;
	public Transform firstLink;

    Rigidbody rb;
	public bool hit = false;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity += transform.forward * 2;
        rb.velocity += Vector3.up * 3;

		firstLink.GetComponent<ChainLink> ().myCar = myCar;
    }

    void Update()
    {
        if (myCar != null && hit)
        {
            if (Vector3.Distance(transform.position, myCar.position) > 10)
            {
                //rb.useGravity = true;
                rb.velocity += (myCar.position - transform.position) / 10;
            }
            else if (rb.useGravity)
            {
                //rb.useGravity = false;
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
            //GetComponent<Collider>().isTrigger = true;
            //rb.useGravity = false;
            rb.velocity = Vector3.zero;
			gameObject.AddComponent<SpringJoint> ();
            GetComponent<SpringJoint>().connectedBody = col.transform.GetComponent<Rigidbody>();
            col.transform.parent = transform;
			hit = true;
        }
    }
}
