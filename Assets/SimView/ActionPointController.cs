// Copyright(c) 2015 Stateware Team -- Licensed GPL v3
// File Name: ActionPointController.cs
// Description: Based on the properties set in Node.c, adds components to the Node Gameobject
// Dependencies: Node.c, Compiler.c 
// Additional Notes:

using UnityEngine;
using System.Collections;

public class ActionPointController : MonoBehaviour
{
    private Node _nodeProperties;

    // Description: Based on the properties set in Node.c, adds components to the Node Gameobject
    // PRE: Node.c component is attached to the gameobject
    // POST: Simview compoenents are added to the gamebobject
    public void InitializeActionPoint()
    {
        _nodeProperties = gameObject.GetComponent<Node>();
        SetActionPointProperties();
    }

    // Description: Determines what components to add
    // PRE: _nodeProperties is properly inintialized
    // POST: Unity components are added to the gameobject
    private void SetActionPointProperties()
    {
        if (_nodeProperties.IsSink)
        {
            SetSinkComponents();
        }
        if (_nodeProperties.IsSource)
        {
            SetSourceComponents();
        }

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

    // Description: Adds the source component to the gameobject
    // PRE: A prefab exists CarPrefab exists in the Resources folder
    // POST: TBD
    private void SetSourceComponents()
    {
        GameObject car = Instantiate(Resources.Load("Prefabs/CarPrefab")) as GameObject;
        
        DontDestroyOnLoad(car);
        car.transform.position = gameObject.transform.position;
    }

    // Description: Adds the sink component to the gameobject
    // PRE: TBD 
    // POST: TBD
    private void SetSinkComponents()
    {
        // TODO: Implement
    }

    // Description: Adds the trafficlight component to the gameobject
    // PRE: TBD
    // POST: TBD
    private void SetTrafficLightComponents()
    {
        // TODO: Implement
    }

    // Description: adds the stop sign component to the gameobject
    // PRE: TBD
    // POST: TBD
    private void SetStopSignComponents()
    {
        // TODO: Implement
    }
}
