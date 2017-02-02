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

		if (Input.GetKeyDown(KeyCode.P))
		{
			fireGrapple();
		}


	}

    void fireGrapple()
    {
		if (grappled) {
			if (currentGrapple != null)
            {
                currentGrapple.GetComponent<GrappleRopeManager>().killNodes();
                currentGrapple = null;
			}
			grappled = false;
		} else {
			grappled = true;
			currentGrapple = Instantiate (grapple, transform.parent.position + transform.parent.forward + (transform.parent.up/5), transform.parent.rotation) as Transform;
			currentGrapple.GetComponent<Grapple> ().addParent (transform);
		}
    }

	public void resetGrappled()
	{
		grappled = false;
	}

}
