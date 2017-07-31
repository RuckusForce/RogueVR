using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offbound : MonoBehaviour
{
	RetryGame rG;
	Vector3 position;

	void Awake()
    {
		rG = GameObject.Find("GameController").GetComponent<RetryGame>();
		position = transform.position;
	}

	void Update() {
		transform.position = new Vector3(
			transform.position.x,
			Mathf.Clamp(transform.position.y, -10f, -10f),
			transform.position.z);
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			//if (Time.timeSinceLevelLoad > PlayerPrefs.GetFloat("High Score"))
			//{
			//	Debug.Log(Time.timeSinceLevelLoad + " > " + PlayerPrefs.GetFloat("High Score"));
			//	PlayerPrefs.SetFloat("High Score", Time.timeSinceLevelLoad);
			//}
			rG.Retry();
		}
		else if (col.gameObject.CompareTag("Enemy")) {
			col.gameObject.SetActive(false);
		}
	}
}
