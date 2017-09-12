using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour {

	Button btn;
	AudioSource audioSource;

	// Use this for initialization
	void Awake () {
		btn = GetComponent<Button>();
		audioSource = GetComponent<AudioSource>();
	}

	public void testButtonPress() {
		Debug.Log("Button pressed.");
		audioSource.Stop();
		audioSource.Play();
		btn.onClick.Invoke();
	}
}
