using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public Transform canvas;
	
	
	// Update is called once per frame
	void Update ()
    {
  //      if (Input.GetKeyDown(KeyCode.KeypadEnter))
  //      {
		//	Pause();
		//}
    }

    public void Pause()
    {
        //if (canvas.gameObject.activeInHierarchy == false)
        //{
            //canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        //}
        //else
        //{
            //canvas.gameObject.SetActive(false);
            //Time.timeScale = 1;
        //}
    }

	public void Unpause() {
		//canvas.gameObject.SetActive(false);
		Time.timeScale = 1;
	}
}
