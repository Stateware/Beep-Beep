// File Name: SimViewCar.cs
// Description: 
// Dependencies: 
// Additional Notes:

using UnityEngine;

public class SimViewCar : MonoBehaviour {
	public SimViewRoad[] roads;

	private float _maxSpeed = 5f;
	private float _speed = 0.0f;
	private float _angle;
	private float _reactionTime = 0.005f;
	private float _acce = 0.5f;
	private float _deac = -0.5f;
	
	private Vector3 _origin;
	private Vector3 _destination;

	private int _currRoadIndex = 0;

	// Description: 
    // PRE:
    // POST: 
	void Start ()
    {
		_origin = roads[_currRoadIndex].origin.transform.position + new Vector3(0, 0, -2);
		transform.position = _origin;
		_destination = roads[_currRoadIndex].destination.transform.position + new Vector3(0, 0, -2);
		SetAngle ();
		transform.eulerAngles = new Vector3 (0, 0, _angle);
	}
	
	// Description:
    // PRE:
    // POST:
	void Update ()
    {
		if (Vector3.Distance(transform.position, _destination) < 0.1f && _currRoadIndex < 3)
        {
			_currRoadIndex++;
			_origin = roads[_currRoadIndex].origin.transform.position + new Vector3(0, 0, -2);
			transform.position = _origin;
			_destination = roads[_currRoadIndex].destination.transform.position + new Vector3(0, 0, -2);
			SetAngle();
			transform.eulerAngles = new Vector3 (0, 0, _angle);
			_speed = 0;
		}
		_speed = GetSpeed ();

		transform.position = Vector3.MoveTowards (transform.position, _destination, _speed * Time.deltaTime);

        // Test
        if (_currRoadIndex == 3)
        {
            _currRoadIndex = -1;
        }
	}

    // Description:
    // PRE:
    // POST: 
	public float GetSpeed() {
		float speedA = _speed + 2.5f * _acce * _reactionTime * (1 - _speed / _maxSpeed) * Mathf.Sqrt (0.025f + _speed / _maxSpeed);

		float travelDistance = Vector3.Distance (transform.position, _origin);
		float roadLength = Vector3.Distance (_origin, _destination);
		float temp = _deac * _reactionTime;
		float speedB = temp + Mathf.Sqrt (temp * temp - _deac * (2 * (roadLength - travelDistance - 0.02f) - _speed * _reactionTime));
		Debug.Log (temp * temp - _deac * (2 * (roadLength - travelDistance - 0.02f) - _speed * _reactionTime));
		return Mathf.Min (speedA, speedB);
	}

    // Description:
    // PRE:
    // POST:
	public void SetAngle() {
		_angle = Mathf.Abs (transform.position.y - _destination.y) / Mathf.Abs (transform.position.x - _destination.x);
		if ((transform.position.y - _destination.y) * (transform.position.x - _destination.x) < 0)
			_angle *= -1;
		
		_angle = Mathf.Rad2Deg * Mathf.Atan (_angle);

		if (transform.position.x - _destination.x > 0)
			_angle += 180;
	}
}
