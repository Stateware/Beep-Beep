// Copyright (c) 2015 Stateware Team -- Licensed GPL v3
// File name:        Node.cs
// Description:      Contains all the setters and getters for gameobjects of type node
// Dependencies:     GameObject - NodePrefab (Clone)
// Additional Notes: This script component will be static across scenes

using UnityEngine;

public class Node : MonoBehaviour
{
    public enum NodeType
    {
        None,
        StopSign,
        TrafficLight
    }

    private NodeType _nodeProperty;
    private bool _isSource;
    private bool _isSink;
    private bool _isConnected;
    private string _name;
    private int _numberOfConnections;

    public int destinationId;
    public int carsPerMinute;

    // Description: Initializes all the nodes with default values and preserves the gameobject between scenes
    // PRE: 
    // POST:
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        this.NodeProperty = NodeType.None;
        this.IsSource = true;
        this.IsSink = true;
        this.NumberOfConnections = 0;
    }

    // Description: Number of links connected to the node instance
    // PRE:
    // POST:
    public int NumberOfConnections
    {
        get { return _numberOfConnections; }
        set { _numberOfConnections = value; }
    }

    // Description: Determines if this node is either a traffic light, stop sign, or none
    // PRE: 
    // POST: 
    public NodeType NodeProperty
    {
        get { return _nodeProperty; }
        set { _nodeProperty = value; }
    }

    // Description: Setter and getter
    // PRE: 
    // POST:
    public bool IsSource
    {
        get { return _isSource; }
        set { _isSource = value; }
    }

    // Description: Setter and getter
    // PRE:
    // POST:
    public bool IsSink
    {
        get { return _isSink; }
        set { _isSink = value; }
    }
}
