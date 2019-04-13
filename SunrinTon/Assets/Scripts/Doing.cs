using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doing : MonoBehaviour {

    Control control;

    void Start()
    {
        control = GameObject.Find("Controller").GetComponent<Control>();
    }

    public void doing()
    {
        if (CompareTag("Reading"))
        {
            control.knowledge += 5;
            control.stamina.value -= 30;
        }
        else if (CompareTag("Exercising"))
        {
            control.health += 5;
            control.stamina.value -= 40;
        }
        else if (CompareTag("Playing"))
        {
            control.honest += 5;
            control.stamina.value -= 20;
        }
    }
}