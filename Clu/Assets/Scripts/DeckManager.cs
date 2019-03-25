﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class DeckManager : MonoBehaviour {
	private Deck deck_;
	public int totalElements;
	public int[] types;
	private List<int> solution;
	public enum DeckElements {Biblioteca, Cocina, Comedor, Estudio, Pasillo,              //igual es mejor ponerlo es un array publico de strings depende de lo demas
	Recibidor, Sala_De_Billar, Salon_De_Baile, Terraza, Sra_Amapola, Sra_Blanco,
	Sra_Celeste, Prof_Mora, Prof_Prado, Cnel_Rubio, Candelabro, Cuerda, Herramienta,
	Pistola, Puñal, Tuberia_De_Plomo};
	public int cardsPerPlayer;
	
	// Use this for initialization
	void Awake () {
		deck_ = new Deck(totalElements, types);
	}

	public void Initialize(){
		deck_.Reset ();
		solution = deck_.getSol ();
		print ("Solucion al puzzle: " + (DeckElements)solution [0] + " " + (DeckElements)solution [1] + " " + (DeckElements)solution [2]);
		this.Deal ();
	}

	public List<int> GetSolution(){
		return this.solution;
	}

	public void Deal(){  //deal "cardperplayer" cards
		for (int i = 0; i < GameManager.instance.players.GetLength (0); i++) {
			List<DeckElements> l = new List<DeckElements> ();
			for (int j = 0; j < cardsPerPlayer; j++) {
				l.Add((DeckElements)deck_.PickCard ());
			}
			GameManager.instance.players [i].setMyCards (l);
		}
	}
}
