using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject[] entities;
    public Text score;

    /*
        public GameObject NorthGoal;
        public GameObject SouthGoal;
    */
    int[] scores = { 0, 0 };

    // Use this for initialization
    void Awake()
    {
        instance = this;
        score.text = scores[0] + " - " + scores[1];
    }

    public static GameManager instanciar()
    {
        return instance;
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void reset()
    {
        scores[0] = 0;
        scores[1] = 0;

        score.text = scores[0] + " - " + scores[1];

        resetGameObjects();
    }

    private void resetGameObjects()
    {
        foreach (GameObject g in entities)
        {
            ResetPosition rp = g.GetComponent<ResetPosition>();

            if (rp != null)
            {
                rp.reset();
            }
        }
    }

    public void goal(int team)
    {
        scores[team]++;
        print("Team: " + team + " scored a goal");
        score.text = scores[0] + " - " + scores[1];

        resetGameObjects();
        //ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //ball.transform.position = new Vector3(-7, 12, -12);
    }
}
