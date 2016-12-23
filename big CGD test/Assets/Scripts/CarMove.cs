using UnityEngine;
using System.Collections;

public class CarMove : MonoBehaviour {

    Rigidbody rb;

    float maxForwardMotion = 10;
    float forwardMotion = 0;

	// Use this for initialization
	void Start () {
        rb = transform.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("0Button1") && forwardMotion < maxForwardMotion)
        {
            forwardMotion += 0.03f;
        }

        if (forwardMotion != 0)
        {
            rb.velocity = Vector3.zero;
            rb.velocity += transform.forward * forwardMotion;

            if (forwardMotion > 0)
            {
                forwardMotion -= 0.003f;
            }
            else
            {
                forwardMotion += 0.003f;
            }

            if ((forwardMotion < 0.002f && forwardMotion > -0.002f) || (rb.velocity.x + rb.velocity.y + rb.velocity.z < 0.002f && rb.velocity.x - rb.velocity.y - rb.velocity.z > -0.002f))
            {
                forwardMotion = 0;
            }
        }

        transform.RotateAround(transform.position, transform.up, Input.GetAxis("XAxis1"));
    }
}
