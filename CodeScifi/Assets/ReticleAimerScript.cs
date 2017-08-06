using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the reticle by generating a RayCastHit2D
/// from the Camera to the Canvas
/// </summary>
public class ReticleAimerScript : MonoBehaviour {
	//RaycastHit2D hit;
	public Vector3 target;
	public Vector3 rayTarget;
	public GameObject reticle;
	Camera someCamera;
	Transform worldCanvasTransform;
	float sensitivity;

	void Awake() {
		reticle = GameObject.Find("Reticle");
		someCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
		worldCanvasTransform = GameObject.Find("WorldCanvas").transform;
		sensitivity = 2f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		#region Raycast, ScreenToWorldPoint Attempt
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.rotation * Vector3.forward * 10f);
		Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 10f);
		if (hit.collider != null)
		{
			//Debug.Log("hit: " + hit.point.x + ", " + hit.point.y);

			rayTarget = new Vector3(
				(hit.point.x) * sensitivity,
				(hit.point.y) * sensitivity,
				worldCanvasTransform.position.z);

			//target = someCamera.transform.position + someCamera.transform.forward * 100f;
			//target = new Vector3(target.x * sensitivity, target.y * sensitivity, worldCanvasTransform.position.z);//keep to the same z-depth as the worldCanvasTransform
			target = someCamera.ScreenToWorldPoint(rayTarget);
			target = new Vector3(target.x * sensitivity, target.y, worldCanvasTransform.position.z);
			reticle.transform.position = target;//drifts back since the sensitivity affects the camera follow 
												//reticle.transform.localPosition = target;//drifts forward since the sensitivity affects the camera follow 
		}
		#endregion

		//#region Hit is relative to the origin, not the canvas 
		//////(previous OVR implementation used ScreenToWorldPoint to make the screen point relate to the world
		//RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, Mathf.Infinity, 1 << LayerMask.NameToLayer("ReticleLayer"));
		//Debug.DrawRay(transform.position, transform.forward * Mathf.Infinity);

		////Debug.Log("hit.transform.position: " + hit.transform.position);//gives the position of the player, not where the ray hit the canvas
		//Debug.Log("hit.point: " + hit.point);
		//Debug.Log("hit transform: " + hit.collider.transform);
		//Debug.Log("hit object: " + hit.collider.gameObject);
		//target = new Vector3(hit.point.x, hit.point.y, worldCanvasTransform.position.z);
		//target = someCamera.WorldToScreenPoint(target);
		//reticle.transform.position = target;
		//#endregion

		//#region From GvrPointerGraphicRaycaster.cs
		////Vector3 screenPoint = cam.WorldToScreenPoint(ray.GetPoint(distance));
		////finalRay = cam.ScreenPointToRay(screenPoint);
		//#endregion
	}
}

