// File Name: ActionPointController.cs
// Description: 
// Dependencies:
// Additional Notes:

using UnityEngine;
using System.Collections;

public class ActionPointController : MonoBehaviour {

    private Node _nodeProperties;

    // Description: 
    // PRE:
    // POST:
    public void initializeActionPoint()
    {
        _nodeProperties = gameObject.GetComponent<Node>();
        SetActionPointProperties();
    }

    // Description:
    // PRE:
    // POST:
    private void SetActionPointProperties()
    {
        if (_nodeProperties.IsSink)
            SetSinkComponents();
        if (_nodeProperties.IsSource)
            SetSourceComponents();

        switch (_nodeProperties.NodeProperty)
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

    // Description:
    // PRE:
    // POST:
    private void SetSourceComponents()
    {
        GameObject car = Instantiate(Resources.Load("Prefabs/CarPrefab")) as GameObject;
        
        DontDestroyOnLoad(car);
        car.transform.position = gameObject.transform.position;
    }

    // Description:
    // PRE:
    // POST: 
    private void SetSinkComponents()
    {
        // TODO: Implement
    }

    // Description:
    // PRE:
    // POST:
    private void SetTrafficLightComponents()
    {
        // TODO: Implement
    }

    // Description:
    // PRE:
    // POST: 
    private void SetStopSignComponents()
    {
        // TODO: Implement
    }
}
