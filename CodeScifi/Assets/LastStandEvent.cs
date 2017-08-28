using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStandEvent : MonoBehaviour {
	GameObject player;
	PlayerInputScript playerInputScript;
	Animator playerAnim;
	GameObject lastBoss;
	bool startEvent;
	bool gotThroughOnce;
	float freeShootingTime;

	void Awake() {
		player = GameObject.Find("Hero2 (1)");
		playerInputScript = player.GetComponentInChildren<PlayerInputScript>();
		playerAnim = player.GetComponent<Animator>();
		lastBoss = GameObject.Find("BigBoss2");
		startEvent = false;
		gotThroughOnce = false;
		freeShootingTime = 10f;
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
		playerInputScript.UnfreezeInput();
	}

	void Continue() { 
	
	}

	void Fade() { 
		
	}
}
