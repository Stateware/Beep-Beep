using UnityEngine;
using System.Collections;

public class SimViewCar : MonoBehaviour {
	public SimViewRoad[] roads;
	private int currRoadIndex = 0;

	private float speed = 8f;
	private float currentTravelDistance = 0.0f;
	private float currRoadLength;
	
	private Vector3 origin;
	private Vector3 destination;
	
	private Vector3[] path = {new Vector3(1, -3, 0), new Vector3(2, 2, 0), new Vector3(-3, -4, 0), new Vector3(2, 1, 0), new Vector3(-3, 5, 0), new Vector3(-2, 1, 0)};

	private int i = 2;
	// Use this for initialization
	void Start () {
		transform.position = path [0];
		destination = path [1];
		transform.eulerAngles = new Vector3 (0, 0, getAngle ());
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position == destination && i <= 5) {
			destination = path [i];
			transform.eulerAngles = new Vector3 (0, 0, getAngle ());
			i++;
		}
		transform.position = Vector3.MoveTowards (transform.position, destination, speed * Time.deltaTime);
	}

	public float getAngle() {
		float angle = Mathf.Abs (transform.position.y - destination.y) / Mathf.Abs (transform.position.x - destination.x);
		if ((transform.position.y - destination.y) * (transform.position.x - destination.x) < 0)
			angle *= -1;
		
		angle = Mathf.Rad2Deg * Mathf.Atan (angle);
<<<<<<< HEAD
=======
		
		// Rotate lineCollider to lineRenderer's angle
		transform.eulerAngles = new Vector3 (0, 0, angle);
		// Vector3 origin = roads [currRoadIndex].origin.transform.position;
		// Vector3 destination = roads [currRoadIndex].destination.transform.position;
		// transform.position = Vector3.Lerp(origin, destination, speed * Time.deltaTime);
		if (transform.position == destination && i != 5) {

			destination = path [i];

			origin = path [i];
			destination = path [i + 1];
			i = i + 1;
		}
		transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

>>>>>>> e606fb2b38bab4d1c3e904915ae3f180c5e3e936

		return angle;
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
