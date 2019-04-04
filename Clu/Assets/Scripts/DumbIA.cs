﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class DumbIA : MonoBehaviour {

    public void Act(Player p)
    {
        System.Random rnd = GameManager.instance.getRandomSeed();

        int elecc = rnd.Next(0, 2);

        switch (elecc)
        {
            case 0:
                //mueve
                moveToRandom(p);
                //acuse
                Suggest(p, rnd);
                break;
            case 1:
                //llama
                int sospechoso = -1;
                do
                {
                    sospechoso = rnd.Next(0, GameManager.instance.sospechososPrefab.GetLength(0));
                } while (p.getMyCards().Contains((DeckManager.DeckElements)(sospechoso + (int)GameManager.Place.Terraza + 1)));

                GameManager.instance.tablero.getSospechosos()[sospechoso].GetComponent<Sospechoso>().CallSuspect();
                //accuse
                Suggest(p, rnd);
                break;
            default:
                break;
        }
    }

    private void Suggest(Player p, System.Random rnd)
    {
        //sospechoso
        List<Sospechoso> l = GameManager.instance.IsSomeoneInMyPlace(p.GetPlace());
        bool found = false;
        int i = 0;
        Sospechoso s = null;
        while(i < l.Count && !found)
        {
            if(!p.getMyCards().Contains((DeckManager.DeckElements)(l[i].getType() + (int)DeckManager.DeckElements.Terraza + 1)))
            {
                s = l[i];
                found = true;
            }

            i++;
        }

        if(s != null) //si puede acusar a un sospechoso
        {
            //arma
            int arma = -1;
            do
            {
                arma = rnd.Next(0, 6);
            } while (p.getMyCards().Contains((DeckManager.DeckElements)(arma + (int)DeckManager.DeckElements.Cnel_Rubio + 1)));

            GameManager.instance.makeAccusation((DeckManager.DeckElements)(s.getType() + (int)GameManager.Place.Terraza + 1), 1);
            GameManager.instance.makeAccusation((DeckManager.DeckElements)(arma + (int)DeckManager.DeckElements.Cnel_Rubio + 1), 2);
            GameManager.instance.showCard.text = this.gameObject.name + " suggests: " + (DeckManager.DeckElements)GameManager.instance.getPlayerActive().getActualCas().getType() + " " + 
                (DeckManager.DeckElements)(s.getType() + (int)GameManager.Place.Terraza + 1) + " " + (DeckManager.DeckElements)(arma + (int)DeckManager.DeckElements.Cnel_Rubio + 1) + " ";
            bool b = false;
            GameManager.instance.Suggest(out b);
            if (b) { GameManager.instance.Accuse(); }
        }
        else if(l.Count > 0)
        {
            //pasa turno
            GameManager.instance.changeTurn(p.order);
        }
    }

    private void moveToRandom(Player pl)
    {
        bool test = true;
        do
        {
            System.Random rnd = GameManager.instance.getRandomSeed();
            int x = rnd.Next(0, GameManager.instance.getRows());
            int y = rnd.Next(0, GameManager.instance.getCols());
            Position p = new Position(x, y);
            test = pl.Move(p);
        } while (test);
    }

}
