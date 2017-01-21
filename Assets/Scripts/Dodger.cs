using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodger : MonoBehaviour {
    public float dodgeRate;
    public float dodgeMagnitude;
    public float startWait;
    public float maneouverTime;

	//damage this object will do when it collides with the player
	public float collisionDamage;

	//ammount to increase score by when this enemy is defeated
	public float scoreValue;

    Rigidbody rb;
    float nextTime;
    public GameController gc;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        gc = FindObjectOfType<GameController>();
        StartCoroutine(Dodge());
	}
	
	// Update is called once per frame
	IEnumerator Dodge () {
        while (gameObject)
        {
            yield return new WaitForSeconds(startWait);
            if (nextTime < Time.time)
            {
                int direction = Random.Range(0, 2);
                if (direction == 0)
                {
                    direction = -1;
                }

                float toDodge = Random.Range(10, dodgeMagnitude) * direction;

                //Check if leaving screen, if so change direction
                if (rb.position.x + toDodge * maneouverTime > gc.boundary.localScale.x / 2)
                {
                    direction = -1;
                }
                else if (rb.position.x + toDodge * maneouverTime < -gc.boundary.localScale.x / 2)
                {
                    direction = 1;
                }
                toDodge = Random.Range(10, dodgeMagnitude) * direction;

                //Perform maneouver
                rb.velocity = new Vector3(toDodge, rb.velocity.y, 0);
                yield return new WaitForSeconds(Random.Range(0.3f, maneouverTime));
                rb.velocity = new Vector3(0, rb.velocity.y, 0);

                nextTime = Time.time + dodgeRate;
            }
        }
        yield return null;        
    }
}
