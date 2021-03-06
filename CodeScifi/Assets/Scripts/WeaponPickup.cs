﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject weaponHere;

    // Use this for initialization
    void Start()
    {
        weaponHere = weapons[Random.Range(0, weapons.Length)];
        GetComponent<SpriteRenderer>().sprite = weaponHere.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D colli)
    {
        if (colli.gameObject.name == "Hero2 (1)") //DO NOT CHANGE IT 
        {
            colli.transform.Find("WeaponSlot").GetComponent<WeaponManager>().UpdateWeapon(weaponHere);
        }
    }
}
