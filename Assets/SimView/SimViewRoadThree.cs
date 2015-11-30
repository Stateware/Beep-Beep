// File Name: 	     SimViewRoadThree.cs
// Description:	     Subclass of SimViewRoad for 3 lanes
// Dependencies:     Compiler.cs, BuildViewLink.cs, SimViewRoad
// Additional Notes: N/A

public class SimViewRoadThree : SimViewRoad {

	// Description: Inherite SimViewRoad.cs
	//PRE:			Link with 3 lanes doesn't have textures
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
