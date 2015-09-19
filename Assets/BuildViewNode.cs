using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BuildViewNode : MonoBehaviour
{

    public Transform node;
    private Rigidbody myRigidBody;
    private enum NodeType  {
        StopSign,
        RedLight,
        Source,
        Sink
    }
    private bool isClicked;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }


    private void OnGUI()
    {
        string msgtxt = "hello world!";
        if (isClicked)
        {
            GUI.Box(new Rect(200, 100, 320, 110), msgtxt);
        }
    }

    
    void OnMouseUp()
    {
        // When you click, change the variables value
        if (isClicked)
        {
            Debug.Log("Hello World");
            isClicked = false;
        }
        else
            isClicked = true;
    }
}
