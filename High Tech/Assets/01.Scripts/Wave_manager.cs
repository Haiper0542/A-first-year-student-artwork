using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_manager : MonoBehaviour
{

    public Transform spawnPoints;
    public Transform Zines;
    public Transform[] point;
    public Transform[] zine;
    public int count = 0;
    public int wave = 0;

    public static Wave_manager instance;

    private void Awake()
    {
        if (Wave_manager.instance == null)
            Wave_manager.instance = this;
    }

    void Start()
    {
        for (int i = 0; i < spawnPoints.childCount; i++)
        {
            point[i] = spawnPoints.GetChild(i);
        }
        for (int i = 0; i < Zines.childCount; i++)
        {
            zine[i] = Zines.GetChild(i);
        }
        InvokeRepeating("Manage", 0.5f, 1);
    }

    public void Manage()
    {
        if (count == 0)
        {
            for (int i = 0; i < (wave + 1) * 0.5f; i++)
            {
                Spawn_zine();
            }
            wave++;
        }
    }

    public void Spawn_zine()
    {
        int i = 0;
        while (zine[i].gameObject.activeSelf)
        {
            i += 1;
        }
        zine[i].gameObject.SetActive(true);
        zine[i].transform.position = point[Random.Range(0, spawnPoints.childCount)].position;
        zine[i].GetComponent<Enemy_move>().hp = 20;
        count++;
    }
}
