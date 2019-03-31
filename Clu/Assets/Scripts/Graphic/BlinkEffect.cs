using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{
    public float time;
    public Player charac; //blink cant be applied to other user controlled characters 
    public DeckManager.DeckElements index;

    private Text text;
    private bool started = false; //sorry
    // Use this for initialization
    void Awake()
    {
        text = this.GetComponentInChildren<Text>();
    }

    void OnDisable()
    {
        started = false;
    }

    void OnEnable()
    {
        if (!started && !charac.getMyCards().Contains(index)) // lazy eval?
        {
            StartBlinking();
            started = true;
        }
        else if (charac.getMyCards().Contains(index))
        {
            StopBlinking();
            text.color = new Color(text.color.r, text.color.g,
                        text.color.b, 1);
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            switch (text.color.a.ToString())
            {
                case "0":
                    text.color = new Color(text.color.r, text.color.g,
                        text.color.b, 1);
                    yield return new WaitForSeconds(time);
                    break;
                case "1":
                    text.color = new Color(text.color.r, text.color.g,
                        text.color.b, 0);
                    yield return new WaitForSeconds(time);
                    break;
            }
        }
    }

    public void StartBlinking()
    {
        StopCoroutine("Blink");
        StartCoroutine("Blink");
    }

    public void StopBlinking()
    {
        StopCoroutine("Blink");
    }
}
