using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Clase sospechoso, tiene toda la funcionalidad especificada en el enunciado, como ser llamado a una sala 
o ser clickado por el usuario humano
 */
public class Sospechoso : MonoBehaviour {

	private int typeCasilla; //casilla sobre la que se encuentra
	private Casilla actualCas;
	public int type;

	public void setActualCas(Casilla c){
		this.actualCas = c;
	}
	public Casilla getActualCas(){
		return this.actualCas;
	}
	//click por el usuario
	public bool OnMouseUpAsButton () {
		Accuse ();
        if(GameManager.instance.getPlayerActive().UserControlled)
		    CallSuspect ();
		return true;
	}

	//se mueve a una casilla random de la sala en la que se encuentra el jugador que le llama.
	public void CallSuspect () {
		if (GameManager.instance.CanInteract ()) {
			Player playerActive = GameManager.instance.getPlayerActive ();

			if (playerActive.getCurrNumAct() < playerActive.numMaxActions && playerActive.GetPlace () != (GameManager.Place) this.getActualCas ().getType()) { //si no es la misma instancia...
				//move to a random block
				List<Casilla> l = GameManager.instance.getCasillasInPlace ((GameManager.Place) playerActive.GetPlace ());

				System.Random rnd = GameManager.instance.getRandomSeed ();

				int c = 0;
				Casilla cas = null;

				do {
					c = rnd.Next (0, l.Count);
					cas = l[c];
				} while (cas.getOcupada ());
                Casilla prevCas = this.getActualCas();
				setActualCas(cas);
				GameManager.instance.MoveTo (this.gameObject, prevCas, cas);
				this.Accuse ();
                playerActive.increaseCount();
			}

		}
	}

	public void Accuse () {
		if (GameManager.instance.CanInteract ()) {
			Player playerActive = GameManager.instance.getPlayerActive ();
			if (playerActive.GetPlace () == (GameManager.Place) this.getActualCas ().getType()) {
				print ("Me acusan, a mi, a " + this.tag);
			}
		}
	}

	public int getType(){
		return this.type;
	}
}