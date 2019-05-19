using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {

    private Vector3 init_position;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        init_position = this.transform.position;
        rb = GetComponent<Rigidbody>();
    }
	
    public void reset()
    {
        this.transform.position = init_position;

        if (rb != null) {
            rb.velocity = Vector3.zero;
        }
    }
}
