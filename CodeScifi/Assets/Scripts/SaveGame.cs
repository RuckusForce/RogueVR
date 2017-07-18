using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    public Transform Player;

    public void Awake()
    {
        
    }

    public void SaveGameSettings(bool Quit)
    {
        PlayerPrefs.SetFloat("x", Player.position.x);
        PlayerPrefs.SetFloat("y", Player.position.y);
        PlayerPrefs.SetFloat("z", Player.position.z);
        PlayerPrefs.SetFloat("Cam_y", Player.eulerAngles.y);

		if (Quit)
        {
			if (Time.timeSinceLevelLoad > PlayerPrefs.GetFloat("High Score"))
			{
				Debug.Log(Time.timeSinceLevelLoad + " > " + PlayerPrefs.GetFloat("High Score"));
				PlayerPrefs.SetFloat("High Score", Time.timeSinceLevelLoad);
			}
			Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
