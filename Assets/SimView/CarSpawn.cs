using UnityEngine;
using System.Collections;

public class CarSpawn : MonoBehaviour {
	public GameObject CarPrefab;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

	public void Spawn () {
		GameObject newCar = Instantiate (CarPrefab);
	}

}
