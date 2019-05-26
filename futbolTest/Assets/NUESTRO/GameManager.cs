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
    private AudioSource sc;
    bool ispaused = false;

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
        sc = GetComponent<AudioSource>();
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
        if (!ispaused)
        {
            scores[0] = 0;
            scores[1] = 0;

            score.text = scores[0] + " - " + scores[1];

            resetGameObjects();

            blueTeamManager_tree.enabled = false;
            blueTeamManager_tree.enabled = true;
        }
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
        foreach (GameObject g in entities)
        {
            BehaviorTree bt = g.GetComponent<BehaviorTree>();

            if(bt != null)
            {
                bt.enabled = !bt.enabled;
            }
            else
            {
                Rigidbody rb = g.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.isKinematic = !rb.isKinematic;
                }
            }
        }

        ispaused = !ispaused;
    }

    void ResetRound()
    {
        pauseGame();
        foreach (GameObject g in entities)
        {
            CelebrateJump j = g.GetComponent<CelebrateJump>();
            if (j != null)
            {
                j.enabled = false;
            }
        }
        resetGameObjects();
        blueTeamManager_tree.enabled = false;
        blueTeamManager_tree.enabled = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void goal(int team)
    {
        scores[team]++;
        print("Team: " + team + " scored a goal");
        score.text = scores[0] + " - " + scores[1];

//        resetGameObjects();
       

        //pausa
        pauseGame();
        //activar celebrar
        foreach(GameObject g in entities)
        {
            CelebrateJump j = g.GetComponent<CelebrateJump>();
            if(j != null && team.ToString() == g.tag)
            {
                j.enabled = true;
            }
        }
        //invoke
        Invoke("ResetRound", 3f);
        sc.Play();
        //ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //ball.transform.position = new Vector3(-7, 12, -12);
    }
}
