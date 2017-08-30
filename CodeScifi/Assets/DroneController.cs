using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour {

	Rigidbody rb;
	Vector3 target;
	bool hasTarget;

	void Awake() {
		rb = GetComponent<Rigidbody>();
		hasTarget = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasTarget) {
			rb.MovePosition(target);
		}
	}

	public void SetTarget(Vector3 newPosition) {
		target = newPosition;
		hasTarget = true;
	}
}
