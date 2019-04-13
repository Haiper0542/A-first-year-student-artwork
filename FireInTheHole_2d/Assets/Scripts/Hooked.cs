using UnityEngine;
using System.Collections;

public class Hooked : MonoBehaviour {

    public LayerMask collisionMask;
    bool col = false;
    public Thrown_hook myThrown_hook;
    float rotate;

    private void Start()
    {
        //rotate = a.GetComponent<Thrown_hook>().transform.rotation.z;
    }
    void Update()
    {
        //float b = a.GetComponent<Thrown_hook>().transform.position;
        //print(b);
        float moveDistance = -5.01f * Time.deltaTime;
        transform.Translate(Vector3.up * moveDistance);
        if (col)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Hole"))
        {
            col = true;
        }
    }
}
