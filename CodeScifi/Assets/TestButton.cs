using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour {

	Button btn;

	// Use this for initialization
	void Awake () {
		btn = GetComponent<Button>();
	}

	public void testButtonPress() {
		Debug.Log("Button pressed.");
		btn.onClick.Invoke();
	}
}
