using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

	public void setDialog(DialogObject dialog) 
	{
		generalText.text = dialog.genText;
		confText.text = dialog.confText;
		canText.text = dialog.cancText;
		confButton.onClick.RemoveAllListeners ();
		confButton.onClick.AddListener (dialog.confEv.Invoke);
	}
}
