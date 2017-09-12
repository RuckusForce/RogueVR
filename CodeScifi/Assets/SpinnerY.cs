using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerY : MonoBehaviour {

	public float rotY;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0f, rotY, 0f);
	}
}
