using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandDeactivator : MonoBehaviour {

	//PlatformRespriter pr;
	PlatformRemesher pr;
	bool ready;
	void Awake() {
		ready = false;
		StartCoroutine(WaitForPlatformInitialization());
	}

	IEnumerator WaitForPlatformInitialization() {
		yield return new WaitForSeconds(2f);
		ready = true;
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Ground")&&ready) {
			//pr = other.gameObject.GetComponent<PlatformRespriter>();
			pr = other.gameObject.GetComponent<PlatformRemesher>();
			if (!pr.hasLeft && !pr.hasRight)
			{
				//Debug.Log(other.gameObject.name + " is reset.");
				pr.ResetChildren();
				other.gameObject.SetActive(false);
			}
		}
	}
}
