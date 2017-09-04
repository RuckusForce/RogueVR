using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If an enemy enters the trigger, then store and pass the location to the shooting script
/// </summary>
public class AimReticleScript : MonoBehaviour {

    GameObject targetObject;
    public List<GameObject> targetList;
	List<Vector3> targetVectors;
	SpriteRenderer sr;

	Dictionary<GameObject, Vector3> targets;

    void Awake()
    {
        targetList = new List<GameObject>();
		targetVectors = new List<Vector3>();
		sr = GetComponentInChildren<SpriteRenderer>();
		targets = new Dictionary<GameObject, Vector3>();
    }

	void Update() {
		if (targetList.Count > 0)
		{
			sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
		}
		else {
			sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, .25f);
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		//Debug.Log(collision.gameObject.name + " has entered.");
		if (collision.gameObject.CompareTag("Enemy"))
		{
			targetObject = collision.gameObject;
			targetList.Add(targetObject);//should use a dictionary instead? That way, we can have both targetObject AND targetVector
			//targetVectors.Add(targetObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position));
			targets.Add(targetObject, collision.bounds.ClosestPoint(this.transform.position));
			//Destroy(collision.gameObject);
			
		}
		else if (collision.gameObject.CompareTag("ReticleButtons"))
		{
			Debug.Log("ReticleButton has entered.");
			collision.gameObject.GetComponent<TestButton>().testButtonPress();
		}
		//else if (collision.gameObject.CompareTag("Shootable")) {
		//	Debug.Log("Shootable has entered.");
		//	targetObject = collision.gameObject;
		//	targetList.Add(targetObject);
		//	targets.Add(targetObject, collision.bounds.ClosestPoint(this.transform.position));
		//}
	}
	private void OnTriggerStay2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Shootable"))
		{
			Debug.Log("Shootable has entered.");
			targetObject = collision.gameObject;
			targetList.Add(targetObject);
			if (targets.ContainsKey(targetObject))
			{
				targets[targetObject] = collision.bounds.ClosestPoint(this.transform.position);
			}
			else {
				targets.Add(targetObject, collision.bounds.ClosestPoint(this.transform.position));
			}
			
		}
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("Enemy"))//should we empty the entire list instead of one by one?
		{
			targetList.Remove(collision.gameObject);
			targets.Remove(collision.gameObject);
		}
		else if (collision.gameObject.CompareTag("Shootable")) {
			targetList.Remove(collision.gameObject);
			targets.Remove(collision.gameObject);
		}
    }

    public bool HasTarget() {
		//Debug.Log("HasTarget()");
		//if (targetList.Count > 0)
		//{
		//    return true;
		//}
		//else {
		//    return false;
		//}
		if (targets.Count > 0)
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
			//return targetList[0].transform.position;
			return targets[targetList[targetList.Count-1]];
        }
        else {
            Debug.LogError("No target in targetList. Size: " + targetList.Count);
            return new Vector3(0f,0f,0f);
        }
    }
}
