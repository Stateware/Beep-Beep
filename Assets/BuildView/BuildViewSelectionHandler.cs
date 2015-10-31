using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BuildViewSelectionHandler : MonoBehaviour {

    private Hashtable connectedNodes;
    private List<Node> selectedNodes;
    public List<BuildViewNode> allNodes;
    public GameObject LinkPrefab;
    public GameObject nodePropertyDropdown;
    public GameObject sourceCheckbox;
    public GameObject sinkCheckbox;

    private struct ConnectedNodes
    {
        public Node origin, destination;

        public ConnectedNodes(Node origin, Node destination)
        {
            this.origin = origin;
            this.destination = destination;
        }
    }

    void Start()
    {
        selectedNodes = new List<Node>();
        connectedNodes = new Hashtable();
    }

    public void DeleteNodeInstances(BuildViewNode existingNode)
    {
        List<ConnectedNodes> nodesToBeDeleted = new List<ConnectedNodes>();
        Node node = existingNode.node;
        selectedNodes.Remove(existingNode.node);
              
        foreach(ConnectedNodes cn in connectedNodes.Keys)
        {
            if(cn.origin == node || cn.destination == node)
                nodesToBeDeleted.Add(cn);
        }

        for(int i = nodesToBeDeleted.Count - 1; i >= 0; i--)
        {
            Destroy((GameObject) connectedNodes[nodesToBeDeleted[i]]);
            updateNumberOfNodeConnections(nodesToBeDeleted[i], false);
            connectedNodes.Remove(nodesToBeDeleted[i]);
        }
    }

    public void AddNode(BuildViewNode newNode)
    {
        if (!selectedNodes.Contains(newNode.node))
        {
            selectedNodes.Add(newNode.node);
            newNode.node.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("BuildViewNode", typeof(Sprite));
            ChangeGuiToggleSetting(true);
        }
    }

    public void ClearSelection()
    {
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            selectedNodes[i].GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("BuildNode", typeof(Sprite));
        }
        selectedNodes.Clear();
        ChangeGuiToggleSetting(false);
    }

    public void Link()
    {
        // spawn a new link where the origin is the first index,
        // and the secondary node is the second node
        if (selectedNodes.Count >= 2)
        {
            for (int i = 1; i < selectedNodes.Count; i++)
            {
                //keys for our hash table, connectedNodes
                ConnectedNodes nodePair = new ConnectedNodes(selectedNodes[i - 1], selectedNodes[i]);
                ConnectedNodes reversedPair = new ConnectedNodes(selectedNodes[i], selectedNodes[i - 1]);
                
                if(!connectedNodes.Contains(nodePair))
                {
                    if (connectedNodes.Contains(reversedPair))
                    {
                        Destroy((GameObject) connectedNodes[reversedPair]);
                        connectedNodes.Remove(reversedPair);
                        updateNumberOfNodeConnections(reversedPair, false);
                    }

                    GameObject newLink = CreateLink(nodePair);
                    connectedNodes.Add(nodePair, newLink);
                    updateNumberOfNodeConnections(nodePair, true);
                }                
            }

            this.ClearSelection();
        }
    }

    private void updateNumberOfNodeConnections(ConnectedNodes nodePair, bool isAddingOneConnection)
    {
        int i = isAddingOneConnection ? 1 : 0;
        nodePair.origin.NumberOfConnections += 2 * i - 1;
        nodePair.destination.NumberOfConnections += 2 * i - 1;
    }

    private GameObject CreateLink(ConnectedNodes nodePair)
    {
        GameObject newLink = Instantiate(LinkPrefab);
        BuildViewLink linkScript = newLink.GetComponent<BuildViewLink>();
        linkScript.origin = nodePair.origin;
        linkScript.destination = nodePair.destination;
        return newLink;
    }

    private void ChangeGuiToggleSetting(bool isInteractable)
    {
        nodePropertyDropdown.GetComponent<Dropdown>().interactable = isInteractable;
        sinkCheckbox.GetComponent<Toggle>().interactable = isInteractable;
        sourceCheckbox.GetComponent<Toggle>().interactable = isInteractable;
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
