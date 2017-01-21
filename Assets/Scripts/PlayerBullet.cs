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
    private bool lineSet = false;
    public PlayerBullet lastShot;

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
        if(!lineSet)
        {
            if (lastShot)
            {
                Vector3 vectorBetween = lastShot.transform.position - transform.position;
                Vector3 centre = transform.position + (vectorBetween) / 2;
                if (vectorBetween.magnitude < 2.5)
                {
                    transform.GetChild(0).LookAt(lastShot.transform);
                    transform.GetChild(0).position = centre;
                    transform.GetChild(0).localScale = new Vector3(0.2f, 0.2f, vectorBetween.magnitude * 8);
                }
            }
            if (currentTime > 1)
            {
                lineSet = true;
            }
        }
        rb.position = new Vector3(player.transform.position.x + (startAmplitude * spread) * fraction, transform.position.y, 0);
	}


}
