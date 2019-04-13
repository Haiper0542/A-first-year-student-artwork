using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_lifeTime : MonoBehaviour {

    public float LifeTime = 1;

	void OnEnable() {
        Invoke("UnActive", LifeTime);
	}
    void UnActive()
    {
        gameObject.transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }
}
