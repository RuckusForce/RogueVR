using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableArea : MonoBehaviour {
	ParticleSystem ps;

	// Use this for initialization
	void Awake () {
		ps = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Bullet")) {			
			other.gameObject.SetActive(false);
			ps.Stop();
			ps.Clear();
			ps.transform.position = other.bounds.ClosestPoint(this.transform.position);
			ps.Play();
		}
	}
}
