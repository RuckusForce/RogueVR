using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private Transform energyballPos;

    [SerializeField]
    protected float movementSpeed;

    protected bool facingRight;

    [SerializeField]
    protected GameObject energyballPrefab;

    public bool Attack { get; set; }

    public Animator MyAnimator { get; private set; }
    
    // Use this for initialization
    public virtual void Start ()
    {
        facingRight = true;
        MyAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public virtual void ThrowEnergyBall(int value)
    {
        if (facingRight)
        {
            GameObject tmp = Instantiate(energyballPrefab, energyballPos.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            tmp.GetComponent<EnergyBall>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = Instantiate(energyballPrefab, energyballPos.position, Quaternion.Euler(new Vector3(0, 0, -180)));
            tmp.GetComponent<EnergyBall>().Initialize(Vector2.left);
        }
    }
}
