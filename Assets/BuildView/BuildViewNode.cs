using UnityEngine;
using System.Collections;

public class BuildViewNode : MonoBehaviour
{

    public Node node;
    private SphereCollider myCollider;
    public GameObject NodePrefab;

	void Awake()
    {
        Debug.Log("awake!");
        myCollider = gameObject.AddComponent<SphereCollider>();
        myCollider.radius = 0.75f;
        node = gameObject.AddComponent<Node>();
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

    public void SpawnNewNode()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = spawnPosition;

        GameObject nodeClone = (GameObject) Instantiate(NodePrefab, transform.position, Quaternion.identity);
	}
}
