using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public Vector3 rotation;
    // Use this for initialization
    void Start()
    {
        rotation = new Vector3(Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation);
    }
}
