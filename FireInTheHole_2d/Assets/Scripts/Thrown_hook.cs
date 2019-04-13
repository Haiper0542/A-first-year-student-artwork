using UnityEngine;
using System.Collections;

public class Thrown_hook : MonoBehaviour {

    public LayerMask collisionMask;
    float Speed = 5f;
    public Hooked hooked;
    public Transform muzzle;
    public float hookrotate;
    GameObject ene;

    public void setSpeed(float newSpeed)
    {
        hookrotate = transform.eulerAngles.z;
        transform.Rotate(0, 0, 180);
    }
    void Update()
    {
        float moveDistance = Speed * Time.deltaTime;
        transform.Translate(Vector3.down * moveDistance);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Enemy"))
        {
            StartCoroutine(Wait1(0.03f));
            StartCoroutine(Wait2(0.1f));
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == ("Enemy"))
        {
            ene = collision.gameObject;
            collision.transform.position = transform.position;
        }
        if (collision.tag == ("Hole") && Speed == -5f)
        {
            Destroy(ene);
            Destroy(gameObject);
        }
    }

    IEnumerator Wait1(float time)
    {
        yield return new WaitForSeconds(time);
        Speed = 0f;
        transform.Rotate(0, 0, 0);
    }
    IEnumerator Wait2(float time)
    {
        yield return new WaitForSeconds(time);
        Speed = -5f;
    }
}