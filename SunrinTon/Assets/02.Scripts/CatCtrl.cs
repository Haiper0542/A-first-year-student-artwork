using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatCtrl : MonoBehaviour
{
    public Vector3 targetPos;
    public float Speed = 2.0f;
    Animator animator;

    public Transform Roads;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Road"))
        {
            targetPos = collision.GetComponent<RoadStruct>().RandomRoad();
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        Roads = GameObject.Find("Roads").transform;
    }

    private void Start()
    {
        int r = Random.Range(0, Roads.childCount);
        transform.position = new Vector3(Roads.GetChild(r).position.x, Roads.GetChild(r).position.y, -5);

        Speed = Random.Range(0.4f, 0.5f);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);

        Vector2 Vec = (targetPos - transform.position).normalized;
        float rot = Mathf.Atan2(Vec.x, Vec.y) * Mathf.Rad2Deg;

        if (rot < 0)
            rot += 360;
        if (rot > 360)
            rot -= 360;

        if (rot > 45 && rot <= 135)
        {
            animator.SetBool("isFront", false);
            animator.SetBool("isRight", true);
            animator.SetBool("isBack", false);
            animator.SetBool("isLeft", false);
        }
        else if (rot > 135 && rot <= 225)
        {
            animator.SetBool("isFront", true);
            animator.SetBool("isRight", false);
            animator.SetBool("isBack", false);
            animator.SetBool("isLeft", false);
        }
        else if (rot > 225 && rot <= 315)
        {
            animator.SetBool("isFront", false);
            animator.SetBool("isRight", false);
            animator.SetBool("isBack", false);
            animator.SetBool("isLeft", true);
        }
        else
        {
            animator.SetBool("isFront", false);
            animator.SetBool("isRight", false);
            animator.SetBool("isBack", true);
            animator.SetBool("isLeft", false);
        }
    }

}