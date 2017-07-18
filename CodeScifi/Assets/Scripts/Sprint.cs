using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{
    PlayerControl pc;
    float wlkSpeed;
    float sprintSpeed = 2.5f;

	// Use this for initialization
	void Start ()
    {
        //On start the Sprint Script is accessing the PlayerControl sprint and naming it to pc because we programmers are lazy with names
        pc = (PlayerControl)GetComponent<PlayerControl>();
        //Again assigning wlkSpeed to pc.walkSpeed to access it's field
        wlkSpeed = pc.walkSpeed;
        sprintSpeed = wlkSpeed * 2;

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        HandleSprint();
	}

    void HandleSprint()
    {
        //When the button is pressed down, the player goes faster
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            pc.walkSpeed = sprintSpeed;
        }
        //When the button is NOT pressed down(released), returns to walking speed
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            pc.walkSpeed = wlkSpeed;
        }
    }
}
