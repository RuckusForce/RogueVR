using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;


    public GameObject enemyGraphic;
    private bool canFlip = true;
    private bool facingRight = false;
    private float flipTime = 2f;
    private float nextFlipChance = 0f;

    //Attacking
    public float chargeTime;
    private float startChargeTime;
    private bool charging;
    Rigidbody2D enemyRB;

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > nextFlipChance)
        {
            if (Random.Range(0, 3) >= 2) flipFacing();
            {
                nextFlipChance = Time.time + flipTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (facingRight && other.transform.position.x < transform.position.x)
            {
                flipFacing();
            }
            else if (!facingRight && other.transform.position.x > transform.position.x)
            {
                flipFacing();
            }
            canFlip = false;
            charging = true;
            startChargeTime = Time.time + chargeTime;
        }
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        if (startChargeTime < Time.time)
    //        {
    //            if (!facingRight)
    //            {
    //                enemyRB.AddForce(new Vector2(-1, 0) * enemySpeed);
    //            }
    //            else
    //            {
    //                enemyRB.AddForce(new Vector2(1, 0) * enemySpeed);
    //            }
    //        }
    //    }
    //}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canFlip = true;
            charging = false;
            enemyRB.velocity = new Vector2(0f, 0f);
        }
    }

    public void flipFacing()
    {
        if (!canFlip) return;
        {
            float facingX = enemyGraphic.transform.localScale.x;
            facingX *= -1f;
            //enemyGraphic.transform.localScale.x = new Vector3(facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
            enemyGraphic.transform.localScale = new Vector3(facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
            facingRight = !facingRight;
        }
    }

}
