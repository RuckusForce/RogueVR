using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeKeeperScript : MonoBehaviour {
	public Text myText;
	public bool pause;

	void Awake () {
		myText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		#region Uncomment to pause on 1sec
		//if (pause) {
		//	if (Time.time == 2)
		//	{
		//		Time.timeScale = 0;
		//	}
		//}
		#endregion
		myText.text = "" + Time.timeSinceLevelLoad.ToString("0.00");
	}
}
