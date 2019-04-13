using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Wall")
        {
            transform.SetParent(GameObject.Find("Main_room").transform);
            Destroy_();
        }
    }

    public void Destroy_()
    {
        Destroy(this.gameObject, 2.2f);
    }
}
