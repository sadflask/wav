using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : MonoBehaviour {
    void OnTriggerExit(Collider c)
    {
        Debug.Log(c.gameObject.name);
        Destroy(c.gameObject);
    }
}
