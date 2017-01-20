using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnWaves());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator SpawnWaves()
    {
        //Spawn waves up until the number of 
        yield return null;
    }
}
