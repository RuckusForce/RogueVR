using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{

    public enum Modes
    { handgun, smg }

    public Sprite sprite;
    public GameObject projectile;
    public float projectileSpeed;
    public Vector3 tip;
    public Modes projectileMode;

    //public Transform shootPoint1;
    //public Transform shootPoint2;
    //public Transform shootPoint3;

    //public float bullet1RX;
    //public float bullet1RY;
    //public float bullet2RX;
    //public float bullet2RY;
    //public float bullet3RX;
    //public float bullet3RY;

    public float fireRate = 1.0f;
    public float nextFire;


    // Use this for initialization
    void Start()
    {
        tip = transform.GetChild(0).transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
