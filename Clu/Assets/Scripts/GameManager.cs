using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Model;

public class GameManager : MonoBehaviour {
	public DialogObject ResetDialog;
	public DialogObject QuitDialog;
    public Board tablero;
	public Color[] colors;
	public static GameManager instance = null;
	public GameObject modalDialog;
	public GameObject[] sospechososPrefab;
	public GameObject[] characters;
	private Mechanics turn; //quien es el turno
	private bool modalsAreActive = false;
	[HideInInspector] public enum Place {Biblioteca, Cocina, Comedor, Estudio, Pasillo,
		Recibidor, Sala_del_billar, Salon_de_baile, Terraza};

	// Use this for initialization
	void Awake () {
		instance = this;
		this.Initialize();
		modalDialog.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Initialize()
    {
        tablero.Initialize(this);
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

	public Mechanics getPlayerActive(){
		return this.turn;
	}

	public void setPlayerActive(Mechanics t){
		this.turn = t;
	}

	public Place whereIsIt(int tipo)
	{
		return (Place)tipo;
	}

	public int IsSomeoneInMyPlace(Place p){ //maybe devolver a todos los sospechosos que esten
		int howMany = 0;
		for (int i = 0; i < tablero.getSospechosos ().GetLength (0); i++) {
			if ((Place)tablero.getSospechosos () [i].GetComponent<Sospechoso>().getType() == p) {
				howMany++;
			}
		}

		return howMany;
	}

	public bool CanInteract(){
		return !this.modalsAreActive;
	}
}
