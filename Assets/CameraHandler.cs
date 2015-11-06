//File Name:        CameraHandler.cs
//Description:      Script that is attached to the camera that controlls the camera movement. Currently 
//                  there is middle mouse to drag the camera around and scroll wheel to move in and out.
//Dependencies:     None
//Additional Notes: Mouse X, Mouse Y and Mouse ScrollWheel needs to be set in Edit -> Project Settings -> Input 

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraHandler : MonoBehaviour
{
    public float panSpeed = 1;

    Vector3 bottomLeft;                 //currently not used but we may want to set camera bounds one day
    Vector3 topRight;

    void Start()
    //Description:  Called on unity when the program starts. 
    //PRE:          None
    //POST:         bottomLeft and topRight will be set according to max camera size
    {
        //set max camera bounds (assumes camera is max zoom and centered on Start)
        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, -transform.position.z));
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -transform.position.z));
    }

    void Update()
    //Description:  Called by Unity once per frame, checks to see if the user is dragging or scrolling
    //PRE:          This script is attached to the camera object in the current scene
    //POST:         Camera will have moved if the user is scrolling or dragging, else nothing happens
    {
        bool bgBrowserEnabled = false;                          //if the file selection browser is up don't scroll

        if (Input.GetMouseButton(2))                            //here uses the middle mouse button, may want or space & click later
        {
                float x = Input.GetAxis("Mouse X") * panSpeed;
                float y = Input.GetAxis("Mouse Y") * panSpeed;
                transform.Translate(x, y, 0);
        }

        //zoom
        GameObject canvas = GameObject.Find("Canvas");          //since file browser only part of the build view 
        if (canvas == null)                                     //  we need to handle if it is in sim view.
        {
            bgBrowserEnabled = false;
        }
        else
        {
            ImageFileFinder browserScript = canvas.GetComponent<ImageFileFinder>();
            bgBrowserEnabled = browserScript.getBrowserEnabled();
        }

        if ((Input.GetAxis("Mouse ScrollWheel") > 0) && Camera.main.orthographicSize > 2 && !bgBrowserEnabled) // forward, max is min distance is 2
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - 0.5f;
        }

        if ((Input.GetAxis("Mouse ScrollWheel") < 0) && Camera.main.orthographicSize < 20 && !bgBrowserEnabled) // back, maxzoom is 20
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + 0.5f;
        }
    }
}
