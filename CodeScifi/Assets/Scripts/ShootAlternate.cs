using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAlternate : MonoBehaviour
{
    public GameObject bullet = null;
    //public float bulletForce = 250f;

    private Rigidbody rigidbodyRef = null;

    private float fireRate = 0.3f;
    private float nextFire;

    AimReticleScript aimReticleScript;

    private void Awake()
    {
        rigidbodyRef = bullet.GetComponent<Rigidbody>();
		aimReticleScript = GameObject.Find("AimReticle").GetComponent<AimReticleScript>();
	}
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		//FireInput();
		FireInputAuto();
	}

    private void FireInput()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, transform.position + (transform.right * 1), transform.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = transform.right * 40;
        }
    }

    private void FireInputAuto() {
        if (aimReticleScript.HasTarget() && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody rb = temp.GetComponent<Rigidbody>();
            Vector3 targetCoordinates = aimReticleScript.ReturnFirstCoordinates();
            Vector3 bulletDirection = targetCoordinates - transform.position;
            //rb.velocity = transform.right * 40;//let's direct the rb towards the enemy
            //rb.AddForceAtPosition(transform.forward * 100, -aimReticleScript.ReturnFirstCoordinates());
            //rb.MovePosition(aimReticleScript.ReturnFirstCoordinates());

            //maybe calculate the angle between bullet source and the target
            //then add force with that angle?

            rb.AddForce(bulletDirection * 2000f);
        }
    }
}
