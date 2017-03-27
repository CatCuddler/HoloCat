using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGameObject : MonoBehaviour {

	private Vector3 originalPosition;
	private Quaternion originalRotation;

	// Use this for initialization
	void Start () {
		originalPosition = transform.localPosition;
		originalRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.R)) {
			OnReset();
		}
	}

	void OnReset() {
		// If the Game Object has a Rigidbody component, remove it to disable physics.
		var rigidbody = this.GetComponent<Rigidbody>();
		if (rigidbody != null) {
			DestroyImmediate(rigidbody);
		}

		transform.localPosition = originalPosition;
		transform.localRotation = originalRotation;
	}
}
