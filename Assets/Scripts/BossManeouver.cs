using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManeouver : MonoBehaviour {

    Rigidbody rb;
    float startTime;
    float maneouverStartTime;
    Vector3 startPosition;
    int firePattern;
    
    public float cooldown;
    public float nextFire;
    public float nextChange;
    public float patternChange;
    public GameObject[] spawns;
    public GameObject enemyBullet;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, -6f, 0);
        startTime = Time.time;
        maneouverStartTime = 0;
	}
	
    void FixedUpdate()
    {
        if (!(GetComponent<Health>().currentHealth < 0))
        {
            return;
        }
        rb.velocity = new Vector3(0, 4, 0);
    }
	// Update is called once per frame
	void Update () {
        if (GetComponent<Health>().currentHealth < 0)
        {
            return;
        }
        else
        {
            float currentTime = Time.time - startTime;
            float timeSinceManeouverStart = Time.time - maneouverStartTime;
            if (currentTime > 1.5)
            {
                if (maneouverStartTime == 0)
                {
                    maneouverStartTime = Time.time;
                    nextChange = Time.time + patternChange;
                }
                float newx = Mathf.Sin(timeSinceManeouverStart / 5) * 3;
                float newy = Mathf.Sin(timeSinceManeouverStart) / 3;
                rb.position = new Vector3(newx, newy) + startPosition;
                //Wait for next fire
                if (nextFire < Time.time)
                {
                    //Fire
                    for (int i = 0; i < spawns.Length; i++)
                    {

                        if (firePattern % 3 == 0)
                        {
                            if (i < 2)
                            {
                                Instantiate(enemyBullet, spawns[i].transform.position, Quaternion.Euler(0, 0, 90));
                            }
                        }
                        else if (firePattern % 3 == 1)
                        {
                            if (i < 4)
                            {
                                Instantiate(enemyBullet, spawns[i].transform.position, Quaternion.Euler(0, 0, 90));
                            }
                        }
                        else if (firePattern % 3 == 2)
                        {
                            Instantiate(enemyBullet, spawns[i].transform.position, Quaternion.Euler(0, 0, 90));
                        }
                    }
                    nextFire = Time.time + cooldown;
                }
                if (Time.time > nextChange)
                {
                    firePattern++;
                    nextChange += patternChange;
                }

            }
            else if (currentTime > 0.333)
            {
                rb.velocity = Vector3.zero;
                startPosition = transform.position;
            }
        }
	}
}
