using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManagerBh : MonoBehaviour {

    public GameObject bot1, bot2, bot3;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateSetUp(int ownScore, int enemyScore)
    {
        if(ownScore + 2 < enemyScore) //Pierde por mas de 2 goles
        {
            //2 atacantes y 1 portero

        }else if(ownScore < enemyScore) //Pierde por un gol
        {
            //1 portero 2 hibrodos
        }else if(ownScore >= enemyScore + 2) // Ganando por mas de 2 goles
        {
            ///1 hibrido 2 atacantes
        }
        else // Empatado o ganando por 1 gol
        {
            // un portero, un hiubrido y un atacante
        }
    }
}
