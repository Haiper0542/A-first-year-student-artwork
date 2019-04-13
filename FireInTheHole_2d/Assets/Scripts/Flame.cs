using UnityEngine;
using System.Collections;

public class Flame : MonoBehaviour {

    public LayerMask collisionMask;
    float Speed = 1.75f;

    public void setSpeed(float newSpeed)
    {
        Vector3 Rotate = new Vector3(0, 0, Random.Range(-6.0f, 6.0f));
        transform.Rotate(Rotate);
        Destroy(this.gameObject, 1.3f);
    }
    void Update()
    {
        float moveDistance = Speed * Time.deltaTime;
        transform.Translate(Vector3.up * moveDistance);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Enemy"))
        {
            Destroy(collision.gameObject,0.2f);
        }
    }
}