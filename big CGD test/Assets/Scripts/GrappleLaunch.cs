using UnityEngine;
using System.Collections;

public class GrappleLaunch : MonoBehaviour {

    [SerializeField] Transform grapple;
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
        Transform made = Instantiate(grapple, transform.position + transform.forward, transform.rotation) as Transform;
        made.GetComponent<Grapple>().addParent(transform);
    }
}
