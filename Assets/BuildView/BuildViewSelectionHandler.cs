using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BuildViewSelectionHandler : MonoBehaviour {

    public List<BuildViewNode> allNodes;

    public GameObject LinkPrefab;

    private List<Node> selectedNodes;

    public GameObject nodePropertyDropdown;
    public GameObject sourceCheckbox;
    public GameObject sinkCheckbox;

    void Start()
    {
        selectedNodes = new List<Node>();
    }


    public void AddNode(BuildViewNode newNode)
    {
        selectedNodes.Add(newNode.node);

        //change this later?
        nodePropertyDropdown.GetComponent<Dropdown>().interactable = true;
        sinkCheckbox.GetComponent<Toggle>().interactable = true;
        sourceCheckbox.GetComponent<Toggle>().interactable = true;
    }

    public void ClearSelection()
    {
        // Set all node textures to deselected texture
        selectedNodes.Clear();
        nodePropertyDropdown.GetComponent<Dropdown>().interactable = false;
        sinkCheckbox.GetComponent<Toggle>().interactable = false;
        sourceCheckbox.GetComponent<Toggle>().interactable = false;
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
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            switch (id)
            {
                case 0:
                    selectedNodes[i].NodeProperty = Node.NodeType.None;
                    break;

                case 1:
                    selectedNodes[i].NodeProperty = Node.NodeType.StopSign;
                    break;

                case 2:
                    selectedNodes[i].NodeProperty = Node.NodeType.TrafficLight;
                    break;
            }
            
        }
    }

}
