using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastForward : MonoBehaviour {
	GameObject reticle;
	float sensitivity;
	RaycastHit2D hit2d;
	RaycastHit2D hit2dButtons;
	//RaycastHit2D[] hitResults;//Have to keep this array local
	int layerMask;
	Transform raymarker;

	CameraFollow cameraFollow;

	void Awake() {
		reticle = GameObject.Find("Reticle");
		sensitivity = 1f;
		layerMask = 1 << LayerMask.NameToLayer("ReticleLayer");
		//raymarker = GameObject.Find("Raymarker").transform;
		cameraFollow = GameObject.Find("CameraContainer").GetComponent<CameraFollow>();
	}

	void FixedUpdate () {
		#region Raycast 2d using GetRayIntersection
		//Vector3 forward = transform.TransformDirection(Vector3.forward) * 100f;
		//Debug.DrawRay(transform.position, forward, Color.green);
		//Ray cameraRay = new Ray(transform.position, transform.forward);
		//hit2d = Physics2D.GetRayIntersection(cameraRay, 100f, layerMask);

		//if (hit2d)
		//{
		//	Debug.Log("hit: " + hit2d.transform.gameObject.tag + ": " + hit2d.transform.gameObject.name);
		//	if (hit2d.collider.gameObject.CompareTag("ReticleButtons")) {
		//		Debug.Log("hit: " + hit2d.collider.gameObject.name);
		//	}

		//}
		#endregion

		#region Raycast3d using GetRayIntersectionAll 
		Vector3 forward = transform.TransformDirection(Vector3.forward) * 100f;
		Debug.DrawRay(transform.position, forward, Color.green);
		Ray cameraRay = new Ray(transform.position, transform.forward);
		RaycastHit2D[] hitResults = Physics2D.GetRayIntersectionAll(cameraRay, 100f, layerMask);
		bool hitSomething = false;
		for (int i = 0; i < hitResults.Length; i++) {
			hit2d = hitResults[i];
			hitSomething = true;
		}
		if (!hitSomething) {
			hit2d = new RaycastHit2D();
			hit2d.point = cameraFollow.ReturnCameraMovement2D();
		}
		#endregion
	}

	public Vector3 returnHitPosition3D() {
		Vector3 hitPosition = new Vector3(hit2d.point.x, hit2d.point.y, hit2d.distance);
		return hitPosition;
	}

	public Vector2 returnHitPosition2D() {
		Vector2 hitPosition2D = new Vector2(hit2d.point.x, hit2d.point.y);
		return hitPosition2D;
	}
}
