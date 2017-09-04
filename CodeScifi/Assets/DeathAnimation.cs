using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour {

	Animator anim;
	Collider2D[] cols;
	Enemy enemyScript;
	bool dying;

	void Awake() {
		anim = GetComponent<Animator>();
		cols = GetComponentsInChildren<Collider2D>();

		enemyScript = GetComponent<Enemy>();
		dying = false;
		TurnOnColliders();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (!dying) {
			if (other.gameObject.CompareTag("Bullet"))
			{
				Debug.Log("OnTriggerEnter: Enemy DeathAnimation");
				StartCoroutine(Die());
			}
			else if (other.gameObject.CompareTag("Player"))
			{
				Debug.Log("OnTriggerEnter: Enemy DeathAnimation PlayerCollision");
				StartCoroutine(Die());
			}
		}

	}

	public IEnumerator Die() {
		dying = true;
		TurnOffColliders();
		anim.SetTrigger("Died");

		//enemyScript.enabled = false;
		//col.enabled = false;
		yield return new WaitForSeconds(3f);
		gameObject.SetActive(false);
	}

	void TurnOffColliders() {
		for (int i = 0; i < cols.Length; i++)
		{
			cols[i].enabled = false;
		}
	}

	void TurnOnColliders() {
		for (int i = 0; i < cols.Length; i++)
		{
			cols[i].enabled = true;
		}
	}

}
