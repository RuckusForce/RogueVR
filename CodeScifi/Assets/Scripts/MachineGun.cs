using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public GameObject rBullet = null;
    private Rigidbody rBodyRef = null;
    
    private float fireRate = 0.1f;
    private float nextFire;

    private void Awake()
    {
        rBodyRef = rBullet.GetComponent<Rigidbody>();
    } 


    // Update is called once per frame
    void Update()
    {
        PewPew();
    }

    public void PewPew()
    {
        if (Input.GetKey(KeyCode.Mouse1) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(rBullet, transform.position + (transform.right * 1), transform.rotation);
            Rigidbody rb = rBullet.GetComponent<Rigidbody>();
            rb.velocity = transform.right * 80;
        }
    }

    
}
