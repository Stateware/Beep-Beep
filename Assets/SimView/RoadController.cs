using UnityEngine;
using System.Collections;

public class RoadController : MonoBehaviour {

    private Link linkProperties;

    public void initializeRoad()
    {
        linkProperties = gameObject.GetComponent<Link>();
        setRoadProperties();
    }

    private void setRoadProperties()
    {
        //get some component. set some values.
    }
}
