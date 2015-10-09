using UnityEngine;
using System.Collections;

public class BuildViewNode : MonoBehaviour
{

    private string[] nodeStrings = new string[] { "1a. StopSign", "1b. Traffic Light", "2. Source", "3. Sink" };
    private enum NodeType { None, StopSign, TrafficLight, Source, Sink }
    private NodeType[] selectedNodeTypes = new NodeType[3];
    private SphereCollider myCollider;
    public GameObject NodePrefab;
  
    void Start()
    {
        myCollider = gameObject.AddComponent<SphereCollider>();
        myCollider.radius = 0.75f;
    }

    void OnMouseDrag()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }
        
    void OnMouseUp()
    {
        // When you click, add self to Selection array. Works with creating links.
        BuildViewSelectionHandler selectionHandler = GameObject.FindObjectOfType<BuildViewSelectionHandler>();
        selectionHandler.AddNode(this);
     }

    //Creates the buttons
    private bool isStopSign;
    private bool isTrafficLight;
    private bool isSink;
    private bool isSource;
    
    private void SetNodePropertyWindow(int windowID)
    {
        GUILayout.BeginVertical();
        GUILayout.Space(10);

        if(GUILayout.Button(nodeStrings[0]))
        {
            if(!isStopSign)
            {
                this.selectedNodeTypes[0] = NodeType.StopSign;
                this.isStopSign = true;
            }
            else
            {
                this.selectedNodeTypes[0] = NodeType.None;
                this.isStopSign = false;
            }
            Debug.Log(this.name + " " + this.selectedNodeTypes[0].ToString() + " " + this.selectedNodeTypes[1].ToString() + " " + this.selectedNodeTypes[2].ToString());
        }
        if (GUILayout.Button(nodeStrings[1]))
        {
            if (!isTrafficLight)
            {
                this.selectedNodeTypes[0] = NodeType.TrafficLight;
                this.isTrafficLight = true;
            }
            else
            {
                this.selectedNodeTypes[0] = NodeType.None;
                this.isTrafficLight = false;
            }
            Debug.Log(this.name + " " + this.selectedNodeTypes[0].ToString() + " " + this.selectedNodeTypes[1].ToString() + " " + this.selectedNodeTypes[2].ToString());
        }
        if (GUILayout.Button(nodeStrings[2]))
        {
            if (!isSource)
            {
                this.selectedNodeTypes[1] = NodeType.Source;
                this.isSource = true;
            }
            else
            {
                this.selectedNodeTypes[1] = NodeType.None;
                this.isSource = false;
            }
            Debug.Log(this.name + " " + this.selectedNodeTypes[0].ToString() + " " + this.selectedNodeTypes[1].ToString() + " " + this.selectedNodeTypes[2].ToString());
        }
        if (GUILayout.Button(nodeStrings[3]))
        {
            if (!isSink)
            {
                this.selectedNodeTypes[2] = NodeType.Sink;
                this.isSink = true;
            }
            else
            {
                this.selectedNodeTypes[2] = NodeType.None;
                this.isSink = false;
            }
            Debug.Log(this.name + " " + this.selectedNodeTypes[0].ToString() + " " + this.selectedNodeTypes[1].ToString() + " " + this.selectedNodeTypes[2].ToString());
        }

        GUILayout.EndVertical();
        GUI.DragWindow();
    }

    int nodeItr = 0;
    public void SpawnNewNode()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = spawnPosition;

        GameObject nodeClone = (GameObject) Instantiate(NodePrefab, transform.position, Quaternion.identity);
        
        nodeClone.GetComponent<BuildViewNode>().selectedNodeTypes = new NodeType[3];
        nodeClone.GetComponent<BuildViewNode>().isStopSign = false;
        nodeClone.GetComponent<BuildViewNode>().isTrafficLight = false;
        nodeClone.GetComponent<BuildViewNode>().isSource = false;
        nodeClone.GetComponent<BuildViewNode>().isSink = false;
        nodeClone.name = "node" + nodeItr;
        nodeItr++;
    }
}
