using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMove : MonoBehaviour {

	public Rigidbody rb;
	public float fallSpeed;
	public string powerUpType;

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
			PlayerController pc = other.gameObject.GetComponent<PlayerController> ();
			pc.PowerUpSound();
			if (powerUpType == "Damage") {
				Debug.Log ("Player collected a PowerUp!");
				pc.UpdatePower(pc.bulletDamage + 0.2f);
				Destroy(gameObject);
			}
			if (powerUpType == "Movement") {
				Debug.Log ("Player collected a PowerUp!");
				pc.speed += 0.5f;
				Destroy(gameObject);
			}
            if (powerUpType == "Laser")
            {
                Debug.Log("Player collected a PowerUp!");
                pc.secondShot = true;
                Destroy(gameObject);
            }
        }
	}
}
