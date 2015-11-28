// File Name:       BuildViewBackground.cs
// Description:     This script controls the interaction between the user and the background. This ensures that the
//                  background persists with the camera zoom changes, and movement, and calls the appropriate function
//                  when clicked.
// Dependencies:    The camera must have a BuildViewSelectionHandler component.
// Additional Notes:
 
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildViewBackground : MonoBehaviour {

    // Description: Unity3D API function that is called when a mouse is released over this object.
    // PRE:         The main camera has a BuildViewSelectionHandler component attached.
    // POST:        The selection of nodes is cleared.
    void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && !Input.GetKey("space"))
        {
            Camera.main.GetComponent<BuildViewSelectionHandler>().ClearSelection();
        }
    }

    // Description: Unity3D API function that is called when the frame is updated.
    // PRE:         N/A
    // POST:        This object is scaled and positioned appropriately to the camera.
    void Update()
    {
        Resize();
    }

    // Description: Rescales the image attached to this object to fit the screen.
    // PRE:         This object has a SpriteRenderer component with a defined sprite.
    // POST:        The image fills the screen, or fills it as much to keep its aspect ratio.
    private void Resize(bool keepAspect = false)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr.sprite != null)
        {
            transform.localScale = new Vector3(1, 1, 1);

            // Example of a 640x480 sprite
            float width = sr.sprite.bounds.size.x; // 4.80f
            float height = sr.sprite.bounds.size.y; // 6.40f

            // A 2D camera at 0,0,-10
            float worldScreenHeight = Camera.main.orthographicSize * 2f + 1; // 10f
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width + 1; // 10f

            Vector3 imgScale = new Vector3(1f, 1f, 1f);

            // Do we scale according to the image, or do we stretch it?
            if (keepAspect)
            {
                Vector2 ratio = new Vector2(width / height, height / width);
                if ((worldScreenWidth / width) > (worldScreenHeight / height))
                {
                    // Wider than tall
                    imgScale.x = worldScreenWidth / width;
                    imgScale.y = imgScale.x * ratio.y;
                }
                else
                {
                    // Taller than wide
                    imgScale.y = worldScreenHeight / height;
                    imgScale.x = imgScale.y * ratio.x;
                }
            }
            else
            {
                imgScale.x = worldScreenWidth / width;
                imgScale.y = worldScreenHeight / height;
            }

            // Apply change
            transform.localScale = imgScale;
        }
    }
}
