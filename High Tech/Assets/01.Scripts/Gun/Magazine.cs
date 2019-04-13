using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour {

    public GameObject room;

    void Start()
    {
        Destroy(this.gameObject, 2.2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        transform.SetParent(GameObject.Find("Main_room").transform);
    }
}
