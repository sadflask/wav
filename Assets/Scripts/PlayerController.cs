using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject shot;
    public float speed;
    public Transform boundary;
    public float fireRate;
	public float spread;
    private float nextTime;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}


    //Once per frame for physics
    void FixedUpdate () {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal"),0,0) * speed;
        rb.position = new Vector3
           (
               Mathf.Clamp(GetComponent<Rigidbody>().position.x, -boundary.localScale.x / 2, boundary.localScale.x / 2),
               Mathf.Clamp(GetComponent<Rigidbody>().position.y, -boundary.localScale.y / 2, boundary.localScale.y / 2),
               0
           );
    }

    //Called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && (Time.time > nextTime))
        {
            nextTime = Time.time + fireRate;
			PlayerBullet b = Instantiate(shot, rb.position + new Vector3(spread * Mathf.Sin(-5*Time.time),0.5f,0), Quaternion.identity).GetComponent<PlayerBullet>();
			b.player = gameObject;
            b.spread = spread;

        }
    }
}
