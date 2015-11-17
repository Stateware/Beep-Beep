////////////////////////////////////////////////////////////////////////////////
//
//	File			: BuildViewLink.cs
//	Description		: This is the source code for BuildViewLink in BuildView
//
//	Author			: Haojun Sui, Yuehui Wang
//	Last Modified	: Fri Nov  6 10:14:36 EST 2015
//

// Include Classes
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class BuildViewLink : MonoBehaviour {

	// Link properties
	private bool isLeftDirected = true;
	private bool isRightDirected = false;
	private bool isUndirected = false;
	private int laneNum = 1;
	private float angle;
	private LineRenderer lineRenderer;
	private BoxCollider lineCollider;
	private float lineRendererWidth = 0.35f;

	public Node origin;
	public Node destination;
	public Link link;

	int originID;
	int destinationID;
	string toLogger;
    
	////////////////////////////////////////////////////////////////////////////////
	//
	// Function     	: Start
	// Description  	: Initialize BuildViewLink
	//
	// Inputs       	: None
	// Outputs      	: None
	//
	// Pre-conditions	: None
	// Post-conditions	: Collider and LineRenderer set to BuildViewLink

	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineCollider = gameObject.AddComponent<BoxCollider> ();
        link = gameObject.AddComponent<Link>();
	}

	////////////////////////////////////////////////////////////////////////////////
	//
	// Function     	: Update
	// Description  	: Update BuildViewLink's collider's angle, and change its
	//					  texture accordingly. Update is called once per frame
	//
	// Inputs       	: None
	// Outputs      	: None
	//
	// Pre-conditions	: BuildViewLink successfully initialzed and has origin and
	//					  destination set
	// Post-conditions	: BuildViewLink's collider's angle and texture updated

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

		// Update angle of the collider for BuildViewLink
		setAngle (originPos, destinationPos);

		// Rotate lineCollider to lineRenderer's angle
		lineCollider.transform.eulerAngles = new Vector3 (0, 0, angle);

		// Tile the texture according to current length
		lineRenderer.material.mainTextureScale = new Vector2(lineLength / 2, 1);
	}

	////////////////////////////////////////////////////////////////////////////////
	//
	// Function     	: OnMouseDrag
	// Description  	: Move BuildViewLink, origin, and destination responsively
	//
	// Inputs       	: None
	// Outputs      	: None
	//
	// Pre-conditions	: origin and destination has been assigned
	// Post-conditions	: BuildViewLink, origin, and destination's positions were changed

	void OnMouseDrag () {
		Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);

		// Get the offset vector
		Vector3 offPosition = new Vector3 (objPosition.x, objPosition.y, transform.position.z) - transform.position;
		origin.transform.position += offPosition;
		destination.transform.position += offPosition;
	}

	////////////////////////////////////////////////////////////////////////////////
	//
	// Function     	: setAngle
	// Description  	: Set collider's angle to current one
	//
	// Inputs       	: originPos - origin's Vector3 position
	//					  destinationPos - destination's Vector3 position
	// Outputs      	: None
	//
	// Pre-conditions	: angle is created
	// Post-conditions	: BuildViewLink's collider's angle updated

	void setAngle(Vector3 originPos, Vector3 destinationPos) {
		angle = Mathf.Abs (originPos.y - destinationPos.y) / Mathf.Abs (originPos.x - destinationPos.x);
		
		// If the angle is in second or fourth quadrant
		if ((originPos.y - destinationPos.y) * (originPos.x - destinationPos.x) < 0)
			angle *= -1;
		
		angle = Mathf.Rad2Deg * Mathf.Atan (angle);
	}

	public int getLaneNum () {
		return laneNum;	
	}

}
