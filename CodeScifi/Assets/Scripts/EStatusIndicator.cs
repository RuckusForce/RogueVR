using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EStatusIndicator : MonoBehaviour
{
    [SerializeField]//allows them to show these variables on the Editor even though it's private
    private RectTransform eHealthBarRect;
    [SerializeField]
    private Text eHealthText;

    // Use this for initialization
    void Start()
    {
        if (eHealthBarRect == null)
        {
            Debug.Log("STATUS INDICATOR: No health bar object referenced!");
        }
        if (eHealthText == null)
        {
            Debug.Log("STATUS INDICATOR: No health text object referenced!");
        }
    }

    public void SetHealth(int _cur, int _max)
    {
        float _value = (float)_cur / _max;
        eHealthBarRect.localScale = new Vector3(_value, eHealthBarRect.localScale.y, eHealthBarRect.localScale.z);
        eHealthText.text = _cur + "/" + _max + " HP";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
