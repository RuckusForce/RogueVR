using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

	float rotX;
	float rotY;
	float rotZ;

	void Awake() {
		rotX = Random.Range(3f, 7f);
		rotY = Random.Range(3f, 7f);
		rotZ = Random.Range(3f, 7f);
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(rotX, 0f, 0f);
	}
}
