using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//clase objeto de dialogo, util para tener diferentes modales en el juego
[System.Serializable]
public class DialogObject{
	public string confText;
	public string cancText;
	public string genText;
	public UnityEvent confEv;
}
public class Modal : MonoBehaviour {

	public Button confButton;
	public Text generalText;
	public Text confText;
	public Text canText;

	//set de los parametros del modal
	public void setDialog(DialogObject dialog) 
	{
		generalText.text = dialog.genText;
		confText.text = dialog.confText;
		canText.text = dialog.cancText;
		confButton.onClick.RemoveAllListeners ();
		confButton.onClick.AddListener (dialog.confEv.Invoke);
	}
}
