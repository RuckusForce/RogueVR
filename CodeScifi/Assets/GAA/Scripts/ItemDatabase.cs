using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;//access files

//Allows us to take JSON data and turn it into a C# object, and vice versa
public class ItemDatabase : MonoBehaviour {
    [SerializeField]
    public List<Item> database = new List<Item>();
    [SerializeField]
    private JsonData itemData;

    public string filePath;
    public WWW www;

    public string myString;
    public bool doneLoading = false;

    [SerializeField]
    public List<Weapon> weaponDatabase = new List<Weapon>();
    [SerializeField]
    private JsonData weaponData;

    public string weaponFilePath;
    public WWW wwwForWeaps;

    void Awake()
    {
        //extract all items from the json file

        myString = "nada";
        //StartCoroutine(LoadJson());
    }

    public IEnumerator LoadJson() {//this is used to resolve loading the streaming json from an internet hierarchy (which introduces extra slashes)
        //Debug.Log("LoadJson()");
        filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "ItemsAgain.json");


        #region Convert Item Json to workable data
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            Debug.Log("filePath contains :/");
            www = new WWW(filePath);
            yield return www;//this should be waiting for www to finish downloading
            itemData = JsonMapper.ToObject(www.text);
        }
        else {
            //Debug.Log("File path doesn't contain :/");
            itemData = JsonMapper.ToObject(File.ReadAllText(filePath));
        }
        yield return itemData;
        #endregion
        StartCoroutine(ConstructItemDatabaseCR());

        #region Convert Weapon Json to workable data
        weaponFilePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Weapons.json");

        if (weaponFilePath.Contains("://") || weaponFilePath.Contains(":///"))
        {
            Debug.Log("weaponFilePath contains :/");
            wwwForWeaps = new WWW(weaponFilePath);
            yield return wwwForWeaps;
            weaponData = JsonMapper.ToObject(wwwForWeaps.text);//web
        }
        else {
            weaponData = JsonMapper.ToObject(File.ReadAllText(weaponFilePath));//file
            
        }        yield return weaponData;
        #endregion
        StartCoroutine(ConstructWeaponDatabase());

    }



    public Item FetchItemByID(int id) {
        //Debug.Log("FetchItemByID: id:" + id + ": " + database.ToString());
        
        if (database.Count > 0){
            for (int i = 0; i < database.Count; i++)
            {
                //Debug.Log(id + " ?= " + database[i].ID);
                if (database[i].ID == id)
                {
                    //Debug.Log("database: i:" + i + ": " + database[i].Sprite.ToString());
                    return database[i];//is an item
                }
            }
        }
        else {
            Debug.Log("Can't fetch items: database: count: " + database.Count);            
        }
        return null;
        
    }

    public Weapon FetchWeaponByID(int id) {
        if (weaponDatabase.Count > 0) {
            for (int i = 0; i < weaponDatabase.Count; i++) {
                if (weaponDatabase[i].ID == id) {
                    return weaponDatabase[i];
                }
            }
        }
        else {
            Debug.Log("Can't fetch weapon: weaponDatabase: count: " + weaponDatabase.Count);
        }
        return null;
    }

    public IEnumerator ConstructItemDatabaseCR() {
        if (database.Count < 1)//if not constructed yet
        {
            for (int i = 0; i < itemData.Count; i++)
            {
                //take the list, and loop through each item, adding them to the database
                database.Add(
                    new Item(
                        (int)itemData[i]["id"],
                        itemData[i]["title"].ToString(),
                        (int)itemData[i]["value"],
                        (int)itemData[i]["stats"]["power"],
                        (int)itemData[i]["stats"]["defence"],
                        (int)itemData[i]["stats"]["vitality"],
                        itemData[i]["description"].ToString(),
                        (bool)itemData[i]["stackable"],
                        (int)itemData[i]["rarity"],
                        itemData[i]["slug"].ToString(),
                        itemData[i]["effect"].ToString()
                    ));
            }
            yield return database;
        }
        else {//if already constructed, then good to go
            yield return true;
        }
    }

    public IEnumerator ConstructWeaponDatabase() {
        if (weaponDatabase.Count < 1)
        {
            for (int i = 0; i < weaponData.Count; i++)
            {
                weaponDatabase.Add(
                    new Weapon(
                        (int)weaponData[i]["id"],
                        weaponData[i]["title"].ToString(),
                        (int)weaponData[i]["value"],
                        (int)weaponData[i]["stats"]["power"],
                        weaponData[i]["description"].ToString(),
                        (int)weaponData[i]["rarity"],
                        weaponData[i]["slug"].ToString()
                    )
                );
            }
            yield return weaponDatabase;
        }
        else {
            yield return true;
        }    
    }
}

public class Weapon {
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public int Power { get; set; }
    public string Description { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite[] SpriteSheet { get; set; }//we can probably make this static
    public Sprite Sprite { get; set; }
    //private bool notFoundInFirstSpriteSheet;

    public Weapon() {//default constructor
        this.ID = -1;
    }

    public Weapon(int id, string title, int value, int power, string description, int rarity, string slug) {//constructor

        this.ID = id;//this.id modified by this.ID via constructor parameter id)
        this.Title = title;
        this.Value = value;
        this.Power = power;
        this.Description = description;
        this.Rarity = rarity;
        this.Slug = slug;

        this.SpriteSheet = Resources.LoadAll<Sprite>("Sprites/FPSIconPack");
        for (int i = 0; i < this.SpriteSheet.Length; i++)
        {
            string temp = this.SpriteSheet[i].name;
            
            if (temp.Equals(slug))
            {
                this.Sprite = this.SpriteSheet[i];
                //Debug.Log("Found: " + slug);
                break;
            }
        }
    }
}

public class Item {//properties start with capitals, and the attribute it works with is the lowercase one. Methods can be attached to each property.
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public int Power { get; set; }
    public int Defence { get; set; }
    public int Vitality { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public string Effect { get; set; }
    public Sprite[] SpriteSheet { get; set; }//required to keep our items in a sprite sheet
    public Sprite Sprite { get; set; }

    private bool notFoundInFirstSpriteSheet;

    //attributes of each property
    public Item(int id, string title, int value, int power, int defence, int vitality, string description, bool stackable, int rarity, string slug, string effect) {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Power = power;
        this.Defence = defence;
        this.Vitality = vitality;
        this.Description = description;
        this.Stackable = stackable;
        this.Rarity = rarity;
        this.Slug = slug;//The name of the asset
        this.Effect = effect;
        //this.Sprite = Resources.Load<Sprite>("Sprites/"+slug);
        /*
        Since we're using a SpriteSheet, we'll need to unpack it into an array first, and then find the actual sprite we want to use.
        The search for the proper sprite should be a Database function instead, so as to keep the Item Class compact.
        But, there should be a function to assign the sprite in the Item class.
        */
        this.SpriteSheet = Resources.LoadAll<Sprite>("Sprites/uipack_rpg_sheet");//will have to change this later to access different sprite sheets.
        for (int i = 0; i < this.SpriteSheet.Length; i++) {
            string temp = this.SpriteSheet[i].name;
            if (temp.Equals(slug))
            {
                this.Sprite = this.SpriteSheet[i];
                notFoundInFirstSpriteSheet = false;
                break;
            }
            else {
                notFoundInFirstSpriteSheet = true;
            }
        }
        if (notFoundInFirstSpriteSheet) {
            this.SpriteSheet = Resources.LoadAll<Sprite>("Sprites/item");//Our 2nd sprite sheet
            for (int i = 0; i < this.SpriteSheet.Length; i++)
            {
                string temp = this.SpriteSheet[i].name;
                if (temp.Equals(slug))
                {
                    this.Sprite = this.SpriteSheet[i];
                    break;
                }
            }
        }
    }

    public Item(int id, string title, int value)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        //this.Power = power;
        //this.Defence = defence;
        //this.Vitality = vitality;
        //this.Description = description;
        //this.Stackable = stackable;
        //this.Rarity = rarity;
        //this.Slug = slug;

    }

    //Default
    public Item() {
        this.ID = -1;
    }
}


