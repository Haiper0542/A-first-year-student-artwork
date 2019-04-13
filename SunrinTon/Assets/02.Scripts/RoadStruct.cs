using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadStruct : MonoBehaviour
{

    public Transform[] LinkedRoad = new Transform[3];
    public int Linkedcount = 3;

    public Vector2 RandomRoad()
    {
        int r = Random.Range(0, Linkedcount);
        return LinkedRoad[r].position;
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, LinkedRoad[0].position, Color.red);
    }

}
