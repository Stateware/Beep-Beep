using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class BuildViewSelectionHandler : MonoBehaviour {

    public Hashtable connectedNodes;
    public List<Node> selectedNodes;
    public List<BuildViewNode> allNodes;
    public GameObject LinkPrefab;
    public GameObject nodePropertyDropdown;
    public GameObject sourceCheckbox;
    public GameObject sinkCheckbox;
    private Color originalCheckboxLabelColor;
    public Color valuesNotUniformColor;
    

    public struct ConnectedNodes
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
        originalCheckboxLabelColor = sourceCheckbox.GetComponentInChildren<Text>().color;
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

    public void RemoveNode(BuildViewNode exisitingNode)
    {
        selectedNodes.Remove(exisitingNode.node);
        exisitingNode.node.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("BuildNode", typeof(Sprite));
        if (selectedNodes.Count == 0)
            RemoveUIInteractibility();
    }

    public void AddNode(BuildViewNode newNode)
    {
        if (!selectedNodes.Contains(newNode.node))
        {
            selectedNodes.Add(newNode.node);
            newNode.node.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("BuildViewNode", typeof(Sprite));
            ChangeGuiToggleSetting(true);
        }
        // the following lines are dependent on the fact that there is at least 1 element in selected nodes,
        // which can be assumed from the above, as if the list doesn't contain it, it'll be added.
        Assert.IsTrue(selectedNodes.Count > 0);

        if (UniformSinkValues())
        {
            sinkCheckbox.GetComponent<Toggle>().isOn = newNode.node.IsSink;
        }
        else
        {
            sinkCheckbox.GetComponent<Toggle>().isOn = true;
            sinkCheckbox.GetComponentInChildren<Text>().color = valuesNotUniformColor;
        }
        if (UniformSourceValues())
        {
            sourceCheckbox.GetComponent<Toggle>().isOn = newNode.node.IsSource;
        }
        else
        {
            sourceCheckbox.GetComponent<Toggle>().isOn = true;
            sourceCheckbox.GetComponentInChildren<Text>().color = valuesNotUniformColor;
        }
    }

    public void ClearSelection()
    {
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            selectedNodes[i].GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("BuildNode", typeof(Sprite));
        }
        selectedNodes.Clear();
        RemoveUIInteractibility();
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

    public void RemoveUIInteractibility()
    {
        nodePropertyDropdown.GetComponent<Dropdown>().interactable = false;
        sinkCheckbox.GetComponent<Toggle>().interactable = false;
        sinkCheckbox.GetComponentInChildren<Text>().color = originalCheckboxLabelColor;
        sourceCheckbox.GetComponent<Toggle>().interactable = false;
        sourceCheckbox.GetComponentInChildren<Text>().color = originalCheckboxLabelColor;
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

    public void SetSink(bool value)
    {
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            selectedNodes[i].IsSink = value;
        }
    }

    public void SetSource(bool value)
    {
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            selectedNodes[i].IsSource = value;
        }
    }

    private bool UniformSinkValues()
    {
        if (selectedNodes.Count <= 1)
        {
            return true;
        }
        else
        {
            bool firstSinkValue = selectedNodes[0].IsSink;
            for (int i = 1; i < selectedNodes.Count; i++)
            {
                if (selectedNodes[i] != firstSinkValue)
                {
                    return false;
                }
            }
            return true;
        }
    }

    private bool UniformSourceValues()
    {
        if (selectedNodes.Count <= 1)
        {
            return true;
        }
        else
        {
            bool firstSourceValue = selectedNodes[0].IsSource;
            for (int i = 1; i < selectedNodes.Count; i++)
            {
                if (selectedNodes[i] != firstSourceValue)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
