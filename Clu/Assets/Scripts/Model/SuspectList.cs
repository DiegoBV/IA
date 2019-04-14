using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectList : MonoBehaviour {

    [HideInInspector]
    public bool[] suspetcs; //LISTA DE SOSPECHOSOS
    public bool[] initSuspetcs; //COPIA DE LA LISTA INICIAL
    [HideInInspector]
    public bool[] weapons; //LISTA DE SOSPECHOSOS
    public bool[] initWeapons; //COPIA DE LA LISTA INICIAL
    [HideInInspector]
    public bool[] rooms; //LISTA DE SOSPECHOSOS
    public bool[] initRooms; //COPIA DE LA LISTA INICIAL

    public bool[] total;

    private int[] difElem_; //NUMERO DE ELEMENTOS DIFERENTES (EJ: 9 Sospechos, 6 Salas, 3 Armas ) 
    private int numElem_; //NUMERO TOTAL DE CARTAS

    public void Initialize (List<DeckManager.DeckElements> deck, int numElem, int[] difElem) {
        //INICIALIZACION DE VARIABLES
        this.numElem_ = numElem;
        this.difElem_ = difElem;

        rooms = new bool[difElem_[0]];
        initRooms = new bool[difElem_[0]];

        suspetcs = new bool[difElem_[1]];
        initSuspetcs = new bool[difElem_[1]];

        weapons = new bool[difElem_[2]];
        initWeapons = new bool[difElem_[2]];

        total = new bool[difElem_[0] + difElem_[1] + difElem_[2]];

        //RECORREMOS LOS ARRAYS Y MARCAMOS COMO TRUE LAS CARTAS QUE TENEMOS

        for (int i = 0; i < deck.Count; i++) {
            if ((int) deck[i] < difElem[0]) {
                rooms[(int) deck[i]] = true;
                initRooms[(int) deck[i]] = true;
            } else if ((int) deck[i] < difElem[0] + difElem[1]) {
                suspetcs[(int) deck[i] - (int) difElem[0]] = true;
                initSuspetcs[(int) deck[i] - (int) difElem[0]] = true;
            } else {
                weapons[(int) deck[i] - (int) difElem[0] - (int) difElem[1]] = true;
                initWeapons[(int) deck[i] - (int) difElem[0] - (int) difElem[1]] = true;
            }

            total[(int) deck[i]] = true;
        }
    }

    //COMPROBAMOS SI TENEMOS UN ELEMENTOS

    public int[] checkElement (DeckManager.DeckElements d) {
        //RETURN INT[TIPO/ELEMENTO]
        if ((int) d < difElem_[0]) {
            rooms[(int) d] = true;
            return new int[] { 0, (int) d };
        } else if ((int) d < difElem_[0] + difElem_[1]) {
            suspetcs[(int) d - (int) difElem_[0]] = true;
            return new int[] { 1, (int) d - (int) difElem_[0] };
        } else {
            weapons[(int) d - (int) difElem_[0] - (int) difElem_[1]] = true;
            return new int[] { 2, (int) d - (int) difElem_[0] - (int) difElem_[1] };
        }

    }

    public bool[] getSuspetcs () {
        return suspetcs;
    }
    public bool[] getWeapons () {
        return weapons;
    }
    public bool[] getRooms () {
        return rooms;
    }

    public bool[] getInitSuspetcs () {
        return initSuspetcs;
    }
    public bool[] getInitWeapons () {
        return initWeapons;
    }
    public bool[] getIntiRooms () {
        return initRooms;
    }

}