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

	// Use this for initialization
	void Start () {
		target = GameObject.Find("Hero2 (1)").transform;
		cameraContainer = transform.parent;
		smoothTimeX = 0f;
		smoothTimeY = .2f;
		offsetX = -12f;
		offsetY = 6f;
		cameraContainer.position = new Vector3(cameraContainer.position.x, cameraContainer.position.y + offsetY, cameraContainer.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        posX = Mathf.SmoothDamp(cameraContainer.position.x, target.transform.position.x, ref velocity.x, smoothTimeX);
		cameraContainer.position = new Vector3(posX - offsetX, cameraContainer.position.y, cameraContainer.position.z);
        //
		//
    }
}
