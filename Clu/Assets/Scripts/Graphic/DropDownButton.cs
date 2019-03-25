using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class DropDownMenuObject
{
    public string masterText;
    public string[] texts;
    public DeckManager.DeckElements [] elements;
    public int index;
}

public class DropDownButton : MonoBehaviour
{
    public DropDownMenuObject descriptor;
    public Text masterText;
    public GameObject panel;

    void Start()
    {
        this.Initialize();
    }

    void Initialize()
    {
        this.masterText.text = descriptor.masterText;

        int i = 0;
        foreach(Transform child in panel.transform)
        {
            if(child.gameObject.GetComponent<Button>() != null) //si tiene botones...
            {
                print("Boton: " + i);
                Text t = child.GetComponentInChildren<Text>();
                AccButton a = child.GetComponent<AccButton>();

                if(a != null)
                {
                    a.element = descriptor.elements[i];
                    a.index = this.descriptor.index;
                }

                if(t != null)
                {
                    t.text = this.descriptor.texts[i];
                    i++;
                }
            }
        }
    }
}
