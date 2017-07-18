using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestScript : MonoBehaviour
{
    Animator anim;
    public Rigidbody2D rb;
    public Vector2 movement;
    public float moveSpeed;
    public bool dead;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = .04f;
        dead = false;
    }

    void Update()
    {

        if (!dead) {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                anim.SetTrigger("Death");
                dead = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                anim.SetBool("Kneel", true);
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                anim.SetBool("Kneel", false);
            }
        }



    }

    void FixedUpdate()
    {
        //rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        //rb.position = Vector2.Lerp(rb.position, rb.position + velocity, Time.fixedDeltaTime);
        if (!dead)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            anim.SetFloat("Move", moveHorizontal);
            if (moveHorizontal < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if(moveHorizontal > 0){
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            movement = new Vector2(moveHorizontal * moveSpeed, moveVertical);
            //rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
            //rb.AddForce(movement, ForceMode2D.Impulse);
        
            transform.position = new Vector2(transform.position.x + moveHorizontal * moveSpeed, transform.position.y);
        }
        //movement may be getting limited by animator? Yep, Animator Component -> UpdateMode -> Animate Physics
    }
}
