using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

	Text highScoreText;

	void Awake() {
		highScoreText = GetComponent<Text>();
		highScoreText.text = PlayerPrefs.GetFloat("High Score").ToString("0.00") + "";
	}

}
