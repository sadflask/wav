using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

    public float life;
    private float start;

	// Use this for initialization
	void Start () {
        start = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - start > life)
        {
            Destroy(gameObject);
        }
	}
}
