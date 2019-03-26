using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class Player : MonoBehaviour {

	public bool UserControlled;
	public int order;
	private bool myTurn = false;
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

	public bool Move(Position p){
		Casilla cas = GameManager.instance.getCasilla (p);
		if(myTurn && !cas.getOcupada()){
			print ("dandole wey");
			GameManager.instance.MoveTo (this.gameObject, actualCas, cas);
			actualCas = cas;

			suspInPlace = GameManager.instance.IsSomeoneInMyPlace ((GameManager.Place)actualCas.getType ());
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
}
