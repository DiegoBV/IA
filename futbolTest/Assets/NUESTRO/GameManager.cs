using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BehaviorDesigner.Runtime;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public GameObject[] entities;
    public Text score;
    public GameObject blueTeamManager;
    [HideInInspector]
    public int[] scores = { 0, 0 };
    private BehaviorTree blueTeamManager_tree;

    /*
        public GameObject NorthGoal;
        public GameObject SouthGoal;
    */

    // Use this for initialization
    void Awake()
    {
        instance = this;
        score.text = scores[0] + " - " + scores[1];
        blueTeamManager_tree = blueTeamManager.GetComponent<BehaviorTree>();
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

        blueTeamManager_tree.enabled = false;
        blueTeamManager_tree.enabled = true;
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

    public void pauseGame()
    {

    }

    public void goal(int team)
    {
        scores[team]++;
        print("Team: " + team + " scored a goal");
        score.text = scores[0] + " - " + scores[1];

        resetGameObjects();
        blueTeamManager_tree.enabled = false;
        blueTeamManager_tree.enabled = true;
        //ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //ball.transform.position = new Vector3(-7, 12, -12);
    }
}
