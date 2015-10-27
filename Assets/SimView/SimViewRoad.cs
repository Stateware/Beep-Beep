using UnityEngine;
using System.Collections;

public class SimViewRoad : MonoBehaviour {

	public BuildViewNode origin;
	public BuildViewNode destination;
	private float length = 0.0f;
	public LineRenderer roadRenderer;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setProperties (BuildViewNode theOrigin, BuildViewNode theDestination, LineRenderer theRoadRenderer){
		origin = theOrigin;
		destination = theDestination;
		length = Vector3.Distance (origin.transform.position, destination.transform.position);
		roadRenderer = theRoadRenderer;
	}
}
