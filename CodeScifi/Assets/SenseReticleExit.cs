using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseReticleExit : MonoBehaviour {
	[SerializeField]	
	PauseGame pg;

	// Use this for initialization
	void Awake () {
		//pg = GameObject.Find("PauseCanvas").GetComponent<PauseGame>();
	}
	
	void OnTriggerExit2D(Collider2D other) {
		Debug.Log(other.gameObject.name + "has exited.");
		if (other.gameObject.name == "GreenCircle") {
			Debug.Log("SenseReticleExit.cs: Pause");
			pg.Pause();
		}
	}
}
