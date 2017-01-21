using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {

	public float playerScore;
	public float scoreModifier;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addScoreFromEnemy(GameObject enemy){
		Debug.Log ("Increased score");
		playerScore += scoreModifier * enemy.gameObject.GetComponent<Dodger> ().scoreValue;

	}

	public void incrementScoreModifier(){
		scoreModifier++;

	}
}
