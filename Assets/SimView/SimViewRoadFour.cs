// Copyright(c) 2015 Stateware Team -- Licensed GPL v3
// File Name: 	     SimViewRoadFour.cs
// Description:	     Subclass of SimViewRoad for 4 lanes
// Dependencies:     Compiler.cs, BuildViewLink.cs, SimViewRoad
// Additional Notes: N/A

public class SimViewRoadFour : SimViewRoad
{
	public BuildViewNode _origin;
	public BuildViewNode _destination;
	private int _numOfLane;
    
	// Description: Inherits SimViewRoad.cs
	// PRE:			Link with 4 lanes doesn't have textures
	// POST:		Variable roadRenderer obtains the component of LineRenderer and the road obatain textures 
	public void Start()
    {
		base.Start();
		base.SetProperties (_origin, _destination, _numOfLane);
	}
}
