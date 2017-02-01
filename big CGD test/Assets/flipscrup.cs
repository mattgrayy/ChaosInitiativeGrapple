using UnityEngine;
using System.Collections;

public class flipscrup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown("1Button1"))
		{
			flup ();
		}
	}

	void flup()
	{
		GetComponent<Rigidbody>().velocity += Vector3.up * 5;
		GetComponent<Rigidbody> ().AddTorque (transform.forward * 5, ForceMode.VelocityChange);
	}
}
