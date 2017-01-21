using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

	public float playerScore;
	public float scoreModifier;
    public Text scoreText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addScoreFromEnemy(GameObject enemy){
		Debug.Log ("Increased score");
		playerScore += scoreModifier * enemy.gameObject.GetComponent<Dodger> ().scoreValue;
        scoreText.text = "Score: " + playerScore.ToString();

	}

	public void incrementScoreModifier(){
		scoreModifier++;

	}
}
