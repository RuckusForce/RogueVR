using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDeactivator : MonoBehaviour {

    SpriteRenderer sr;
    Sprite[] spriteSheet;

    PlatformRemesher pr;

	GameObject[] dt;
	ParticleSystem[] ps;

	GameObject dt1;
	GameObject dt2;
	ParticleSystem ps1;
	ParticleSystem ps2;
	int particlePlayingCounter;

	void Awake() {
        spriteSheet = Resources.LoadAll<Sprite>("Sprites/02_platform units");

		dt = GameObject.FindGameObjectsWithTag("DustTriggers");
		ps = new ParticleSystem[dt.Length];

		for (int i = 0; i < dt.Length; i++) {
			ps[i] = dt[i].GetComponent<ParticleSystem>();
		}

		//dt1 = GameObject.Find("DustTrigger1");
		//dt2 = GameObject.Find("DustTrigger2");

		//ps1 = dt1.GetComponent<ParticleSystem>();
		//ps2 = dt2.GetComponent<ParticleSystem>();

		particlePlayingCounter = 0;
	}

    void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log("PlatformDeactivator collided with: " + other.gameObject.name);
		//if (other.gameObject.CompareTag("Ground"))
		//{
		//	sr = other.gameObject.GetComponent<SpriteRenderer>();
		//	sr.sprite = spriteSheet[1];

		//	pr = other.gameObject.GetComponent<PlatformRemesher>();
		//	pr.ResetChildren();

		//	other.gameObject.SetActive(false);
		//}
		if (other.gameObject.CompareTag("DeactivateOnPass")) {
			pr = other.transform.parent.gameObject.GetComponent<PlatformRemesher>();
			pr.ResetChildren();
			other.transform.parent.gameObject.SetActive(false);
			TriggerDust(other.transform.position);
		}
		else if (other.gameObject.CompareTag("Obstacle")
		|| other.gameObject.CompareTag("Background")
		|| other.gameObject.CompareTag("Enemy")
		)
		{
			other.gameObject.SetActive(false);
		}
	}

	void TriggerDust(Vector3 position) {
		//Debug.Log("TriggerDust()");
		//if (!ps1.IsAlive())
		//{
		//	dt1.transform.position = position;
		//	//ps1.Clear();
		//	ps1.GetComponent<ParticleSystem>().Simulate(GetComponent<ParticleSystem>().main.duration);
		//	ps1.GetComponent<ParticleSystem>().Play();
		//}
		//else {
		//	dt2.transform.position = position;
		//	//ps2.Clear();
		//	ps2.GetComponent<ParticleSystem>().Simulate(GetComponent<ParticleSystem>().main.duration);
		//	ps2.GetComponent<ParticleSystem>().Play();
		//}
		bool nonAvailable = true;
		for (int i = 0; i < dt.Length; i++) {
			if (!ps[i].IsAlive()) {
				//Debug.Log("PS: " + i + " is Active.");
				dt[i].transform.position = position;
				ps[i].Clear();
				ps[i].GetComponent<ParticleSystem>().Simulate(GetComponent<ParticleSystem>().main.duration);
				ps[i].GetComponent<ParticleSystem>().Play();
				nonAvailable = false;
				break;
			}
		}
		if (nonAvailable) {//cycle through and disable
			//Debug.Log("PS: " + particlePlayingCounter % dt.Length + " is Active.");
			dt[particlePlayingCounter%dt.Length].transform.position = position;
			ps[particlePlayingCounter % dt.Length].Clear();
			ps[particlePlayingCounter % dt.Length].GetComponent<ParticleSystem>().Simulate(GetComponent<ParticleSystem>().main.duration);
			ps[particlePlayingCounter % dt.Length].GetComponent<ParticleSystem>().Play();
			particlePlayingCounter++;
		}

	}
}
