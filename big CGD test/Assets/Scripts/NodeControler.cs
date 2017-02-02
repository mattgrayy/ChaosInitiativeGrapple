using UnityEngine;
using System.Collections;

public class NodeControler : MonoBehaviour {


	public GameObject target;
	public GameObject node;
	public bool lastNode = true;
	public LineRenderer rope;
	public GrappleRopeManager myMan;

	void Update () {

        if (target != null)
        {
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, target.transform.position);


            RaycastHit hit;
            //ray to target
            if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit))
            {



                if (hit.collider.gameObject != target && hit.collider.tag != "car")
                {
                    Vector3 spawnPoint = hit.point - transform.position;
                    spawnPoint = Vector3.Reflect(spawnPoint, hit.normal);

                    spawnPoint = (spawnPoint / 5) + hit.point;


                 
                    spawnPoint.y = hit.point.y;



                    //create a node at hit position
                    GameObject thing = Instantiate(node, spawnPoint, Quaternion.identity) as GameObject;
                    myMan.addNode(thing);
                    thing.GetComponent<NodeControler>().myMan = myMan;
                    thing.transform.parent = hit.transform;
                    thing.GetComponent<NodeControler>().target = target;
                    target = thing;

                    if (lastNode)
                    {
                        thing.GetComponent<NodeControler>().lastNode = true;
                        lastNode = false;
                    }
                }

            }
            if (!lastNode &&target.GetComponent<NodeControler>().target != null)
            {
                //		//ray to target's target
                if (Physics.Raycast(transform.position, target.GetComponent<NodeControler>().target.transform.position - transform.position, out hit))
                {

                    if (hit.collider.gameObject == target.GetComponent<NodeControler>().target)
                    {

                        if (target.GetComponent<NodeControler>().lastNode)
                        {
                            lastNode = true;
                                }
                        GameObject TT = target.GetComponent<NodeControler>().target;
                        // call delete on the target
                        myMan.GetComponent<GrappleRopeManager>().killNode(target);

                        target = TT;
                    }
                }
            }
        }
            }


}
