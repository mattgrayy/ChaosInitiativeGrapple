using UnityEngine;
using System.Collections;

public class NodeControler : MonoBehaviour {


	public GameObject target;
	public GameObject node;
	public bool lastNode = true;
	public LineRenderer rope;
	public GrappleRopeManager myMan;

    public float ropeLength = 0;

	void Update () {

        if (target != null)
        {
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, target.transform.position);
            ropeLength = Vector3.Distance(transform.position, target.transform.position);

            RaycastHit hit;
            //ray to target
            if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit))
            {
                if (hit.collider.gameObject != target && hit.collider.tag != "car")
                {
                    // bounce the swapn position off the normal of the object we hit a little bit (0.2 of a unit)
                    Vector3 spawnPoint = (Vector3.Reflect(hit.point - transform.position, hit.normal) / 10) + hit.point;
                    spawnPoint.y = hit.point.y;

                    //create a node at hit position
                    GameObject thing = Instantiate(node, spawnPoint, Quaternion.identity) as GameObject;
                    thing.transform.parent = hit.transform;
                    thing.GetComponent<NodeControler>().myMan = myMan;
                    thing.GetComponent<NodeControler>().target = target;

                    target = thing;

                    if (lastNode)
                    {
                        thing.GetComponent<NodeControler>().lastNode = true;
                        myMan.setLastNode(thing);
                        lastNode = false;
                    }

                    myMan.addNode(thing);
                }

            }

            if (!lastNode &&target.GetComponent<NodeControler>().target != null)
            {
                //ray to target's target
                if (Physics.Raycast(transform.position, target.GetComponent<NodeControler>().target.transform.position - transform.position, out hit))
                {

                    if (hit.collider.gameObject == target.GetComponent<NodeControler>().target)
                    {
                        if (target.GetComponent<NodeControler>().lastNode)
                        {
                            lastNode = true;
                            myMan.setLastNode(gameObject);
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
