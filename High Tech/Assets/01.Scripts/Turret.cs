using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public GameObject Laser;
    public GameObject Head;
    public GameObject Muzzle;
    public float shot_delay = 0.5f;
    public float time = 0;

    public int shotType = 0; // 0:rot, 1:targeting
    
	void Update ()
    {
        if (shotType == 0)
        {
            Head.transform.Rotate(0, 0, 1);
        }
        else if (shotType == 1)
        {
            //타겟팅
        }
        if (time < shot_delay)
        {
            time += 0.01f;
        }
        else
        {
            Instantiate(Laser, Muzzle.transform.position, Quaternion.Euler(0, Head.transform.eulerAngles.y, 0));
            time = 0;
        }
    }
}
