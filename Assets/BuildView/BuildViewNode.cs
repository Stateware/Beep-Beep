using UnityEngine;
using System.Collections;

// File Name: BuildViewNode.cs
// Description: This Unity class defines all user interactions for node prefabs. 
//              It also creates node prefabs when SpawnNewNode is called.
// Dependencies: Gameobject: NodePrefab (Clone), Components: Node.c
// Additional Notes: 

public class BuildViewNode : MonoBehaviour
{
    public Node node;
    public GameObject NodePrefab;
    private BuildViewSelectionHandler selectionHandler;

    //Description: Sets up references between scripts 
    //Pre: Script Components Node.c and BuildViewSelectionHandler.c exist
    //Post: Gameobject NodePrefab (Clone) has a component Node.c. selectionHandler is initalized to the singleton script BuildViewSelectionHandler.c
    void Awake()
    {
        node = gameObject.AddComponent<Node>();
        selectionHandler = GameObject.FindObjectOfType<BuildViewSelectionHandler>();
    }

    //Description: Changes the gameobject's position in the game world
    //Pre:  Mouse cursor is over a NodePrefab (Clone) gameobject, and the left mouse button is held down for a duration of time. 
    //Post: The gameobject's position is at the location where the left mouse button is released.
    void OnMouseDrag()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        gameObject.transform.position = objPosition;
    }

    //Description: Modifies BuildViewSelectionHandler.c's selectedNodes List
    //Pre: Mouse cursor is over a NodePrefab (Clone) gameobject, the left mouse button is held down for a duration of time, 
    //      and finally the mouse button is released independent of the mouse's position.
    //Post: Adds a unique gameobject to the selectedNodes list. If the gameobject is not unique, remove it from the list.
    void OnMouseUp()
    {
        // When you click, add self to Selection array. Works with creating links.
        if (!selectionHandler.selectedNodes.Contains(this.node))
            selectionHandler.AddNode(this);
        else
            selectionHandler.RemoveNode(this);
     }

    //Description: Destroys the NodePrefab (Clone) and any Link gameobjects connected to it
    //Pre: Mouse cursor is over a NodePrefab (Clone) gameobject, and the right mouse button is clicked.
    //Post: The NodePrefab (Clone) gameobject is destroyed and removed from the selectedNodes list.
    void OnMouseOver()
    {
        if (Input.GetMouseButton(1)) // for right mouse click
        {
            selectionHandler.DeleteNodeInstances(this);
            Destroy(this.NodePrefab);
        }
    }

    //Description: Creates a NodePrefab (Clone) at the mouse's location
    //Pre: The Node button in the gameworld is clicked 
    //Post: A NodePrefab (Clone) is created and active in the gameworld.
    public void SpawnNewNode()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = spawnPosition;

        GameObject nodeClone = (GameObject) Instantiate(NodePrefab, transform.position, Quaternion.identity);
        nodeClone.tag = "Node";
	}
}
