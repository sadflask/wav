using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary
{
    public float xmin, xmax, ymin, ymax;
}  

public class PlayerController : MonoBehaviour {
    public GameObject shot;
    public float speed;
    public Boundary boundary;
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
               Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xmin, boundary.xmax),
               Mathf.Clamp(GetComponent<Rigidbody>().position.y, boundary.ymin, boundary.ymax),
               0
           );
    }

    //Called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && (Time.time > nextTime))
        {
            nextTime = Time.time + fireRate;
			GameObject b = Instantiate(shot, rb.position + new Vector3(spread * Mathf.Sin(-5*Time.time),5,0), Quaternion.identity);
			b.GetComponent<PlayerBullet>().player = gameObject;

        }
    }
}
