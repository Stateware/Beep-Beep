using UnityEngine;
using System.Collections;

public class BuildViewNode : MonoBehaviour
{
    public enum NodeType { None, StopSign, TrafficLight}
    public enum NodeGateType {None, Source, Sink, Dual}
    private NodeType nodeProperty;
    private NodeGateType nodeGateTypeProperty;
    private SphereCollider myCollider;
    public GameObject NodePrefab;
    
    void Awake()
    {
        this.nodeProperty = NodeType.None;
        this.nodeGateTypeProperty = NodeGateType.None;
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
        return this.nodeProperty;
    }

    public NodeGateType getNodeGateTypeProperty()
    {
        return this.nodeGateTypeProperty;
    }

    public void setNodeProperty(NodeType newProperty)
    {
        this.nodeProperty = newProperty;
    }

    public void setNodeGateTypeProperty(NodeGateType newProperty)
    {
        this.nodeGateTypeProperty = newProperty;
    }

    public void SpawnNewNode()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = spawnPosition;

        GameObject nodeClone = (GameObject) Instantiate(NodePrefab, transform.position, Quaternion.identity);
    }
}
