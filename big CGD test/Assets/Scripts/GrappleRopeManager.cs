using UnityEngine;
using System.Collections.Generic;

public class GrappleRopeManager : MonoBehaviour {

    List<GameObject> nodes = new List<GameObject>();

    public GameObject myCar;

	void Update () {

		if (nodes.Count > 3)
        {
            killNodes();
		}
	}

    public void addNode(GameObject node)
    {
        nodes.Add(node);
    }

    public void killNodes()
    {
        foreach (GameObject node in nodes)
        {
            Destroy(node);
        }
        Destroy(gameObject);
    }
}
