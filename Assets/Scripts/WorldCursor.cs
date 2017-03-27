using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCursor : MonoBehaviour {

	private MeshRenderer meshRenderer;

	// Use this for initialization
	void Start () {
	    meshRenderer = this.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		// Do a raycast into the world based on the user's head position and orientation.
		Vector3 headPosition = Camera.main.transform.position;
		Vector3 gazeDirection = Camera.main.transform.forward;

		RaycastHit hitInfo;

		if (Physics.Raycast(headPosition, gazeDirection, out hitInfo)) {
			meshRenderer.enabled = true;

			// Move thecursor to the point where the raycast hit.
			this.transform.position = hitInfo.point;

			// Rotate the cursor to hug the surface of the hologram.
			this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
		} else {
			// If the raycast did not hit a hologram, hide the cursor mesh.
			meshRenderer.enabled = false;
		}
	}
}
