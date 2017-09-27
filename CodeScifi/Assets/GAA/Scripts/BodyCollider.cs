using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollider : MonoBehaviour {

    PlayerAttributesScript ps;

    void Awake() {
        ps = transform.parent.parent.GetComponentInChildren<PlayerAttributesScript>();
    }

    void OnCollisionEnter2D(Collision2D other) {
		//Debug.Log("Body collided with: " + other.gameObject.tag);
		if (other.gameObject.CompareTag("Enemy")||other.gameObject.CompareTag("Obstacle")) {
            ps.PlayerDecreaseHealth(100f);
        }
    }
}
