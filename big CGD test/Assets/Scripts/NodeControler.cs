using UnityEngine;
using System.Collections;

public class NodeControler : MonoBehaviour {


	public GameObject target;
	public GameObject node;
	public bool lastNode = true;
	public LineRenderer rope;
	public GrappleRopeManager myMan;

	void Update () {

		if (myMan.killAll) {

			if (lastNode) {
				target.GetComponent<GrappleLaunch> ().resetGrappled ();
			}

			Destroy (this.gameObject);
		}


		if (target != null)
		{
		rope.SetPosition (0, transform.position);
		rope.SetPosition (1, target.transform.position);


		RaycastHit hit;
		//ray to target
			if (Physics.Raycast (transform.position, target.transform.position - transform.position, out hit, 15f)) {



					if (hit.collider.gameObject != target && hit.collider.tag != "car") {

					Vector3 spawnPoint = (Vector3.Normalize (hit.point - hit.transform.position)/5) + hit.point;
					spawnPoint.y = hit.point.y;
					Debug.Log (hit.collider.name);

					//create a node at hit position
					GameObject thing = Instantiate (node, spawnPoint, Quaternion.identity) as GameObject;
					myMan.numberOfNodes++;
					thing.GetComponent<NodeControler> ().myMan = myMan;
					thing.transform.parent = hit.transform;
					thing.GetComponent<NodeControler> ().target = target;
					target = thing;

					if (lastNode) {
						thing.GetComponent<NodeControler> ().lastNode = true;
						lastNode = false;
					}
				}
			
		}

//		//ray to target's target
//		if (Physics.Raycast (transform.position, target.GetComponent<NodeControler>().target.transform.position - transform.position, out hit, 15f)) {
//
//			if (hit.collider.gameObject == target.GetComponent<NodeControler>().target) {
//
//				//call delete on the target
//
//
//
//			}
//		}
		}
	}


}
