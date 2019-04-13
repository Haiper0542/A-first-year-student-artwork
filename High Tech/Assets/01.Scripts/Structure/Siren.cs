using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siren : MonoBehaviour {

    public GameObject[] Red_Light = new GameObject[2];


    void Update()
    {
        Red_Light[0].transform.Rotate(1, 1, 0);
        if (Red_Light[1] != null)
        {
            Red_Light[1].transform.Rotate(0, 1, 1);
        }
    }
}
