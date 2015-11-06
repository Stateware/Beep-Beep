using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class BuildViewLink : MonoBehaviour {

	// Link properties
	private bool isLeftDirected = true;
	private bool isRightDirected = false;
	private bool isUndirected = false;
	public Node origin;
	public Node destination;
	private int laneNum = 1;
	private LineRenderer lineRenderer;
	private BoxCollider lineCollider;
	private float lineRendererWidth = 0.35f;
	int originID;
	int destinationID;
	string toLogger;
    public Link link;

	// Initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineCollider = gameObject.AddComponent<BoxCollider> ();
        link = gameObject.AddComponent<Link>();
	}
	
	// Update is called once per frame
	void Update () {
		// Store origin and destination position
		Vector3 originPos = origin.transform.position;
		Vector3 destinationPos = destination.transform.position;
		
		// Draw links
		lineRenderer.SetWidth (lineRendererWidth, lineRendererWidth);
		if (isUndirected) {
			lineRenderer.SetPosition (0, originPos);
			lineRenderer.SetPosition (1, destinationPos);
		}
		else if (isLeftDirected) {
			lineRenderer.SetPosition (0, originPos);
			lineRenderer.SetPosition (1, destinationPos);
		}
		else if (isRightDirected) {
			lineRenderer.SetPosition (0, destinationPos);
			lineRenderer.SetPosition (1, originPos);
		}

		// Set the size of lineCollider to the lineRenderer size
		float lineLength = Vector3.Distance (originPos, destinationPos);
		lineCollider.size = new Vector3 (lineLength, lineRendererWidth, 0);
		lineCollider.transform.position = (originPos + destinationPos) / 2;

		float angle = Mathf.Abs (originPos.y - destinationPos.y) / Mathf.Abs (originPos.x - destinationPos.x);

		// If the angle is in second or fourth quadrant
		if ((originPos.y - destinationPos.y) * (originPos.x - destinationPos.x) < 0)
			angle *= -1;

		angle = Mathf.Rad2Deg * Mathf.Atan (angle);

		// Rotate lineCollider to lineRenderer's angle
		lineCollider.transform.eulerAngles = new Vector3 (0, 0, angle);

		lineRenderer.material.mainTextureScale = new Vector2(lineLength / 2, 1);
		/*
		if (!origin.IsConnected || !destination.IsConnected ) {
			origin.IsConnected=true;
			destination.IsConnected=true;
		}
*/
		//toLogger = "Node #"+origin.name + " -> " + "Node #"+destination.name;
		//Debug.Log (toLogger);
	}

	void OnMouseDrag () {
		Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);

		// Get the offset vector
		Vector3 offPosition = new Vector3 (objPosition.x, objPosition.y, transform.position.z) - transform.position;
		origin.transform.position += offPosition;
		destination.transform.position += offPosition;
	}

	// Select link property
	void setLinkProperty(string prop) {

	}

	// Remove selected links
	void removeSelected() {

	}

	// Remove all links
	void removeAll() {

	}
}
