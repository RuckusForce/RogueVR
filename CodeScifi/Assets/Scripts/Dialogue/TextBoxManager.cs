using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextBoxManager : MonoBehaviour
{
    public GameObject textBox;//GAA: Will now be set by the ActivateTextLines script

    public Text theText;

	public TextAsset textfile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public PlayerInputScript playerInputScript;

    public bool isActive;

    public bool stopPlayerMovement;

	bool noMoreLines;
	bool endWithNoMoreLines;

	// Use this for initialization
	void Awake()
    {
		playerInputScript = FindObjectOfType<PlayerInputScript>(); 
        if (textfile != null)
        {
            textLines = (textfile.text.Split('\n'));
        }
        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
		noMoreLines = false;
		endWithNoMoreLines = true;
	}

    void Update()
    {
        theText.text = textLines[currentLine];

        if(currentLine > endAtLine && !noMoreLines)
        {
			//textBox.SetActive(false);
			noMoreLines = true;
			if (endWithNoMoreLines) {
				DisableTextBox();
			}

		}

		#region Next Text moved to PlayerInputScript
		//else
		//{
		//         if(Input.GetKeyDown(KeyCode.Return))
		//         {
		//	Debug.Log("TextBoxManager.Update(): Enter Key pressed");
		//	currentLine += 1;
		//}			
		//}
		#endregion

		#region Enables the initial text box. We should probably just use ActivateTextLines for every Text event instead
		//if (isActive)
  //      {
  //          EnableTextBox();
  //      }
		//else
		//{
		//    DisableTextBox();
		//}
		#endregion

		//GAA: Below code is duplicated from earlier in Update();
		//if(currentLine > endAtLine)
		//{
		//    DisableTextBox();
		//}
	}

	public void EnableTextBox()
    {
        textBox.SetActive(true);
		noMoreLines = false;
        isActive = true;//can't we just do a textBox.isActive() check?
		playerInputScript.FreezeInput();

    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
        isActive = false;
		playerInputScript.UnfreezeInput();
    }

    public void ReloadScript(TextAsset theText)
    {
        if(theText != null)
        {
			Debug.Log("theText is not null");
			//textLines = new string[1];
			//textLines = theText.text.Split('n');
			textLines = textfile.text.Split('\n');
		}
    }

	public void StartText(GameObject newTextBox, TextAsset theText, int start, int end, bool shouldEndWhenNoMoreLines) {
		if (theText != null) {			
			endWithNoMoreLines = shouldEndWhenNoMoreLines;//allows other events to unfreeze the player
			//ReloadScript(theText);
			textLines = theText.text.Split('\n');
			textBox = newTextBox;//not functional yet, keep newTextBox as the original textbox
			currentLine = start;
			endAtLine = end;
			EnableTextBox();
		}
	}

	public void EndText() {
		DisableTextBox();
	}
}
