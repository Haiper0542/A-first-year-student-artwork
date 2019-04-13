using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject Enemy;
    Vector3 pos = new Vector3(0, 4, 0);

	void Start () {
        Instantiate(Enemy, pos, Quaternion.identity);
	}
	
	void Update () {
	
	}
}