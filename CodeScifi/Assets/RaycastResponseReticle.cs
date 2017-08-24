using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.VR;

public class RaycastResponseReticle : MonoBehaviour {

	[Tooltip("UI Raycaster")][SerializeField]
	GraphicRaycaster gr;
	[SerializeField]
	Vector3 eventPosition;
	[SerializeField]
	PointerEventData ped;
	RaycastForward raycastScript;
	[SerializeField]
	Vector3 raycastHitPos;
	[Tooltip("Hits by GraphicRaycaster")]
	[SerializeField]
	int resultSize;

	GameObject graphicCursor;
	GameObject reticle;
	float sensitivity;
	GameObject cameraContainer;

	[SerializeField]
	Vector3 gRaycaster;
	[SerializeField]
	Vector3 pRaycaster;

	List<GameObject> markerPool;
	GameObject sphereMarker;

	EventSystem eventSystem;
	Camera cam;

	[SerializeField]
	Vector2 lookPosition;

	[SerializeField]
	string[] vrDeviceList;

	// Use this for initialization
	void Awake()
	{
		gr = GetComponent<GraphicRaycaster>();
		ped = new PointerEventData(EventSystem.current);
		raycastScript = GameObject.Find("Raycast").GetComponent<RaycastForward>();

		graphicCursor = GameObject.Find("GraphicCursor");
		reticle = GameObject.Find("CanvasCursor");

		cameraContainer = GameObject.Find("CameraContainer");
		cam = GameObject.Find("PlayerCamera").GetComponent<Camera>();
		sensitivity = 1f;

		eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		//SetupMarkerPool(10);

	}
	void Update() {
		//resultSize = results.Count;
		PositionUpdate();
		//StartRaycast();
		//StartEventSystemRaycastAll();
	}

	/// <summary>
	/// Update PointerEventData.position with where the user is looking. (The reticle x and y) 
	/// Uses Physics2D.GetRayIntersectionAll from RaycastForward.cs
	/// </summary>
	void PositionUpdate() {//uses the panel's 2d colliders
		raycastHitPos = raycastScript.returnHitPosition2D();
		ped.position = raycastHitPos;
		graphicCursor.transform.position = ped.position;
		reticle.transform.position = ped.position;

		//reticle.transform.localPosition = AdjustForCameraFollow(raycastHitPos) * sensitivity;
		//reticle.transform.localPosition = AdjustForCameraFollow(raycastScript.returnHitPosition2D());
	}

	#region Raycasting Canvas Attempts
	/// <summary>
	/// After researching, looks like Unity 5.6 has issues with Google VR. Will go with Plan B: Turn Graphics to Interactable Sprites
	/// </summary>
	void StartRaycast() {//uses native graphic raycaster, shouldn't be reliant on raycastScript (which is for sprites)
		//List<RaycastResult> results;
		//results = new List<RaycastResult>();
		//pRaycaster = AdjustForCameraFollow(raycastScript.returnHitPosition2D());
		//ped.position = new Vector3(pRaycaster.x, pRaycaster.y, pRaycaster.z);
		
		////usually ped is for the mouse cursor, not the raycast viewpoint, does this interfere?
		////need to verify if ped is the same as the local space
		//gr.Raycast(ped, results);//are we only using the first hit? Does the ped change effectively?
		//						 //Debug.Log("ped: " + ped);
		//anotherReticle.transform.localPosition = AdjustForCameraFollow(raycastHitPos);
		//for (int i = 0; i < results.Count; i++)
		//{
		//	//if (results[i].gameObject.CompareTag("ReticlePanel")) {
		//	//	Debug.Log("Graphic Hit: " + results[i].gameObject.name);
		//	//}
		//	gRaycaster = results[i].screenPosition;
		//	Debug.Log("Items: " + results.Count + "\nGraphic Hit: " + results[i].gameObject.name + ": " + results[i].gameObject.tag);
		//	//Debug.Log(ped.position + "?" + results[i].gameObject.transform.position + "?" + results[i].gameObject.transform.localPosition);
		//	//Debug.Log("World Point: " + results[i].worldPosition);
		//}
		//results.Clear();
	}

	void StartEventSystemRaycastAll() {
		//List<RaycastResult> eventResults = new List<RaycastResult>();
		////pRaycaster = AdjustForCameraFollow(raycastScript.returnHitPosition2D());
		////pRaycaster = cam.WorldToViewportPoint(AdjustForCameraFollow(raycastScript.returnHitPosition2D()));
		////pRaycaster = AdjustForCameraFollow(raycastScript.returnHitPosition2D());
		//pRaycaster = raycastScript.returnHitPosition2D();

		//ped.position = pRaycaster;
		//graphicCursor.transform.position = ped.position;
		////reticle.transform.position = ped.position;
		//EventSystem.current.RaycastAll(ped, eventResults);
		//for (int i = 0; i < eventResults.Count; i++)
		//{
		//	Debug.Log("PED Hit: " + eventResults[i].gameObject.name);
		//	//GameObject marker = ReturnInactiveMarker();
		//	//marker.SetActive(true);
		//	//marker.transform.position = eventResults[i].gameObject.transform.position;
		//}
		//eventResults.Clear();
	}

	void StartRaycastWithHotspot() {
		//List<RaycastResult> eventResults = new List<RaycastResult>();

		//Vector2 hotspot = new Vector2(0.5f, 0.5f);
		//if (VRSettings.enabled)
		//	ped.position = new Vector2(hotspot.x * UnityEngine.VR.VRSettings.eyeTextureWidth, hotspot.y * UnityEngine.VR.VRSettings.eyeTextureHeight);
		//else
		//	ped.position = new Vector2(hotspot.x * Screen.width, hotspot.y * Screen.height);

		//graphicCursor.transform.position = ped.position;

		//EventSystem.current.RaycastAll(ped, eventResults);
		//for (int i = 0; i < eventResults.Count; i++)
		//{
		//	Debug.Log("PED Hit: " + eventResults[i].gameObject.name);
		//}
		//eventResults.Clear();
	}
	#endregion

	///// <summary>
	///// Initialize markerPool with count
	///// </summary>
	///// <param name="markerPoolCount"></param>
	//void SetupMarkerPool(int markerPoolCount) {
	//	sphereMarker = Resources.Load("SphereMarker") as GameObject;
	//	markerPool = new List<GameObject>();
	//	for (int i = 0; i < markerPoolCount; i++)
	//	{
	//		markerPool.Add(Instantiate(sphereMarker));
	//		markerPool[i].SetActive(false);
	//	}
	//}

	//GameObject ReturnInactiveMarker() {
	//	GameObject returnMe;
	//	returnMe = markerPool[0].gameObject;
	//	for (int i = 0; i < markerPool.Count; i++) {
	//		if (!markerPool[i].activeSelf)
	//		{
	//			returnMe = markerPool[i].gameObject;
	//		}
	//	}
	//	return returnMe;
	//}
}
