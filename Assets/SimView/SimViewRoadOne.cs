﻿// Copyright(c) 2015 Stateware Team -- Licensed GPL v3
// File Name: 	     SimViewRoadOne.cs
// Description:	     Subclass of SimViewRoad for 1 lane
// Dependencies:     Compiler.cs, BuildViewLink.cs, SimViewRoad
// Additional Notes: N/A

public class SimViewRoadOne : SimViewRoad
{
    private int _numOfLane;

    public BuildViewNode origin;
	public BuildViewNode destination;
	
    // Description: Inherits SimViewRoad.cs
	// PRE:			Link with 1 lane doesn't have textures
	// POST:		Variable roadRenderer obtains the component of LineRenderer and the road obatain textures
    public void Start()
    {
        base.Start();
		base.SetProperties (origin, destination, _numOfLane);
    }
}
	