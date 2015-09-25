using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
public class BuildViewNode : MonoBehaviour
{

    private string[] nodeStrings = new string[] { "1a. StopSign", "1b. Traffic Light", "2. Source", "3. Sink" };
    private enum NodeType { None, StopSign, TrafficLight, Source, Sink }
    private NodeType[] selectedNodeTypes = new NodeType[3];

    private SphereCollider myCollider;
    private Rigidbody myRigidBody;
    private Rect windowSize = new Rect(0, 0, Screen.width / 4, Screen.height / 4);

    private bool isNodeClicked;

    //Pesudo Constructor for Node
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myCollider = gameObject.AddComponent<SphereCollider>();
        myCollider.radius = 0.75f;
        myRigidBody.useGravity = false;
    }

    void OnMouseDrag()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;
    }


    void OnMouseUp()
    {
        // When you click, change the variables value
        if (isNodeClicked) isNodeClicked = false;
        else isNodeClicked = true;
    }

    private void OnGUI()
    {
        //Creates the Node Property Window
        if (isNodeClicked)
            windowSize = GUI.Window(0, windowSize, SetNodePropertyWindow, "Set Node Property");
    }

    //Creates the buttons
    private bool isStopSign = false;
    private bool isTrafficLight = false;
    private bool isSink = false;
    private bool isSource = false;
    
    private void SetNodePropertyWindow(int windowID)
    {
        GUILayout.BeginVertical();
        GUILayout.Space(10);

        if(GUILayout.Button(nodeStrings[0]))
        {
            if(!isStopSign)
            {
                selectedNodeTypes[0] = NodeType.StopSign;
                isStopSign = true;
            }
            else
            {
                selectedNodeTypes[0] = NodeType.None;
                isStopSign = false;
            }
            Debug.Log(selectedNodeTypes[0].ToString() + " " + selectedNodeTypes[1].ToString() + " " + selectedNodeTypes[2].ToString());

        }
        if (GUILayout.Button(nodeStrings[1]))
        {
            if (!isTrafficLight)
            {
                selectedNodeTypes[0] = NodeType.TrafficLight;
                isTrafficLight = true;
            }
            else
            {
                selectedNodeTypes[0] = NodeType.None;
                isTrafficLight = false;
            }
            Debug.Log(selectedNodeTypes[0].ToString() + " " + selectedNodeTypes[1].ToString() + " " + selectedNodeTypes[2].ToString());
        }
        if (GUILayout.Button(nodeStrings[2]))
        {
            if (!isSource)
            {
                selectedNodeTypes[1] = NodeType.Source;
                isSource = true;
            }
            else
            {
                selectedNodeTypes[1] = NodeType.None;
                isSource = false;
            }
            Debug.Log(selectedNodeTypes[0].ToString() + " " + selectedNodeTypes[1].ToString() + " " + selectedNodeTypes[2].ToString());
        }
        if (GUILayout.Button(nodeStrings[3]))
        {
            if (!isSink)
            {
                selectedNodeTypes[2] = NodeType.Sink;
                isSink = true;
            }
            else
            {
                selectedNodeTypes[2] = NodeType.None;
                isSink = false;
            }
            Debug.Log(selectedNodeTypes[0].ToString() + " " + selectedNodeTypes[1].ToString() + " " + selectedNodeTypes[2].ToString());
        }

        GUILayout.EndVertical();

        GUI.DragWindow();
    }
}
