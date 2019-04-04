using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspectsTable : MonoBehaviour {

    bool[][] p1_;
    bool[][] p2_;
    bool[][] p3_;

    public GameObject TheBigTable;

    public GameObject[] PButtons = new GameObject[3];

    public GameObject[] SmrtButtons = new GameObject[3];

    public GameObject[] DumButtons = new GameObject[3];

    public GameObject SpyLayer;
    bool spyFlag = true;

    bool flag = false;

    public void initialize(Player p1, Player p2, Player p3)
    {
        p1_ = new bool[][] { p1.GetSuspectList().rooms, p1.GetSuspectList().suspetcs, p1.GetSuspectList().weapons };

        p2_ = new bool[][] { p2.GetSuspectList().rooms, p2.GetSuspectList().suspetcs, p2.GetSuspectList().weapons };

        p3_ = new bool[][] { p3.GetSuspectList().rooms, p3.GetSuspectList().suspetcs, p3.GetSuspectList().weapons };

        visualize(p1_, PButtons);
        visualize(p2_, SmrtButtons);
        visualize(p3_, DumButtons);

    }

    
    public void visualize(bool[][] player, GameObject[] ticks)
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < player[j].Length; i++)
            {
                if (player[j][i])
                {
                    foreach (Transform transform in ticks[j].transform)
                    {
                        if (transform.gameObject.name == (i + 1).ToString())
                        {
                            transform.gameObject.GetComponent<Image>().color = new Color(255, 0, 0);
                        }
                    }
                }
            }
        }
    }

    public void CheckElem(int[] pos, int ord)
    {
        GameObject[] ticks = new GameObject[0];
        switch (ord)
        {
            case 0:
                ticks = PButtons;
                break;
            case 1:
                ticks = SmrtButtons;
                break;
            case 2:
                ticks = DumButtons;
                break;
        }

        print("TICKS ===>" + ord + "TYPE ============>" + pos[0] + "ELEM===========>" + pos[1]);

        foreach (Transform transform in ticks[pos[0]].transform)
        {
            if (transform.gameObject.name == (pos[1] + 1).ToString())
            {
                transform.gameObject.GetComponent<Image>().color = new Color(0, 255, 255);
            }
        }
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            
            flag = !flag;
            TheBigTable.SetActive(flag);
        }
    }

    public void Spy()
    {
        spyFlag = !spyFlag;
        SpyLayer.SetActive(spyFlag);
    }

    public void reset()
    {
        spyFlag = true;
        //ARRAY
        for (int i = 0; i < p1_.Length; i++){
            for(int j = 0; j < p1_[i].Length;j++)
            {
                p1_[i][j] = false;
            }
        }
        for (int i = 0; i < p2_.Length; i++)
        {
            for (int j = 0; j < p2_[i].Length; j++)
            {
                p2_[i][j] = false;
            }
        }
        for (int i = 0; i < p3_.Length; i++)
        {
            for (int j = 0; j < p3_[i].Length; j++)
            {
                p3_[i][j] = false;
            }
        }
        //BOTONES
        for (int i = 0; i < PButtons.Length; i++)
        {
            foreach (Transform transform in PButtons[i].transform)
            {
                transform.gameObject.GetComponent<Image>().color = new Color(0, 255, 0);
            }
        }
        for (int i = 0; i < SmrtButtons.Length; i++)
        {
            foreach (Transform transform in SmrtButtons[i].transform)
            {
                transform.gameObject.GetComponent<Image>().color = new Color(0, 255, 0);
            }
        }
        for (int i = 0; i < DumButtons.Length; i++)
        {
            foreach (Transform transform in DumButtons[i].transform)
            {
                transform.gameObject.GetComponent<Image>().color = new Color(0, 255, 0);
            }
        }

    }
}
