using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public bool rotateLeft;	

	// Update is called once per frame
	void Update () {
		if (rotateLeft)
		{
			transform.Rotate(0f, 0f, 1f);
		}
		else {
			transform.Rotate(0f, 0f, -1f);
		}
	}
}
