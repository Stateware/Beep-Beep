using UnityEngine;
using System.Collections;

public class SimViewRoad : MonoBehaviour {

	public BuildViewNode origin;
	public BuildViewNode destination;
	private float length = 0.0f;
	private LineRenderer roadRenderer;



	// Use this for initialization
	void Start () {
		roadRenderer = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Store origin and destination position
		Vector3 originPos = origin.transform.position;
		Vector3 destinationPos = destination.transform.position;
		
		// Draw links
		roadRenderer.SetWidth (0.35f, 0.35f);
		roadRenderer.SetPosition (0, originPos);
		roadRenderer.SetPosition (1, destinationPos);
		
		// Set the size of lineCollider to the lineRenderer size
		float lineLength = Vector3.Distance (originPos, destinationPos);
		transform.position = (originPos + destinationPos) / 2;
		
		float angle = Mathf.Abs (originPos.y - destinationPos.y) / Mathf.Abs (originPos.x - destinationPos.x);
		
		// If the angle is in second or fourth quadrant
		if ((originPos.y - destinationPos.y) * (originPos.x - destinationPos.x) < 0)
			angle *= -1;
		
		angle = Mathf.Rad2Deg * Mathf.Atan (angle);
		
		// Rotate lineCollider to lineRenderer's angle
		transform.eulerAngles = new Vector3 (0, 0, angle);
		
		roadRenderer.material.mainTextureScale = new Vector2(lineLength / 2, 1);
	
	}

	public void setProperties (BuildViewNode theOrigin, BuildViewNode theDestination, LineRenderer theRoadRenderer){
		origin = theOrigin;
		destination = theDestination;
		length = Vector3.Distance (origin.transform.position, destination.transform.position);
		roadRenderer = theRoadRenderer;
	}
}
