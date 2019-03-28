﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectList : MonoBehaviour {

    [HideInInspector]
    public bool[] suspetcs;
    [HideInInspector]
    public bool[] weapons;
    [HideInInspector]
    public bool[] rooms;

    private int[] difElem_;
    private int numElem_;

    public void Initialize(List<DeckManager.DeckElements> deck, int numElem, int[] difElem)
    {
        this.numElem_ = numElem;
        this.difElem_ = difElem;

        rooms = new bool[difElem_[0]];
        suspetcs = new bool[difElem_[1]];
        weapons = new bool[difElem_[2]];

       for(int i = 0; i < deck.Count; i++)
        {
            if((int)deck[i] < difElem[0])
            {
                rooms[(int)deck[i]] = true;
            }
            else if ((int)deck[i] < difElem[0] + difElem[1])
            {
                suspetcs[(int)deck[i] - (int)difElem[0]] = true;
            }
            else 
            {
                weapons[(int)deck[i] - (int)difElem[0] - (int)difElem[1]] = true;
            }
        }
    }

    public int[] checkElement(DeckManager.DeckElements d)
    {
        if ((int)d < difElem_[0])
        {
            rooms[(int)d] = true;
            return new int[] { 0, (int)d };
        }
        else if ((int)d < difElem_[0] + difElem_[1])
        {
            suspetcs[(int)d - (int)difElem_[0]] = true;
            return new int[] { 1, (int)d - (int)difElem_[0] };
        }
        else
        {
            weapons[(int)d - (int)difElem_[0] - (int)difElem_[1]] = true;
            return new int[] { 2, (int)d - (int)difElem_[0] - (int)difElem_[1] };
        }

    }
    public bool[] getSuspetcs()
    {
        return suspetcs;
    }
    public bool[] getWeapons()
    {
        return weapons;
    }
    public bool[] getRooms()
    {
        return rooms;
    }

}
