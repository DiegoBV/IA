using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectsTable : MonoBehaviour {

    bool[][] p1_;
    bool[][] p2_;
    bool[][] p3_;

    public GameObject TheBigTable;

    public GameObject roomTable;
    public GameObject murdererTable;
    public GameObject weaponTable;

    public GameObject[] PButtons = new GameObject[3];

    public GameObject[] SmrtButtons = new GameObject[3];

    public GameObject[] DumButtons = new GameObject[3];

    bool flag = false;

    private void Start()
    {
        p1_ = new bool[3][];
        p2_ = new bool[3][];
        p3_ = new bool[3][];
    }

    public void initialize(Player p1, Player p2, Player p3)
    {
        p1_[0] = p1.GetSuspectList().rooms;
        p1_[1] = p1.GetSuspectList().suspetcs;
        p1_[2] = p1.GetSuspectList().weapons;

        p2_[0] = p2.GetSuspectList().rooms;
        p2_[1] = p2.GetSuspectList().suspetcs;
        p2_[2] = p2.GetSuspectList().weapons;

        p3_[0] = p3.GetSuspectList().rooms;
        p3_[1] = p3.GetSuspectList().suspetcs;
        p3_[2] = p3.GetSuspectList().weapons;
    }

    
    public void visualize()
    {

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            flag = !flag;
            TheBigTable.SetActive(flag);
        }
    }
}
