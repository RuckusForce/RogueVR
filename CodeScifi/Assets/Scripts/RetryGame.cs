using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryGame : MonoBehaviour
{
    public GameObject rCanvas;
	public PauseGame pauseGame;
	AudioSource bgMusic;
	PlayerInputScript playerInputScript;

	public void Awake() {
		pauseGame = GetComponent<PauseGame>();
		playerInputScript = GameObject.Find("Hero2 (1)").GetComponentInChildren<PlayerInputScript>();
	}

	void Update() {
		transform.position = new Vector3
		(transform.position.x,
		0f,
		transform.position.z);

	}

    //Retry pop up and Retry button should be two different methods
    public void Retry()
    {
        //Debug.Log("Retry()");
        if (rCanvas.gameObject.activeInHierarchy == false)
        {			
			if (Time.timeSinceLevelLoad > PlayerPrefs.GetFloat("High Score"))
			{
				Debug.Log(Time.timeSinceLevelLoad + " > " + PlayerPrefs.GetFloat("High Score"));
				PlayerPrefs.SetFloat("High Score", Time.timeSinceLevelLoad);
			}

			//turn down volume, should reset on level reload
			bgMusic = GameObject.Find("PlayerCamera").GetComponent<AudioSource>();
			bgMusic.volume = .2f;

			//PlayerPrefs.SetFloat("High Score", 0f);
			rCanvas.gameObject.SetActive(true);
			//pauseGame.enabled = false;
			//Time.timeScale = 0;//Need to keep Time.timeScale at 1, otherwise the reticle wouldn't work, will freeze Horizontal Movement Instead.
			playerInputScript.FreezeInput();

			//SceneManager.LoadScene("SciFiDemo2"); //that was a no no but figured it out


		}
        else
        {
            rCanvas.gameObject.SetActive(false);
			pauseGame.enabled = true;
			Time.timeScale = 1;
        }
    }

    public void RestartGame(bool Restart)//Is a bool here required? Why not just only call it RestartGame when you need it to come up?
    {
        if (Restart) //This is what was missing, setting up the OnClickButton so when the Retry or Game over menu pops up, the user can click on the Restart button
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("ScifiDemo2");
        }
    }

	/// <summary>
	/// Overload of RestartGame function with no params
	/// </summary>
	public void RestartGame()//Is a bool here required? Why not just only call it RestartGame when you need it to come up?
	{
		//Time.timeScale = 1;
		playerInputScript.UnfreezeInput();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void GiveUp(bool gUp) //Set up the OnClickButton so when the user falls off to death, user can click on the Quit button to go back to the Main Menu
    {
        if (gUp)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }

	///<summary>
	/// Overload of GiveUp function with no params
	/// </summary>
	public void GiveUp() {
		//Time.timeScale = 1;
		playerInputScript.UnfreezeInput();
		SceneManager.LoadScene("MainMenu");
	}

}
