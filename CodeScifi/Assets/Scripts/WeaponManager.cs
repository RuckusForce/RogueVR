using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject activeWeapon;
    WeaponBehavior wpn;


    // Use this for initialization
    void Start()
    {
        wpn = activeWeapon.GetComponent<WeaponBehavior>();
        GetComponent<SpriteRenderer>().sprite = wpn.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        HandgunFire();
        SMG_Fire();
        Shotgun_Fire();
    }

    public void UpdateWeapon(GameObject newWeapon)
    {
        activeWeapon = newWeapon;
        wpn = activeWeapon.GetComponent<WeaponBehavior>();
        GetComponent<SpriteRenderer>().sprite = wpn.sprite;

    }

    public void HandgunFire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector3 rotation = transform.localScale.x == 1 ? Vector3.zero : Vector3.forward * 180;
            GameObject projectile = (GameObject)Instantiate(wpn.projectile, transform.position + wpn.tip, Quaternion.Euler(rotation));

            if (wpn.projectileMode == WeaponBehavior.Modes.handgun)
            {
                projectile.GetComponent<Rigidbody2D>().velocity = transform.parent.localScale.x * Vector2.right * wpn.projectileSpeed;
            }
        }
    }

    public void SMG_Fire()
    {
        if (Input.GetKey(KeyCode.Mouse1) && wpn.projectileMode == WeaponBehavior.Modes.smg)
        {

            Vector3 rotation = transform.localScale.x == 1 ? Vector3.zero : Vector3.forward * 180;
            GameObject projectile = (GameObject)Instantiate(wpn.projectile, transform.position + wpn.tip, Quaternion.Euler(rotation));
            wpn.nextFire = Time.time + wpn.fireRate;
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * 20;
            projectile.GetComponent<Rigidbody2D>().velocity = transform.parent.localScale.x * Vector2.right * wpn.projectileSpeed;
        }
    }

    public void Shotgun_Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && wpn.projectileMode == WeaponBehavior.Modes.shotgun)
        {
            Vector3 rotation = transform.localScale.x == 1 ? Vector3.zero : Vector3.forward * 180;
            GameObject projectile = (GameObject)Instantiate(wpn.projectile, transform.position + wpn.tip, Quaternion.Euler(rotation));
            projectile.GetComponent<Rigidbody2D>().velocity = transform.parent.localScale.x * Vector2.right * wpn.projectileSpeed;


            //wpn.bullet1RX = Random.Range(0, 0);
            //wpn.bullet1RY = Random.Range(0, 0);
            //wpn.bullet2RX = Random.Range(0, 0);
            //wpn.bullet2RY = Random.Range(0, 0);
            //wpn.bullet3RX = Random.Range(0, 0);
            //wpn.bullet3RY = Random.Range(0, 0);

            //GameObject Pellet1 = (GameObject)Instantiate(projectile, wpn.shootPoint1.position, wpn.shootPoint1.rotation);
            //Pellet1.GetComponent<Transform>().Rotate(wpn.bullet1RX, wpn.bullet1RY, 0);
            //Pellet1.GetComponent<Rigidbody2D>().AddRelativeForce(wpn.shootPoint1.right * 2, ForceMode2D.Impulse);
            //Destroy(Pellet1, 6);

            //GameObject Pellet2 = (GameObject)Instantiate(projectile, wpn.shootPoint2.position, wpn.shootPoint2.rotation);
            //Pellet2.GetComponent<Transform>().Rotate(wpn.bullet2RX, wpn.bullet2RY, 0);
            //Pellet2.GetComponent<Rigidbody2D>().AddRelativeForce(wpn.shootPoint2.right * 2, ForceMode2D.Impulse);
            //Destroy(Pellet2, 6);

            //GameObject Pellet3 = (GameObject)Instantiate(projectile, wpn.shootPoint3.position, wpn.shootPoint3.rotation);
            //Pellet3.GetComponent<Transform>().Rotate(wpn.bullet3RX, wpn.bullet3RY, 0);
            //Pellet3.GetComponent<Rigidbody2D>().AddRelativeForce(wpn.shootPoint3.right * 2, ForceMode2D.Impulse);
            //Destroy(Pellet3, 6);
        }
    }
}
