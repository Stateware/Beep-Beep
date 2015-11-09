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
            SetSinkComponents();
        if (nodeProperties.IsSource)
            SetSourceComponents();

        switch (nodeProperties.NodeProperty)
        {
            case Node.NodeType.StopSign:
                SetStopSignComponents();
                break;
            case Node.NodeType.TrafficLight:
                SetTrafficLightComponents();
                break;
            default:
                break;
        }
    }

    private void SetSourceComponents()
    {
        GameObject car = Instantiate(Resources.Load("Prefabs/CarPrefab")) as GameObject;
        
        DontDestroyOnLoad(car);
        car.transform.position = gameObject.transform.position;
    }

    private void SetSinkComponents()
    {
        //do something
    }

    private void SetTrafficLightComponents()
    {
        //do something
    }

    private void SetStopSignComponents()
    {
        //do something
    }
}
