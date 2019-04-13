using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Control : MonoBehaviour {

	public int remainingDate = 5;
    public bool day = false;
    public Slider stamina;

    public int knowledge = 0;
    public int health = 0;
    public int honest = 0;


    void Update()
    {
        if (stamina.value <= 1)
        {
            stamina.value = 100;
            remainingDate--;
            day = true;
            if (remainingDate == 0)
            {
                SceneManager.LoadScene("Interview");
            }
        }   
    }
}