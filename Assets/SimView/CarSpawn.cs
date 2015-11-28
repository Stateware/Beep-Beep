// File Name: CarSpawn.cs
// Description: 
// Dependencies:
// Additional Notes: 

using UnityEngine;
using System.Collections;

public class CarSpawn : MonoBehaviour {
	public GameObject CarPrefab;

	// Description: 
    // PRE: 
    // POST:
	void Start ()
    {
        // TODO: Implement?
	}
	
	// Description: 
    // PRE:
    // POST:
	void Update ()
    {
	    // TODO: Implement?
	}

    // Description:
    // PRE:
    // POST:
	public void Spawn () {
		GameObject newCar = Instantiate (CarPrefab);
	}
}
