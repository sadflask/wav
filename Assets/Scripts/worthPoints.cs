using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worthPoints : MonoBehaviour {

	//ammount to increase score by when this enemy is defeated
	public float scoreValue;
    public GameObject explosion;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Bullet")) {
			Debug.Log ("Hit");
			//take damage based on the player's damage level
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			gameObject.GetComponent<Health>().takeDamage (player.GetComponent<PlayerController>().bulletDamage);

			//remove the buller that hit the enemy
			Destroy (other.gameObject);

			//enemy dies
			if (gameObject.GetComponent<Health> ().currentHealth <= 0) {
				//increase player's score
				Debug.Log("Player score increased");
                Instantiate(explosion, transform.position, transform.rotation);
				player.GetComponent<PlayerScore> ().addScore (scoreValue);

			}
		}
	}
}
