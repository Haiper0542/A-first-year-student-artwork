using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCtrl : MonoBehaviour
{
    public Vector2 targetPos;
    public bool isStop = true;

    public float Speed = 10.0f;

    void Update()
    {
        if (!isStop)
            transform.Translate(Quaternion.LookRotation(targetPos) * Vector3.forward * Speed * Time.deltaTime);
        if(!isStop && Vector2.Distance(transform.position,targetPos) < 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
