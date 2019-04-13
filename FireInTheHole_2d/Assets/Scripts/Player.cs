using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    FireThrower fireThrower;
    Hook hook;
    public int RotateSpeed = 1;
    float ChRotate = 1;
    int flare = 0;

    void Start()
    {
        fireThrower = GetComponent<FireThrower>();
        hook = GetComponent<Hook>();
    }
    void Update () {
        float Rotate = 0.0f;

        
        if (Input.GetKey("a"))
        {

            Rotate += 2.0f;
            ChRotate = ChRotate - RotateSpeed;
            if (ChRotate == 0)
            {
                ChRotate = 8;
            }
        }
        if (Input.GetKey("d"))
        {
            Rotate -= 2.0f;
            ChRotate = ChRotate + RotateSpeed;
            if (ChRotate == 9)
            {
                ChRotate = 1;
            }
        }
        Vector3 FireRotate = new Vector3(0,0, Rotate);
        transform.Rotate(FireRotate);

        if (Input.GetKey("e"))
        {
            flare = 20 + Random.Range(-2, 4);
        }
        if (flare>0)
        {
            flare--;
            fireThrower.Shoot();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            hook.Shoot();
        }

    }
}
