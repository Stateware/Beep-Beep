using UnityEngine;
using System.Collections;

public class BuildViewLink : MonoBehaviour {

	// Link properties
	private bool isLeftDirected = false;
	private bool isRightDirected = false;
	private bool isUndirected = true;
	public BuildViewNode origin;
	public BuildViewNode destination;
	private int laneNum = 1;
	private LineRenderer lineRenderer;
	private BoxCollider lineCollider;
	private float lineRendererWidth = 0.35f;
	
	// Initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineCollider = gameObject.AddComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Store origin and destination position
		Vector3 originPos = origin.transform.position;
		Vector3 destinationPos = destination.transform.position;

		// Draw links
		lineRenderer.SetPosition (0, originPos);
		lineRenderer.SetWidth (lineRendererWidth, lineRendererWidth);
		lineRenderer.SetPosition (1, destinationPos);

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
