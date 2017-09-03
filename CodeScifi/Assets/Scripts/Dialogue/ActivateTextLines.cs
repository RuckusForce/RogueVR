﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextLines : MonoBehaviour
{
    public TextAsset theText;

    public int startLine;
    public int endLine;

    public TextBoxManager theTextBox;

    public bool destroyWhenActivated;
    // Use this for initialization
	void Start ()
    {
        theTextBox = FindObjectOfType<TextBoxManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;

            if (endLine!= 0)
            {
                theTextBox.endAtLine = endLine;
            }
            else
            {
                endLine = theTextBox.endAtLine;
            }

            theTextBox.EnableTextBox();

            if(destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
    }
}
