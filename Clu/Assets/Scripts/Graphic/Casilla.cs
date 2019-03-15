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
		/*if (rend != null) {
			switch (this.type) {
			case 0:
				rend.material.SetColor("_Color", colors[0]);
				break;
			case 1:
				rend.material.SetColor("_Color", Color.gray);
				break;
			case 2:
				rend.material.SetColor("_Color", new Color (0.91f, 0.89f, 0.09f, 1f));
				break;
			case 3:
				rend.material.SetColor("_Color", new Color (0.71f, 0.2f, 0.71f, 1f));
				break;
			case 4:
				rend.material.SetColor("_Color", new Color (1f, 0f, 0.71f, 1f));
				break;
			case 5:
				rend.material.SetColor("_Color", new Color (1f, 0.6f, 0f, 1f));
				break;
			case 6:
				rend.material.SetColor("_Color", Color.green);
				break;
			case 7:
				rend.material.SetColor("_Color", Color.red);
				break;
			case 8:
				rend.material.SetColor("_Color", new Color (0.4f, 0.2f, 0f, 1f));
				break;
			default:
				Debug.Log ("Tipo no valido");
				break;
			}*/
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
