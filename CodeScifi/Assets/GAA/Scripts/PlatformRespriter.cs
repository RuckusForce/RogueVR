﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRespriter : MonoBehaviour
{
	Sprite[] beginSheet;
	Sprite[] midSheet;
	Sprite[] endSheet;

	Sprite[] spriteSheet;
	Sprite beginPlatform;
	Sprite midPlatform;
	Sprite endPlatform;
	public List<GameObject> platformList;

	SpriteRenderer sr;

	//can attempt a platform buffer system
	//two booleans
	//00 - all empty
	//01 - leading edge
	//11 - mid section
	//10 - trailing edge
	RightChecker rightChecker;
	public bool hasRight;
	LeftChecker leftChecker;
	public bool hasLeft;

	public bool isMidSection;
	public bool isLeftEdge;
	public bool isRightEdge;

	PlatformGenerator pg;

	// Use this for initialization
	void Start()
	{
		spriteSheet = Resources.LoadAll<Sprite>("Sprites/02_platform units");
		beginPlatform = spriteSheet[0];
		midPlatform = spriteSheet[1];
		endPlatform = spriteSheet[2];

        #region Debug platforms (disabled)
        //beginSheet = Resources.LoadAll<Sprite>("Sprites/100x10green");
        //beginPlatform = beginSheet[0];
        //midSheet = Resources.LoadAll<Sprite>("Sprites/100x10rect");
        //midPlatform = midSheet[0];
        //endSheet = Resources.LoadAll<Sprite>("Sprites/100x10red");
        //endPlatform = endSheet[0];

        #endregion

        platformList = new List<GameObject>();//is this used?
		rightChecker = GameObject.Find("RightChecker").GetComponent<RightChecker>();
		leftChecker = GameObject.Find("LeftChecker").GetComponent<LeftChecker>();

		sr = this.GetComponent<SpriteRenderer>();
		sr.sprite = midPlatform;

		isLeftEdge = false;
		isRightEdge = false;
		isMidSection = false;

		pg = GameObject.Find("PlatformGenerator").GetComponent<PlatformGenerator>();
	}

	public void HasLeft() {
		hasLeft = true;
	}

	public void HasRight() {
		hasRight = true;
	}

	public void ResetChildren() {
		hasLeft = false;
		hasRight = false;
		isLeftEdge = false;
		isRightEdge = false;
		isMidSection = false;
	}

	public void ChangeToLeftEdge() {
		//	Debug.Log("RisingEdge");		
		sr.sprite = beginPlatform;
		isLeftEdge = true;
	}

	public void ChangeToRightEdge() {
		//	Debug.Log("FallingEdge");
		sr.sprite = endPlatform;
		isRightEdge = true;
	}

	public void ChangeToMidSection() {
		//	Debug.Log("MidPlatform");
		sr.sprite = midPlatform;
		isMidSection = true;
		int rand = (int)Random.Range(0, 14);
		//int rand = 2;
		if (Time.timeSinceLevelLoad>pg.obstacleTimeInterval) {
			GameObject tempObstacle = pg.returnInactiveObstacle();
			SpriteRenderer tempSr = tempObstacle.GetComponent<SpriteRenderer>();
			tempObstacle.transform.position = new Vector3(
			this.transform.position.x,
			this.transform.position.y + (tempSr.sprite.rect.height/tempSr.sprite.pixelsPerUnit)/2,
			this.transform.position.z);			
			tempObstacle.SetActive(true);
			pg.obstacleTimeInterval = pg.obstacleTimeInterval + Random.Range(3f,6f);
		}
	}

	//public void ChangeToIsland()
	//{
	//	Debug.Log("ChangeToIsland()");
	//}
}