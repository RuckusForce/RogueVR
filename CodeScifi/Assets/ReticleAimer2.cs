using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parented to a railed camera
/// Hold reference to reticle canvas
/// Raycast into world canvas
/// Store hit position
/// Change reticle canvas global position
/// </summary>

public class ReticleAimer2 : MonoBehaviour {

	Transform reticleCanvasTransform;
	Transform worldCanvasTransform;
	Ray cameraRay;

	// Use this for initialization
	void Awake () {
		reticleCanvasTransform = GameObject.Find("Reticle").transform;
		worldCanvasTransform = GameObject.Find("WorldCanvas").transform;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		cameraRay = new Ray(transform.position, transform.forward);
		RaycastHit2D hit = Physics2D.Raycast(cameraRay.origin, cameraRay.direction*100f);
		Debug.DrawRay(cameraRay.origin, cameraRay.direction*100f, Color.green);
		if (hit.collider != null) {
			Debug.Log(hit.collider.gameObject + " at " + hit.collider.transform.position);
		}
	}
}
