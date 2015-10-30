using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BuildViewBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Camera.main.GetComponent<BuildViewSelectionHandler>().ClearSelection();
            Debug.Log("Seleciton (attempted) cleared");
        }
    }
}
