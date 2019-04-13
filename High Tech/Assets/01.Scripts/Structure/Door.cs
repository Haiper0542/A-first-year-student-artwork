using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public AnimationClip open;
    private Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.clip = open;
            anim.Play();
        }
    }

    public void Check(int a)
    {
        Debug.Log("Open" + a);
    }
}
