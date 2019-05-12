using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public int TeamOwner;


    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Ball")
        {
            GameManager.instanciar().goal(TeamOwner); 
        }
    }
}
