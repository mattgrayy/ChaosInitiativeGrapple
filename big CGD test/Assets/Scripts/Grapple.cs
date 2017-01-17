using UnityEngine;
using System.Collections;

public class Grapple : MonoBehaviour {

    Transform myCar;

    Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity += transform.forward * 20;
        rb.velocity += Vector3.up * 3;
    }

    void Update()
    {
        if (myCar != null)
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

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Draggable")
        {
            //GetComponent<Collider>().isTrigger = true;
            //rb.useGravity = false;
            rb.velocity = Vector3.zero;
            GetComponent<SpringJoint>().connectedBody = col.transform.GetComponent<Rigidbody>();
            col.transform.parent = transform;
        }
    }
}
