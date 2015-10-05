using UnityEngine;
using System.Collections.Generic;

public class BuildViewSelectionHandler : MonoBehaviour {

    public GameObject LinkPrefab;

    private List<BuildViewNode> selectedNodes;

    void Start()
    {
        selectedNodes = new List<BuildViewNode>();
    }

    public void AddNode(BuildViewNode newNode)
    {
        Debug.Log("A node was added.");
        selectedNodes.Add(newNode);
    }

    public void ClearSelection()
    {
        selectedNodes.Clear();
    }

    public void Link()
    {
        // spawn a new link where the origin is the first index,
        // and the secondary node is the second node
        if (selectedNodes.Count >= 2)
        {
            GameObject newLink = Instantiate(LinkPrefab);

            BuildViewLink linkScript = newLink.GetComponent<BuildViewLink>();

            linkScript.origin = selectedNodes[0];
            linkScript.destination = selectedNodes[1];
        }
        else
        {
            Debug.LogError("Cannot instatiate a link with only less than 2 nodes selected.");
        }
    }
}
