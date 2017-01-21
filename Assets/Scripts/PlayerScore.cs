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

	public void addScore(float score){
		Debug.Log ("Increased score");
        playerScore += scoreModifier * score;
        scoreText.text = "Score: " + playerScore.ToString();

	}

	public void incrementScoreModifier(){
		scoreModifier++;

	}
}
