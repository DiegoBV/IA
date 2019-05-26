using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class ChangeBeaviour : MonoBehaviour {

    private BehaviorDesigner.Runtime.BehaviorTree[] behaviours;

    // Use this for initialization
    void Start () {
        behaviours = GetComponents<BehaviorDesigner.Runtime.BehaviorTree>();
	}

    public void changeBehaviour(string name)
    {
        BehaviorTree activate_B = null;
        foreach(BehaviorDesigner.Runtime.BehaviorTree b in behaviours)
        {
            b.enabled = false;
            if(b.BehaviorName == name)
            {
                activate_B = b;
            }
        }

        if (activate_B != null)
        {
            activate_B.enabled = true;
        }

        //BehaviorManager.instance.EnableBehavior(newBehaviour);
    }

}
