using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudioManager : MonoBehaviour {

	AudioSource au;

	// Use this for initialization
	void Awake () {
		au = GameObject.Find("PlayerCamera").GetComponent<AudioSource>();
		au.Stop();
	}

}
