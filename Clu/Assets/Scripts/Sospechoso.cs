using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sospechoso : MonoBehaviour {

	private int typeCasilla; //casilla sobre la que se encuentra

	public void setType(int type){
		typeCasilla = type;
	}
	public int getType(){
		return this.typeCasilla;
	}
}
