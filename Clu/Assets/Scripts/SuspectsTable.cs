using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectsTable : MonoBehaviour {

    bool[][] p1_ = new bool[3][];
    bool[][] p2_ = new bool[3][];
    bool[][] p3_ = new bool[3][];

    public void initialize(Player p1, Player p2, Player p3)
    {
        p1_[0] = p1.Slist.rooms;
        p1_[1] = p1.Slist.suspetcs;
        p1_[2] = p1.Slist.weapons;

        p2_[0] = p1.Slist.rooms;
        p2_[1] = p1.Slist.suspetcs;
        p2_[2] = p1.Slist.weapons;

        p3_[0] = p1.Slist.rooms;
        p3_[1] = p1.Slist.suspetcs;
        p3_[2] = p1.Slist.weapons;
    }

    public void visualize()
    {

    }
}
