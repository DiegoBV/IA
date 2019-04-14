using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Boton que vale tanto para sugerir como para acusar (se reutiliza,
cambia el color, texto y funcion). Hace uso de metodos del GameManager
para realizar ambas funciones.
 */
public class AccButton : MonoBehaviour
{
    [HideInInspector] public DeckManager.DeckElements element;
    [HideInInspector] public int index;
    public GameObject parent;
    public Color c;

    public void selectOption()
    {
        GameManager.instance.makeAccusation(this.element, this.index);

        foreach(Transform child in parent.transform)
        {
            child.gameObject.GetComponent<Image>().color = Color.white;
        }

        this.gameObject.GetComponent<Image>().color = c;
    }

    public void reset()
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}
