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
            GameObject actionPoint = (GameObject)Instantiate(ActionPointPrefab, node.transform.position, node.transform.rotation);
            node.transform.parent = actionPoint.transform;
            Node n = actionPoint.GetComponentInChildren<Node>();
            actionPoint.tag = "ActionPoint";
            node.SetActive(false);
            Debug.Log("node " + n.IsSource.ToString());
        }
        
        foreach(GameObject link in links)
        {
            GameObject road = (GameObject)Instantiate(RoadPrefab, link.transform.position, link.transform.rotation);
            link.transform.parent = road.transform;
            Link l = road.GetComponentInChildren<Link>();
            road.tag = "Road";
            link.SetActive(false);
        }
        /*
        foreach (BuildViewSelectionHandler.ConnectedNodes cn in connectedNodes.Keys)
        {

        }
        */
    }
}
