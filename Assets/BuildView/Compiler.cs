using UnityEngine;
using System.Collections;
using System.Collections.Generic;	

public class Compiler : MonoBehaviour {
    public GameObject[] nodes;
    public GameObject[] links;
    public Hashtable connectedNodes;
    public BuildViewSelectionHandler selectionHandler;
    public Node[] DisconnectedNodes;
	public Node[] disconnected_nodes=new Node[1000];

    void Awake()
    {
        selectionHandler = GameObject.FindObjectOfType<BuildViewSelectionHandler>();
    }

    void OnLevelWasLoaded(int level)
    {
        Debug.Log("Checking Level for compiler");
        if(level == 1)
        {
            Debug.Log("Level 1 was loaded.");
        }
        if(level == 0)
        {
            Debug.Log("Still in level 0");
        }
    }
/*
	private void IdentifyDisconnectedNodes()
	{
		List<Node> diconnected_nodes;
        DisconnectedNodes = Node.FindObjectsOfType<Node> ();
		int index = 0;
		for(int i=0; i<nodes.GetLength(1); i++) {
			if(DisconnectedNodes[i].NumberOfConnections <= 0)
				disconnected_nodes[index++]= DisconnectedNodes[i];
		}
	}
*/
    public void check_for_connectedness ()
    {

	}

    private void GetAllGameObjects()
    {
        nodes = GameObject.FindGameObjectsWithTag("node");
        links = GameObject.FindGameObjectsWithTag("link");
        connectedNodes = selectionHandler.connectedNodes;
    }

    private void ChangeBuildViewObjectsToSimViewObjects()
    {
        foreach(GameObject n in nodes)
        {

        }
        foreach(GameObject l in links)
        {

        }
        foreach(BuildViewSelectionHandler.ConnectedNodes cn in connectedNodes.Keys)
        {
            Debug.Log(cn.ToString());
        }
    }

    private void SwitchScenes()
    {
        Debug.Log("Loading Simulation View Scene.");
        Application.LoadLevelAsync("SimViewScene");
    }

    public void Compile()
    {
        GetAllGameObjects();
        //IdentifyDisconnectedNodes();
        ChangeBuildViewObjectsToSimViewObjects();
        SwitchScenes();
    }

}
