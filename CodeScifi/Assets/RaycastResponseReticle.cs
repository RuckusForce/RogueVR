using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycastResponseReticle : MonoBehaviour {

	[Tooltip("UI Raycaster")][SerializeField]
	GraphicRaycaster gr;
	[SerializeField]
	Vector3 eventPosition;
	PointerEventData ped;
	RaycastForward raycastScript;
	[SerializeField]
	Vector3 raycastHitPos;
	[Tooltip("Hits by GraphicRaycaster")]
	[SerializeField]
	int resultSize;
	List<RaycastResult> results;

	GameObject anotherReticle;
	GameObject reticle;
	float sensitivity;
	GameObject cameraContainer;

	// Use this for initialization
	void Awake()
	{
		gr = GetComponent<GraphicRaycaster>();
		ped = new PointerEventData(null);
		raycastScript = GameObject.Find("Raycast").GetComponent<RaycastForward>();
		results = new List<RaycastResult>();
		anotherReticle = GameObject.Find("AnotherReticle");
		reticle = GameObject.Find("Reticle");
		cameraContainer = GameObject.Find("CameraContainer");
		sensitivity = 1f;

	}
	void Update() {
		eventPosition = ped.position;
		resultSize = results.Count;
		PositionUpdate();
		StartRaycast();
	}

	/// <summary>
	/// Update PointerEventData.position with where the user is looking. (The reticle x and y) 
	/// </summary>
	void PositionUpdate() {
		raycastHitPos = raycastScript.returnHitPosition2D();
		anotherReticle.transform.localPosition = AdjustForCameraFollow(raycastHitPos);
		reticle.transform.localPosition = AdjustForCameraFollow(raycastHitPos)*sensitivity;
		ped.position = AdjustForCameraFollow(raycastHitPos);		
	}

	/// <summary>
	/// From the origin of the created Raycast (this transform)
	/// Check the PointerEventData (updated from raycastScript)
	/// For any graphic hits
	/// </summary>
	void StartRaycast() {
		gr.Raycast(ped, results);
	}

	Vector2 AdjustForCameraFollow(Vector2 rayPos) {
		Vector2 adjusted = new Vector2(
		rayPos.x - cameraContainer.transform.position.x, 
		rayPos.y - cameraContainer.transform.position.y);

		return adjusted;
	}
}
