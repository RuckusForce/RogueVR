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
	float lerpAccumulationCF;
	float timeToFadeCF;
	ParticleSystem particleSystem;
	Animator cyborgAnimator;

	void Awake() {
		player = GameObject.Find("Hero2 (1)");
		cyborgSoldier = GameObject.Find("CyborgSoldier");
		particleSystem = GameObject.Find("CyborgParticleSystem").GetComponent<ParticleSystem>();
		cyborgAnimator = cyborgSoldier.GetComponent<Animator>();
		spriteMeshArray = cyborgSoldier.GetComponentsInChildren<SpriteMeshInstance>();
		playerInputScript = player.GetComponentInChildren<PlayerInputScript>();
		playerAnim = player.GetComponent<Animator>();
		lastBoss = GameObject.Find("BigBoss2");
		startEvent = false;
		gotThroughOnce = false;
		freeShootingTime = 10f;
		cyborgFade = false;
		timeToFadeCF = 200f;
		newTransparency = 1f;
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
				lerpAccumulationCF += (Time.deltaTime / timeToFadeCF);
				newTransparency = Mathf.Lerp(newTransparency, 0f, lerpAccumulationCF);
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
		cyborgAnimator.SetTrigger("Point");
		yield return new WaitForSeconds(freeShootingTime);
		particleSystem.Play();
		Flip();		
		playerInputScript.UnfreezeInput();
		cyborgFade = true;
	}

	void Continue() { 
	
	}

	void Fade() { 
		
	}
}
