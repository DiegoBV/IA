using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccButton : MonoBehaviour
{
    [HideInInspector] public DeckManager.DeckElements element;
    [HideInInspector] public int index;

    public void selectOption()
    {
        GameManager.instance.makeAccusation(this.element, this.index);
    }
}
