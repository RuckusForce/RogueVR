using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Declaring public or private variables
    public float walkSpeed = 4.0f;
    public float jumpForce = 400f;
    private bool isJumping = true;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    // FixedUpdate handles all physics
    void FixedUpdate()
    {
        HandleMovement();
        HandleJumping();
    }

    //Handles Player's movement 
    private void HandleMovement()
    {
        //Move Left or Right on the X axis
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.fixedDeltaTime * walkSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * Time.fixedDeltaTime * -walkSpeed;
        }
    }

    // Handle's the player's jump ability on the Y axis
    private void HandleJumping()
    {
        if (!isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Accessings the player's Rigidbody and adding Force on the Y Axis (x, y, z)
                transform.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
                isJumping = true;
            }
        }
    }

    //This Handles the collision, wheter the player will collide with another objects that are declared
    private void OnCollisionEnter(Collision c)
    {
        // If the player enters the collision or collides with these named objects, it keeps the player from jumping infinitely by spaming the space bar
        if (c.gameObject.name == "Ground" || c.gameObject.name == "Platform")
        {
            isJumping = false;
        }
    }

}
