using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public bool OnMouseUpAsButton () {
		Accuse ();
		CallSuspect ();
		return true;
	}

	public void CallSuspect () {
		if (GameManager.instance.CanInteract ()) {
			Player playerActive = GameManager.instance.getPlayerActive ();

			if (playerActive.GetPlace () != (GameManager.Place) this.getActualCas ().getType()) { //si no es la misma instancia...
				//move to a random block
				List<Casilla> l = GameManager.instance.getCasillasInPlace ((GameManager.Place) playerActive.GetPlace ());

				System.Random rnd = GameManager.instance.getRandomSeed ();

				int c = 0;
				Casilla cas = null;

				do {
					c = rnd.Next (0, l.Count);
					cas = l[c];
				} while (cas.getOcupada ());

				GameManager.instance.MoveTo (this.gameObject, this.getActualCas(), cas);
				setActualCas(cas);
				this.Accuse ();
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