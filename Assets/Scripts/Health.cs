using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public float maxHealth;
	public float currentHealth;
    public float dropChance;
	public GameObject explosion;

	//power up to spawn when out of health
	public GameObject powerUp;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;	
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0) {
			Instantiate (explosion, transform.position, transform.rotation);
			if (GameObject.FindGameObjectWithTag ("Player") != null) {
				if (gameObject.CompareTag ("Enemy")) {
                    int drop = (int)Random.Range(1,dropChance);
                    if (drop == 1)
                    {
                        Instantiate(powerUp, gameObject.GetComponent<Rigidbody>().position, Quaternion.identity);
                    }
				}
			}
			if(gameObject.CompareTag("Player")) {
				FindObjectOfType<GameController> ().PlayExplosion ();
			}
			Debug.Log ("Destroyed " + gameObject.name + " due to lack of health");
			Destroy (gameObject);
		}
	}

	public void takeDamage(float damage){
		currentHealth -= damage;
	}

	public void gainHealth(float healing){
		currentHealth += healing;
	}

}
