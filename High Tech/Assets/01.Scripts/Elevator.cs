using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    enum State {stay, up, down}
    State state = State.stay;

    public float min = 2.5f;
    public float max = 7.75f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (state == State.stay && transform.position.y == min)
            {
                state = State.up;
            }
            else if (state == State.stay && transform.position.y == max)
            {
                state = State.down;
            }
        }
    }

    private void Update()
    {
        if (state == State.up && transform.position.y < max)
        {
            gameObject.transform.Translate(0, 0.05f, 0);
        }
        else if (state == State.up && transform.position.y > max - 0.1f)
        {
            gameObject.transform.position = new Vector3(-23.47919f, max, -14.46f);
            state = State.stay;
        }
        else if(state == State.down && transform.position.y > min)
        {
            gameObject.transform.Translate(0, -0.05f, 0);
        }
        else if (state == State.down && transform.position.y < min + 0.1f)
        {
            gameObject.transform.position = new Vector3(-23.47919f, min, -14.46f);
            state = State.stay;
        }
        else
        {
            state = State.stay;
        }
    }
}
