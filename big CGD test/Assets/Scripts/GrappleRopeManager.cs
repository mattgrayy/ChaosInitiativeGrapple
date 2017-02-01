using UnityEngine;
using System.Collections;

public class GrappleRopeManager : MonoBehaviour {


	public GameObject myCar;
	public int numberOfNodes;
	public bool killAll = false;


	//lenght of rope
	//all the nodes (it knows the grapple as its attached to it)


	void Update () {

		if (numberOfNodes > 3) {
			killAll = true;
		}


	}


}
