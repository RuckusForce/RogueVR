using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate2D : MonoBehaviour {

	public bool rotateLeft;
	public bool rotateRight;
	public bool rotateRandom;
	
	// Update is called once per frame
	void Update () {
		if (rotateLeft)
		{
			transform.Rotate(0f, 0f, 1f);
		}
		else if (rotateRandom)
		{
			transform.Rotate(
				Random.Range(0f, 1f),
				Random.Range(0f, 1f),
				Random.Range(0f, 1f)
			);
		}
		else if (rotateRight) {
			transform.Rotate(0f, 0f, -1f);
		}
	}
}
