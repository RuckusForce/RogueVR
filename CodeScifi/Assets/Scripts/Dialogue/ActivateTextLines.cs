using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextLines : MonoBehaviour
{
    public TextAsset theTextAsset;

    public int startLine;
    public int endLine;

    public TextBoxManager theTextBox;
	public GameObject targetTextUI;//GAA: Set in Editor, should be inactive

    public bool destroyWhenActivated;
	bool activatedOnce;
	public bool shouldEndWhenNoMoreLines;//GAA: Gives room for other events to finish and unfreeze the player

	// Use this for initialization
	void Awake ()
    {
        theTextBox = FindObjectOfType<TextBoxManager>();
		activatedOnce = false;
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
		//Debug.Log("TextZone triggered by: " + other.tag);
		if (other.CompareTag("Player") && !activatedOnce)
        {
			activatedOnce = true;
			//theTextBox.LoadScript(theText);
			theTextBox.StartText(targetTextUI, theTextAsset, startLine, endLine, shouldEndWhenNoMoreLines);//targetTextUI would be for later when we want to target other text boxes
			#region Moved to TextBoxManager.StartText()
			//theTextBox.currentLine = startLine;
			//if (endLine!= 0)
			//         {
			//             theTextBox.endAtLine = endLine;
			//         }
			//         else
			//         {
			//             endLine = theTextBox.endAtLine;
			//         }

			//         theTextBox.EnableTextBox();
			#endregion

			if (destroyWhenActivated)//the booleans are a bit ambiguous
            {
				//Destroy(gameObject);
				//gameObject.SetActive(false);
            }
        }
    }
}
