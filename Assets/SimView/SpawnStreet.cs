using UnityEngine;
using System.Collections;

public class SpawnStreet : MonoBehaviour {

	private LineRenderer roadRenderer;
	public BuildViewLink[] allLinks;

	public GameObject roadPrefab;
	public GameObject road1lanePrefab;
	public GameObject road2lanePrefab;
	public GameObject road3lanePrefab;
	public GameObject road4lanePrefab;


	// Use this for initialization
	void Start () {
		roadRenderer = GetComponent<LineRenderer> ();
		allLinks = UnityEngine.Object.FindObjectsOfType<BuildViewLink>();


		foreach (object go in allLinks) {
			BuildViewLink link = (BuildViewLink)go;

			Vector3 originPos = link.origin.transform.position;
			Vector3 destiPos = link.destination.transform.position;

			roadRenderer.SetWidth(0.35f, 0.35f);
			roadRenderer.SetPosition (0, originPos);
			roadRenderer.SetPosition (1, destiPos);

	
			GameObject curRoad = Instantiate(roadPrefab);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
