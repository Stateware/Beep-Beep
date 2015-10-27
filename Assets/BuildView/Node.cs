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

    public Node()
    {
        this.NodeProperty = NodeType.None;
        this.IsSource = true;
        this.IsSink = true;
        this.IsConnected = false;
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
        set { _isSink = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public bool IsConnected
    {
        get { return _isConnected; }
        set { _isConnected = value; }
    }
}
