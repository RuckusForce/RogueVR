using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	Transform target;
	float posX;
	float posY;
	Vector2 velocity;
	float smoothTimeX;
	float smoothTimeY;
	float offsetX;

	// Use this for initialization
	void Start () {
		target = GameObject.Find("Hero2 (1)").transform;
		smoothTimeX = 0f;
		smoothTimeY = .2f;
		offsetX = -4f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        posX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref velocity.x, smoothTimeX);
        transform.position = new Vector3(posX - offsetX, transform.position.y, transform.position.z);
        
    }
}
