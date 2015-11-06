using UnityEngine;
using UnityEditor;
using System.Collections;
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

	private void GetAllGameObjects()
	{
		nodes = GameObject.FindGameObjectsWithTag("Node");
		links = GameObject.FindGameObjectsWithTag("Link");
    }

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

    public void check_for_connectedness ()
    {

	}

    private void SwitchScenes()
    {
        Application.LoadLevel("SimViewScene");
    }

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
