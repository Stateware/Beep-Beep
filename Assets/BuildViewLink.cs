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
	private float counter;
	private float dist;

	public float lineDrawSpeed = 6f;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetPosition (0, origin.transform.position);
		lineRenderer.SetWidth (.45f, .45f);

		dist = Vector3.Distance (origin.transform.position, destination.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (counter < dist) {
			counter += .1f / lineDrawSpeed;

			float x = Mathf.Lerp (0, dist, counter);

			Vector3 pointA = origin.transform.position;
			Vector3 pointB = destination.transform.position;

			// Get the unit vector in the desired direction, multiply by the desired length and add the starting point
			Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

			lineRenderer.SetPosition(1, pointAlongLine);
		}
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
