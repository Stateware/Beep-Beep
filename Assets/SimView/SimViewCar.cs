using UnityEngine;
using System.Collections;

public class SimViewCar : MonoBehaviour {
	public SimViewRoad[] roads;
	private int currRoadIndex = 0;

	private float speed = 2f;
	private float currentTravelDistance = 0.0f;
	private float currRoadLength;
	
	private Vector3 origin = new Vector3(-7, -3, 0);
	private Vector3 destination = new Vector3(4, 4, 0);
	
	private Vector3[] path = {new Vector3(1, -3, 0), new Vector3(2, 2, 0), new Vector3(-3, -4, 0), new Vector3(2, 1, 0), new Vector3(-3, 5, 0)};

	private int i = 0;
	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		origin = new Vector3 (Random.Range(-9, 9), Random.Range(-5, 5), 0);
		destination = new Vector3 (Random.Range(-9, 9), Random.Range(-5, 5), 0);
		transform.position = origin;
		destination = path [i];
	}
	
	// Update is called once per frame
	void Update () {
		// Vector3 origin = roads [currRoadIndex].origin.transform.position;
		// Vector3 destination = roads [currRoadIndex].destination.transform.position;
		// transform.position = Vector3.Lerp(origin, destination, speed * Time.deltaTime);
		if (transform.position == destination && i != 5) {
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
