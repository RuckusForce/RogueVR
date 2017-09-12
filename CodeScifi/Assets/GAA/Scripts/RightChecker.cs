using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightChecker : MonoBehaviour {

	//PlatformRespriter pr;
	PlatformRemesher pr;

	// Use this for initialization
	void Awake()
	{
		//pr = GetComponentInParent<PlatformRespriter>();
		pr = GetComponentInParent<PlatformRemesher>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log(other.gameObject.name + " has entered");
		if (!pr.situated && other.gameObject.CompareTag("Ground")) {
			pr.HasRight();
			if (pr.hasLeft && pr.hasRight)
			{
				pr.ChangeToMidSection();
			}
			else if (!pr.hasLeft && pr.hasRight)
			{//only has a right 
				pr.ChangeToLeftEdge();
			}
			else if (pr.hasLeft && !pr.hasRight)
			{//only has left
				pr.ChangeToRightEdge();
			}
			//else if (!pr.hasLeft && !pr.hasRight)
			//{
			//	//island
			//}
		}
	}
}
