using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMove : MonoBehaviour {

	public Rigidbody rb;
	public float fallSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3 (0, -fallSpeed, 0);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//when the player picks up a power up
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			Debug.Log ("Player collected a PowerUp!");
			PlayerController pc = other.gameObject.GetComponent<PlayerController> ();
			pc.bulletDamage += 10;
			//pc.bulletSpeed++;
			//pc.fireRate /= 2;
			//pc.speed++;
			//pc.bulletSpeed++;

		}
	}
}
