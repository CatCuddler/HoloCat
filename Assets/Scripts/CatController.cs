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

		if (Application.isEditor) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				OnSelect();
			}
		}
	}

	// Called by GazeGestureManager when the user performs a Select gesture
	void OnSelect() {
		Debug.Log("OnSelect");

		// If the hologram has no Rigidbody component, add one to enable physics.
		if (!Application.isEditor) {
			if (!this.GetComponent<Rigidbody>()) {
				var rigidbody = this.gameObject.AddComponent<Rigidbody>();
				rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
			}
		}

		moveCat(state + 1);

	}

	private void moveCat(int catState) {

		state = catState;

		if (state > 2) {
			state = 0;
		}

		if (state == 0) {
			speed = 0.0f;
			Debug.Log("Idle");
		} else if (state == 1) {
			speed = 0.1f;
			Debug.Log("Walk");
		} else if (state == 2) {
			speed = 0.5f;
			Debug.Log("Run");
		}

		animator.SetInteger("State", state);
	}

	public void OnIdle() {
		moveCat(0);
	}

	public void OnWalk() {
		moveCat(1);
	}

	public void OnRun() {
		moveCat(2);
	}

	void OnReset() {
		state = 0;
	}
}
