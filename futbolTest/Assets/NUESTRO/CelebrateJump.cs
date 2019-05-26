using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebrateJump : MonoBehaviour
{
    private float h;

    void OnEnable()
    {
        h = 0;
    }

    void Update()
    {
        h += Time.deltaTime;
        // In Math.PingPong() number 3 is y axix value. In this case your object will go from 0 to 3 (in Y axis) and then back
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.PingPong(h, 1), transform.position.z);
    }
}