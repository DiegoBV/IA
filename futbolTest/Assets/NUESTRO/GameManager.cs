using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    /*
        public GameObject ball;
        public GameObject NorthGoal;
        public GameObject SouthGoal;
    */

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void goal(int team)
    {
        print("Team: " + team + " scored a goal");
    }
}
