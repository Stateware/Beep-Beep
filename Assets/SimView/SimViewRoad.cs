using UnityEngine;
using System.Collections;


//File Name: 	SimViewRoad.cs
//Description:	This is a class of the road, that SpawnStreet uses to build streets 
//Dependencies:	compiler.cs, BuildViewLink.cs
//Additional Notes: 


public class SimViewRoad : MonoBehaviour {

	public BuildViewNode origin;
	public BuildViewNode destination;
	private float length = 0.0f;
	private LineRenderer roadRenderer;
	

	//Description:	Initialize the roadRenderer with component LineRenderer and add the road texture 
	//				the links retrived from build view
	//PRE:			No roads on sim view
	//Post:			variable roadRenderer obtains the component of LineRenderer and the road obatain textures
	void Start () {
		roadRenderer = GetComponent<LineRenderer> ();
		// Store origin and destination position
		Vector3 originPos = origin.transform.position;
		Vector3 destinationPos = destination.transform.position;
		
		// Draw links with the new roadRenderer
		roadRenderer.SetWidth (0.35f, 0.35f);
		roadRenderer.SetPosition (0, originPos);
		roadRenderer.SetPosition (1, destinationPos);
	}
			
	void Update () {
	}

	//Description:	Setter for SimViewRoad
	//PRE:			The properties of origin, destination and roadRenderer are uninitialized
	//Post:			Initialize origin, destination and roadRenderer with the parameters passed in 

	public void setProperties (BuildViewNode theOrigin, BuildViewNode theDestination, LineRenderer theRoadRenderer){
		origin = theOrigin;
		destination = theDestination;
		length = Vector3.Distance (origin.transform.position, destination.transform.position);
		roadRenderer = theRoadRenderer;
	}
}
