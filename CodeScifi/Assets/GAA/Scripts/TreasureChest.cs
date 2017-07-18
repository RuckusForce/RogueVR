using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Instantiate a set amount of droppedItems
//Set their items
//Set them inactive
//Generate method for releasing droppedItems

public class TreasureChest : MonoBehaviour {

    //GameObject item;
    public List<GameObject> lootList;
    int lootCount;
    int equipmentCount;
    int minLootIndex;
    int maxLootIndex;
    GameObject droppedItem;
    GameObject droppedWeapon;
    bool empty;

    void Awake() {
        lootCount = 10;
        equipmentCount = 1;
        //item = Resources.Load("Prefabs/SmallMedicPack") as GameObject;
        droppedItem = Resources.Load("Prefabs/TreasureChest/DroppedItem") as GameObject;
        droppedWeapon = Resources.Load("Prefabs/TreasureChest/DroppedWeapon") as GameObject;
        empty = false;
        ModifyLootRange(0, 4);
    }

    //void Start() {
    //    InitializeDroppedItems();//We're already assuming that the itemDatabase.database is finished
    //}

    void ModifyLootRange(int min, int max) {
        minLootIndex = min;
        maxLootIndex = max;
    }

    void SpawnLoot()
    {
        PopulateLootListWithItems();
        PopulateLootListWithWeapons();
        ExplodeOutLoot();
    }

    void PopulateLootListWithItems(){
        GameObject temp;
        int newItemId;
        for (int i = 0; i < lootCount; i++)
        {
            temp = Instantiate(droppedItem) as GameObject;
            DroppedItem dItem = temp.GetComponent<DroppedItem>();
            newItemId = (int)Random.Range(minLootIndex, maxLootIndex + 1);
            //Debug.Log("fRange: " + fRange);
            StartCoroutine(dItem.InitializeDroppedItem(newItemId));//Populates the treasure chest with items
            //temp.SetActive(false);
            temp.transform.localScale = temp.transform.localScale * .5f;
            //temp.transform.SetParent(this.transform);
            temp.transform.position = this.transform.position;

            lootList.Add(temp);
        }
    }

    void PopulateLootListWithWeapons() {
        Debug.Log("PopulateLootListWithItems");
        GameObject temp;
        int newWeaponId;

        for (int i = 0; i < equipmentCount; i++)
        {
            temp = Instantiate(droppedWeapon) as GameObject;
            DroppedWeapon dWeapon = temp.GetComponent<DroppedWeapon>();
            newWeaponId = 1;
            dWeapon.InitializeDroppedWeapon(newWeaponId);
            temp.transform.position = this.transform.position;

            lootList.Add(temp);
        }

    }

    void ExplodeOutLoot()
    {//explode, then freeze on floor?
        float x;
        float y;
        Vector2 lootVector;

        for (int i = 0; i < lootCount; i++)
        {
            x = Random.Range(-3f, 3f);
            y = Random.Range(4f, 2f);
            lootVector = new Vector2(x, y);
            Debug.Log("Setting item[" + i + "] active");
            lootList[i].SetActive(true);
            lootList[i].GetComponent<Rigidbody2D>().AddForce(lootVector, ForceMode2D.Impulse);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Equals)) {
            if (!empty) {
                SpawnLoot();
                empty = true;
            }
        }

    }

}
