using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
This method should be updated to work with Raycasts
*/
public class GroundCheck : MonoBehaviour {
	public PlayerAttributesScript playerAttributes;
	public Animator anim;
	ArrayList groundContacts;

	void Awake() {
		playerAttributes = FindObjectOfType<PlayerAttributesScript>();
		anim = GameObject.Find("Hero2 (1)").GetComponent<Animator>();
		groundContacts = new ArrayList();
	}

	//void Update() {
	//anim.SetBool("Falling", false);
	//}

	void OnTriggerEnter2D(Collider2D other)
	{
		#region Ground Check == true
		if (other.gameObject.CompareTag("Ground"))
		{
			playerAttributes.grounded = true;
			//anim.SetBool("Falling", false);
			groundContacts.Add(other);
		}
		#endregion
	}

	void OnTriggerExit2D(Collider2D other)
	{
		#region Ground Check == false
		if (other.gameObject.CompareTag("Ground"))
		{
			//anim.SetBool("Falling", true);
			try
			{
				groundContacts.Remove(other);
			}
			catch (Exception e){
				Debug.Log(e);
			}

			if (groundContacts.Count < 1) {
				playerAttributes.grounded = false;
			}
		}

		
		#endregion
	}


}
