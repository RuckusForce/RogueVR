using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMoveLeft : MonoBehaviour {

	Rigidbody2D rb;
	float speed;
	Vector3 left;

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
		speed = 3f;
		left = new Vector3(-1, 0,0);
	}

	// Update is called once per frame
	void FixedUpdate () {
		rb.MovePosition(transform.position+left*speed*Time.deltaTime);
	}
}
