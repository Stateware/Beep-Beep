using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BuildViewBackground : MonoBehaviour {

    void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Camera.main.GetComponent<BuildViewSelectionHandler>().ClearSelection();
        }
    }

    void Update()
    {
        Resize();
    }

    void Resize(bool keepAspect = false)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr.sprite != null)
        {
            transform.localScale = new Vector3(1, 1, 1);

            // example of a 640x480 sprite
            float width = sr.sprite.bounds.size.x; // 4.80f
            float height = sr.sprite.bounds.size.y; // 6.40f

            // and a 2D camera at 0,0,-10
            float worldScreenHeight = Camera.main.orthographicSize * 2f + 1; // 10f
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width + 1; // 10f

            Vector3 imgScale = new Vector3(1f, 1f, 1f);

            // do we scale according to the image, or do we stretch it?
            if (keepAspect)
            {
                Vector2 ratio = new Vector2(width / height, height / width);
                if ((worldScreenWidth / width) > (worldScreenHeight / height))
                {
                    // wider than tall
                    imgScale.x = worldScreenWidth / width;
                    imgScale.y = imgScale.x * ratio.y;
                }
                else
                {
                    // taller than wide
                    imgScale.y = worldScreenHeight / height;
                    imgScale.x = imgScale.y * ratio.x;
                }
            }
            else
            {
                imgScale.x = worldScreenWidth / width;
                imgScale.y = worldScreenHeight / height;
            }

            // apply change
            transform.localScale = imgScale;
        }
    }
}
