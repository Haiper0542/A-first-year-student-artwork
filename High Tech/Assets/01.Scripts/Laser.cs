using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public int type = 0;

	void Start () {
		if(this.name == "Blue_Laser")
        {
            type = 0;
        }
        else if(this.name == "Red_Laser")
        {
            type = 1;
        }
        Destroy(this.gameObject, 5);
	}

    private void Update()
    {
        transform.Translate(Vector3.forward * 12 * Time.deltaTime);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (type == 0)
            {
                other.SendMessage("knok",transform.rotation * Vector3.one);
            }
            else if (type == 1)
            {
                other.SendMessage("Damage", 5);
            }
        }
    }
}
