using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraHandler : MonoBehaviour
{
    public float panSpeed = 1;

    Vector3 bottomLeft;
    Vector3 topRight;

    void Start()
    {
        //set max camera bounds (assumes camera is max zoom and centered on Start)
        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, -transform.position.z));
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -transform.position.z));
    }

    void Update()
    {
        bool bgBrowserEnabled = false;

        if (Input.GetMouseButton(2))
        {
                float x = Input.GetAxis("Mouse X") * panSpeed;
                float y = Input.GetAxis("Mouse Y") * panSpeed;
                transform.Translate(x, y, 0);
        }

        //zoom
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            bgBrowserEnabled = false;
        }
        else
        {
            ImageFileFinder browserScript = canvas.GetComponent<ImageFileFinder>();
            bgBrowserEnabled = browserScript.getBrowserEnabled();
        }

        if ((Input.GetAxis("Mouse ScrollWheel") > 0) && Camera.main.orthographicSize > 2 && !bgBrowserEnabled) // forward   
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - 0.5f;
        }

        if ((Input.GetAxis("Mouse ScrollWheel") < 0) && Camera.main.orthographicSize < 20 && !bgBrowserEnabled) // back  && Camera.main.orthographicSize < maxZoom
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + 0.5f;
        }
    }
}