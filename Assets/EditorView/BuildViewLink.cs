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
	private BoxCollider myCollider;


	// Initialization
	void Start () {
		myCollider = gameObject.GetComponent<BoxCollider> ();
		lineRenderer = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		lineRenderer.SetPosition (0, origin.transform.position);
		lineRenderer.SetWidth (.25f, .25f);
		lineRenderer.SetPosition(1, destination.transform.position);

		// Positions the BoxCollider in the middle of the link
		transform.position = (origin.transform.position + destination.transform.position) / 2;

		// Fit the BoxCollider with the parameter of the link
		transform.position = (origin.transform.position + destination.transform.position) / 2;
		float colliderWidth = Mathf.Max (origin.transform.position.x, destination.transform.position.x) - Mathf.Min (origin.transform.position.x, destination.transform.position.x);
		float colliderHeight = Mathf.Max (origin.transform.position.y, destination.transform.position.y) - Mathf.Min (origin.transform.position.y, destination.transform.position.y);
		myCollider.size = new Vector3 (colliderWidth, colliderHeight, myCollider.size.z);
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
