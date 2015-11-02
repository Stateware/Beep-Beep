using UnityEngine;
using System.Collections;

public class SimViewCar : MonoBehaviour {
	public SimViewRoad[] roads;
	private int currRoadIndex = 0;

	private float speed = 4f;
	private float currentTravelDistance = 0.0f;
	private float currRoadLength;
	
	private Vector3 origin = new Vector3(-7, -3, 0);
	private Vector3 destination = new Vector3(4, 4, 0);
	
	private Vector3[] path = {new Vector3(1, -3, 0), new Vector3(2, 2, 0), new Vector3(-3, -4, 0), new Vector3(2, 1, 0), new Vector3(-3, 5, 0), new Vector3(-2, 1, 4)};

	private int i = 0;
	// Use this for initialization
	void Start () {
		origin = path [0];
		destination = path [1];
	}
	
	// Update is called once per frame
	void Update () {
		float angle = Mathf.Abs (origin.y - destination.y) / Mathf.Abs (origin.x - destination.x);
		
		// If the angle is in second or fourth quadrant
		if ((origin.y - destination.y) * (origin.x - destination.x) < 0)
			angle *= -1;
		
		angle = Mathf.Rad2Deg * Mathf.Atan (angle);
		
		// Rotate lineCollider to lineRenderer's angle
		transform.eulerAngles = new Vector3 (0, 0, angle);
		// Vector3 origin = roads [currRoadIndex].origin.transform.position;
		// Vector3 destination = roads [currRoadIndex].destination.transform.position;
		// transform.position = Vector3.Lerp(origin, destination, speed * Time.deltaTime);
		if (transform.position == destination && i != 5) {
			origin = path [i];
			destination = path [i + 1];
			i = i + 1;



		}
		transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);


	}

    void OnLevelWasLoaded(int level)
    {
        Debug.Log("Checking Level in simviewcar");
        if(level == 1)
        {
            Debug.Log("level 1");
        }
    }
}
