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
    
    public float bulletDamage;
    public Text damageText;
    public GameObject shotSpawn;

	public GameObject explosion;
    public bool secondShot;
    private PlayerBullet lastCreatedShot;
    private PlayerBullet lastCreatedShot2;
    public Queue<PlayerBullet> bullets;
    public Queue<PlayerBullet> bullets2;
    public float bulletSpeed;
	public float bulletWaveAmplitude;

	AudioSource[] sources;
	AudioSource beamSound;
	AudioSource powerUpSound;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        bullets = new Queue<PlayerBullet>();
		sources = GetComponents<AudioSource> ();
		beamSound = sources [0];
		powerUpSound = sources [1];
        bullets2 = new Queue<PlayerBullet>();
	}



	public void PowerUpSound() {
		powerUpSound.Play ();
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
		
		float rotation = Mathf.Clamp (rb.velocity.x * 15, -15, 15);
		rb.transform.rotation = Quaternion.Euler (0,-rotation-180,-180);

	}
		
    //Called once per frame
    void Update()
    {

        //modify player bullet spread
        spread = (-bulletWaveAmplitude * (Input.GetAxis("Vertical"))) + bulletSpreadConstant;

        if (Input.GetButton("Fire1") && (Time.time > nextTime))
        {
			if (!beamSound.isPlaying) {
				beamSound.Play();
			}
            nextTime = Time.time + fireRate;
            PlayerBullet b = Instantiate(shot, shotSpawn.transform.position, Quaternion.identity).GetComponent<PlayerBullet>();
            b.player = gameObject;
            b.spread = Mathf.Clamp(spread, 0, 5);
            b.speed = bulletSpeed;
            b.startAmplitude = Mathf.Sin(-5 * Time.time);
            b.lastShot = lastCreatedShot;
            bullets.Enqueue(b);
            lastCreatedShot = b;
            if(secondShot)
            {
                PlayerBullet b2 = Instantiate(shot, shotSpawn.transform.position, Quaternion.identity).GetComponent<PlayerBullet>();
                b2.player = gameObject;
                b2.spread = Mathf.Clamp(spread, 0, 5);
                b2.speed = bulletSpeed;
                b2.startAmplitude = -Mathf.Sin(-5 * Time.time);
                b2.lastShot = lastCreatedShot2;
                bullets2.Enqueue(b2);
                lastCreatedShot2 = b2;
            }
        }
        if (lastCreatedShot)
        {
            if (bullets.Peek() == null)
            {
                bullets.Dequeue();
            }
        }
        if (lastCreatedShot2)
        {
            if (bullets2.Peek() == null)
            {
                bullets2.Dequeue();
            }
        }
        foreach (PlayerBullet b in bullets)
        {
            if (b.spread != spread)
            {
                b.spread = spread;
                b.lineSet = false;
            }
        }
        foreach (PlayerBullet b2 in bullets2)
        {
            if (b2.spread != spread)
            {
                b2.spread = spread;
                b2.lineSet = false;
            }
        }
        
        damageText.text = "Weapon: " + ((int)(bulletDamage * 100)).ToString() + "%";
        
    }

}
