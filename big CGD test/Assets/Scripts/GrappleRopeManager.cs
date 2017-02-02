using UnityEngine;
using System.Collections.Generic;

public class GrappleRopeManager : MonoBehaviour {

    List<GameObject> nodes = new List<GameObject>();

    public GameObject myCar;

	void Update () {

		if (nodes.Count > 10)
        {
            killAllNodes();
		}
	}

    public void addNode(GameObject node)
    {
        nodes.Add(node);
    }

    public void killAllNodes()
    {
        foreach (GameObject node in nodes)
        {
            Destroy(node);
        }
        Destroy(gameObject);
    }

    public void killNode(GameObject TN)
    {
        int u=0;
        for (int c = 0; c<nodes.Count; c++)
        {
          
            if (nodes[c] == TN)
            {
                u = c;
               
            }
        }
        Destroy(nodes[u]);
        nodes.RemoveAt(u);
    }
}
