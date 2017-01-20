using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
	Rigidbody rb;
	public GameObject player;
	public float speed;
	public float spread;
	private float startTime;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(0, speed, 0);
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		float currentTime = Time.time - startTime;

		rb.position = new Vector3 (player.transform.position.x + spread * Mathf.Sin(currentTime - 5*Time.time), transform.position.y, 0);

		if (rb.position.y > player.GetComponent<PlayerController> ().boundary.ymax || 
			rb.position.y < player.GetComponent<PlayerController> ().boundary.ymin ||
			rb.position.x > player.GetComponent<PlayerController> ().boundary.xmax ||
			rb.position.x < player.GetComponent<PlayerController> ().boundary.xmin) {
			Destroy (gameObject);
		}
	}

}
