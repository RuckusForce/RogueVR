using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the reticle by generating a RayCastHit2D
/// from the Camera to the Canvas
/// </summary>
public class ReticleAimerScript : MonoBehaviour {
	RaycastHit2D hit;
	public Vector3 target;
	GameObject reticle;
	Camera someCamera;
	Transform worldCanvasTransform;
	float sensitivity;

	void Awake() {
		reticle = GameObject.Find("Reticle");
		someCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
		worldCanvasTransform = GameObject.Find("WorldCanvas").transform;
		sensitivity = .7f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		hit = Physics2D.Raycast(transform.position, transform.rotation * Vector3.forward*100f);
		Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 100f);
		if (hit.collider != null) {
			//Debug.Log("hit: " + hit.point.x + ", " + hit.point.y);

			//target = new Vector3(
			//	(hit.point.x) * sensitivity, 
			//	(hit.point.y) * sensitivity, 
			//	worldCanvasTransform.position.z);
			target = someCamera.transform.position+ someCamera.transform.forward * 100f;
			target = new Vector3(target.x*sensitivity, target.y*sensitivity, worldCanvasTransform.position.z);//keep to the same z-depth as the worldCanvasTransform
			reticle.transform.position = target;//drifts back since the sensitivity affects the camera follow 
			reticle.transform.localPosition = target;//drifts forward since the sensitivity affects the camera follow 
		}



		//adjust target by accounting for player position;
		// Move the gaze cursor to keep it in the middle of the view
		//reticle.transform.position = someCamera.transform.position + someCamera.transform.forward * 100f;
		//transform.position = new Vector3(transform.position.x*sensitivity, transform.position.y* sensitivity, 0f);//keep to the same z-depth as the platforms
		//reticle.transform.localPosition = new Vector3(hit.point.x*1f, hit.point.y*1f, 0f);
	}
}
