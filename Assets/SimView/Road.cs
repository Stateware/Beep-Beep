using UnityEngine;
using System.Collections;

public class Road : MonoBehaviour
{
    private Link _linkProperties;
    
    public Road(Link linkProperties)
    {
        this.LinkProperties = linkProperties;
    }

    public Link LinkProperties
    {
        get { return _linkProperties; }
        set { _linkProperties = value; SetRoadProperties(); }
    }

    private void SetRoadProperties()
    {
        setNumberOfRoads(_linkProperties.NumberOfLanes);
    }

    private void setNumberOfRoads(int numberOfRoads)
    {
        //do something
    }

}
