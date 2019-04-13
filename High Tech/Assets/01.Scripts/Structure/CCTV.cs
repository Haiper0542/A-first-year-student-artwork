using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour {

    public float rotation = 45f;
    bool direct = false;
    int sec = 0;

	void Start ()
    {
        Vector3 rot = new Vector3(0, 44, 0);
        transform.Rotate(rot);
    }


    void Update()
    {
        sec++;
        if(sec % 180 == 0 && sec != 0)
        {
            direct = !direct;
            sec = -170;
        }

        if (sec >= 0)
        {
            if (direct)
                rotation += 0.5f;
            else
                rotation -= 0.5f;
        }
        Vector3 rot = new Vector3(20, rotation, 0);
        transform.rotation = Quaternion.Euler(rot);
    }
}
