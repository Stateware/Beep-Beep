// Copyright(c) 2015 Stateware Team -- Licensed GPL v3
// File Name: 	     SimViewRoad.cs
// Description:	     This is a class of the road, that RoadController uses to build streets 
// Dependencies:     Compiler.cs, BuildViewLink.cs
// Additional Notes: N/A 

using UnityEngine;
using System.Collections.Generic;

public class SimViewRoad : MonoBehaviour
{
    private float _length = 0.0f;
    private int _numOfLane;

    public BuildViewNode origin;
	public BuildViewNode destination;
    public LineRenderer roadRenderer;
	public List<SimViewCar> roadQueue = new List<SimViewCar>();
	
	// Description:	Initialize the roadRenderer with component LineRenderer and add the road texture 
	//				the links retrived from build view
	// PRE:			No roads on sim view
	// POST:	    Variable roadRenderer obtains the component of LineRenderer and the road obatain textures
	public void Start()
    {
		// Store origin and destination position
		Vector3 originPos = origin.transform.position;
		Vector3 destinationPos = destination.transform.position;

		roadRenderer = SetRoadRender(originPos, destinationPos);
	}		
	
	// Description:	Setter for SimViewRoad
	// PRE:			The properties of origin, destination and roadRenderer are uninitialized
	// POST:		Initialize origin, destination and roadRenderer with the parameters passed in 
	public void SetProperties(BuildViewNode theOrigin, BuildViewNode theDestination, int theNumOfLane)
    {
		origin = theOrigin;
		destination = theDestination;
		_length = Vector3.Distance(origin.transform.position, destination.transform.position);
		_numOfLane = theNumOfLane;
	}

	// Description:	Setter for the RoadRender, being called by start
	// PRE:			N/A
	// POST:		Set the roadRender
	public LineRenderer SetRoadRender(Vector3 oriPos, Vector3 desPos) 
	{
		LineRenderer roadRenderer = GetComponent<LineRenderer> ();
		// Draw links with the new roadRenderer
		roadRenderer.SetWidth (0.75f, 0.75f);
		roadRenderer.SetPosition (0, oriPos);
		roadRenderer.SetPosition (1, desPos);

		// Set the size of lineCollider to the lineRenderer size
		float lineLength = Vector3.Distance (oriPos, desPos);
		
		// Tile the texture according to current length
		roadRenderer.material.mainTextureScale = new Vector2(lineLength * 2, 1);
		return roadRenderer;
	}
}
