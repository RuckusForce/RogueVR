using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastForward : MonoBehaviour {
	GameObject reticle;
	float sensitivity;

	void Awake() {
		reticle = GameObject.Find("Reticle");
		sensitivity = 2f;
	}

	// Update is called once per frame
	void Update () {
		#region Raycast 3d
		//RaycastHit hit;
		////Debug Raycast in the Editor
		//Vector3 forward = transform.TransformDirection(Vector3.forward) * 100f;
		//Debug.DrawRay(transform.position, forward, Color.green);

		//if (Physics.Raycast(transform.position, forward, out hit))
		//{
		//	Debug.Log(hit.point);
		//}
		#endregion

		#region Raycast 2d
		
		Vector3 forward = transform.TransformDirection(Vector3.forward) * 100f;
		Debug.DrawRay(transform.position, forward, Color.green);
		Ray cameraRay = new Ray(transform.position, transform.forward);
		RaycastHit2D hit2d;

		hit2d = Physics2D.GetRayIntersection(cameraRay, 100f);
		if (hit2d) {
			//Debug.Log(hit2d.collider.gameObject + " is at " + hit2d.point);
			reticle.transform.position = new Vector3(hit2d.point.x, hit2d.point.y*sensitivity, 0f);
		}
		#endregion

		
	}
}
