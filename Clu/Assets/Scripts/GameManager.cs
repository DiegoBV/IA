using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using Model;

public class GameManager : MonoBehaviour {
	public DialogObject ResetDialog;
	public DialogObject QuitDialog;
    public Board tablero;
	public DeckManager deckManager;
	public Color[] colors;
	public static GameManager instance = null;
	public GameObject modalDialog;
	public GameObject[] sospechososPrefab;
	public Player[] players;
	private Player turn; //quien es el turno
	private bool modalsAreActive = false;
	[HideInInspector] public enum Place {Biblioteca, Cocina, Comedor, Estudio, Pasillo,
		Recibidor, Sala_del_billar, Salon_de_baile, Terraza};
	private System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());

	// Use this for initialization
	void Awake () {
		instance = this;
		modalDialog.SetActive (false);
	}

	void Start(){
		this.Initialize();
	}

	// Update is called once per frame
	void Update () {
		
	}

    void Initialize()
    {
        tablero.Initialize(this);
		deckManager.Initialize ();
		foreach (Player element in players)
			element.Initialize ();
    }

	public void resetGame()
	{
		this.Initialize (); //no memory worries
		cancelModalDialog();
	}

	public void activeModalDialogReset()
	{
		modalDialog.SetActive (true);
		modalDialog.GetComponent<Modal> ().setDialog (ResetDialog);
		modalsAreActive = true;
	}

	public void activeModalDialogQuit()
	{
		modalDialog.SetActive (true);
		modalDialog.GetComponent<Modal> ().setDialog (QuitDialog);
		modalsAreActive = true;
	}

	public void cancelModalDialog()
	{
		modalDialog.SetActive (false);
		modalsAreActive = false;
	}

	public void Quit()
	{
		Application.Quit ();
	}

	public Casilla getCasilla(Position p){
		return this.tablero [p.getRow (), p.getCol ()];
	}

	public Player getPlayerActive(){
		return this.turn;
	}

	public void setPlayerActive(Player t){
		this.turn = t;
	}

	public Place whereIsIt(int tipo)
	{
		return (Place)tipo;
	}

	public int IsSomeoneInMyPlace(Place p){ //maybe devolver a todos los sospechosos que esten
		int howMany = 0;
		for (int i = 0; i < tablero.getSospechosos ().GetLength (0); i++) {
			if ((Place)tablero.getSospechosos () [i].GetComponent<Sospechoso>().getActualCas().getType() == p) {
				howMany++;
			}
		}

		return howMany;
	}

	public bool CanInteract(){
		return !this.modalsAreActive;
	}

	public System.Random getRandomSeed() { return rnd; }

	public DeckManager GetDeckManager() {
		return deckManager;
	}

	public List<Casilla> getCasillasInPlace(Place p){
		List<Casilla> l = tablero.getCasillasOfType ((int)p);

		return l;
	}

	public void MoveTo(GameObject o, Casilla orCas, Casilla destCas){
		o.transform.position = new Vector3 (destCas.transform.position.x, 
			destCas.transform.position.y + destCas.transform.localScale.y / 2, 
			destCas.transform.position.z);

		destCas.setOcupada (true);
		orCas.setOcupada (false);
	}
}
