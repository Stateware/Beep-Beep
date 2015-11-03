using UnityEngine;
using System.Collections;

public class ActionPoint : MonoBehaviour
{
    private Node _nodeProperties;

    public void SetType(Node nodeProperties)
    {
        this.NodeProperties = nodeProperties;
    }
     
    public Node NodeProperties
    {
        get { return _nodeProperties; }
        set { _nodeProperties = value; SetActionPointProperties(); }
    }

    private void SetActionPointProperties()
    {
        if (_nodeProperties.IsSink)
            SetSink();
        if (_nodeProperties.IsSource)
            SetSource();

        switch(_nodeProperties.NodeProperty)
        {
            case Node.NodeType.StopSign:
                SetStopSign();
                break;
            case Node.NodeType.TrafficLight:
                SetTrafficLight();
                break;
            default:
                break;
        }
    }

    private void SetSource()
    {
        //do something
    }

    private void SetSink()
    {
        //do something
    }

    private void SetTrafficLight()
    {
        //do something
    }

    private void SetStopSign()
    {
        //do something
    }

}
