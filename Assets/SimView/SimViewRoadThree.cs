// File Name: 	     SimViewRoadThree.cs
// Description:	     Subclass of SimViewRoad for 3 lanes
// Dependencies:     Compiler.cs, BuildViewLink.cs, SimViewRoad
// Additional Notes: N/A

public class SimViewRoadThree : SimViewRoad {

	public BuildViewNode _origin;
	public BuildViewNode _destination;
	private int _numOfLane;

	// Description: Inherite SimViewRoad.cs
	//PRE:			Link with 3 lanes doesn't have textures
	//POST:			Variable roadRenderer obtains the component of LineRenderer and the road obatain textures
    public void Start()
    {
        base.Start();
		base.SetProperties (_origin, _destination, _numOfLane);
    }
	
}
