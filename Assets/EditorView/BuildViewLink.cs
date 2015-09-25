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
	private Collider myCollider;
	
	public Vector2 offSetOri;			//offSet for moving the links
	public Vector2 offSetDes;

	// Use this for initialization
	void Start () {
		myCollider = gameObject.GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetPosition (0, origin.transform.position);
		lineRenderer.SetWidth (.25f, .25f);

		lineRenderer.SetPosition(1, destination.transform.position);

		//the collider is positioned in the middle of the link
		transform.position = (origin.transform.position + destination.transform.position) / 2;

		//Calculate the offSets
		offSetOri = origin.transform.position - transform.position;
		offSetDes = destination.transform.position - transform.position;
	}

	void OnMouseDrag()
	{
		Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

		//Move the nodes along with the link being moved
		origin.transform.position = objPosition + offSetOri;
		destination.transform.position = objPosition + offSetDes;

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
