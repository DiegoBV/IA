using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class Player : MonoBehaviour {

	public bool UserControlled;
	private bool myTurn = true;
	private Casilla actualCas = null;
	private List<DeckManager.DeckElements> myCards;
    public SuspectList Slist;
	private List<Sospechoso> suspInPlace;

	// Use this for initialization
	void Start () {
		if (UserControlled) {
			Renderer rend = this.GetComponent<Renderer> ();
			rend.enabled = true;
			rend.material.SetColor("_Color", Color.black);
		}
	}

	public void Initialize(){
		if(UserControlled)
			GameManager.instance.setPlayerActive (this);

		print ("Mi baraja: ");
		foreach (DeckManager.DeckElements element in myCards)
			print (element);

		print ("------------");
	}

	public void Move(Position p){
		Casilla cas = GameManager.instance.getCasilla (p);
		if(myTurn && !cas.getOcupada()){
			GameManager.instance.MoveTo (this.gameObject, actualCas, cas);
			actualCas = cas;

			suspInPlace = GameManager.instance.IsSomeoneInMyPlace ((GameManager.Place)actualCas.getType ());
			print("Hay " + suspInPlace.Count + " sospechosos conmigo");
			//comprobar cosas......
		}
	}

	public bool isMyTurn(){
		return myTurn;
	}

	public void toggleTurn(){
		myTurn = !myTurn;

		if(myTurn)
			GameManager.instance.setPlayerActive (this);
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
		
}
