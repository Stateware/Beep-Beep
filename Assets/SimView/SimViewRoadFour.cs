// File Name: 	     SimViewRoadFour.cs
// Description:	     Subclass of SimViewRoad for 4 lanes
// Dependencies:     Compiler.cs, BuildViewLink.cs, SimViewRoad
// Additional Notes: N/A

public class SimViewRoadFour : SimViewRoad {
    
	//Description: Inherite SimViewRoad.cs
	//PRE:			Link with 4 lanes doesn't have textures
	//POST:			Variable roadRenderer obtains the component of LineRenderer and the road obatain textures 
	void Start()
    {
		base.Start();
	}
}
