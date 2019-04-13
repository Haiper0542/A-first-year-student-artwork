using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCtrl : MonoBehaviour {

    public float Speed = 5;

	void Start () {
        transform.localScale = Vector3.one * Random.Range(0.5f, 1f);
	}
    private void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
        if (transform.position.x > 5)
            Destroy(gameObject);
    }
}
