// File Name: 	     SimViewRoadOne.cs
// Description:	     Subclass of SimViewRoad for 1 lane
// Dependencies:     Compiler.cs, BuildViewLink.cs, SimViewRoad
// Additional Notes: N/A

public class SimViewRoadOne : SimViewRoad {

    // Description: Inherite SimViewRoad.cs
	//PRE:			Link with 1 lane doesn't have textures
	//POST:			Variable roadRenderer obtains the component of LineRenderer and the road obatain textures
    void Start()
    {
        base.Start();
    }

    // Description: 
    // PRE:
    // POST:
    void Update() { }
}
	