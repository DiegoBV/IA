using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {



    public static GameManager instance = null;
    public GameObject ball;
    /*
        public GameObject NorthGoal;
        public GameObject SouthGoal;
    */
    int[] scores = { 0, 0 };

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    public static GameManager instanciar()
    {
        return instance;
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void goal(int team)
    {
        scores[team]++;
        print("Team: " + team + " scored a goal");

        ball.transform.position = new Vector3(-7, 15, -12);
    }
}
