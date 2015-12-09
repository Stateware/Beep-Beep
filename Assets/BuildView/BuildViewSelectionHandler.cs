// Copyright (c) 2015 Stateware Team -- Licensed GPL v3
// File Name:        BuildViewSelectionHandler.cs
// Description:      This files handles the majority of UI interaction in the BuildView, in
//                   addition to much of the logic that edits the data model.
// Dependencies:    
// Additional Notes: N/A

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BuildViewSelectionHandler : MonoBehaviour
{
    private Color _originalCheckboxLabelColor;
    private Color _originalDropdownColor;

    public Hashtable connectedNodes;
    public List<Node> selectedNodes;
    public List<BuildViewNode> allNodes;
    public GameObject LinkPrefab;
    public GameObject nodePropertyDropdown;
    public GameObject sourceCheckbox;
    public GameObject sinkCheckbox;
    public GameObject swapButton;
    public Color valuesNotUniformColor;

    // Description: This is a helper class that holds two nodes. It
    //              is the base for what the ConnectedNodes hashtable uses.
    public struct ConnectedNodes
    {
        public Node origin, destination;

        // Description: Constructs a ConnectedNode
        // PRE:         N/A
        // POST:        A ConnectedNodes object is created.
        public ConnectedNodes(Node origin, Node destination)
        {
            this.origin = origin;
            this.destination = destination;
        }
    }

    // Description: This is the Unity API function that is called before the first frame is drawn.
    // PRE:         There is a text component in the sourceCheckbox and an image in the nodePropertyDropdown.
    //              In addition, both of those should also be set in the inspector in the scene.
    // POST:        The proper data structures are initialized and the default UI colors are stored.
    void Start()
    {
        selectedNodes = new List<Node>();
        connectedNodes = new Hashtable();
        _originalCheckboxLabelColor = sourceCheckbox.GetComponentInChildren<Text>().color;
        _originalDropdownColor = nodePropertyDropdown.GetComponent<Image>().color;
    }

    // Description: Deletes a node and ensures all connected links are also deleted.
    // PRE:         existingNode is a node that exists in the scene
    // POST:        The node and it's connected links are removed/deleted
    public void DeleteNodeInstances(BuildViewNode existingNode)
    {
        List<ConnectedNodes> nodesToBeDeleted = new List<ConnectedNodes>();
        Node node = existingNode.node;
        selectedNodes.Remove(existingNode.node);
              
        foreach(ConnectedNodes cn in connectedNodes.Keys)
        {
            if (cn.origin == node || cn.destination == node)
            {
                nodesToBeDeleted.Add(cn);
            }
        }

        for(int i = nodesToBeDeleted.Count - 1; i >= 0; i--)
        {
            Destroy((GameObject) connectedNodes[nodesToBeDeleted[i]]);
            UpdateNumberOfNodeConnections(nodesToBeDeleted[i], false);
            connectedNodes.Remove(nodesToBeDeleted[i]);
        }
    }

    // Description: Removes a node from the list of selected nodes
    // PRE:         The node given exists in selectedNodes and has a SpriteRenderer
    // POST:        The node will no longer exist in selectedNodes and the UI
    //              will accurately reflect this change
    public void RemoveNode(BuildViewNode exisitingNode)
    {
        selectedNodes.Remove(exisitingNode.node);
        exisitingNode.node.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("BuildNode", typeof(Sprite));
        UpdateNodeInspector();
        UpdateLinkInspector();
    }

    // Description: Adds a node to the list of selected nodes and reflects these updates in the UI
    // PRE:         The node given exists in the scene and has a SpriteRenderer
    // POST:        The node is in the list of selected nodes
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

    // Description: Removes all the selected nodes from selection list and reflects these updates in the UI
    // PRE:         The nodes have a SpriteRenderer
    // POST:        The list of selected nodes is empty
    public void ClearSelection()
    {
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            selectedNodes[i].GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("BuildNode", typeof(Sprite));
        }
        selectedNodes.Clear();
        UpdateNodeInspector();
    }

    // Description: Adds a link between every node in order that they were selected
    // PRE:         N/A
    // POST:        There exists a link between every node selected (n-1 links)
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

    // Description: Updates the number of connections in the BuildViewNode's information
    // PRE:         The given nodepair has an origin and a destination nodes
    // POST:        The number of connections for these nodes has been updated
    private void UpdateNumberOfNodeConnections(ConnectedNodes nodePair, bool isAddingOneConnection)
    {
        int i = isAddingOneConnection ? 1 : 0;
        nodePair.origin.NumberOfConnections += 2 * i - 1;
        nodePair.destination.NumberOfConnections += 2 * i - 1;
    }

    // Description: Creates the Link object to be placed between two nodes
    // PRE:         nodePair contains two nodes
    // POST:        There exists a link between each of the nodes in nodePair
    private GameObject CreateLink(ConnectedNodes nodePair)
    {
        GameObject newLink = Instantiate(LinkPrefab);
        newLink.tag = "Link";
        BuildViewLink linkScript = newLink.GetComponent<BuildViewLink>();
        linkScript.origin = nodePair.origin;
        linkScript.destination = nodePair.destination;
        return newLink;
    }

    // Description: Sets the property of every node in the list of selected nodes
    // PRE:         id is between 0 and 2 inclusive
    // POST:        All of the selected ndoes have a property that matches the given id
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

    // Description: Sets the value of all of the selected nodes' sink attribute to the given value
    // PRE:         N/A
    // POST:        All of the selected nodes have matching Sink values that were given
    public void SetSink(bool value)
    {
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            selectedNodes[i].IsSink = value;
        }
        UpdateNodeInspector();
    }

    // Description: Sets the value of all the selected nodes' source attribute to the given value
    // PRE:         N/A
    // POST:        All of the selected nodes have matching Source values that were given
    public void SetSource(bool value)
    {
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            selectedNodes[i].IsSource = value;
        }
        UpdateNodeInspector();
    }

    // Description: Will swap the direction of the links that exist between the selected nodes
    // PRE:         N/A
    // POST:        N/A
    public void Swap()
    {
        Debug.Log("This will swap the direction of the link.");
    }

    // Description: Updates all link related fields in the inspector
    // PRE:         N/A
    // POST:        The link related fields in the inspector accurately reflect the data
    private void UpdateLinkInspector()
    {
        Debug.Log(connectedNodes.Count);
        /*if (there is a link selected)
        {
            swapButton.GetComponent<Button>().interactable = true;
        }*/
    }

    // Description: Updates all the node related fields in the inspector
    // PRE:         N/A
    // POST:        The node related fields in the inspector accurately reflect the data
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

    // Description: 
    // PRE:         
    // POST:        
    public void SetDestination(string destinationId)
    {
        if (destinationId.Equals(""))
        {
            return; // do nothing
        }
        if (selectedNodes.Count == 1)
        {
            int destinationInt = 0;
            if (int.TryParse(destinationId, out destinationInt))
            {
                selectedNodes[0].destinationId = destinationInt;
            }
            else
            {
                Debug.LogError("The given string can not parse to an int.");
            }
        }
        else
        {
            Debug.LogError("Destinations can only be set for 1 node at a time. Currently selected: " + selectedNodes.Count);
        }
    }

    // Description: 
    // PRE:         
    // POST:
    public void SetRate(string carsPerMinute)
    {
        if (carsPerMinute.Equals(""))
        {
            return; // do nothing
        }
        int carsPerMinuteInt = 0;
        if (!int.TryParse(carsPerMinute, out carsPerMinuteInt))
        {
            Debug.LogError("The given string can not parse to an int.");
            return; // do nothing
        }
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            selectedNodes[i].carsPerMinute = carsPerMinuteInt;
        }
    }
}
