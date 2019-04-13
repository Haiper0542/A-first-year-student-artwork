using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCover : MonoBehaviour {

    public Transform Cover;
    public Transform Cover1;
    public Transform Cover2;

    void Start()
    {
        int r = Random.Range(1, 8);
        switch (r)
        {
            case 1:
                Cover.GetComponent<MeshRenderer>().material.color = Color.red;
                Cover1.GetComponent<MeshRenderer>().material.color = Color.red;
                Cover2.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case 2:
                Cover.GetComponent<MeshRenderer>().material.color = Color.blue;
                Cover1.GetComponent<MeshRenderer>().material.color = Color.blue;
                Cover2.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case 3:
                Cover.GetComponent<MeshRenderer>().material.color = Color.gray;
                Cover1.GetComponent<MeshRenderer>().material.color = Color.gray;
                Cover2.GetComponent<MeshRenderer>().material.color = Color.gray;
                break;
            case 4:
                Cover.GetComponent<MeshRenderer>().material.color = Color.green;
                Cover1.GetComponent<MeshRenderer>().material.color = Color.green;
                Cover2.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case 5:
                Cover.GetComponent<MeshRenderer>().material.color = Color.grey;
                Cover1.GetComponent<MeshRenderer>().material.color = Color.grey;
                Cover2.GetComponent<MeshRenderer>().material.color = Color.grey;
                break;
            case 6:
                Cover.GetComponent<MeshRenderer>().material.color = Color.yellow;
                Cover1.GetComponent<MeshRenderer>().material.color = Color.yellow;
                Cover2.GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            case 7:
                Cover.GetComponent<MeshRenderer>().material.color = Color.magenta;
                Cover1.GetComponent<MeshRenderer>().material.color = Color.magenta;
                Cover2.GetComponent<MeshRenderer>().material.color = Color.magenta;
                break;
        }
        int r2 = Random.Range(1, 5);
        switch (r2)
        {
            case 1:
                transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                break;
            case 2:
                transform.localScale = new Vector3(0.7f, 0.85f, 0.7f);
                break;
            case 3:
                transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                break;
        }
    }
}
