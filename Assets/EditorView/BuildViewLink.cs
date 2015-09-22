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

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetPosition (0, origin.transform.position);
		lineRenderer.SetWidth (.45f, .45f);

		lineRenderer.SetPosition(1, destination.transform.position);
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
