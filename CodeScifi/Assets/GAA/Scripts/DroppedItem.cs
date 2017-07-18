using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Have method to change itemId
//Have method to change sprite into the item's sprite
//Get destroyed on player contact
public class DroppedItem : MonoBehaviour {

    Inventory inv;
    ItemDatabase itemDatabase;
    public int itemId = 0;

    public Item itemToAdd;
    public Sprite mySprite;
    public Color color;
    public bool fade70;
    public bool fade30;
    public bool fade00;


    //void Awake() {
    //    StartCoroutine(Disappear());
    //    fade70 = false;
    //    fade30 = false;
    //    fade00 = false;
    //}

    //public IEnumerator Disappear() {
    //    int seconds = Random.Range(5, 8);
    //    yield return new WaitForSeconds(seconds);
    //    color = gameObject.GetComponent<SpriteRenderer>().color;
    //    fade70 = true;
    //    //this.gameObject.SetActive(false);
    //}

    //void Update() {
    //    if (fade70)
    //    {
    //        color = new Color(color.r, color.g, color.b, .7f);
    //    }
    //    else if (fade30)
    //    {
    //        color = new Color(color.r, color.g, color.b, .4f);
    //    }
    //    else if (fade00) {
    //        color = new Color(color.r, color.g, color.b, 0f);
    //    }
    //    gameObject.GetComponent<SpriteRenderer>().color = color;
    //}

    public IEnumerator InitializeDroppedItem(int i)//this is getting called before ConstructItemDatabase
    {
        Debug.Log("InitializeDroppedItem");
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        itemDatabase = inv.GetComponent<ItemDatabase>();
        yield return itemDatabase;//we're going to fetch from this database, currently being initialized by ItemDatabase, so gotta wait for it to be completed
        ChangeItemId(i);       

        if (itemDatabase.database.Count > 0)
        {
            StartCoroutine(GetItemFromDB(itemDatabase));
        }
        else {
            Debug.Log("Has itemDatabase completed loading? Nope.");
        }
        
    }

    IEnumerator GetItemFromDB(ItemDatabase db) {
        itemToAdd = itemDatabase.FetchItemByID(itemId);
        yield return itemToAdd;

        if (itemToAdd != null)
        {
            Debug.Log("itemToAdd is not null");
            this.GetComponent<SpriteRenderer>().sprite = itemToAdd.Sprite;
            gameObject.AddComponent<PolygonCollider2D>();
            PolygonCollider2D col = gameObject.GetComponent<PolygonCollider2D>();
            //col.SetPath(0, this.GetComponent<SpriteRenderer>().sprite.vertices);//would this be too intensive? About 10 points
            col.isTrigger = true;
            GameObject child = gameObject.transform.GetChild(0).gameObject;
            child.AddComponent<SpriteRenderer>();
            child.GetComponent<SpriteRenderer>().sprite = itemToAdd.Sprite;
            child.AddComponent<PolygonCollider2D>();
        }
        else {
            Debug.Log("itemToAdd is null");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (inv.CheckForEmptySlot()) {
                inv.AddItem(itemId);
                Destroy(this.gameObject);
            } else if (inv.CheckIfItemIsInInventory(itemId)) {
                inv.AddItem(itemId);
                Destroy(this.gameObject);
            }
        }
    }

    public void ChangeItemId(int id) {
        itemId = id;
        //Debug.Log("itemId: " + itemId);
    }
}
