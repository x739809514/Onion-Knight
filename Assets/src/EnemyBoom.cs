using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoom : MonoBehaviour {
    public float timeToDestory;

	void Start () {
        Destroy(gameObject, timeToDestory);
	}
	

	void Update () {
		
	}
}
