using UnityEngine;
using System.Collections;

public class SimViewCar : MonoBehaviour {
	public SimViewRoad[] roads;
	private int currRoadIndex = 0;

	private float speed = 2f;
	private float currentTravelDistance = 0.0f;
	private float currRoadLength;

	private Vector3 origin;
	private Vector3 destination;


	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		origin = new Vector3 (Random.Range(-9, 9), Random.Range(-5, 5), 0);
		destination = new Vector3 (Random.Range(-9, 9), Random.Range(-5, 5), 0);
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
