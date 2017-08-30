using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// If an enemy enters the trigger, then store and pass the location to the shooting script
/// </summary>
public class AimReticleScript : MonoBehaviour {

    GameObject targetObject;
    public List<GameObject> targetList;

    void Awake()
    {
        targetList = new List<GameObject>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
		//Debug.Log(collision.gameObject.name + " has entered.");
		if (collision.gameObject.CompareTag("Enemy"))
		{
			targetObject = collision.gameObject;
			targetList.Add(targetObject);
			//Destroy(collision.gameObject);
		}
		else if (collision.gameObject.CompareTag("ReticleButtons"))
		{
			Debug.Log("ReticleButton has entered.");
			collision.gameObject.GetComponent<TestButton>().testButtonPress();

		}
		else if (collision.gameObject.CompareTag("Shootable")) {
			Debug.Log("Shootable has entered.");
			targetObject = collision.gameObject;
			targetList.Add(targetObject);
		}
	}
    private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("Enemy"))
		{
			targetList.Remove(collision.gameObject);
		}
		else if (collision.gameObject.CompareTag("Shootable")) {
			Debug.Log("Shootable has exited.");
			targetList.Remove(collision.gameObject);
		}
    }

    public bool HasTarget() {
        if (targetList.Count > 0)
        {
            return true;
        }
        else {
            return false;
        }
    }

    public Vector3 ReturnFirstCoordinates()
    {
        if (HasTarget())
        {
            return targetList[0].transform.position;
        }
        else {
            Debug.LogError("No target in targetList. Size: " + targetList.Count);
            return new Vector3(0f,0f,0f);
        }
    }
}
