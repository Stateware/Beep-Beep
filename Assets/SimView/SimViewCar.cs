using UnityEngine;
using System.Collections;

public class SimViewCar : MonoBehaviour {
	public Transform origin;
	public Transform destination;

	public BuildViewStreet[] path;
	private int currentPathNum = 0;

	private float speed = 0.5f;
	private float currentTravelDistance = 0.0f;
	private float currentPathLength;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (origin != NULL && destination != NULL)
			currentPathLength = Vector3.Distance (path[currentPathNum].origin.transform.position, path[currentPathNum].destination.transform.position);
		if (currentTravelDistance + speed >= path[currentPathNum]) {
		}
	}
}
