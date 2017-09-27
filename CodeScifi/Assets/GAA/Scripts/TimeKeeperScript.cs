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
	GameObject islandDeactivator;
	GameObject level1C;
	Vector3 lastPlatformPosition;
	float groundHeight;
	public float levelTimeLimit;
	//public float timeUntilFade; // moved to LastStandEvent
	//public float timeToFade;// moved to LastStandEvent
	//public float fadeVar;// moved to LastStandEvent
	//GameObject fadeToBlack;// moved to LastStandEvent
	//SpriteRenderer sr;// moved to LastStandEvent	
	//float lerpAccumulation;// moved to LastStandEvent
	public bool pgOff = false;

	void Awake() {
		myText = GetComponent<Text>();
		level1C = GameObject.Find("Level1C");
		level1C.SetActive(false);
		platformGenerators = GameObject.Find("PlatformGenerators");
		platformGeneratorArray = platformGenerators.GetComponentsInChildren<Transform>();
		islandDeactivator = GameObject.Find("IslandDeactivator");
		groundHeight = -4f;
		levelTimeLimit = 30f;
		//timeUntilFade = 21f;//should be the same as shootingTime
		//timeToFade = 2f;//should load screen afterwards
		//fadeToBlack = GameObject.Find("FadeToBlack");
		//sr = fadeToBlack.GetComponent<SpriteRenderer>();
		//lerpAccumulation = 0f;
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

		#region Level Setter

			#region Placed these events in LastStandEvent
			//if (Time.timeSinceLevelLoad > (levelTimeLimit + timeUntilFade + timeToFade)) {//should happen after the scene turns black
			//	SceneManager.LoadScene("MainMenu");
			//}
			//else if (Time.timeSinceLevelLoad > (levelTimeLimit + timeUntilFade))//Should be triggered by passing through the portal
			//{
			//	Debug.Log("Fade");
			//	lerpAccumulation += (Time.deltaTime / timeToFade);
			//	fadeVar = Mathf.Lerp(fadeVar, 1f, lerpAccumulation);
			//	myText.text = "" + fadeVar.ToString("0.00");
			//	//myText.text = "" + lerpAccumulation.ToString("0.00");
			//	sr.color = new Color(0f, 0f, 0f, fadeVar);
			//}
			//else
			#endregion

		if (Time.timeSinceLevelLoad > levelTimeLimit)//loads Level1C
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
		islandDeactivator.SetActive(false);
		for (int i = 0; i < platformGeneratorArray.Length; i++) {
			platformGeneratorArray[i].gameObject.SetActive(false);
		}
	}
}
