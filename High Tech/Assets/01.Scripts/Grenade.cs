using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    public GameObject Lever;
    public GameObject Pin;
    public Transform Explosions;
    public Transform[] Exp = new Transform[10];
    public Transform Smokes;
    public Transform[] Smo = new Transform[50];

    public int Damage = 1;
    public int Power = 600;

    public void Start()
    {
        for (int i = 0; i < Explosions.childCount; i++)
        {
            Exp[i] = Explosions.GetChild(i);
        }
        for (int i = 0; i < Smokes.childCount; i++)
        {
            Smo[i] = Smokes.GetChild(i);
        }
    }

    public void Bang()
    {
        print("!");
        Invoke("Time_bomb", 3);
        InvokeRepeating("Smoke", 0, 0.1f);

        Rigidbody Pin_rb = Pin.GetComponent<Rigidbody>();
        Collider Pin_col = Pin.GetComponent<Collider>();

        Pin_rb.isKinematic = false;
        Pin_col.enabled = true;

        Pin.transform.SetParent(Camera.main.transform);

        Pin_rb.AddForce(Vector3.forward * 50);
        Invoke("Lever_off", 0.2f);
    }

    public void Lever_off()
    {
        Rigidbody Lever_rb = Lever.GetComponent<Rigidbody>();
        Collider Lever_col = Lever.GetComponent<Collider>();

        Lever_rb.isKinematic = false;
        Lever_col.enabled = true;

        Lever.transform.SetParent(Camera.main.transform);
        Lever_rb.AddForce(Vector3.up * 20);
        Lever_rb.AddForce(Vector3.forward * 20);
    }

    public void Smoke()
    {
        int i = 0;
        while (Smo[i].gameObject.activeSelf)
        {
            i += 1;
        }
        Smo[i].gameObject.SetActive(true);
        Smo[i].transform.position = transform.position;
    }

    public void Time_bomb()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, 8);

        foreach (Collider coll in colls)
        {
            Rigidbody rbody = coll.GetComponent<Rigidbody>();
            if (rbody != null)
            {
                rbody.mass = 1.0f;
                rbody.AddExplosionForce(Power, transform.position, 8, 10);
            }
            if (coll.GetComponent<Enemy_move>() != null)
            {
                coll.GetComponent<Enemy_move>().hp -= Damage;
            }
            if (coll.GetComponent<Player_Controller>() != null)
            {
                coll.GetComponent<CharacterController>().enabled = false;
                coll.GetComponent<Player_Controller>().Damage(Damage);
                GameObject.Find("Player").GetComponent<Player_Controller>().CharacterOn();
                print("Damaged!");
            }
        }

        int i = 0;
        while (Exp[i].gameObject.activeSelf)
        {
            i += 1;
        }
        Exp[i].gameObject.SetActive(true);
        Exp[i].transform.position = transform.position;

        Destroy(gameObject);
    }
}
