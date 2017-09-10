using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputScript : MonoBehaviour {

	public PlayerAttributesScript attributes;
	public Inventory inventory;
	private Transform parent;

	public Animator anim;
	public Rigidbody2D rb;
	public Vector2 movement;
	public float moveSpeed;
	public bool dead;
	public float jumpStrength;
	public float automaticHorizontalMoveSpeed;
	public float jumpVelocity;
	public int maxJumpCount;

	public float levelTime;
	public float levelTwo;
	public float levelThree;
	public float levelFour;
	public float levelFive;
	public float levelSix;
	public float levelSeven;
	public float levelEight;
	public float levelNine;

	AudioSource jumpAudioSource;

	public bool freezeHorizontalMovement;
	bool freeze;

	TextBoxManager textBoxManager;

	void Awake() {

		parent = transform.parent;
		attributes = parent.GetComponentInChildren<PlayerAttributesScript>();
		inventory = FindObjectOfType<Inventory>();
		anim = GetComponentInParent<Animator>();
		rb = GetComponentInParent<Rigidbody2D>();
		//moveSpeed = .1f;
		
		dead = false;
		jumpStrength = 5f;
		automaticHorizontalMoveSpeed = 1f;
		moveSpeed = 6f;
		jumpVelocity = 0f;
		maxJumpCount = 2;

		levelTime = 0f;
		levelTwo = 10f;
		levelThree = 20f;
		levelFour = 30f;
		levelFive = 40f;
		levelSix = 45f;
		levelSeven = 50f;
		levelEight = 55f;
		levelNine = 60f;

		jumpAudioSource = GetComponent<AudioSource>();

		freeze = false;
		if (SceneManager.GetActiveScene().buildIndex == 1) {
			textBoxManager = GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>();
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (SceneManager.GetActiveScene().buildIndex != 0) {//Don't allow these types of input in Scene 0 (Main Scene)
			#region Falling Attributes
			if (!attributes.grounded && !anim.GetBool("Falling"))
			{
				anim.SetBool("Falling", true);
			}
			else if (attributes.grounded)
			{
				anim.SetBool("Falling", false);
			}
			#endregion

			#region Horizontal Movement Controls (Disabled)
			//if (!dead)
			//{
			//	float moveHorizontal = Input.GetAxisRaw("Horizontal");
			//	float moveVertical = Input.GetAxisRaw("Vertical");

			//	anim.SetFloat("Move", moveHorizontal);
			//	if (moveHorizontal < 0)
			//	{
			//		parent.transform.localScale = new Vector3(-1f, 1f, 1f);
			//	}
			//	else if (moveHorizontal > 0)
			//	{
			//		parent.transform.localScale = new Vector3(1f, 1f, 1f);
			//	}

			//	movement = new Vector2(moveHorizontal * moveSpeed, moveVertical);

			//	parent.transform.position = new Vector2(parent.transform.position.x + moveHorizontal * moveSpeed, parent.transform.position.y);
			//}
			////movement may be getting limited by animator? Yep, Animator Component -> UpdateMode -> Animate Physics

			#endregion

			#region Click/Tap Controls (Next Text, Jump, and Resume from Pause) 
			if (Input.GetMouseButtonDown(0))
			{
				#region Tap for Next Text - pulled from TextBoxManager.cs
				if (textBoxManager.isActive)
				{
					textBoxManager.currentLine += 1;
					return;
				}
				#endregion

				if (!freeze)
				{
					#region Tap to Jump - doesn't trigger if textBoxManager is Active
					if (Time.timeScale == 1)
					{
						if (!dead)
						{
							if (attributes.grounded)
							{
								maxJumpCount = 2;//double jump capacity
								maxJumpCount--;
								rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);//should change this to modify the character's horizontal movement
								jumpAudioSource.Play();
							}
							else if (!attributes.grounded && maxJumpCount > 0)
							{
								maxJumpCount--;
								rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);//should change this to modify the character's horizontal movement
								jumpAudioSource.Play();
							}
						}
					}
					#endregion
				}
				#region Tap to Resume from Pause
				
				if (Time.timeScale == 0)
				{
					Time.timeScale = 1f;
				}
				#endregion

			}
			#endregion

			#region Attribute Controls (Disabled)
			//if (Input.GetKeyDown(KeyCode.Y))
			//{
			//	attributes.PlayerDecreaseHealth(10f);
			//}
			//if (Input.GetKeyDown(KeyCode.U))
			//{
			//	attributes.PlayerRecoverHealth(10f);
			//}

			//if (Input.GetKeyDown(KeyCode.H))
			//{
			//	attributes.PlayerDecreaseShield(10f);
			//}
			//if (Input.GetKeyDown(KeyCode.J))
			//{
			//	attributes.PlayerRecoverShield(10f);
			//}

			//if (Input.GetKeyDown(KeyCode.N))
			//{
			//	attributes.PlayerDecreaseEnergy(10f);
			//}
			//if (Input.GetKeyDown(KeyCode.M))
			//{
			//	attributes.PlayerRecoverEnergy(10f);
			//}

			#endregion

			#region Item Controls (Disabled)
			//if (Input.GetKeyDown(KeyCode.Alpha1))
			//{
			//	inventory.UseItemFromSlot(0);
			//}
			//if (Input.GetKeyDown(KeyCode.Alpha2))
			//{
			//	inventory.UseItemFromSlot(1);
			//}
			//if (Input.GetKeyDown(KeyCode.Alpha3))
			//{
			//	inventory.UseItemFromSlot(2);
			//}

			//if (Input.GetKeyDown(KeyCode.Alpha4))
			//{
			//	inventory.AddItem(0);
			//}
			//if (Input.GetKeyDown(KeyCode.Alpha5))
			//{
			//	inventory.AddItem(1);
			//}
			//if (Input.GetKeyDown(KeyCode.Alpha6))
			//{
			//	inventory.AddItem(2);
			//}
			//if (Input.GetKeyDown(KeyCode.Alpha7))
			//{
			//	inventory.AddItem(3);
			//}
			//if (Input.GetKeyDown(KeyCode.Alpha8))
			//{
			//	inventory.AddItem(4);
			//}
			#endregion

			#region Weapon Controls (Disabled)
			//if (Input.GetKeyDown(KeyCode.R))
			//{
			//	inventory.CycleWeaponBackward();
			//}
			//if (Input.GetKeyDown(KeyCode.T))
			//{
			//	inventory.CycleWeaponForward();
			//}
			#endregion

			#region [DEBUG] Restart with Esc Button
			if (Input.GetKey(KeyCode.Escape))
			{
				SceneManager.LoadScene("RogueVR2");
				Time.timeScale = 1f;
			}
			#endregion
		}

	}

	void FixedUpdate() {
		if (!freeze)
		{
			#region Automatic Horizonal Movement (rb.AddForce) 
			//if (!dead)
			//{
			//	moveSpeed = 3200f;
			//	anim.SetFloat("Move", automaticHorizontalMoveSpeed);
			//	parent.transform.localScale = new Vector3(1f, 1f, 1f);//will be used later for changing direction

			//	movement = new Vector2(automaticHorizontalMoveSpeed * moveSpeed, rb.velocity.y);
			//	//parent.transform.position = new Vector2(parent.transform.position.x + moveHorizontal * moveSpeed, parent.transform.position.y);
			//	//above may be causing slip through, using Continuous Collision Detection seems to work for now, but modifying position directly is bad practice.
			//	//rb.MovePosition(parent.transform.position + transform.right * moveHorizontal * Time.deltaTime);//only works when the Rigidbody is kinematic
			//	//rb.velocity = movement;//velocity gets modified, but character doesn't move. May be due to Apply Root Animation of Animator modifying velocity at the same time.
			//	rb.AddForce(movement);//has drift that may or may not be a good thing
			//}
			#endregion

			#region Automatic Horizontal Movement (rb.velocity)
			if (!dead)
			{
				anim.SetFloat("MoveX", automaticHorizontalMoveSpeed);
				parent.transform.localScale = new Vector3(1f, 1f, 1f);//used for changing direction

				#region moveSpeed Variance per time passing
				if (Time.timeSinceLevelLoad > levelTime)
				{
					levelTime = levelTime + 5f;
					//moveSpeed = moveSpeed + .4f;

				}
				#endregion
				
				movement = new Vector2(automaticHorizontalMoveSpeed * moveSpeed, rb.velocity.y);
				if (!freezeHorizontalMovement)
				{//toggle movement in editor
					rb.velocity = movement;
					//velocity gets modified, but character doesn't move. May be due to Apply Root Animation of Animator modifying velocity at the same time.
					//above now works. Animator needed to have Apply Root Motion, Normal Update Mode, Cull Completely Culling Mode
					//also had a simulated dynamic rigidbody2d
				}
			}
			else {
				
			}
			#endregion
		}
		else {
			anim.SetFloat("MoveX", 0f);
		}
	}

	public void FreezeInput() {
		Debug.Log("FreezeInput()");
		freeze = true;
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
	}

	public void UnfreezeInput() {
		Debug.Log("UnfreezeInput()");
		freeze = false;
		rb.constraints = RigidbodyConstraints2D.None;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	public bool WalkTowards(Vector3 target) {
		Vector2 newTarget = new Vector2(target.x, target.y);
		Vector2.MoveTowards(rb.position, newTarget, Time.deltaTime);
		return true;
	}
}