using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastForward : MonoBehaviour {
	GameObject reticle;
	float sensitivity;
	RaycastHit2D hit2d;
	RaycastHit2D[] hitResults;

	void Awake() {
		reticle = GameObject.Find("Reticle");
		sensitivity = 1f;
	}

	void FixedUpdate () {
		#region Raycast 2d using GetRayIntersection
		Vector3 forward = transform.TransformDirection(Vector3.forward) * 100f;
		Debug.DrawRay(transform.position, forward, Color.green);
		Ray cameraRay = new Ray(transform.position, transform.forward);
		hit2d = Physics2D.GetRayIntersection(cameraRay, 100f, 1 << LayerMask.NameToLayer("ReticleLayer"));
		if (hit2d)
		{
			Debug.Log("hit: " + hit2d.transform.gameObject.name);
			if (hit2d.transform.gameObject.name == "Button")
			{//can't be collider based
				hit2d.transform.gameObject.GetComponent<TestButton>().testButtonPress();
			}
		}
		#endregion

		#region Raycast3d using GetRayIntersectionNonAlloc

		//Vector3 forward = transform.TransformDirection(Vector3.forward) * 100f;
		//Debug.DrawRay(transform.position, forward, Color.green);
		//Ray cameraRay = new Ray(transform.position, transform.forward);
		//Physics2D.GetRayIntersectionNonAlloc(cameraRay, hitResults);
		//try
		//{
		//	if (hitResults.Length > 0)
		//	{
		//		Debug.Log("Hit #: " + hitResults.Length);
		//		////Debug.Log(hit2d.collider.gameObject.name + " is at " + hit2d.point + " w/ distance: " + hit2d.distance);
		//		////reticle.transform.position = new Vector3(hit2d.point.x, hit2d.point.y*sensitivity, 0f);
		//		//reticle.transform.position = new Vector3(hit2d.point.x, hit2d.point.y * sensitivity, hit2d.distance * 100f);//100f is related to the GetRayIntersection

		//		//if (hit2d.transform.gameObject.name == "Button")
		//		//{//can't be collider based
		//		//	hit2d.transform.gameObject.GetComponent<TestButton>().testButtonPress();
		//		//}
		//	}
		//}
		//catch (System.Exception e) {
		//	Debug.Log(e);
		//}



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
