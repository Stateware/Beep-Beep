using UnityEngine;
using System.Collections;

public class SimViewCar : MonoBehaviour {
	public SimViewRoad[] roads;
	private int currRoadIndex = 0;

	private float speed = 2f;
	private float currentTravelDistance = 0.0f;
	private float currRoadLength;

	private Vector3 origin = new Vector3(-7, -3, 0);
	private Vector3 destination = new Vector3(6, 6, 0);


	// Use this for initialization
	void Start () {
		transform.position = origin;
	}
	
	// Update is called once per frame
	void Update () {
		// Vector3 origin = roads [currRoadIndex].origin.transform.position;
		// Vector3 destination = roads [currRoadIndex].destination.transform.position;
		// transform.position = Vector3.Lerp(origin, destination, speed * Time.deltaTime);

		transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
	}
}
