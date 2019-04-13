using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_move : MonoBehaviour {

    enum State { moving, staying, chasing };
    public Transform[] point;
    public Transform points;

    public GameObject Exp;

    public int arr_size;
    public int now_point = 0;

    public int hp = 20;

    public NavMeshAgent Navi;

    public static Enemy_move instance;

    private void Awake()
    { 
        if (Enemy_move.instance == null)
            Enemy_move.instance = this;
    }

    void Start()
    {
        for(int i = 0;i< points.childCount; i++)
        {
            point[i] = points.GetChild(i);
        }

        Navi = GetComponent<NavMeshAgent>();

        arr_size = points.childCount;
        InvokeRepeating("Next_point", 0, 8f);
        InvokeRepeating("Upload", 0, 0.1f);
    }

    void Next_point()
    {
        now_point = Random.Range(0, arr_size);
    }
    void Upload()
    {
        if(hp > 0)
        {
            Navi.SetDestination(point[now_point].position);
        }
        else if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            Wave_manager.instance.count--;
            Instantiate(Exp, transform.position, Quaternion.identity);
        }
    }

    public void dmg(int i)
    {
        hp -= i;
        print(hp);
    }
}
