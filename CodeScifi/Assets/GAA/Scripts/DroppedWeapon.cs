using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This object will be activated by the TreasureChest script
//TreasureChest script initializes a list of instanced droppedWeapon prefabs with this script attached
//On InitializeDroppedWeapon(should be well after Inventory initalizes the db), change the sprite to the inventory's itemdatabase.weaponDatabase sprite
public class DroppedWeapon : MonoBehaviour {

    Sprite sprite;
    public int id;
    Inventory inv;

    void Awake() {        
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        id = -1;
    }

    public void InitializeDroppedWeapon(int newId) {
        id = newId;
        sprite = inv.GetComponent<ItemDatabase>().FetchWeaponByID(newId).Sprite;
        GetComponent<SpriteRenderer>().sprite = sprite;
        gameObject.AddComponent<PolygonCollider2D>();
        PolygonCollider2D col = gameObject.GetComponent<PolygonCollider2D>();
        //col.SetPath(0, this.GetComponent<SpriteRenderer>().sprite.vertices);//would this be too intensive? About 10 points
        col.isTrigger = true;
        GameObject child = gameObject.transform.GetChild(0).gameObject;
        child.AddComponent<SpriteRenderer>();
        child.GetComponent<SpriteRenderer>().sprite = sprite;
        child.AddComponent<PolygonCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            //set weaponFlag to true in Inventory
            inv.AddToActiveWeapons(id);
            this.gameObject.SetActive(false);
        }
    }

}
