using UnityEngine;
using System.Collections;

public class CarTurn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Input.GetAxis("XAxis1"));
    }
}
