using UnityEngine;
using System.Collections.Generic;

public class BuildViewSelectionHandler : MonoBehaviour {

	public List<BuildViewNode> allNodes;

    public GameObject LinkPrefab;

    private List<BuildViewNode> selectedNodes;

    public GameObject nodePropertyDropdown;
    public GameObject sourceCheckbox;
    public GameObject sinkCheckbox;

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
			for (int i = 1; i < selectedNodes.Count; i++) {
				if (selectedNodes[i - 1] != selectedNodes[i]) {
					GameObject newLink = Instantiate(LinkPrefab);
					
					BuildViewLink linkScript = newLink.GetComponent<BuildViewLink>();
					
					linkScript.origin = selectedNodes[i - 1];
					linkScript.destination = selectedNodes[i];
				}
			}
			selectedNodes.Clear();
        }
        else
        {
            Debug.LogError("Cannot instatiate a link with only less than 2 nodes selected.");
        }
    }

    public void SetProperty(int id)
    {
        Debug.Log("this " + id);
    }

}
