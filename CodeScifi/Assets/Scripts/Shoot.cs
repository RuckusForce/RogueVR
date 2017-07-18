using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet = null;
    //public float bulletForce = 250f;

    private Rigidbody rigidbodyRef = null;

    private float fireRate = 0.3f;
    private float nextFire;

    private void Awake()
    {
        rigidbodyRef = bullet.GetComponent<Rigidbody>();
    }
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        FireInput();
	}

    private void FireInput()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, transform.position + (transform.right * 1), transform.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = transform.right * 40;
        }
    }
}
