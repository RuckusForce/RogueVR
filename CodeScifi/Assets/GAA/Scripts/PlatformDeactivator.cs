using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDeactivator : MonoBehaviour {

    SpriteRenderer sr;
    Sprite[] spriteSheet;

    PlatformRespriter pr;

    void Awake() {
        spriteSheet = Resources.LoadAll<Sprite>("Sprites/02_platform units");
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log("PlatformDeactivator collided with: " + other.gameObject.name);
		if (other.gameObject.CompareTag("Ground"))
		{
			sr = other.gameObject.GetComponent<SpriteRenderer>();
			sr.sprite = spriteSheet[1];

			pr = other.gameObject.GetComponent<PlatformRespriter>();
			pr.ResetChildren();

			other.gameObject.SetActive(false);
		} else if (other.gameObject.CompareTag("Obstacle") 
		|| other.gameObject.CompareTag("Background")
		|| other.gameObject.CompareTag("Enemy")
		) {
			other.gameObject.SetActive(false);
		}
	}
}
