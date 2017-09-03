using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
	public float timeUntilFade;
	public float timeToFade;
	public float fadeVar;
	GameObject fadeToBlack;
	SpriteRenderer sr;
	public bool pgOff = false;
	float lerpAccumulation;

	void Awake() {
		myText = GetComponent<Text>();
		level1C = GameObject.Find("Level1C");
		level1C.SetActive(false);
		platformGenerators = GameObject.Find("PlatformGenerators");
		platformGeneratorArray = platformGenerators.GetComponentsInChildren<Transform>();
		groundHeight = -4f;
		levelTimeLimit = 5f;
		timeUntilFade = 21f;//should be the same as shootingTime
		timeToFade = 2f;//should load screen afterwards
		fadeToBlack = GameObject.Find("FadeToBlack");
		sr = fadeToBlack.GetComponent<SpriteRenderer>();
		lerpAccumulation = 0f;
	}

	// Update is called once per frame
	void FixedUpdate() {
		#region Uncomment to pause on 2sec
		//if (pause) {
		//	if (Time.time == 2)
		//	{
		//		Time.timeScale = 0;
		//	}
		//}
		#endregion
		//myText.text = "" + Time.timeSinceLevelLoad.ToString("0.00");
		//myText.text = "" + Time.deltaTime.ToString("0.00");
		//Need to figure out fading to black


		#region Level Setter
		if (Time.timeSinceLevelLoad > (levelTimeLimit + timeUntilFade + timeToFade)) {
			SceneManager.LoadScene("MainMenu");
		}
		else if (Time.timeSinceLevelLoad > (levelTimeLimit + timeUntilFade))
		{
			Debug.Log("Fade");
			lerpAccumulation += (Time.deltaTime / timeToFade);
			fadeVar = Mathf.Lerp(fadeVar, 1f, lerpAccumulation);
			myText.text = "" + fadeVar.ToString("0.00");
			//myText.text = "" + lerpAccumulation.ToString("0.00");
			sr.color = new Color(0f, 0f, 0f, fadeVar);
		}
		else if (Time.timeSinceLevelLoad > levelTimeLimit)
		{
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
