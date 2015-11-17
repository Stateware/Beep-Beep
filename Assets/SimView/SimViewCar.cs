using UnityEngine;
using System.Collections;

public class SimViewCar : MonoBehaviour {
	public SimViewRoad[] roads;

	private float maxSpeed = 3f;
	private float speed = 0.0f;
	private float angle;
	private float reactionTime = 0.2f;
	private float acce = 0.02f;
	private float deac = -0.5f;
	
	private Vector3 origin;
	private Vector3 destination;
	
	private Vector3[] path = {new Vector3(4, -3, -2), new Vector3(2, 4, -2), new Vector3(-3, -4, -2), new Vector3(7, 1, -2), new Vector3(-3, 3, -2), new Vector3(4, -3, -2)};

	private int i = 2;

	// Use this for initialization
	void Start () {
		origin = path [0];
		transform.position = path [0];
		destination = path [1];
		setAngle ();
		transform.eulerAngles = new Vector3 (0, 0, angle);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position == destination && i <= 5) {
			origin = path [i - 1];
			destination = path [i];
			setAngle ();
			transform.eulerAngles = new Vector3 (0, 0, angle);
			i++;
			speed = 0;
		}
		speed = getSpeed ();

		transform.position = Vector3.MoveTowards (transform.position, destination, speed * Time.deltaTime);

		// Test
		if (i == 6)
			i = 1;
	}

	public float getSpeed() {
		return speed + 2.5f * acce * reactionTime * (1 - speed / maxSpeed) * Mathf.Sqrt (0.025f + speed / maxSpeed);
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
