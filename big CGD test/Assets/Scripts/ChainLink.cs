using UnityEngine;
using System.Collections;

public class ChainLink : MonoBehaviour {

	public Transform chainLink;
	public Transform myCar;

	bool endChain = true;

	// Use this for initialization
	void Start () {
	
	}

	void Update ()
	{
		if(endChain && myCar != null)
		{
			if(Vector3.Distance(transform.position, myCar.position + myCar.forward) > 0.01f)
			{
				Transform made = Instantiate (chainLink, myCar.position + myCar.forward, chainLink.rotation) as Transform;
				made.parent = transform;
				//made.GetComponent<ChainLink> ().myCar = myCar;
				endChain = false;
			}
		}
	}
}