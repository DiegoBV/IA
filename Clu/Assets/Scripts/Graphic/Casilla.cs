using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

/*
Clase casilla, se ocupa de la parte visual de cada casilla del tablero y de la interaccion con el usuario (click).
 */
public class Casilla : MonoBehaviour {

	private Position position; //posicion en el tablero
	private int type;
	private Board b;
	private bool ocupada;
	//private CasillaMatrix innerMatrix; //booleanos, true si hay algo

	// Use this for initialization
	void Start () {
		this.gameObject.SetActive (true);
	}

	private void changeColor(){
		Renderer rend = this.GetComponent<Renderer> ();
		rend.enabled = true;
		rend.material.SetColor("_Color", b.getManager().colors[this.getType()]);
	}

	public void Initialize(Board b, Position p){
		this.b = b;
		setPosition (p);
	}

	//el usuario se mueve a esta casilla si hace click en ella y esta libre
	public bool OnMouseUpAsButton(){
        if (GameManager.instance.CanInteract())
        {
            if(GameManager.instance.getPlayerActive().userControlled())
                GameManager.instance.getPlayerActive().Move(this.position);
        }
		
		return true;
	}

	//sets y gets
	
	public void setPosition(Position p) {
		position = p;
	}
	public void setOcupada(bool b){
		this.ocupada = b;
	}
	public void setType(int t) {
		type = t;
		changeColor ();
	}
	public Position getPosition(){
		return this.position;
	}
	public int getType(){
		return this.type;
	}
	public bool getOcupada(){
		return this.ocupada;
	}
}
