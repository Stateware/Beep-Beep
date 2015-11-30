//	File:            BuildViewLink.cs
//	Description:     This is the source code for BuildViewLink in BuildView
//
// Dependencies:     N/A
// Additional Notes: N/A

using UnityEngine;

public class BuildViewLink : MonoBehaviour {

	// Link properties
	private bool _isLeftDirected = true;
	private bool _isRightDirected = false;
	private bool _isUndirected = false;
	private int _laneNum = 1;
	private float _angle;
	private LineRenderer _lineRenderer;
	private BoxCollider _lineCollider;
	private float _lineRendererWidth = 0.35f;

	public Node origin;
	public Node destination;
	public Link link;

	int originID;
	int destinationID;
	string toLogger;
    
	// Description:  Initialize BuildViewLink
	// PRE:          N/A
	// POST:         Collider and LineRenderer set to BuildViewLink
	void Start () {
		_lineRenderer = GetComponent<LineRenderer> ();
		_lineCollider = gameObject.AddComponent<BoxCollider> ();
        link = gameObject.AddComponent<Link>();
	}

	// Description:  Update BuildViewLink's collider's angle, and change its texture accordingly. Update is called once per frame
	// PRE:          BuildViewLink successfully initialzed and has origin and destination set
	// POST:         BuildViewLink's collider's angle and texture updated
	void Update () {
		// Store origin and destination position
		Vector3 originPos = origin.transform.position;
		Vector3 destinationPos = destination.transform.position;
		
		// Draw links
		_lineRenderer.SetWidth (_lineRendererWidth, _lineRendererWidth);

		if (_isUndirected) {
			_lineRenderer.SetPosition (0, originPos);
			_lineRenderer.SetPosition (1, destinationPos);
		}
		else if (_isLeftDirected) {
			_lineRenderer.SetPosition (0, originPos);
			_lineRenderer.SetPosition (1, destinationPos);
		}
		else if (_isRightDirected) {
			_lineRenderer.SetPosition (0, destinationPos);
			_lineRenderer.SetPosition (1, originPos);
		}

		// Set the size of lineCollider to the lineRenderer size
		float lineLength = Vector3.Distance (originPos, destinationPos);
		_lineCollider.size = new Vector3 (lineLength, _lineRendererWidth, 0);
		_lineCollider.transform.position = (originPos + destinationPos) / 2;

		// Update angle of the collider for BuildViewLink
		SetAngle (originPos, destinationPos);

		// Rotate lineCollider to lineRenderer's angle
		_lineCollider.transform.eulerAngles = new Vector3 (0, 0, _angle);

		// Tile the texture according to current length
		_lineRenderer.material.mainTextureScale = new Vector2(lineLength / 2, 1);
	}

	// Description: Move BuildViewLink, origin, and destination responsively
	// PRE:         Origin and destination has been assigned
	// POST:        BuildViewLink, origin, and destination's positions were changed
	void OnMouseDrag () {
		Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);

		// Get the offset vector
		Vector3 offPosition = new Vector3 (objPosition.x, objPosition.y, transform.position.z) - transform.position;
		origin.transform.position += offPosition;
		destination.transform.position += offPosition;
	}

	// Description: Set collider's angle to current one
	// PRE:         Angle is created
	// POST:        BuildViewLink's collider's angle updated
	void SetAngle(Vector3 originPos, Vector3 destinationPos) {
		_angle = Mathf.Abs (originPos.y - destinationPos.y) / Mathf.Abs (originPos.x - destinationPos.x);
		
		// If the angle is in second or fourth quadrant
		if ((originPos.y - destinationPos.y) * (originPos.x - destinationPos.x) < 0)
			_angle *= -1;
		
		_angle = Mathf.Rad2Deg * Mathf.Atan (_angle);
	}

    // Description: Return the private variable _laneNum
    // PRE:  private variable is not accessible from outside       
    // POST:	return _laneNum as integer        
    public int GetLaneNum () {
		return _laneNum;	
	}
}
