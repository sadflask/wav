﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
	public void Quit() {
		//Quit the game
		Application.Quit();
	}
	public void Load() {
		//Start
		SceneManager.LoadScene("Main");
	}
}
