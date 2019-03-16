using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class Mechanics : MonoBehaviour {

	public bool UserControlled;
	bool myTurn = true;
	Casilla actualCas = null;
	// Use this for initialization
	void Start () {
		if (UserControlled) {
			GameManager.instance.setPlayerActive (this);
			Renderer rend = this.GetComponent<Renderer> ();
			rend.enabled = true;
			rend.material.SetColor("_Color", Color.black);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
