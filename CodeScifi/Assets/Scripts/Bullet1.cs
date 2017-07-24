using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    private float speed = 30.0f;
    public string skipTag;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter(Collision c)
    {
        //destroy this object when it collides with anything 
        if (c.gameObject.tag != skipTag)
            GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.SetActive(false);
        }
    }
}
