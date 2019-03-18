using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class Player : MonoBehaviour {

	public bool UserControlled;
	private bool myTurn = true;
	private Casilla actualCas = null;
	private List<DeckManager.DeckElements> myCards;

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
			this.transform.position = new Vector3 (cas.transform.position.x, 
				cas.transform.position.y + cas.transform.localScale.y / 2, 
					cas.transform.position.z);
			actualCas = cas;

			int s = GameManager.instance.IsSomeoneInMyPlace ((GameManager.Place)actualCas.getType ());
			print("Hay " + s + " sospechosos conmigo");
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
		
}
