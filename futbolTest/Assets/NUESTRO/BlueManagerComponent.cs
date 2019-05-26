using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class BlueManagerComponent : MonoBehaviour
{
    public GameObject[] team;
    private bool defense = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void JustScored(int tR, int tB)
    {
        if(tR >= tB && !defense)
        {
            //cambia a configuracion defensiva
            foreach(GameObject g in team)
            {
                BehaviorDesigner.Runtime.Behavior[] bs = g.GetComponents<BehaviorDesigner.Runtime.Behavior>();
                BehaviorManager.instance.DisableBehavior(bs[0]);
                BehaviorManager.instance.EnableBehavior(bs[1]);
            }
            defense = true;
        }
        else if(defense)
        {
            //cambiar a configuracion de ataque
            //cambia a configuracion defensiva
            foreach (GameObject g in team)
            {
                Behaviour[] bs = g.GetComponents<Behaviour>();
                bs[0].enabled = !bs[0].enabled;
                bs[1].enabled = !bs[1].enabled;
            }
            defense = false;
        }
    }
}
