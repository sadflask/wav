using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public GameObject shot;
    public float speed;
    public Transform boundary;
    public float fireRate;
	public float spread;
    private float nextTime;
	public float bulletSpreadConstant;
    Rigidbody rb;
    public Queue<PlayerBullet> bullets;
	public float bulletDamage;
    public Text damageText;

    private PlayerBullet lastCreatedShot;
	public float bulletSpeed;
	public float bulletWaveAmplitude;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        bullets = new Queue<PlayerBullet>();
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
        //modify player bullet spread
        spread = (-bulletWaveAmplitude * (Input.GetAxis("Vertical"))) + bulletSpreadConstant;

        if (Input.GetButton("Fire1") && (Time.time > nextTime))
        {
            nextTime = Time.time + fireRate;
            PlayerBullet b = Instantiate(shot, rb.position, Quaternion.identity).GetComponent<PlayerBullet>();
            b.player = gameObject;
            b.spread = Mathf.Clamp(spread, 0, 5);
            b.speed = bulletSpeed;
            b.startAmplitude = Mathf.Sin(-5 * Time.time);
            b.lastShot = lastCreatedShot;
            bullets.Enqueue(b);
            lastCreatedShot = b;
        }
        if (lastCreatedShot)
        {
            if (bullets.Peek() == null)
            {
                bullets.Dequeue();
            }
        }
        damageText.text = "Weapon: " + ((int)bulletDamage * 100).ToString() + "%";
        foreach(PlayerBullet b in bullets)
        {
            if (b.spread!=spread)
            {
                b.spread = spread;
                b.lineSet = false;
            }
        }
    }
}
