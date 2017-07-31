using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
-Stays ahead of the other platforms to generate more
-Position 
*/

public class PlatformGenerator : MonoBehaviour {
	public GameObject thePlatform;
	public Transform generationPoint;
	public float distanceBetween;

	float platformWidth;//initialized on Start
	Sprite platformSprite;
	public List<GameObject> platformPool;
	GameObject tempPlatform;
	int poolCount;

	public GameObject theObstacle;
	float obstacleHeight;
	Sprite obstacleSprite;
	public List<GameObject> obstaclePool;
	GameObject tempObstacle;
	int obstaclePoolCount;

	public GameObject theEnemy;
	float enemyHeight;
	public List<GameObject> enemyPool;
	GameObject tempEnemy;
	int enemyPoolCount;

	public int randomInt;
	public float newPlatformHeight;
	public float currentPlatformHeight;
	public float maxPlatformHeight;
	public float minPlatformHeight;

	GameObject trailingPlatform;
	GameObject upcomingPlatform;

	

	public float obstacleTimeInterval;
	public float heightVariance;//10
	public float deactivateVariance;

	bool minTwo;

	//// Use this for initialization
	void Awake()
	{
		thePlatform = Resources.Load("Prefabs/Platforms/Platform") as GameObject;
		generationPoint = GameObject.Find("PlatformGenerationPoint").transform;//this is where this generator would reset to

		obstacleTimeInterval = 3f;

		platformSprite = thePlatform.GetComponent<SpriteRenderer>().sprite;
		Camera main = FindObjectOfType<Camera>();
        platformWidth = platformSprite.rect.width / platformSprite.pixelsPerUnit;
        //since the platform width may vary based on the sizes of the sprite, let's calculate this on every respriting
        //OR, make sure that each determined sprite is 100x100
        //Since we're using an edge collider, we'll have to use a different measure of width		

        //distanceBetween = platformWidth*3f;
        distanceBetween = 0f;

		poolCount = 70;
		for (int i = 0; i < poolCount; i++) {
			platformPool.Add(Instantiate(thePlatform, transform.position, transform.rotation));
			platformPool[i].gameObject.SetActive(false);
		}
		//maxPlatformHeight = 3f;
		//minPlatformHeight = -3f;

		theObstacle = Resources.Load("Prefabs/DummyObstacle") as GameObject;
		obstaclePoolCount = 20;
		for (int i = 0; i < obstaclePoolCount; i++) {
			obstaclePool.Add(Instantiate(theObstacle));//can this be in the PlatformRespriter? No because we don't need multiple object pools
			obstaclePool[i].gameObject.SetActive(false);
		}

		theEnemy = Resources.Load("Prefabs/Within_Range") as GameObject;
		enemyPoolCount = 20;
		for (int i = 0; i < enemyPoolCount; i++)
		{
			enemyPool.Add(Instantiate(theEnemy));
			enemyPool[i].gameObject.SetActive(false);
		}


	}

	// Update is called once per frame
	void Update () {
		if (transform.position.x < generationPoint.position.x) {
			#region Modify Platform Height
			randomInt = (int)Random.Range(0, heightVariance);
			//Instead of a range, increment/decrement height

			switch (randomInt)
			{
				case 0:
					currentPlatformHeight = currentPlatformHeight - 1f;
					break;
				case 1:
					currentPlatformHeight = currentPlatformHeight - 1f;
					break;
				case 2:
					currentPlatformHeight = currentPlatformHeight + 1f;
					break;
				case 3:
					currentPlatformHeight = currentPlatformHeight + 1f;
					break;
				default:
					currentPlatformHeight = transform.position.y;//should maintain status quo
					break;
			}

			#endregion

			//platformWidth = platformSprite.rect.width / platformSprite.pixelsPerUnit;//since the platform width may vary based on the sizes of the sprite, let's calculate this on every respriting
			
			//if above max or below min, reset to max/ min
			if (currentPlatformHeight > maxPlatformHeight
				|| currentPlatformHeight < minPlatformHeight)
			{
				currentPlatformHeight = transform.position.y;
			}

			transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween,
												currentPlatformHeight, 
												transform.position.z);


			try
			{
				tempPlatform = returnInactivePlatform();
				tempPlatform.SetActive(true);



				#region Deactivate Some Platforms/Spawn Enemy 
				randomInt = (int)Random.Range(0, deactivateVariance);
				if (randomInt % 30 == 0)
				{
					tempPlatform.SetActive(false);
				}
				//else if (randomInt % 45 == 0) {
				//	tempEnemy = returnInactiveEnemy();
				//	tempEnemy.transform.position = transform.position;
				//	tempEnemy.SetActive(true);
				//}
				#endregion

				tempPlatform.transform.position = this.transform.position;
			}
			catch (System.Exception e) {
				//Debug.Log(e);
			}
			

		}

		distanceBetween = 0f;//this is for the solid ground
		//We can modify distanceBetween after this line, with an increasing distance
		//Or deactive some platforms instead
	}
	
	GameObject returnInactivePlatform() {
		GameObject returnMe = null;
		for (int i = 0; i < platformPool.Count; i++) {
			if (!platformPool[i].activeInHierarchy) {
				returnMe = platformPool[i];
				break;
			}
		}
		return returnMe;
	}

	public GameObject returnInactiveObstacle() {
		GameObject returnMe = null;
		for (int i = 0; i < obstaclePoolCount; i++) {
			if (!obstaclePool[i].activeInHierarchy) {
				returnMe = obstaclePool[i];
				break;
			}
		}
		return returnMe;
	}

	public GameObject returnInactiveEnemy()
	{
		GameObject returnMe = null;
		for (int i = 0; i < enemyPoolCount; i++)
		{
			if (!enemyPool[i].activeInHierarchy)
			{
				returnMe = enemyPool[i];
				break;
			}
		}
		if (returnMe == null) {
			enemyPool[0].SetActive(false);
			returnMe = enemyPool[0];
		}
		return returnMe;
	}


}
