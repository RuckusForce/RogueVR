using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 45.0f;
    public string skipTag;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnCollision2D(Collision2D c)
    {
        //destroy this object when it collides with anything 
        if (c.gameObject.tag == "Enemy" || c.gameObject.name == "Ground")
            GameObject.Destroy(gameObject);
    }
}
