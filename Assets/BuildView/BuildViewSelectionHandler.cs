// File Name:        BuildViewSelectionHandler.cs
// Description:      
// Dependencies:    
// Additional Notes: N/A

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BuildViewSelectionHandler : MonoBehaviour {

    public Hashtable connectedNodes;
    public List<Node> selectedNodes;
    public List<BuildViewNode> allNodes;
    public GameObject LinkPrefab;
    public GameObject nodePropertyDropdown;
    public GameObject sourceCheckbox;
    public GameObject sinkCheckbox;
    public GameObject swapButton;
    public Color valuesNotUniformColor;
    private Color _originalCheckboxLabelColor;
    private Color _originalDropdownColor;

    // Description: 
    // PRE:         
    // POST:
    public struct ConnectedNodes
    {
        public Node origin, destination;

        // Description: 
        // PRE:         
        // POST:
        public ConnectedNodes(Node origin, Node destination)
        {
            this.origin = origin;
            this.destination = destination;
        }
    }

    // Description: 
    // PRE:         
    // POST:
    void Start()
    {
        selectedNodes = new List<Node>();
        connectedNodes = new Hashtable();
        _originalCheckboxLabelColor = sourceCheckbox.GetComponentInChildren<Text>().color;
        _originalDropdownColor = nodePropertyDropdown.GetComponent<Image>().color;
    }

    // Description: 
    // PRE:         
    // POST:
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
            UpdateNumberOfNodeConnections(nodesToBeDeleted[i], false);
            connectedNodes.Remove(nodesToBeDeleted[i]);
        }
    }

    // Description: 
    // PRE:         
    // POST:
    public void RemoveNode(BuildViewNode exisitingNode)
    {
        selectedNodes.Remove(exisitingNode.node);
        exisitingNode.node.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("BuildNode", typeof(Sprite));
        UpdateNodeInspector();
        UpdateLinkInspector();
    }

    // Description: 
    // PRE:         
    // POST:
    public void AddNode(BuildViewNode newNode)
    {
        if (!selectedNodes.Contains(newNode.node))
        {
            selectedNodes.Add(newNode.node);
            newNode.node.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("BuildViewNode", typeof(Sprite));
            UpdateNodeInspector();
            UpdateLinkInspector();
        }
    }

    // Description: 
    // PRE:         
    // POST:
    public void ClearSelection()
    {
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            selectedNodes[i].GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("BuildNode", typeof(Sprite));
        }
        selectedNodes.Clear();
        UpdateNodeInspector();
    }

    // Description: 
    // PRE:         
    // POST:
    public void Link()
    {
        // Spawn a new link where the origin is the first index,
        // and the secondary node is the second node
        if (selectedNodes.Count >= 2)
        {
            for (int i = 1; i < selectedNodes.Count; i++)
            {
                // Keys for our hash table, connectedNodes
                ConnectedNodes nodePair = new ConnectedNodes(selectedNodes[i - 1], selectedNodes[i]);
                ConnectedNodes reversedPair = new ConnectedNodes(selectedNodes[i], selectedNodes[i - 1]);
                
                if(!connectedNodes.Contains(nodePair))
                {
                    if (connectedNodes.Contains(reversedPair))
                    {
                        Destroy((GameObject) connectedNodes[reversedPair]);
                        connectedNodes.Remove(reversedPair);
                        UpdateNumberOfNodeConnections(reversedPair, false);
                    }

                    GameObject newLink = CreateLink(nodePair);
                    connectedNodes.Add(nodePair, newLink);
                    UpdateNumberOfNodeConnections(nodePair, true);
                }                
            }

            this.ClearSelection();
        }
        UpdateLinkInspector();
    }

    // Description: 
    // PRE:         
    // POST:
    private void UpdateNumberOfNodeConnections(ConnectedNodes nodePair, bool isAddingOneConnection)
    {
        int i = isAddingOneConnection ? 1 : 0;
        nodePair.origin.NumberOfConnections += 2 * i - 1;
        nodePair.destination.NumberOfConnections += 2 * i - 1;
    }

    // Description: 
    // PRE:         
    // POST:
    private GameObject CreateLink(ConnectedNodes nodePair)
    {
        GameObject newLink = Instantiate(LinkPrefab);
        newLink.tag = "Link";
        BuildViewLink linkScript = newLink.GetComponent<BuildViewLink>();
        linkScript.origin = nodePair.origin;
        linkScript.destination = nodePair.destination;
        return newLink;
    }

    // Description: 
    // PRE:         
    // POST:
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
        UpdateNodeInspector();
    }

    // Description: 
    // PRE:         
    // POST:
    public void SetSink(bool value)
    {
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            selectedNodes[i].IsSink = value;
        }
        UpdateNodeInspector();
    }

    // Description: 
    // PRE:         
    // POST:
    public void SetSource(bool value)
    {
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            selectedNodes[i].IsSource = value;
        }
        UpdateNodeInspector();
    }

    // Description: 
    // PRE:         
    // POST:
    public void Swap()
    {
        Debug.Log("This will swap the direction of the link.");
    }

    // Description: 
    // PRE:         
    // POST:
    private void UpdateLinkInspector()
    {
        Debug.Log(connectedNodes.Count);
        /*if (there is a link selected)
        {
            swapButton.GetComponent<Button>().interactable = true;
        }*/
    }

    // Description: 
    // PRE:         
    // POST:
    private void UpdateNodeInspector()
    {
        if (selectedNodes.Count <= 0)
        {
            nodePropertyDropdown.GetComponent<Dropdown>().interactable = false;
            nodePropertyDropdown.GetComponent<Image>().color = _originalDropdownColor;
            sinkCheckbox.GetComponent<Toggle>().interactable = false;
            sinkCheckbox.GetComponentInChildren<Text>().color = _originalCheckboxLabelColor;
            sourceCheckbox.GetComponent<Toggle>().interactable = false;
            sourceCheckbox.GetComponentInChildren<Text>().color = _originalCheckboxLabelColor;
        }
        else if (selectedNodes.Count == 1)
        {
            nodePropertyDropdown.GetComponent<Dropdown>().interactable = true;
            nodePropertyDropdown.GetComponent<Dropdown>().value = (int) selectedNodes[0].NodeProperty;
            nodePropertyDropdown.GetComponent<Image>().color = _originalDropdownColor;
            sinkCheckbox.GetComponent<Toggle>().interactable = true;
            sinkCheckbox.GetComponent<Toggle>().isOn = selectedNodes[0].IsSink;
            sinkCheckbox.GetComponentInChildren<Text>().color = _originalCheckboxLabelColor;
            sourceCheckbox.GetComponent<Toggle>().interactable = true;
            sourceCheckbox.GetComponent<Toggle>().isOn = selectedNodes[0].IsSource;
            sourceCheckbox.GetComponentInChildren<Text>().color = _originalCheckboxLabelColor;
        }
        else //implied (selectedNodes.Count > 1)
        {
            bool firstSinkValue = selectedNodes[0].IsSink;
            bool uniformSinkValues = true;
            bool firstSourceValue = selectedNodes[0].IsSource;
            bool uniformSourceValues = true;
            Node.NodeType firstNodeType = selectedNodes[0].NodeProperty;
            bool uniformNodeTypes = true;

            for (int i = 1; i < selectedNodes.Count && (uniformSinkValues || uniformSourceValues || uniformNodeTypes); i++)
            {
                if (uniformSinkValues && firstSinkValue != selectedNodes[i].IsSink)
                {
                    uniformSinkValues = false;
                }

                if (uniformSourceValues && firstSourceValue != selectedNodes[i].IsSource)
                {
                    uniformSourceValues = false;
                }

                if (uniformNodeTypes && firstNodeType != selectedNodes[i].NodeProperty)
                {
                    uniformNodeTypes = false;
                }
            }
            
            if (!uniformSinkValues)
            {
                sinkCheckbox.GetComponentInChildren<Text>().color = valuesNotUniformColor;
            }
            if (!uniformSourceValues)
            {
                sourceCheckbox.GetComponentInChildren<Text>().color = valuesNotUniformColor;
            }
            if (!uniformNodeTypes)
            {
                nodePropertyDropdown.GetComponent<Image>().color = valuesNotUniformColor;
            }

        }
    }
}
