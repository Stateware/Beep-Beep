using UnityEngine;
using System.Collections;

public class BuildViewNode : MonoBehaviour
{
    public Node node;
    public GameObject NodePrefab;
    private BuildViewSelectionHandler selectionHandler;

    void Awake()
    {
        node = gameObject.AddComponent<Node>();
        selectionHandler = GameObject.FindObjectOfType<BuildViewSelectionHandler>();
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
        selectionHandler.AddNode(this);
     }

    //define miscellaneous mouse functions
    void OnMouseOver()
    {
        if (Input.GetMouseButton(1)) // for right mouse click
        {
            selectionHandler.DeleteNodeInstances(this);
            Destroy(this.NodePrefab);
        }
    }

    public void SpawnNewNode()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = spawnPosition;

        GameObject nodeClone = (GameObject) Instantiate(NodePrefab, transform.position, Quaternion.identity);
	}
}
