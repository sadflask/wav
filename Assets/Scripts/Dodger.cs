using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodger : MonoBehaviour {
    public float dodgeRate;
    public float dodgeMagnitude;
    public float startWait;
    public float maneouverTime;
    Rigidbody rb;
    float nextTime;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Dodge());
	}
	
	// Update is called once per frame
	IEnumerator Dodge () {
        while (gameObject)
        {
            yield return new WaitForSeconds(startWait);
            if (nextTime < Time.time)
            {
                Debug.Log("Dodge" + Time.time);
                int direction = Random.Range(0, 2);
                if (direction == 0)
                {
                    direction = -1;
                }

                float toDodge = Random.Range(10, dodgeMagnitude) * direction;
                rb.velocity = new Vector3(toDodge, rb.velocity.y, 0);
                yield return new WaitForSeconds(Random.Range(0.3f, maneouverTime));
                rb.velocity = new Vector3(0, rb.velocity.y, 0);

                //Check if leaving screen

                nextTime = Time.time + dodgeRate;
            }
        }
        yield return null;        
    }
}
