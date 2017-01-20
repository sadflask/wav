using UnityEngine;

public class ObjectMove : MonoBehaviour {
    Rigidbody rb;
    public float speed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, speed, 0);
	}
}
