using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextBoxManager : MonoBehaviour
{

    public GameObject textBox;

    public Text theText;

    public TextAsset textfile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public PlayerInputScript player;

    public bool isActive;

    public bool stopPlayerMovement;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerInputScript>(); 

        if (textfile != null)
        {
            textLines = (textfile.text.Split('\n'));
        }

        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
    }

    void Update()
    {
        theText.text = textLines[currentLine];

        if(currentLine > endAtLine)
        {
            textBox.SetActive(false);
        }
        else
        {
			#region Next Text moved to PlayerInputScript
			//         if(Input.GetKeyDown(KeyCode.Return))
			//         {
			//	Debug.Log("TextBoxManager.Update(): Enter Key pressed");
			//	currentLine += 1;
			//}
			#endregion
		}
		if (isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }

        if(currentLine > endAtLine)
        {
            DisableTextBox();
        }
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;

        if(stopPlayerMovement)
        {
            player.freezeHorizontalMovement = true;
        }
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
        isActive = false;
        
		player.freezeHorizontalMovement = false;
    }

    public void ReloadScript( TextAsset theText)
    {
        if(theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('n'));
        }
    }
}
