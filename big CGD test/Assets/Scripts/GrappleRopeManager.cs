using UnityEngine;
using System.Collections.Generic;

public class GrappleRopeManager : MonoBehaviour {

    public List<GameObject> nodes = new List<GameObject>();

    public GameObject myCar;
    public GameObject lastNode;

	void Update () {

		if (nodes.Count > 10)
        {
            killAllNodes();
		}
	}

    public float calculateRopeLength()
    {
        float length = 0;

        foreach (GameObject node in nodes)
        {
            length += node.GetComponent<NodeControler>().ropeLength;
        }
        length += GetComponent<NodeControler>().ropeLength;

        return length;
    }

    public void addNode(GameObject node)
    {
        nodes.Add(node);
    }

    public void setLastNode(GameObject node)
    {
        lastNode = node;
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
        int targetIndex = 0;
        for (int i = 0; i < nodes.Count; i++)
        {
          
            if (nodes[i] == TN)
            {
                targetIndex = i;
               
            }
        }
        Destroy(nodes[targetIndex]);
        nodes.RemoveAt(targetIndex);
    }
}
