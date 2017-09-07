using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public GameObject rBullet = null;
    private Rigidbody rBodyRef = null;
    
    private float fireRate = 0.1f;
    private float nextFire;

	AimReticleScript aimReticleScript;

	private void Awake()
    {
        rBodyRef = rBullet.GetComponent<Rigidbody>();
		aimReticleScript = GameObject.Find("CanvasCursor").GetComponent<AimReticleScript>();
	} 


    // Update is called once per frame
    void Update()
    {
		//PewPew();
		PewPewAuto();
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

	public void PewPewAuto() {
		if (aimReticleScript.HasTarget() && Time.time > nextFire)
		{
			Debug.Log("PewPewAuto");
			nextFire = Time.time + fireRate;
			GameObject temp = Instantiate(rBullet, transform.position, transform.rotation);
			Rigidbody2D rb = temp.GetComponent<Rigidbody2D>();
			Vector3 targetCoordinates = aimReticleScript.ReturnFirstCoordinates();
			Vector3 bulletDirection = targetCoordinates - transform.position;
			transform.LookAt(aimReticleScript.transform);
			//rb.velocity = transform.right * 40;//let's direct the rb towards the enemy
			//rb.AddForceAtPosition(transform.forward * 100, -aimReticleScript.ReturnFirstCoordinates());
			//rb.MovePosition(aimReticleScript.ReturnFirstCoordinates());

			//maybe calculate the angle between bullet source and the target
			//then add force with that angle?

			rb.AddForce(bulletDirection * 200f);
		}
	}

    
}
