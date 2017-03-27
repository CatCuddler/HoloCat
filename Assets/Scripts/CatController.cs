using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour {

	private float speed = 0.0f;

	private Animator animator;

	private int state = 0;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * Time.deltaTime * speed);

		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Joystick1Button0)) {
			state = 1;
		} else if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Joystick1Button1)) {
			state = 2;
		} else if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Joystick1Button2)) {
			state = 0;
		} else if (Input.GetKeyDown(KeyCode.Space)) {
			OnSelect();
		}

		moveCat();
	}

	// Called by GazeGestureManager when the user performs a Select gesture
	void OnSelect() {
		Debug.Log("OnSelect");

		// If the hologram has no Rigidbody component, add one to enable physics.
		if (!this.GetComponent<Rigidbody>()) {
			var rigidbody = this.gameObject.AddComponent<Rigidbody>();
			rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
		}

		state += 1;

	}

	private void moveCat() {

		if (state > 2) {
			state = 0;
		}

		if (state == 1) {
			speed = 0.1f;
			Debug.Log("Walk");
		} else if (state == 2) {
			speed = 0.5f;
			Debug.Log("Run");
		} else if (state == 0) {
			speed = 0.0f;
			Debug.Log("Idle");
		}

		animator.SetInteger("State", state);
	}
}
