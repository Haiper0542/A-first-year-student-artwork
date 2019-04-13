using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float Speed = 5.0f;
    public GameObject shot_hole;

    void Start () {
		Destroy (this.gameObject, 50);
	}

	void Update () {
		transform.Translate(Vector3.right * Speed * Time.deltaTime);
	}
    public void OnCollisionEnter(Collision collision)
    {
        Instantiate(shot_hole, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
