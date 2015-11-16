﻿// Compiler.cs
// Description:  Forms the bridge between editor view and simulation view - stops compilation
//			     with error(s) if isolated nodes or no source nodes are found; compiles with warnings
//			     if no sink nodes are found.
// Dependencies: GameObject-	 Node

using UnityEngine;
using System.Collections.Generic;	

public class Compiler : MonoBehaviour {
	private GameObject[] nodes;
	private GameObject[] links;
	private List<Node> disconnectedNodes;
	private ErrorView errorView;
	
	void Awake()
	{
		errorView = gameObject.AddComponent<ErrorView>();
		disconnectedNodes = new List<Node>();
	}
	//Desription: constructs Node and Link arrays 
	//PRE: -
	//POST: nodes and links are filled with Node objects and Link objects
	private void GetAllGameObjects()
	{
		nodes = GameObject.FindGameObjectsWithTag("Node");
		links = GameObject.FindGameObjectsWithTag("Link");
	}
	
	//Description: Finds Node objects that are not connected to any other nodes and adds 
	//			   them to Node array disconnectedNodes
	//PRE: nodes[], disconnectedNodes[] are initialized
	//POST: disconnectedNode contains Node objects that are have no links to and from them
	private bool IdentifyDisconnectedNodes()
	{
		bool found_disconnected = false;	
		
		for (int i=0; i<nodes.Length; i++)
		{
			if (nodes [i].GetComponent<BuildViewNode>().node.NumberOfConnections == 0)
			{
				found_disconnected = true;
				disconnectedNodes.Add(nodes [i].GetComponent<BuildViewNode>().node);
			}
		}
		
		return found_disconnected;
	}
	
	//Description: Displays error with number of isolated nodes found and changes sprite of the respective nodes to highlight them
	//PRE: disconnectedNodes is not null
	//POST: sprites of disconnected nodes is changed to the error sprite
	private void DisplayDisconnectedNodes()
	{
		string error_text = "You have " + disconnectedNodes.Count + " disconnected nodes.";
		errorView.appendErrorText(error_text);
		errorView.setDisplayGui(true);
		for (int j = 0; j < disconnectedNodes.Count; j++)
		{
			disconnectedNodes[j].GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("BuildViewNodeDISCONNECTED", typeof(Sprite));
		}
		
		disconnectedNodes.Clear();
	}
	
	//	Description: Changes links to roads and nodes to action points
	// Pre: arrays nodes and links contain nonzero number of node and link gameobjects
	// Post: Log displays the number of nodes and links that have been transformed to action points and roads
	private void ChangeBuildViewObjectsToSimViewObjects()
	{
		Debug.Log("Number of nodes compiled into Action Points: " + nodes.Length);
		Debug.Log("Number of links compiled into Roads: " + links.Length);
		
		foreach (GameObject node in nodes)
		{
			Destroy(node.GetComponent<BuildViewNode>());
			ActionPointController ap = node.AddComponent<ActionPointController>();
			ap.initializeActionPoint();
			node.name = "Action Point";
			Debug.Log("This node is a source: " + node.GetComponent<Node>().IsSource.ToString());
		}
		
		foreach (GameObject link in links)
		{
			Destroy(link.GetComponent<BuildViewLink>());
			RoadController rc = link.AddComponent<RoadController>();
			rc.initializeRoad();
			link.name = "Road";
		}        
	}

	//description: switches scenes
	//pre: -
	//post: current scene has been switched to the sim view scene
	private void SwitchScenes()
	{
		Application.LoadLevel("SimViewScene");
	}

	//description: main compile function that calls the other functions
	//pre: -
	//post: -
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
