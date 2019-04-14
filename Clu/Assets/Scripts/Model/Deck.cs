using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Clase deck, maneja la parte logica de la baraja, COMO YA HA SIDO EXPLICADO EN LA MEMORIA DE LA PRACTICA.
 */
namespace Model {
	public class Deck {

		private List<int> deck_;
		private int numElem_;
		private int[] difElem_;
		private List<int> sol_;

        public List<int> getDeck()
        {
            return deck_;
        }

		public Deck (int numElem, int[] difElem) { //numero de elementos totales y array que indica el numero total de cada tipo diferente que existe (estancia, sospechosos, objetos, puede haber mas)
			this.numElem_ = numElem;
			this.difElem_ = difElem;
		}

		//la baraja tiene q estar ordenada por categorias para que este metodo funcione. De la baraja ordenada, coge
		//3 elementos diferentes y consigue la solucion
		private List<int> ChooseSolution () {
			List<int> newSol = new List<int> ();
			int prevElem = 0;
			for (int i = 0; i < this.difElem_.GetLength (0); i++) {
				int e = GameManager.instance.getRandomSeed ().Next (prevElem, prevElem + this.difElem_[i]);
				newSol.Add (e);
				prevElem = prevElem + this.difElem_ [i];
				deck_.Remove (e);
			}
			return newSol;
		}

		//rellena con numeros
		private void FillDeck () {
			deck_ = new List<int> ();
			for (int i = 0; i < this.numElem_; i++) {
				deck_.Add (i);
			}
		}

		//barajar la deck
		private void Shuffle () {
			for (int i = 0; i < this.deck_.Count; i++) {
				int rand = i + GameManager.instance.getRandomSeed ().Next (0, this.deck_.Count - i);
				//no me deja el swap no ref?
				int temp = this.deck_ [i];
				this.deck_ [i] = this.deck_ [rand];
				this.deck_ [rand] = temp;
			}
		}

		//devuelve la primera carta y la elimina de la baraja
		public int PickCard () {
			if (this.deck_.Count > 0) {
				int card = this.deck_[0];
				this.deck_.RemoveAt (0);
				return card;
			} else {
				return -1;
			}
		}
		
		//metodos auxiliares
		
		private void Swap (int x, int y) {
			int temp = x;
			x = y;
			y = temp;
		}

		public List<int> getSol () {
			return this.sol_;
		}

		public int this[int k1]{
			get{
				return deck_ [k1];
			}
		}

		public void Reset(){
			FillDeck ();
			this.sol_ = ChooseSolution ();
			Shuffle ();
		}

	}
}