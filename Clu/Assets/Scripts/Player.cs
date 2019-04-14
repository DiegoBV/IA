using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

/*
Clase player, compartida por el usuario y ambos bots
 */
public class Player : MonoBehaviour {

    public bool UserControlled;
    public int order;
    public int numMaxActions;
    private bool myTurn = false;
    private Casilla actualCas = null;
    private List<DeckManager.DeckElements> myCards;
    private List<Sospechoso> suspInPlace;
    private List<DeckManager.DeckElements> discoveredCards;
    private bool eliminado = false;
    private int currNumActions = 0;

    // Use this for initialization
    void Start () {
        discoveredCards = new List<DeckManager.DeckElements> ();
    }

    //init del player + cambio de color
    public void Initialize () {
        Renderer rend = this.GetComponent<Renderer> ();
        rend.enabled = true;
        if (UserControlled) {
            GameManager.instance.setPlayerActive (this);
            rend.material.SetColor ("_Color", Color.black);
        } else if (this.GetComponent<SmartIA> () != null) {
            rend.material.SetColor ("_Color", Color.blue);
        } else {
            rend.material.SetColor ("_Color", new Color (0.8F, 0.6F, 0.1F));
        }

        print ("Mi baraja: ");
        foreach (DeckManager.DeckElements element in myCards)
            print (element);

        //It Breaks here
        int[] array = new int[] { 9, 6, 6 }; //Temp
        GetComponent<SuspectList> ().Initialize (myCards, 21, array);

        eliminado = false;
    }

    public SuspectList GetSuspectList () {
        return GetComponent<SuspectList> ();
    }

    //se mueve a una casilla si puede. Si la sala esta vacia, pasa turno
    public bool Move (Position p) {
        Casilla cas = GameManager.instance.getCasilla (p);
        if (myTurn && !cas.getOcupada () && currNumActions < numMaxActions) {
            print ("dandole wey");
            GameManager.instance.MoveTo (this.gameObject, actualCas, cas);
            actualCas = cas;

            print ("Hay " + suspInPlace.Count + " sospechosos conmigo");

            //comprobar cosas......
            if (suspInPlace.Count == 0) { //testeo
                GameManager.instance.changeTurn (this.order);
            } else {
                currNumActions++;
            }
        }

        return cas.getOcupada ();
    }
    //gets y sets

    public bool isMyTurn () {
        return myTurn;
    }

    public void toggleTurn () {
        myTurn = !myTurn;
    }

    public void setMyTurn (bool b) {
        this.myTurn = b;
    }

    public List<DeckManager.DeckElements> getMyCards () {
        return this.myCards;
    }

    public void setMyCards (List<DeckManager.DeckElements> l) {
        this.myCards = l;
    }

    public GameManager.Place GetPlace () {
        return (GameManager.Place) this.actualCas.getType ();
    }

    public void Activate () { //si esta eliminado, pasa turno, si no, empieza la ia si es un bot
        if (eliminado) {
            GameManager.instance.changeTurn (this.order);
        } else if (!UserControlled) {
            Invoke ("BotBehaviour", 3);
        }
        myTurn = true;
        currNumActions = 0;
    }

    //comprueba la sugerencia de otro jugador. Si tiene una carta que se encuentre en su baraja, se la comunicas
    public bool checkSuggestion (DeckManager.DeckElements[] acc, Player p) {
        bool canAccuse = true;
        foreach (DeckManager.DeckElements d in myCards) {
            if (this.GetSuspectList ().total[(int) d]) {
                if (acc[0] == d && canAccuse) {
                    //comunicarle carta al jugador
                    p.addCard (d);
                    if (GameManager.instance.getPlayerActive ().UserControlled)
                        GameManager.instance.showCard.text = this.name + " showed you this card: \n \n" + d;
                    canAccuse = false;
                } else if (acc[1] == d && canAccuse) {
                    p.addCard (d);
                    if (GameManager.instance.getPlayerActive ().UserControlled)
                        GameManager.instance.showCard.text = this.name + " showed you this card: \n \n" + d;
                    canAccuse = false;
                } else if (acc[2] == d && canAccuse) {
                    p.addCard (d);
                    if (GameManager.instance.getPlayerActive ().UserControlled)
                        GameManager.instance.showCard.text = this.name + " showed you this card: \n \n" + d;
                    canAccuse = false;
                }
            }
        }

        return canAccuse;
    }

    //metodo que se llama cuando se es eliminado, muestra todas las cartas a todos los jugadores
    public void showEveryCard () {
        foreach (Player p in GameManager.instance.players) {
            if (p.gameObject != this.gameObject) {
                for (int i = 0; i < myCards.Count; i++) {
                    if (!this.GetSuspectList ().total[i]) {
                        p.addCard (myCards[i]);
                    }
                }
            }
        }
    }

    //dependiendo del tipo, se comportara de una manera o de otra
    private void BotBehaviour () {
        DumbIA cmp = this.GetComponent<DumbIA> ();
        SmartIA sia = this.GetComponent<SmartIA> ();
        if (cmp != null) {
            cmp.Act (this);
        } else if (sia != null) {
            sia.Act (this);
        }
    }

    //gets y sets

    public Casilla getActualCas () {
        return this.actualCas;
    }

    public void setActualCas (Casilla c) {
        this.actualCas = c;
    }

    public List<Sospechoso> getSuspectsInPlace () {
        return this.suspInPlace;
    }

    public void setSuspectsInPlace (List<Sospechoso> l) {
        this.suspInPlace = l;
    }

    public bool userControlled () {
        return this.UserControlled;
    }

    public bool getEliminado () {
        return eliminado;
    }

    public void setEliminado (bool b) {
        eliminado = b;

        if (eliminado) {
            Renderer rend = GetComponent<Renderer> ();
            rend.enabled = true;
            rend.material.SetColor ("_Color", Color.grey);
        }
    }

    //anyade una carta a la baraja, actualiza la tabla
    public void addCard (DeckManager.DeckElements d) {
        print ("ADD" + "          " + d);
        myCards.Add (d);
        GameManager.instance.table.CheckElem (GetComponent<SuspectList> ().checkElement (d), order);
    }

    public void increaseCount () {
        this.currNumActions++;
    }

    public int getCurrNumAct () {
        return this.currNumActions;
    }
}