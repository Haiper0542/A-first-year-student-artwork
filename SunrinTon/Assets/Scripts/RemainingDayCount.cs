using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainingDayCount : MonoBehaviour {

    Control control;
    Text text;

	void Start ()
    {
        control = GameObject.Find("Controller").GetComponent<Control>();
        text = GetComponent<Text>();
	}

    void Update()
    {
        if(control.day)
        {
            text.text = "" + control.remainingDate;
        }
    }
}
