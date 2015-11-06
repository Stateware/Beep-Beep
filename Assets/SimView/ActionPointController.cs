using UnityEngine;
using System.Collections;

public class ActionPointController : MonoBehaviour {

    private Node nodeProperties;

    public void initializeActionPoint()
    {
        nodeProperties = gameObject.GetComponent<Node>();
        SetActionPointProperties();
    }

    private void SetActionPointProperties()
    {
        if (nodeProperties.IsSink)
            SetSink();
        if (nodeProperties.IsSource)
            SetSource();

        switch (nodeProperties.NodeProperty)
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
