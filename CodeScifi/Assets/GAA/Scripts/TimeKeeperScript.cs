using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeKeeperScript : MonoBehaviour {
	public Text myText;
	public bool pause;
	GameObject platformGenerators;
	[SerializeField]
	Transform[] platformGeneratorArray;
	GameObject level1C;
	Vector3 lastPlatformPosition;
	float groundHeight;
	public float levelTimeLimit;
	
	void Awake() {
		myText = GetComponent<Text>();
		level1C = GameObject.Find("Level1C");
		level1C.SetActive(false);
		platformGenerators = GameObject.Find("PlatformGenerators");
		platformGeneratorArray = platformGenerators.GetComponentsInChildren<Transform>();
		groundHeight = -4f;
		levelTimeLimit = 4f;
	}

	// Update is called once per frame
	void FixedUpdate() {

		#region Uncomment to pause on 1sec
		//if (pause) {
		//	if (Time.time == 2)
		//	{
		//		Time.timeScale = 0;
		//	}
		//}
		#endregion
		myText.text = "" + Time.timeSinceLevelLoad.ToString("0.00");

		#region Level Setter
		//After 10s, stop the platform generators
		if (Time.timeSinceLevelLoad > levelTimeLimit) {
			lastPlatformPosition = platformGeneratorArray[1].position;
			lastPlatformPosition = new Vector3(
				lastPlatformPosition.x,
				groundHeight,
				lastPlatformPosition.z
			);
			TurnOffPlatformGenerators();
			//Activate Level1C
			level1C.SetActive(true);
			level1C.transform.position = lastPlatformPosition;
		}
		#endregion
	}

	public void TurnOffPlatformGenerators() {
		for (int i = 0; i < platformGeneratorArray.Length; i++) {
			platformGeneratorArray[i].gameObject.SetActive(false);
		}
	}
}
