using UnityEngine;
using System.Collections;

public class SimViewRoad : MonoBehaviour {

	private Vector3 origin;
	private Vector3 destination;
	private float length = 0.0f;
	private LineRenderer roadRenderer;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setProperties (Vector3 theOrigin, Vector3 theDestination, LineRenderer theRoadRenderer){
		origin = theOrigin;
		destination = theDestination;
		length = Vector3.Distance (origin, destination);
		roadRenderer = theRoadRenderer;
	}

	public Vector3 getOrigin (){
		return origin;
	}

	public Vector3 getDestination(){
		return destination;
	}
	public float getLength (){
		return length;
	}
}
