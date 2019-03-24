using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectList : MonoBehaviour {

    public bool[] suspetcs;
    public bool[] weapons;
    public bool[] rooms;

    private int[] difElem_;
    private int numElem_;

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

    public void Initialized(List<int> deck, int numElem, int[] difElem)
    {
        this.numElem_ = numElem;
        this.difElem_ = difElem;


        suspetcs = new bool[difElem_[0]];
        weapons = new bool[difElem_[1]];
        rooms = new bool[difElem_[2]];
        int index = 0;



        for (int i = 0; i < rooms.Length; i++)
        {
            if (deck[index] < difElem_[0])
            {
                suspetcs[deck[index]] = true;
                index++;
            }
            else i = suspetcs.Length;
        }
        for (int i = 0; i < suspetcs.Length; i++)
        {
            if (deck[index] < difElem_[0])
            {
                suspetcs[deck[index] - difElem_[0]] = true;
                index++;
            }
            else i = suspetcs.Length;
        }
        for (int i = 0; i < weapons.Length; i++)
        {
            if (deck[index] < difElem_[0])
            {
                suspetcs[deck[index] - (difElem_[0] + difElem_[1])] = true;
                index++;
            }
            else i = suspetcs.Length;
        }


    }
}
