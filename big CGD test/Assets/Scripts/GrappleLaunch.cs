using UnityEngine;
using System.Collections;

public class GrappleLaunch : MonoBehaviour {

    [SerializeField] Transform grapple;
	Transform currentGrapple;

    bool grappled = false;

    void Update()
    {
        if (Input.GetButtonDown("0Button1"))
        {
            fireGrapple();
        }
	}

    void fireGrapple()
    {
		if(grappled)
		{
			currentGrapple.GetComponent<Grapple> ().removeChildren ();
			Destroy (currentGrapple.gameObject);
		}

		grappled = true;
        currentGrapple = Instantiate(grapple, transform.position + transform.forward, transform.rotation) as Transform;
        currentGrapple.GetComponent<Grapple>().addParent(transform);
    }
}
