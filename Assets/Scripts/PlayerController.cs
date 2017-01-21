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
	public float bulletSpreadConstant;
    Rigidbody rb;
    public Queue<PlayerBullet> bullets;
    public PlayerBullet[] bulletArray;

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
			b.startAmplitude = Mathf.Sin(-5*Time.time);
            bullets.Enqueue(b);

        }

        if(bullets.Peek() == null)
        {
            //bullets.Dequeue();
        }
        bulletArray = bullets.ToArray();
        for(int i=0; i<bulletArray.Length; i++)
        {
            if (i == 0)
            {
                Debug.Log("First");
            }
            else
            {
                if (bulletArray[i] != null && bulletArray[i - 1] != null)
                {
                    Vector3 vectorBetween = bulletArray[i].transform.position - bulletArray[i - 1].transform.position;
                    Vector3 centre = bulletArray[i-1].transform.position + (vectorBetween) / 2;
                    Debug.Log(centre);
                    if (vectorBetween.magnitude < 2)
                    {
                        bulletArray[i].transform.GetChild(0).position = centre;
                        bulletArray[i].transform.GetChild(0).LookAt(bulletArray[i - 1].transform);
                        bulletArray[i].transform.GetChild(0).localScale = new Vector3(0.1f, 0.1f, vectorBetween.magnitude*4); 
                    }
                }
            }
        }
    }
}
