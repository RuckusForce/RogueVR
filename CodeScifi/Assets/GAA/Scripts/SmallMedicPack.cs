using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMedicPack : MonoBehaviour {

    Inventory inv;

    void Awake() {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("SmallMedicPack: OnTriggerEnter2D:");
        if (other.CompareTag("Player")) {
            //Debug.Log("SmallMedicPack: OnTriggerEnter2D: Player");
            inv.AddItem(0);
            Destroy(this.gameObject);
        }
    }
}
