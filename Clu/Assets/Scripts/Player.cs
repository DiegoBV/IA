﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class Player : MonoBehaviour {

	public bool UserControlled;
	public int order;
	private bool myTurn = false;
	private Casilla actualCas = null;
	private List<DeckManager.DeckElements> myCards;
	private List<Sospechoso> suspInPlace;
    private List<DeckManager.DeckElements> discoveredCards;
    

	// Use this for initialization
	void Start () {
		if (UserControlled) {
			Renderer rend = this.GetComponent<Renderer> ();
			rend.enabled = true;
			rend.material.SetColor("_Color", Color.black);
		}

        discoveredCards = new List<DeckManager.DeckElements>();
    }

	public void Initialize(){
		if(UserControlled)
			GameManager.instance.setPlayerActive (this);

		print ("Mi baraja: ");
		foreach (DeckManager.DeckElements element in myCards)
			print (element);

		
        //It Breaks here
        int[] array = new int[] { 9, 6, 6 }; //Temp
        GetComponent<SuspectList>().Initialize(myCards, 21, array);
	}

    public SuspectList GetSuspectList()
    {
        return GetComponent<SuspectList>();
    }

    public bool Move(Position p){
		Casilla cas = GameManager.instance.getCasilla (p);
		if(myTurn && !cas.getOcupada()){
			print ("dandole wey");
			GameManager.instance.MoveTo (this.gameObject, actualCas, cas);
			actualCas = cas;

			print("Hay " + suspInPlace.Count + " sospechosos conmigo");

			//comprobar cosas......
			if (suspInPlace.Count == 0 || !UserControlled) { //testeo
				GameManager.instance.changeTurn (this.order);
			}
		}

		return cas.getOcupada ();
	}

	public bool isMyTurn(){
		return myTurn;
	}

	public void toggleTurn(){
		myTurn = !myTurn;
	}

    public void setMyTurn(bool b){
        this.myTurn = b;
    }

	public List<DeckManager.DeckElements> getMyCards(){
		return this.myCards;
	}

	public void setMyCards(List<DeckManager.DeckElements> l){
		this.myCards = l;
	}

	public GameManager.Place GetPlace(){
		return (GameManager.Place)this.actualCas.getType ();
	}

	public void Activate(){ //garbage
        if (!UserControlled)
        {
            Invoke("BotBehaviour", 3);
        }
	}

    public bool checkSuggestion(DeckManager.DeckElements[] acc, Player p)
    {
        bool canAccuse = true;
        foreach(DeckManager.DeckElements d in myCards)
        {
            if(acc[0] == d && canAccuse)
            {
                //comunicarle carta al jugador
                p.addCard(d);
                canAccuse = false;
            }
            else if(acc[1] == d && canAccuse)
            {
                p.addCard(d);
                canAccuse = false;
            }
            else if(acc[2] == d && canAccuse)
            {
                p.addCard(d);
                canAccuse = false;
            }

        }

        return canAccuse;
    }

    private void BotBehaviour()
    {
        bool test = true;
        do
        {
            System.Random rnd = GameManager.instance.getRandomSeed();
            int x = rnd.Next(0, GameManager.instance.getRows());
            int y = rnd.Next(0, GameManager.instance.getCols());
            Position p = new Position(x, y);
            test = this.Move(p);
        } while (test);
    }

	public Casilla getActualCas(){
		return this.actualCas;
	}

	public void setActualCas(Casilla c){
		this.actualCas = c;
	}

	public List<Sospechoso> getSuspectsInPlace(){
		return this.suspInPlace;
	}

	public void setSuspectsInPlace(List<Sospechoso> l){
		this.suspInPlace = l;
	}
	
    public bool userControlled()
    {
        return this.UserControlled;
    }

    public void addCard(DeckManager.DeckElements d)
    {
        print("ADDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD" + "          " + d);
        myCards.Add(d);
        GameManager.instance.table.CheckElem(GetComponent<SuspectList>().checkElement(d), order);
        
    }
}
