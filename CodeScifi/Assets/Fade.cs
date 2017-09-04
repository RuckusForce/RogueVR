using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {
	bool startFade;
	//public float timeUntilFade;
	public float timeToFade;
	public float fadeVar;
	GameObject fadeToBlack;
	SpriteRenderer sr;
	float lerpAccumulation;

	// Use this for initialization
	void Awake () {
		startFade = false;
		timeToFade = 2f;//should load screen afterwards
		fadeToBlack = GameObject.Find("FadeToBlack");
		sr = fadeToBlack.GetComponent<SpriteRenderer>();
		lerpAccumulation = 0f;
		//timeUntilFade = 21f;//should be the same as shootingTime
	}
	
	// Update is called once per frame
	void Update () {
		if (startFade)//Should be triggered by passing through the portal
		{
			Debug.Log("Fade");
			lerpAccumulation += (Time.deltaTime / timeToFade);
			fadeVar = Mathf.Lerp(fadeVar, 1f, lerpAccumulation);
			sr.color = new Color(0f, 0f, 0f, fadeVar);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			StartFading();
		}
	}

	public void StartFading()
	{
		startFade = true;
	}
}
