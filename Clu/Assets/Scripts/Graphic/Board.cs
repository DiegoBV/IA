﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Model;

public class Board : MonoBehaviour {

    private Matrix matriz;
    private Casilla[,] tablero; //en vez de game objects, deberia der de casillas
	public Casilla casillaPrefab;
    private GameManager manager;
	private GameObject[] sospechosos;  //el de arriba es de prefab, no se toca
	private int rows;
	private int cols;
	System.Random rnd;

    public void Initialize(GameManager manager)
    {
		DestroyBoard ();
		DestroyCharacters ();
        this.manager = manager;
        matriz = new Matrix();
        matriz.Initialize();
		rnd = matriz.getRandomSeed ();
		rows = matriz.getRows();
		cols = matriz.getCols();
        GenerateBoard();
		GenerateCharacters ();
    }

    void GenerateBoard()
    {
		tablero = new Casilla[rows*rows, cols*cols];

		for(int i = 0; i < tablero.GetLength(0); i++)
        {
			for(int j = 0; j < tablero.GetLength(1); j++)
            {
				Casilla cas = Instantiate(casillaPrefab, new Vector3(j * casillaPrefab.gameObject.transform.localScale.x*1.1f , 0, -i * casillaPrefab.gameObject.transform.localScale.z*1.1f), Quaternion.identity);
				tablero [i, j] = cas;
				cas.Initialize (this, new Position(i, j));
				cas.setOcupada (false);
				cas.setType (matriz[i/matriz.getRows(), j/matriz.getCols()]);
            }
        }
    }

	Casilla getNoOcupCasilla(){
		int k1 = 0;
		int k2 = 0;

		do{
			k1 = rnd.Next (0, tablero.GetLength(0));
			k2 = rnd.Next (0, tablero.GetLength(1));
		}while(tablero[k1, k2].getOcupada());

		Casilla cas = tablero [k1, k2];

		return cas;
	}

	void GenerateCharacters()
	{
		sospechosos = new GameObject[GameManager.instance.sospechososPrefab.GetLength (0)];

		for (int i = 0; i < GameManager.instance.sospechososPrefab.GetLength (0); i++) {
			Casilla cas = getNoOcupCasilla ();
			cas.setOcupada (true);
			GameObject o = Instantiate (GameManager.instance.sospechososPrefab [i]);
			o.transform.position = new Vector3 (cas.transform.position.x, cas.transform.position.y + cas.transform.localScale.y/2, cas.transform.position.z);
			sospechosos [i] = o;
			sospechosos [i].GetComponent<Sospechoso> ().setType (cas.getType());
		}

		for(int i = 0; i < GameManager.instance.characters.GetLength(0); i++){
			Casilla cas = getNoOcupCasilla ();
			cas.setOcupada (true);
			GameManager.instance.characters[i].transform.position = new Vector3 (cas.transform.position.x, cas.transform.position.y + cas.transform.localScale.y/2, cas.transform.position.z);
		}
	}

	void DestroyBoard()
	{
		if (tablero != null) { //if called the first time, does nothing
			for (int i = 0; i < tablero.GetLength(0); i++)
				for (int j = 0; j < tablero.GetLength(1); j++) {
					if (tablero [i, j] != null) {
						Destroy (tablero [i, j].gameObject);
					}
				}
		}
	}

	void DestroyCharacters()
	{
		if (sospechosos != null) { //same as above
			for(int i = 0; i < sospechosos.GetLength(0); i++){
				if (sospechosos [i] != null) {
					Destroy (sospechosos [i].gameObject);
				}
			}
		}
	}

	public Casilla this[int k1, int k2]{
		get{
			return tablero [k1, k2];
		}
		set{
			tablero [k1, k2] = value;
		}
	}

	public GameManager getManager(){
		return this.manager;
	}

	public GameObject[] getSospechosos(){
		return sospechosos;
	}
}
