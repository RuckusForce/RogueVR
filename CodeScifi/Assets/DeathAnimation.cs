using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour {

	Animator anim;
	public Collider2D[] cols;
	Enemy enemyScript;
	bool dying;
	ParticleSystem ps;
	PlayerAttributesScript playerAttributes;
	Rigidbody2D rb;
	ShootableArea shootableScript;

	void Awake() {

		anim = GetComponent<Animator>();
		cols = GetComponentsInChildren<Collider2D>();
		ps = GetComponent<ParticleSystem>();
		enemyScript = GetComponent<Enemy>();
		dying = false;
		playerAttributes = GameObject.Find("Hero2 (1)").GetComponentInChildren<PlayerAttributesScript>();
		TurnOnColliders();
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = Vector2.zero;
		shootableScript = GetComponentInChildren<ShootableArea>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDisable() {
		transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
		dying = false;
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (!dying) {
			if (other.gameObject.CompareTag("Bullet"))
			{
				//Debug.Log("OnTriggerEnter: Enemy DeathAnimation");
				StartCoroutine(Die());
			}
			else if (other.gameObject.CompareTag("Player"))
			{
				Debug.Log("OnTriggerEnter: Enemy DeathAnimation PlayerCollision");
				playerAttributes.PlayerDecreaseHealth(100f);
				StartCoroutine(Die());

			}
		}

	}

	public IEnumerator Die() {
		dying = true;
		//TurnOffColliders();
		shootableScript.RemoveFromReticleList();
		StartCoroutine(TurnOffCollidersTemporarily());

		//if (!ps.IsAlive())
		//{
		//	//Debug.Log("PS: " + i + " is Active.");
		//	ps.transform.position = this.transform.position;
		//	ps.Clear();
		//	ps.GetComponent<ParticleSystem>().Simulate(GetComponent<ParticleSystem>().main.duration);
		//	ps.GetComponent<ParticleSystem>().Play();
		//}

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

	IEnumerator TurnOffCollidersTemporarily() {
		for (int i = 0; i < cols.Length; i++)
		{
			cols[i].enabled = false;
		}
		yield return new WaitForSeconds(2f);
		for (int i = 0; i < cols.Length; i++)
		{
			cols[i].enabled = true;
		}		
	}


	void TurnOnColliders() {//does not work
		for (int i = 0; i < cols.Length; i++)
		{
			//Debug.Log(cols[i].gameObject.name + " enabled.");
			cols[i].enabled = true;
		}
	}
}
