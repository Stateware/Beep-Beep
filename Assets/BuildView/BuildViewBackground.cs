using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BuildViewBackground : MonoBehaviour {

    void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Camera.main.GetComponent<BuildViewSelectionHandler>().ClearSelection();
            Debug.Log("Seleciton (attempted) cleared");
        }
    }
}
