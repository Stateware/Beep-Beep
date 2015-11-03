using UnityEngine;
using System.Collections;
using System.Collections.Generic;	

public class Compiler : MonoBehaviour {
    private GameObject[] nodes;
    private GameObject[] links;
    private List<GameObject> actionPoints;
    private List<GameObject> roads;
    private Hashtable connectedNodes;
    private BuildViewSelectionHandler selectionHandler;
    public Hashtable adjacencyList;
    public GameObject ActionPointPrefab;
    public GameObject RoadPrefab;
    public List<Node> DisconnectedNodes=new List<Node>();
	//public Node[] disconnected_nodes=new Node[1000];

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
	private void GetAllGameObjects()
	{
		nodes = GameObject.FindGameObjectsWithTag("Node");
		links = GameObject.FindGameObjectsWithTag("Link");
		connectedNodes = selectionHandler.connectedNodes;
	}

	private void IdentifyDisconnectedNodes()
	{
		bool found_disconnected = false;	
		
		
		int index = 0;
		for (int i=0; i<nodes.GetLength(1); i++) {
			if (nodes [i].GetComponent<BuildViewNode>().node.NumberOfConnections == 0) {
				found_disconnected = true;
				DisconnectedNodes.Add(nodes [i].GetComponent<BuildViewNode>().node);
			}
		}
		
		if (found_disconnected) {
			ErrorView error=new ErrorView();
			string error_text="You have " + DisconnectedNodes.Count + " disconnected nodes.";
			error.appendErrorText(error_text);
			for(int j=0; j<DisconnectedNodes.Count; j++){	
				DisconnectedNodes[j].GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("BuildViewNodeDISCONNECTED", typeof(Sprite));
			}
		}
	}
    public void check_for_connectedness ()
    {

	}



    struct DestinationActionPointAndRoad
    {
        public GameObject destination;
        public GameObject road;

        public DestinationActionPointAndRoad(GameObject destination, GameObject road)
        {
            this.destination = destination;
            this.road = road;
        }
    }

    private void ChangeBuildViewObjectsToSimViewObjects()
    {
        Debug.Log("nodes " + nodes.Length);
        Debug.Log("links " + links.Length);

        foreach (GameObject n in nodes)
        {
            GameObject actionPoint = (GameObject) Instantiate(ActionPointPrefab);
            actionPoint.transform.parent = n.transform;
            actionPoint.tag = "ActionPoint";
            //actionPoints.Add(actionPoint);
            Debug.Log("node " + actionPoint.GetComponentInParent<BuildViewNode>().node.IsSource.ToString());
        }
   /*
        foreach(GameObject l in links)
        {
            GameObject road = (GameObject)Instantiate(RoadPrefab);
            road.transform.parent = l.transform;
            road.GetComponentInParent<BuildViewLink>();
            road.tag = "Road";
            roads.Add(road);
        }
    */
        foreach (BuildViewSelectionHandler.ConnectedNodes cn in connectedNodes.Keys)
        {
            
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
        SwitchScenes();
        //IdentifyDisconnectedNodes();
        ChangeBuildViewObjectsToSimViewObjects();
    }

}
