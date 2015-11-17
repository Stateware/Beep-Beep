using UnityEngine;
using System.Collections;

//File Name: 	RoadController.cs
//Description:	This file should switch the components from links to roads depending on the number of lanes
//Dependencies:	BuildViewLink.cs Link.cs
//Additional Notes: We only take care of the numoflanes here, directional is for future


public class RoadController : MonoBehaviour {

    private Link linkProperties;
    public void initializeRoad()
    {
        linkProperties = gameObject.GetComponent<Link>();
        setRoadProperties();
    }

	//Description:	This sets road properties depending on the num of lanes.
	//PRE:			the scene has links
	//Post:			the scene will have roads
	
    private void setRoadProperties()
    {
		int numOfLanes = linkProperties.NumberOfLanes;
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
