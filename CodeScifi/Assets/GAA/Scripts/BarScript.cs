﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

    [SerializeField]//allows private vars to be visible in Inspector
    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image content;

    [SerializeField]
    private Text valueText;

    [SerializeField]
    private Color fullColor;

    [SerializeField]
    private Color lowColor;

    [SerializeField]
    private bool lerpColors;

    public float temp;

    public float MaxValue { get; set; }

    public float Value {//property - used by StatScript
        set {
            //Debug.Log("BarScript: " + valueText);
            string[] tmp = valueText.text.Split(':');
            valueText.text = tmp[0] + ": " + value;//Why even use an array?
            fillAmount = Map(value, 0, MaxValue, 0, 1);//fillAmount is getting changed here instead of in "Update"
            //uses the currentVal from StatScript
        }
    }

	// Use this for initialization
	void Awake () {
        content = GetComponentsInChildren<Image>()[2];//0: HealthBar image, 1: Mask image, 2: RadialContent image
        valueText = GetComponentsInChildren<Text>()[0];//0: Text text
        //Debug.Log("BarScript: Awake(): " + valueText);
        lerpSpeed = 10f;

        if (lerpColors)
        {
            content.color = fullColor;
        }
    }

    // Update is called once per frame
    void Update () {
        HandleBar();
	}

    private void HandleBar() {
        if (fillAmount != content.fillAmount)
        {
            //Debug.Log("HandleBar(): " + fillAmount + ":" + content.fillAmount);
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }

        if (lerpColors)
        {
            content.color = Color.Lerp(lowColor, fullColor, fillAmount);
        }
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax) {
        //Debug.Log(value + ":" + inMin + " " + outMax + ":" + outMin + " " + inMax + ":" + inMin + " " + outMin);
        temp = (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;// health * smallestUnitOfHealth + readjust with outMin: (80 - 0) * (1-0) / (100 - 0) + 0
        return temp;
        //actualValue * scalingFactor + scalingMin
    }

}
