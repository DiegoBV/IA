using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
