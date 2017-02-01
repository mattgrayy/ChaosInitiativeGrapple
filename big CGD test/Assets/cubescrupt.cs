using UnityEngine;
using System.Collections;

public class cubescrupt : MonoBehaviour {

	bool up = true;

	// Update is called once per frame
	void Update ()
	{
		if(transform.position.y > 10 && up)
		{
			up = false;
		}
		if(transform.position.y < 0.5f && !up)
		{
			up = true;
		}

		if (up)
		{
			transform.Translate (Vector3.up * Time.deltaTime);
		}
		else
		{
			transform.Translate (-Vector3.up * Time.deltaTime);
		}
	}
}
