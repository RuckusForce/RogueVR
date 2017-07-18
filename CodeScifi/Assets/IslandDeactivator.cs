using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandDeactivator : MonoBehaviour {

	PlatformRespriter pr;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Ground")) {
			pr = other.gameObject.GetComponent<PlatformRespriter>();
			if (!pr.hasLeft && !pr.hasRight) {
				//Debug.Log("Reset");
				pr.ResetChildren();
				other.gameObject.SetActive(false);
			}
		}
	}
}
