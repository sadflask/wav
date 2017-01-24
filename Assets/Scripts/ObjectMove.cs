using UnityEngine;
using System.Collections;

public class ObjectMove : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public Transform boundary;

    // Use this for initialization
    void Start()
    {
        boundary = FindObjectOfType<GameController>().boundary;
        rb = GetComponent<Rigidbody>();
        rb.velocity += new Vector3(0, speed, 0);
    }

    void FixedUpdate()
    {
        if (gameObject.CompareTag("Star"))
        {
            if (transform.position.y < -boundary.localScale.y / 2)
            {
                rb.position = new Vector3(transform.position.x, boundary.localScale.y / 2, rb.position.z); //Figure out Y defaults
            }
        }
    }
}

