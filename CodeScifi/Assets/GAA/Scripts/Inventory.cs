using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    

    [SerializeField]
    GameObject inventoryPanel;
    [SerializeField]
    GameObject slotPanel;

    private int slotAmount;
    public ItemDatabase itemDatabase;

    //[SerializeField]
    public GameObject slotPrefab;
    //[SerializeField]
    public GameObject itemPrefab;

    [SerializeField]
    GameObject weaponPanel;
    GameObject weaponPrefab;
    int weaponIndex = 0;

    [SerializeField]
    public List<Item> itemStash = new List<Item>();
    public List<GameObject> slotsThatAreVisible = new List<GameObject>();
    public List<ItemSlot> itemSlotsThatAreVisible = new List<ItemSlot>();
    public ItemEffects itemEffects;

    public List<GameObject> weaponList = new List<GameObject>();//all weapons from the JSON file
    public List<GameObject> activeWeaponList = new List<GameObject>();//just the weapons that have been activated

    void Awake() {
        slotPrefab = Resources.Load("Prefabs/UISet/Slot") as GameObject;
        itemPrefab = Resources.Load("Prefabs/UISet/Item") as GameObject;
        itemDatabase = GetComponent<ItemDatabase>();//since the Inventory script needs to wait for the database, why not have the database be awakened earlier?
        itemEffects = GameObject.Find("ItemEffectsObject").GetComponent<ItemEffects>();
        inventoryPanel = GameObject.Find("InventoryPanel");
        slotPanel = inventoryPanel.transform.Find("SlotPanel").gameObject;
        weaponPanel = inventoryPanel.transform.Find("WeaponPanel").gameObject;
        weaponPrefab = Resources.Load("Prefabs/UISet/Weapon") as GameObject;
        //attach one of the weaponList to weaponPanel
    }

    IEnumerator Start() {
        slotAmount = 3;

        yield return StartCoroutine(itemDatabase.LoadJson());//Would this finish the LoadJson and give us a complete database? And then continue initializing the Inventory with a now populated database?

        //Wow, turning Start into a Coroutine works!

        for (int i = 0; i < slotAmount; i++) {
            //add a blank inventory slot into the slots collective
            itemStash.Add(new Item());//back-end, initialize items
            slotsThatAreVisible.Add(Instantiate(slotPrefab));//back-end, setup slots
            slotsThatAreVisible[i].GetComponent<ItemSlot>().slotID = i;
            slotsThatAreVisible[i].transform.SetParent(slotPanel.transform);//front-end, show slots
        }

        for (int i = 0; i < itemDatabase.weaponDatabase.Count; i++) {//maybe for the future, have the weaponList actually store Weapon Types, rather than GameObjects
            weaponList.Add(Instantiate(weaponPrefab));
            weaponList[i].SetActive(false);
            weaponList[i].transform.SetParent(weaponPanel.transform);
            weaponList[i].transform.position = weaponList[i].transform.parent.position;
            //weaponList[i].GetComponent<Image>() = itemDatabase.weaponDatabase[i].Sprite;
            Weapon temp = FetchWeapon(i);
            weaponList[i].GetComponent<Image>().sprite = temp.Sprite;
            weaponList[i].GetComponent<WeaponData>().ID = temp.ID;
        }

        AddToActiveWeapons(0);//add weaponList[0], not itemdatabase.weaponDatabase[0]
		ShowCurrentWeapon();
	}

	public Weapon FetchWeapon(int id) {
        Weapon weapon = null;

        if (id == -1)
        {
            weapon = null;
        }
        else {
            weapon = itemDatabase.FetchWeaponByID(id);
        }
        return weapon;
    }

    public void AddToActiveWeapons(int id) {//keep in mind, address in weaponsList != weapond Id

        if (!CheckIfInActiveWeapons(id))//if not in active yet
        {
            WeaponData weap;
            for (int i = 0; i < weaponList.Count; i++)
            {
                weap = weaponList[i].GetComponent<WeaponData>();
                if (weap.ID == id)
                {
                    activeWeaponList.Add(weaponList[i]);
                }
            }
        }
        else {
            Debug.Log("Weapon already in active");
        }
    }

    bool CheckIfInActiveWeapons(int id) {
        bool inActiveWeapons = false;

        WeaponData weap;
        for(int i=0; i<activeWeaponList.Count; i++) {
            weap = activeWeaponList[i].GetComponent<WeaponData>();
            if (weap.ID == id) {
                Debug.Log("[weapon.ID == id] " + weap.ID + " == " + id);
                inActiveWeapons = true;
                break;
            }
        }

        return inActiveWeapons;
    }

    void RemoveFromActiveWeapons(int id) {
        WeaponData weap;
        for (int i = 0; i < weaponList.Count; i++) {
            weap = weaponList[i].GetComponent<WeaponData>();
            if (weap.ID == id) {
                activeWeaponList.Remove(weaponList[i]);
            }
        }
    }

    void ShowCurrentWeapon() {
        for (int i = 0; i < activeWeaponList.Count; i++) {
            activeWeaponList[i].SetActive(false);
        }
        //Debug.Log("weaponIndex: " + weaponIndex);
        activeWeaponList[weaponIndex].SetActive(true);
    }

    public Weapon whatsMyCurrentWeapon() {
        return activeWeaponList[weaponIndex].GetComponent<Weapon>();
    }

    public void CycleWeaponForward() {
        weaponIndex++;
        weaponIndex = weaponIndex % activeWeaponList.Count;
        ShowCurrentWeapon();
    }

    public void CycleWeaponBackward() {
        //weaponIndex--;
        //weaponIndex = weaponIndex % weaponList.Count; //doesn't work backwards

        //Ex: 2=2,1=1,0=0,-1=2,-2=1,-3=0

        weaponIndex--;
        if (weaponIndex < 0) {
            weaponIndex = activeWeaponList.Count-1;
        }
        Debug.Log(weaponIndex);
        ShowCurrentWeapon();
    }

    /*
    Fetch Item
        */
    public Item FetchItem(int id) {
        Item itemToAdd = null;

        if (id == -1)
        {
            itemToAdd = null;
        }
        else {
            Debug.Log("Fetching item: " + id);
            if (itemDatabase.FetchItemByID(id) != null)
            {
                Debug.Log("Database fetch is not null!");
                itemToAdd = itemDatabase.FetchItemByID(id);//Grab the item from our item library
                                                           //Debug.Log("Inventory.cs: itemToAdd: " + itemToAdd.ToString());
            }
            else {
                Debug.Log("database is null");
                itemToAdd = null;
            }
        }

        return itemToAdd;
    }

    /*
    Add item into items list and slots list.
        */
    public void AddItem(int id) {
        Item itemToAdd = FetchItem(id);

        if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))//update ItemData instead of adding a new item
        {
            Debug.Log("Stackable and in Inventory");
            for (int i = 0; i < itemStash.Count; i++)
            {
                if (itemStash[i].ID == id)
                {
                    ItemData data = slotsThatAreVisible[i].transform.GetChild(0).GetComponent<ItemData>();//the itemData can change if the item is dragged to other values, in other words, the ItemData data.slot needs to be refreshed
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount + "";
                    break;
                }
            }
        }
        else {//add a new item
            Debug.Log("Not Stackable");
            for (int i = 0; i < itemStash.Count; i++)
            {
                //if empty slot (-1 id), then add the itemTo Add to that slot
                if (itemStash[i].ID == -1)
                {
                    Debug.Log("Stashing");
                    //Debug.Log("items[i].ID != -1");
                    itemStash[i] = itemToAdd;//add the item into the items list
                    GameObject itemObj = Instantiate(itemPrefab);//just the item template, no icon yet
                    itemObj.GetComponent<ItemData>().item = itemToAdd;//Update the item in the itemData
                    itemObj.GetComponent<ItemData>().amount = 1;
                    itemObj.GetComponent<ItemData>().slot = i;//update slot location of item

                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;//the itemToAdd from the library should have everything in it. 
                                                                            //itemObj.GetComponent<SpriteRenderer>().sprite = itemToAdd.Sprite;
                                                                            //Debug.Log("Setting itemObj sprite: " + itemObj.GetComponent<SpriteRenderer>().sprite.ToString());
                                                                            //Problem above is that the Image component should maybe instead be a SpriteRenderer one?
                    itemObj.transform.position = slotsThatAreVisible[i].transform.position;
                    itemObj.transform.SetParent(slotsThatAreVisible[i].transform);//attach item to corresponding slot

                    //Debug.Log(itemObj.transform.position + ": " + itemObj.GetComponentInParent<Transform>().position);
                    itemObj.name = itemToAdd.Title;

                    ItemData data = slotsThatAreVisible[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount + "";

                    break;
                }
            }
        }
    }

    public void RemoveItemFromSlot(int slotToRemove) {
        //slotToRemove = 0;//dummy value
                         //Debug.Log("Remove Item on: " + slotToRemove);
                         //Destroy(slots[slotToRemove].GetComponentInChildren<ItemData>().gameObject);
                         //Need to reverse AddNewItem without stacking, and AddNewItem with stacking.

        //remove item from itemStash

        //remove item from slotsThatAreVisible
        //slotsThatAreVisible.RemoveAt(slotToRemove);//does this remove the slot or the item? Slot
        if (slotsThatAreVisible[slotToRemove].transform.childCount > 0) {
            //itemStash.RemoveAt(slotToRemove);

            if (slotsThatAreVisible[slotToRemove].transform.GetChild(0).GetComponent<ItemData>().amount > 1)
            {
                ItemData data = slotsThatAreVisible[slotToRemove].transform.GetChild(0).GetComponent<ItemData>();//the itemData can change if the item is dragged to other values, in other words, the ItemData data.slot needs to be refreshed
                data.amount--;
                data.transform.GetChild(0).GetComponent<Text>().text = data.amount + "";
            }
            else {
                itemStash[slotToRemove] = new Item();
                Destroy(slotsThatAreVisible[slotToRemove].transform.GetChild(0).gameObject);
            }
        }

    }

    public void UseItemFromSlot(int slotToUse) {//uses, then destroys/decrements item
        if (slotsThatAreVisible[slotToUse].transform.childCount > 0)
        {
            //itemStash.RemoveAt(slotToRemove);

            if (slotsThatAreVisible[slotToUse].transform.GetChild(0).GetComponent<ItemData>().amount > 1)//if stacked
            {
                ItemData data = slotsThatAreVisible[slotToUse].transform.GetChild(0).GetComponent<ItemData>();//the itemData can change if the item is dragged to other values, in other words, the ItemData data.slot needs to be refreshed
                Item item = data.item;
                if (itemEffects.RequestEffect(item)) {
                    data.amount--;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount + "";
                }
            }
            else {//if only one item
                ItemData data = slotsThatAreVisible[slotToUse].transform.GetChild(0).GetComponent<ItemData>();//the itemData can change if the item is dragged to other values, in other words, the ItemData data.slot needs to be refreshed
                Item item = data.item;
                if (itemEffects.RequestEffect(item)) {
                    itemStash[slotToUse] = new Item();
                    Destroy(slotsThatAreVisible[slotToUse].transform.GetChild(0).gameObject);
                }

            }
        }
    }

    public bool CheckIfItemIsInInventory(int id) {
        Item itemToCheck = FetchItem(id);
        return CheckIfItemIsInInventory(itemToCheck);
    }

    public bool CheckIfItemIsInInventory(Item item) {
        bool isInInventory = false;
        for (int i = 0; i < itemStash.Count; i++) {
            //Debug.Log(items[i].ID + " ? " + item.ID);

            if (itemStash[i].ID == item.ID)
            {//here, the items[i].ID and item.ID can be matched more than once. Looks like items[i].ID is NOT getting updated during the switch
                //Debug.Log("[" + i + "]" + ": " + items[i].ID + ": " + item.ID + " <- Found Match");
                isInInventory = true;
            }
            else {
                //Debug.Log("[" + i + "]" + ": " + items[i].ID + ": " + item.ID);
            }
        }
        return isInInventory;
    }

    public bool CheckForEmptySlot() {
        bool hasEmptySlot = false;
        for (int i = 0; i < slotAmount; i++) {
            if (slotsThatAreVisible[i].transform.childCount == 0) {
                hasEmptySlot = true;              
            }
        }
        return hasEmptySlot;
    }
}
