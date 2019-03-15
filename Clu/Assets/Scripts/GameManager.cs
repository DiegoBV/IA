﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
	public DialogObject ResetDialog;
	public DialogObject QuitDialog;
    public Board tablero;
	public Color[] colors;
	public static GameManager instance = null;
	public GameObject modalDialog;
    //lo del instance....

	// Use this for initialization
	void Awake () {
        this.Initialize();
		instance = this;
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
	}

	public void activeModalDialogQuit()
	{
		modalDialog.SetActive (true);
		modalDialog.GetComponent<Modal> ().setDialog (QuitDialog);
	}

	public void cancelModalDialog()
	{
		modalDialog.SetActive (false);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}