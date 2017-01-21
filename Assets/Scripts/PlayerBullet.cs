using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
	public Rigidbody rb;
	public GameObject player;
	public float speed;
	public float spread;
	public float startAmplitude;
	private float startTime;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		startTime = Time.time;
		rb.velocity = new Vector3 (0, speed, 0);
	}

	// Update is called once per frame
	void Update () {
		float currentTime = Time.time - startTime;
		float fraction = Mathf.Clamp01 (currentTime * 2);

		rb.position = new Vector3 (player.transform.position.x + (startAmplitude * spread) * fraction, transform.position.y, 0);
	}

}
