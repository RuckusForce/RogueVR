using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private float speed = 45.0f;
	public string skipTag;
	//public string skipTag;


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

	//private void OnCollision2D(Collision2D c)
	//{
	//	Debug.Log("Bullet.OnCollision2D()");
	//	//destroy this object when it collides with anything 
	//	if (c.gameObject.tag == "Enemy" || c.gameObject.name == "Ground" || c.gameObject.tag == "Shootable")
	//		GameObject.Destroy(gameObject);
	//}

	//void OnCollisionEnter2D(Collision2D c)
	//{
	//	Debug.Log("Bullet.OnCollision2D()");
	//	//destroy this object when it collides with anything 
	//	if (c.gameObject.tag == "Enemy" || c.gameObject.name == "Ground" || c.gameObject.tag == "Shootable")
	//		GameObject.Destroy(gameObject);
	//}

	//void OnTriggerEnter2D(Collider2D c)
	//{
	//	Debug.Log("Bullet.OnTriggerEnter2D()");
	//	//destroy this object when it collides with anything 
	//	if (c.gameObject.tag == "Enemy" || c.gameObject.name == "Ground" || c.gameObject.tag == "Shootable")
	//		GameObject.Destroy(gameObject);
	//}


}
