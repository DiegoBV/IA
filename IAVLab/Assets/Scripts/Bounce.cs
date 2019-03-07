using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

	// Use this for initialization
	void Start () {
        OriPosition = this.transform.position.y;
	}
    float OriPosition;
    float MaxDeviation = .4f;
    bool direction = false;
	// Update is called once per frame
	void Update () {
        if (direction)
        {
            this.transform.position += new Vector3(0.0f, 0.02f, 0.0f);
            if (this.transform.position.y > OriPosition + MaxDeviation) direction = !direction;
        }
        else
        {
            this.transform.position += new Vector3(0.0f, -0.02f, 0.0f);
            if (this.transform.position.y < OriPosition - MaxDeviation) direction = !direction;
        }
        
    }
}
