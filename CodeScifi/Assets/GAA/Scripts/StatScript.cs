using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//no methods, so no need to be a MonoBehaviour. But since not a Monobehaviour, need to be made Serializable

//This class is also not dragged into a gameobject like other scripts, instead it instantiates itself within another script

//The AttributesScript should be instantiating these

[Serializable]
public class StatScript {

    private BarScript bar;

    public BarScript Bar {
        get {
            return bar;
        }
        set {
            this.bar = value;
        }
    }

    [SerializeField]
    private float currentVal;//current value of this stat

    [SerializeField]
    private float maxVal;

    public float CurrentVal {
        get {
            return this.currentVal;
        }
        set {
            this.currentVal = Mathf.Clamp(value, 0, MaxVal);
            //Debug.Log("StatScript: " + bar.ToString());
            this.bar.Value = Mathf.Clamp(value, 0, MaxVal);//calls the Value property of BarScript
        }
    }

    public float MaxVal
    {
        get
        {
            return this.maxVal;
        }

        set
        {
            this.maxVal = value;
            bar.MaxValue = value;
        }
    }
    /* The initial setting of the MaxVal and CurrentVal allows the StatScript.MaxVal to trigger the BarScript.MaxValue, which sets the BarScript.Value, which updates the fillAmount properly for BarScript.HandleBar. 
    */
    //public void Initialize(BarScript barUI) {
    //    bar = barUI;
    //    this.MaxVal = maxVal;
    //    this.CurrentVal = maxVal;
    //}
}
