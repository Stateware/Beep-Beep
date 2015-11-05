using UnityEngine;
using System.Collections;

public class ActionPoint : MonoBehaviour
{
    private GameObject _nodeProperties;

    public void setActionPoint(GameObject nodeProperties)
    {
        this.NodeProperties = nodeProperties;
    }
     
    public GameObject NodeProperties
    {
        get { return _nodeProperties; }
        set { _nodeProperties = value; SetActionPointProperties(); }
    }

    private void SetActionPointProperties()
    {
        Node node = _nodeProperties.GetComponent<BuildViewNode>().node;
        if (node.IsSink)
            SetSink();
        if (node.IsSource)
            SetSource();

        switch(node.NodeProperty)
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
