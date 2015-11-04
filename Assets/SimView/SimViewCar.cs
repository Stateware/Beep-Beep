using UnityEngine;
using System.Collections;

public class SimViewCar : MonoBehaviour {
	public SimViewRoad[] roads;
	private int currRoadIndex = 0;

	private float speed = 4f;
	private float angle;
	private float currentTravelDistance = 0.0f;
	private float currRoadLength;
	
	private Vector3 origin;
	private Vector3 destination;
	
	private Vector3[] path = {new Vector3(4, -3, -2), new Vector3(2, 4, -2), new Vector3(-3, -4, -2), new Vector3(7, 1, -2), new Vector3(-3, 3, -2), new Vector3(4, -3, -2)};

	private int i = 2;
	// Use this for initialization
	void Start () {
		transform.position = path [0];
		destination = path [1];
		setAngle ();
		transform.eulerAngles = new Vector3 (0, 0, angle);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position == destination && i <= 5) {
			destination = path [i];
			setAngle ();
			transform.eulerAngles = new Vector3 (0, 0, angle);
			i++;
		}
		transform.position = Vector3.MoveTowards (transform.position, destination, speed * Time.deltaTime);
		// Test
		if (i == 6)
			i = 1;
	}

	public void setAngle() {
		angle = Mathf.Abs (transform.position.y - destination.y) / Mathf.Abs (transform.position.x - destination.x);
		if ((transform.position.y - destination.y) * (transform.position.x - destination.x) < 0)
			angle *= -1;
		
		angle = Mathf.Rad2Deg * Mathf.Atan (angle);

		if (transform.position.x - destination.x > 0)
			angle += 180;
	}
}
