using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offbound : MonoBehaviour
{
	RetryGame rG;

	void Awake()
    {
		rG = GameObject.Find("GameController").GetComponent<RetryGame>();
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
