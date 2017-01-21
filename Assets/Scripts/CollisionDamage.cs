using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour {
	public float collisionDamage;
	public bool destroyOnCollison;

	//if the player gets hit by an enemy the player takes damage
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			Debug.Log ("Player Hit in collision");
			other.gameObject.GetComponent<Health>().takeDamage (collisionDamage);

			if (destroyOnCollison) {
				Destroy (gameObject);
			}
		}
	}
}
