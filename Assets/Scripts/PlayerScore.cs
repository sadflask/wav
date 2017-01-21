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

	public void addScoreFromEnemey(GameObject enemey){
		Debug.Log ("Increased score");
		playerScore += scoreModifier * enemey.gameObject.GetComponent<Dodger> ().scoreValue;

	}

	public void incrementScoreModifier(){
		scoreModifier++;

	}
}
