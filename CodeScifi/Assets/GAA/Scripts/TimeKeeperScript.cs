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
	public float timeToFade;
	public float fadeVar;
	GameObject fadeToBlack;
	SpriteRenderer sr;
	public bool pgOff = false;

	void Awake() {
		myText = GetComponent<Text>();
		level1C = GameObject.Find("Level1C");
		level1C.SetActive(false);
		platformGenerators = GameObject.Find("PlatformGenerators");
		platformGeneratorArray = platformGenerators.GetComponentsInChildren<Transform>();
		groundHeight = -4f;
		levelTimeLimit = 10f;
		timeToFade = levelTimeLimit + 3f;
		fadeToBlack = GameObject.Find("FadeToBlack");
		sr = fadeToBlack.GetComponent<SpriteRenderer>();
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
		
		//Need to figure out fading to black
		//fadeVar = Mathf.Lerp(fadeVar, 255f, 3f);

		#region Level Setter
		if (Time.timeSinceLevelLoad > timeToFade) {
			Debug.Log("Fade");
			//sr.color = new Color(1f, 1f, 1f, fadeVar);
		}
		else if (Time.timeSinceLevelLoad > levelTimeLimit) {
			pgOff = true;
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
