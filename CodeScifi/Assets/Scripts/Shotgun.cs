using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Transform shootPoint1;
    public Transform shootPoint2;
    public Transform shootPoint3;

    public float bullet1RX;
    public float bullet1RY;
    public float bullet2RX;
    public float bullet2RY;
    public float bullet3RX;
    public float bullet3RY;

    public GameObject bPrefab = null;
    private Rigidbody rigid = null;

    private float fireRate = 0.5f;
    private float nextFire;

	// Use this for initialization
	void Start ()
    {
        rigid = bPrefab.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Spread();
	}

    void Spread()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            bullet1RX = Random.Range(-2, 2);
            bullet1RY = Random.Range(-2, 2);
            bullet2RX = Random.Range(-2, 2);
            bullet2RY = Random.Range(-2, 2);
            bullet3RX = Random.Range(-2, 2);
            bullet3RY = Random.Range(-2, 2);

            GameObject Pellet1 = (GameObject)Instantiate(bPrefab, shootPoint1.position, shootPoint1.rotation);
            Pellet1.GetComponent<Transform>().Rotate(bullet1RX, bullet1RY, 0);
            Pellet1.GetComponent<Rigidbody>().AddRelativeForce(shootPoint1.right * 2, ForceMode.Impulse);
            Destroy(Pellet1, 6);

            GameObject Pellet2 = (GameObject)Instantiate(bPrefab, shootPoint2.position, shootPoint2.rotation);
            Pellet2.GetComponent<Transform>().Rotate(bullet2RX, bullet2RY, 0);
            Pellet2.GetComponent<Rigidbody>().AddRelativeForce(shootPoint2.right * 2, ForceMode.Impulse);
            Destroy(Pellet2, 6);

            GameObject Pellet3 = (GameObject)Instantiate(bPrefab, shootPoint3.position, shootPoint3.rotation);
            Pellet3.GetComponent<Transform>().Rotate(bullet3RX, bullet3RY, 0);
            Pellet3.GetComponent<Rigidbody>().AddRelativeForce(shootPoint3.right * 2, ForceMode.Impulse);
            Destroy(Pellet3, 6);
        }
    }
}
