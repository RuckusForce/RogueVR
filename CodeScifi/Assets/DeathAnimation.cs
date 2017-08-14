using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour {

	Animator anim;

	void Awake() {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Bullet"))
		{
			Debug.Log("OnTriggerEnter: Enemy DeathAnimation");
			StartCoroutine(Die());
		}
		else if (other.gameObject.CompareTag("Player")) {
			Debug.Log("OnTriggerEnter: Enemy DeathAnimation PlayerCollision");
			StartCoroutine(Die());
		}
	}

	public IEnumerator Die() {
		anim.SetTrigger("Died");
		yield return new WaitForSeconds(1f);
		gameObject.SetActive(false);
	}

}
