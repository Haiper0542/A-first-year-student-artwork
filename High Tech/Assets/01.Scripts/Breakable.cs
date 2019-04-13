using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

    public int hp = 5;
    public float flash = 0.5f;
    public int time = 4;
    public Material orginally;
    public Material broken;

    public void Update()
    {
        if (hp == 0)
        {
            StartCoroutine("Spark");
            GetComponent<Breakable>().enabled = false;
        }
    }

    public void damage()
    {
        if(hp > 0)
        hp--;
    }

    IEnumerator Spark()
    {
        for (int i = 0; i < time; i++)
        {
            GetComponent<MeshRenderer>().material = orginally;
            yield return new WaitForSeconds(flash);
            GetComponent<MeshRenderer>().material = broken;
            yield return new WaitForSeconds(flash);
        }
        for (int i = 0; i < time * 0.5f; i++)
        {
            GetComponent<MeshRenderer>().material = orginally;
            yield return new WaitForSeconds(flash * 2);
            GetComponent<MeshRenderer>().material = broken;
            yield return new WaitForSeconds(flash * 2);
        }
    }
}
