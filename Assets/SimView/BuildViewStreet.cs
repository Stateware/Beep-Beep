﻿using UnityEngine;
using System.Collections;

public class BuildViewStreet : MonoBehaviour {

	private LineRenderer roadRenderer;
	public BuildViewLink[] allLinks;

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
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}