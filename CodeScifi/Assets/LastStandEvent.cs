using Anima2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStandEvent : MonoBehaviour {
	GameObject player;
	GameObject cyborgSoldier;
	bool cyborgFade;
	SpriteMeshInstance[] spriteMeshArray;
	PlayerInputScript playerInputScript;
	Animator playerAnim;
	GameObject lastBoss;
	bool startEvent;
	bool gotThroughOnce;
	float freeShootingTime;
	Color tempColor;
	float newTransparency;
	float timeForLerping;

	void Awake() {
		player = GameObject.Find("Hero2 (1)");
		cyborgSoldier = GameObject.Find("CyborgSoldier");
		spriteMeshArray = cyborgSoldier.GetComponentsInChildren<SpriteMeshInstance>();
		playerInputScript = player.GetComponentInChildren<PlayerInputScript>();
		playerAnim = player.GetComponent<Animator>();
		lastBoss = GameObject.Find("BigBoss2");
		startEvent = false;
		gotThroughOnce = false;
		freeShootingTime = 10f;
		cyborgFade = false;
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			Debug.Log("Player enters Last Stand");
			startEvent = true;
		}
	}

	void Update() {
		if (startEvent) {
			if (!gotThroughOnce) {
				StartLastStand();//called multiple times if character is still falling
			}
			
		}

		if (cyborgFade) {		
			for (int i = 0; i < spriteMeshArray.Length; i++)
			{
				tempColor = spriteMeshArray[i].color;
				timeForLerping += (Time.deltaTime / 300f);
				newTransparency = Mathf.Lerp(newTransparency, 0f, timeForLerping);
				spriteMeshArray[i].color = new Color(tempColor.r, tempColor.g, tempColor.b, newTransparency);
			}
		}
	}

	void StartLastStand() {
		//Stop();
		Debug.Log("StartLastStand()");
		if (playerAnim.GetBool("Falling"))
		{ //if still falling
			return;
		}
		else {
			gotThroughOnce = true;
			Debug.Log("gotThroughOnce = true");
		}
		playerInputScript.FreezeInput();
		Flip();
		//Stand();
		//Crouch();
		StartCoroutine(TimeForShooting());
		//Stand();
		//Continue();
		//Fade();
	}

	void Flip() {
		Debug.Log("Flip()");
		lastBoss.transform.SetParent(this.transform);
		player.transform.Rotate(0f, 180f, 0f);
	}

	void Stand() { 
	
	}

	void Crouch() { 
	
	}

	IEnumerator TimeForShooting() {
		//yield return (playerInputScript.WalkTowards(this.transform.position));
		yield return new WaitForSeconds(freeShootingTime);
		Flip();
		cyborgFade = true;
		playerInputScript.UnfreezeInput();
	}

	void Continue() { 
	
	}

	void Fade() { 
		
	}
}
