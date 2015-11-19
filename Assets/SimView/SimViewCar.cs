using UnityEngine;
using System.Collections;

public class SimViewCar : MonoBehaviour {
	public SimViewRoad[] roads;

	private float maxSpeed = 5f;
	private float speed = 0.0f;
	private float angle;
	private float reactionTime = 0.005f;
	private float acce = 0.5f;
	private float deac = -0.5f;
	
	private Vector3 origin;
	private Vector3 destination;

	private int currRoadIndex = 0;

	// Use this for initialization
	void Start () {
		origin = roads[currRoadIndex].origin.transform.position + new Vector3(0, 0, -2);
		transform.position = origin;
		destination = roads[currRoadIndex].destination.transform.position + new Vector3(0, 0, -2);
		setAngle ();
		transform.eulerAngles = new Vector3 (0, 0, angle);
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position, destination) < 0.1f && currRoadIndex < 3) {
			currRoadIndex++;
			origin = roads[currRoadIndex].origin.transform.position + new Vector3(0, 0, -2);
			transform.position = origin;
			destination = roads[currRoadIndex].destination.transform.position + new Vector3(0, 0, -2);
			setAngle ();
			transform.eulerAngles = new Vector3 (0, 0, angle);
			speed = 0;
		}
		speed = getSpeed ();

		transform.position = Vector3.MoveTowards (transform.position, destination, speed * Time.deltaTime);

		// Test
		if (currRoadIndex == 3)
			currRoadIndex = -1;
	}

	public float getSpeed() {
		float speedA = speed + 2.5f * acce * reactionTime * (1 - speed / maxSpeed) * Mathf.Sqrt (0.025f + speed / maxSpeed);

		float travelDistance = Vector3.Distance (transform.position, origin);
		float roadLength = Vector3.Distance (origin, destination);
		float temp = deac * reactionTime;
		float speedB = temp + Mathf.Sqrt (temp * temp - deac * (2 * (roadLength - travelDistance - 0.02f) - speed * reactionTime));
		Debug.Log (temp * temp - deac * (2 * (roadLength - travelDistance - 0.02f) - speed * reactionTime));
		return Mathf.Min (speedA, speedB);
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
