using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableArea : MonoBehaviour {
	ParticleSystem ps;
	AimReticleScript aimReticleScript;

	// Use this for initialization
	void Awake () {
		ps = GetComponentInChildren<ParticleSystem>();
		aimReticleScript = GameObject.Find("CanvasCursor").GetComponent<AimReticleScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Bullet")) {			
			other.gameObject.SetActive(false);
			if (ps.isPlaying){
				ps.Stop();
				ps.Clear();
			}			
			ps.transform.position = other.bounds.ClosestPoint(this.transform.position);
			ps.Play();
		}
	}

	public void RemoveFromReticleList() { //if in the aim reticle list, remove self
		if (aimReticleScript != null) {
			if (aimReticleScript.HasTarget()) {
				if (aimReticleScript.ContainsTheTarget(this.gameObject))
				{
					aimReticleScript.RemoveTheTarget(this.gameObject);
				}
			}
		}		
	}
}
