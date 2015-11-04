using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {

    public GameObject ActionPointPrefab;
    public GameObject RoadPrefab;
    private GameObject[] nodes;
    private GameObject[] links;
    public Hashtable adjacencyList;
    //private Hashtable connectedNodes;
    private BuildViewSelectionHandler selectionHandler;

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

    void Awake()
    {
        selectionHandler = GameObject.FindObjectOfType<BuildViewSelectionHandler>();
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            Debug.Log("Entering SimView Mode");
            GetAllGameObjects();
            ChangeBuildViewObjectsToSimViewObjects();
        }
    }

    private void GetAllGameObjects()
    {
        BuildViewNode[] mynodes = Resources.FindObjectsOfTypeAll<BuildViewNode>();
        Debug.Log("mynodes " + mynodes.Length);

        nodes = GameObject.FindGameObjectsWithTag("Node");
        links = GameObject.FindGameObjectsWithTag("Link");
        //connectedNodes = selectionHandler.connectedNodes;
    }

    private void ChangeBuildViewObjectsToSimViewObjects()
    {
        Debug.Log("nodes " + nodes.Length);
        Debug.Log("links " + links.Length);

        foreach (GameObject node in nodes)
        {
            GameObject actionPoint = (GameObject)Instantiate(ActionPointPrefab);
            node.transform.parent = actionPoint.transform;
            actionPoint.tag = "ActionPoint";
            //actionPoints.Add(actionPoint);
            Debug.Log("node " + actionPoint.GetComponentInChildren<BuildViewNode>().node.IsSource.ToString());
            node.SetActive(false);
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
        
        foreach (BuildViewSelectionHandler.ConnectedNodes cn in connectedNodes.Keys)
        {

        }
        */
    }
}
