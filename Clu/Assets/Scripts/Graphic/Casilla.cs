using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class Casilla : MonoBehaviour {

	private Position position; //posicion en el tablero
	private int type;
	private Board b;
	private bool ocupada;
	//private CasillaMatrix innerMatrix; //booleanos, true si hay algo

	// Use this for initialization
	void Start () {
		this.gameObject.SetActive (true);
		ocupada = false;
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
