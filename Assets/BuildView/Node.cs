using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
    public enum NodeType { None, StopSign, TrafficLight }
    private NodeType _nodeProperty;
    private bool _isSource;
    private bool _isSink;
    private bool _isConnected;
    private string _name;
    private int _numberOfConnections;

    void Start()
    {
        this.NodeProperty = NodeType.None;
        this.IsSource = true;
        this.IsSink = true;
        this.NumberOfConnections = 0;
    }

    public void setNode(Node node)
    {
        this.NodeProperty = node.NodeProperty;
        this.IsSource = node.IsSource;
        this.IsSink = node.IsSink;
        this.NumberOfConnections = node.NumberOfConnections;
    }

    public NodeType NodeProperty
    {
        get { return _nodeProperty; }
        set { _nodeProperty = value; }
    }

    public bool IsSource
    {
        get { return _isSource; }
        set { _isSource = value; }
    }

    public bool IsSink
    {
        get { return _isSink; }
        set { Debug.Log("Setting sink"); _isSink = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int NumberOfConnections
    {
        get { return _numberOfConnections; }
        set { _numberOfConnections = value; }
    }
}
