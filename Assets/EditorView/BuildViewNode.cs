using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
public class BuildViewNode : MonoBehaviour
{

    public string[] nodeStrings = new string[] { "StopSign", "Traffic Light", "Source", "Sink" };
    private enum NodeType { StopSign, TrafficLight, Source, Sink}
    private NodeType nodeType;

    public GameObject node;
    private SphereCollider myCollider;
    private Rigidbody myRigidBody;
    private Rect windowSize = new Rect(0, 0, Screen.width/4, Screen.height/4);
    
    private bool isClicked;

    //Pesudo Constructor for Node
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myCollider = gameObject.AddComponent<SphereCollider>();
        myCollider.radius = 0.75f;
        myRigidBody.useGravity = false;
    }

    private void OnGUI()
    {
        //Creates the Node Property Window
        if (isClicked)
            windowSize = GUI.Window(0, windowSize, NodePropertyWindow, "Set Node Property");
    }

    //Creates the buttons
    private void NodePropertyWindow(int windowID)
    {
        GUILayout.BeginVertical();
        GUILayout.Space(10);

        if(GUILayout.Button(nodeStrings[0]))
        {
            nodeType = NodeType.StopSign;
            Debug.Log(nodeType);
        }
        if (GUILayout.Button(nodeStrings[1]))
        {
            nodeType = NodeType.TrafficLight;
            Debug.Log(nodeType);
        }
        if (GUILayout.Button(nodeStrings[2]))
        {
            nodeType = NodeType.Source;
            Debug.Log(nodeType);
        }
        if (GUILayout.Button(nodeStrings[3]))
        {
            nodeType = NodeType.Sink;
            Debug.Log(nodeType);
        }

        GUILayout.EndVertical();

        GUI.DragWindow();
    }
    
    void OnMouseUp()
    {
        // When you click, change the variables value
        if (isClicked) isClicked = false;
        else isClicked = true;
    }
}
