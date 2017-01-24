using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTilt : MonoBehaviour {

	public float xInitialRotation;
	public float yInitialRotation;
	public float zInitialRotation;
	public float rotationAmmount;

	private Rigidbody rb;
	void Start(){
		rb = gameObject.GetComponent<Rigidbody> ();

	}

	// Update is called once per frame
	void FixedUpdate () {
		float rotation = Mathf.Clamp (rb.velocity.x * rotationAmmount, -rotationAmmount, rotationAmmount);
		rb.transform.rotation = Quaternion.Euler (xInitialRotation,-rotation-yInitialRotation,-zInitialRotation);
	}
}
