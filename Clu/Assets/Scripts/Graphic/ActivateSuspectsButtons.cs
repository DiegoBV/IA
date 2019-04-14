using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Activa todos los elementos del menu contextual de sospechosos, dependiendo de
si se encuentran o no con el jugador en cuestion (mejor feedback)
 */
public class ActivateSuspectsButtons : MonoBehaviour {
	public GameObject panel;
	private List<GameObject> buttons;

	void Start(){
		buttons = new List<GameObject> ();
		foreach (Transform child in panel.transform) {
			buttons.Add (child.gameObject);
		}
	}

	public void Activate(){
		List<Sospechoso> l = GameManager.instance.getPlayerActive ().getSuspectsInPlace ();

		foreach (GameObject element in buttons) {
			element.SetActive (false);
		}

		if (l != null) {
			foreach (Sospechoso element in l) {
				buttons [element.getType ()].SetActive (true);
			}
		}
	}
}
