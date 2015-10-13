using UnityEngine;
using System.Collections;

public class BuildViewNode : MonoBehaviour
{
    public enum NodeType {None, StopSign, TrafficLight}
    private NodeType property;
    private bool isSource;
    private bool isSink;
    private SphereCollider myCollider;
    public GameObject NodePrefab;
  
    void Awake()
    {
        this.isSink = false;
        this.isSource = false;
        this.property = NodeType.None;
        Debug.Log("awake!");
    }

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

    public NodeType getNodeProperty()
    {
        return this.property;
    }

    public bool getIsSource()
    {
        return this.isSource;
    }

    public bool getIsSink()
    {
        return this.isSink;
    }

    public void setNodeProperty(NodeType newProperty)
    {
        this.property = newProperty;
    }

    public void setIsSource(bool newIsSource)
    {
        this.isSource = newIsSource;
    }

    public void setIsSink(bool newIsSink)
    {
        this.isSink = newIsSink;
    }

    public void SpawnNewNode()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = spawnPosition;

        GameObject nodeClone = (GameObject) Instantiate(NodePrefab, transform.position, Quaternion.identity);

        nodeClone.GetComponent<BuildViewNode>();

    }
}
