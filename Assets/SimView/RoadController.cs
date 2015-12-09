// Copyright(c) 2015 Stateware Team -- Licensed GPL v3
// File Name: 	     RoadController.cs
// Description:	     This file should switch the components from links to roads depending on the number of lanes
// Dependencies:	 BuildViewLink.cs Link.cs
// Additional Notes: We only take care of the numOfLanes here, directional is for future

using UnityEngine;

public class RoadController : MonoBehaviour
{
    private Link _linkProperties;

    // Description: Initialize the links with components of Link
    // PRE:	        The links have their original properties
    // POST:        The links have roads' properties
    public void InitializeRoad()
    {
        _linkProperties = gameObject.GetComponent<Link>();
        SetRoadProperties();
    }

	// Description:	This sets road properties depending on the num of lanes.
	// PRE:			The scene has links
	// POST:		The scene will have roads
    private void SetRoadProperties()
    {
		int numOfLanes = _linkProperties.NumberOfLanes;
		if (numOfLanes == 1) {
			gameObject.AddComponent<SimViewRoadOne> ();
		} else if (numOfLanes == 2) {
			gameObject.AddComponent<SimViewRoadTwo> ();	
		} else if (numOfLanes == 3) {
			gameObject.AddComponent<SimViewRoadThree> ();	
		} else if (numOfLanes == 4) {
			gameObject.AddComponent<SimViewRoadFour> ();	
		}
    }
}
