// Copyright (c) 2015 Stateware Team -- Licensed GPL v3
// File Name:        Compiler.cs
// Description:      Forms the bridge between editor view and simulation view - stops compilation
//			         with error(s) if isolated nodes or no source nodes are found; compiles with warnings
//			         if no sink nodes are found.
// Dependencies:     GameObject- Node
// Additional Notes: 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;	

public class Compiler : MonoBehaviour
{
    private GameObject[] _nodes;
    private GameObject[] _links;
    private List<Node> _disconnectedNodes;
    private ErrorView _errorView;
    private Hashtable _connectedNodes;
    public static Hashtable adjacencyList;

    struct DestinationActionPointAndRoad
    {
        public GameObject destination, road;
        public DestinationActionPointAndRoad(GameObject destination, GameObject road)
        {
            this.destination = destination;
            this.road = road;
        }
    }

    // Description: Initializes Unity dependent variables
	// PRE: 		N/A
	// POST: 		Creates errorview to enable displaying errors, ArrayList disconnectedNodes is initialized 
    void Awake()
	{
		_errorView = gameObject.AddComponent<ErrorView>();
		_disconnectedNodes = new List<Node>();
	}

	// Description: Constructs Node and Link arrays 
	// PRE:        N/A
	// POST:       Nodes and links are filled with Node objects and Link objects
	private void GetAllGameObjects()
	{
		_nodes = GameObject.FindGameObjectsWithTag("Node");
		_links = GameObject.FindGameObjectsWithTag("Link");
        _connectedNodes = GameObject.FindObjectOfType<BuildViewSelectionHandler>().connectedNodes;
    }
	
	// Description: Finds Node objects that are not connected to any other nodes and adds 
	//			    them to Node array disconnectedNodes
	// PRE:         nodes[], disconnectedNodes[] are initialized
	// POST:        disconnectedNode contains Node objects that are have no links to and from them
	private bool IdentifyDisconnectedNodes()
	{
		bool found_disconnected = false;	
		
		for (int i=0; i<_nodes.Length; i++)
		{
			if (_nodes [i].GetComponent<BuildViewNode>().node.NumberOfConnections == 0)
			{
				found_disconnected = true;
				_disconnectedNodes.Add(_nodes [i].GetComponent<BuildViewNode>().node);
			}
		}
		
		return found_disconnected;
	}
	
	// Description: Displays error with number of isolated nodes found and changes sprite of the respective nodes to highlight them
	// PRE:         disconnectedNodes list is not null
	// POST:        Sprites of disconnected nodes are changed to the error sprite
	private void DisplayDisconnectedNodes()
	{
		string error_text = "You have " + _disconnectedNodes.Count + " disconnected nodes.";
		_errorView.AppendErrorText(error_text);
		_errorView.SetDisplayGui(true);
		for (int j = 0; j < _disconnectedNodes.Count; j++)
		{
			_disconnectedNodes[j].GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("BuildViewNodeDISCONNECTED", typeof(Sprite));
		}
		
		_disconnectedNodes.Clear();
	}
	
	// Description: Changes links to roads and nodes to action points
	// PRE:         Arrays nodes and links contain nonzero number of node and link gameobjects
	// POST:        Log displays the number of nodes and links that have been transformed to action points and roads
	private void ChangeBuildViewObjectsToSimViewObjects()
	{
		Debug.Log("Number of nodes compiled into Action Points: " + _nodes.Length);
		Debug.Log("Number of links compiled into Roads: " + _links.Length);
		
		foreach (GameObject node in _nodes)
		{
			Destroy(node.GetComponent<BuildViewNode>());
			ActionPointController ap = node.AddComponent<ActionPointController>();
			ap.InitializeActionPoint();
			node.name = "Action Point";
			Debug.Log("This node is a source: " + node.GetComponent<Node>().IsSource.ToString());
		}
		
		foreach (GameObject link in _links)
		{
			Destroy(link.GetComponent<BuildViewLink>());
			RoadController rc = link.AddComponent<RoadController>();
			rc.InitializeRoad();
			link.name = "Road";
		}

        foreach (BuildViewSelectionHandler.ConnectedNodes cn in _connectedNodes.Keys)
        {
            DestinationActionPointAndRoad dapar = new DestinationActionPointAndRoad(cn.destination.gameObject, (GameObject) _connectedNodes[cn]);
            GameObject origin = cn.origin.gameObject;

            if (!adjacencyList.ContainsKey(origin))
            {
                List<DestinationActionPointAndRoad> originConnections = new List<DestinationActionPointAndRoad>();
                originConnections.Add(dapar);
                adjacencyList.Add(origin, originConnections);
            }
            else
            {
                (adjacencyList[origin] as List<DestinationActionPointAndRoad>).Add(dapar);
            }
        }
    }

	// Description: Switches scenes
	// PRE:         N/A
	// POST:        Current scene has been switched to the sim view scene
	private void SwitchScenes()
	{
		Application.LoadLevel("SimViewScene");
	}

    // Description: Main compile function that calls the other functions
    // PRE:         N/A
    // POST:        Calls compile functions procedurally
    public void Compile()
	{
		GetAllGameObjects();
		if(IdentifyDisconnectedNodes())
		{
			DisplayDisconnectedNodes();
		}
		else
		{
			ChangeBuildViewObjectsToSimViewObjects();
			SwitchScenes();
		}
	}	
}
