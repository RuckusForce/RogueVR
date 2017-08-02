using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	Transform target;
	Transform cameraContainer;
	float posX;
	float posY;
	Vector2 velocity;
	float smoothTimeX;
	float smoothTimeY;
	float offsetX;
	float offsetY;
	float offsetZ;

	// Use this for initialization
	void Awake () {
		target = GameObject.Find("Hero2 (1)").transform;
		smoothTimeX = 0f;
		smoothTimeY = .2f;
		offsetX = -15f;
		offsetY = 2f;
		offsetZ = -8f;
		transform.position = new Vector3(transform.position.x, transform.position.y + offsetY, transform.position.z + offsetZ);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        posX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref velocity.x, smoothTimeX);
		transform.position = new Vector3(posX - offsetX, transform.position.y, transform.position.z);
    }
}
