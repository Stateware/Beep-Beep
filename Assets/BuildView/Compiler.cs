using UnityEngine;
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

    private void PreserveGameObjects()
    {
        foreach (GameObject node in nodes)
        {
            DontDestroyOnLoad(node);
        }
        foreach (GameObject link in links)
        {
            DontDestroyOnLoad(link);
        }
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
        for (int j = 0; j < disconnectedNodes.Count; j++)
        {
            disconnectedNodes[j].GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("BuildViewNodeDISCONNECTED", typeof(Sprite));
        }

        disconnectedNodes.Clear();
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
            PreserveGameObjects();
            SwitchScenes();
        }
    }

}
