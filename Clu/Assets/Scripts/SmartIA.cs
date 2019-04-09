using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class SmartIA : MonoBehaviour
{

    public void Act(Player p)
    {
        System.Random rnd = GameManager.instance.getRandomSeed();

        if (!p.getMyCards().Contains((DeckManager.DeckElements)(p.getActualCas().getType())))
        {
            bool test = true;
            do
            {
                int x = rnd.Next(0, GameManager.instance.getRows());
                int y = rnd.Next(0, GameManager.instance.getCols());
                Position po = new Position(x, y);
                test = p.Move(po);
            } while (test);
        }
        else
        {
            //mueve a una que no tenga (preferiblemente con gente)
            //mueve
            DeckManager.DeckElements[] sol = checkOptions(p);
            if(sol[0] != sol[1])
            {
                moveTo(p, sol[0]);
                GameManager.instance.makeAccusation((DeckManager.DeckElements)
                    (sol[1]), 1);
                chooseWeapon(p);
            }
            else
            {
                int s = call(p); //llama al primer sospechoso que no tenga
                GameManager.instance.makeAccusation((DeckManager.DeckElements)
                    (s), 1);
                chooseWeapon(p);
            }
            //acuse
        }
    }

    private void Suggest(Player p, System.Random rnd)
    {
        //sospechoso
        List<Sospechoso> l = GameManager.instance.IsSomeoneInMyPlace(p.GetPlace());
        bool found = false;
        int i = 0;
        Sospechoso s = null;
        while (i < l.Count && !found)
        {
            if (!p.getMyCards().Contains((DeckManager.DeckElements)(l[i].getType() + (int)DeckManager.DeckElements.Terraza + 1)))
            {
                s = l[i];
                found = true;
            }

            i++;
        }

        if (s != null) //si puede acusar a un sospechoso
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
        else if (l.Count > 0)
        {
            //pasa turno
            GameManager.instance.changeTurn(p.order);
        }
    }

    private DeckManager.DeckElements[] checkOptions(Player p)
    {
        DeckManager.DeckElements[] sol = new DeckManager.DeckElements[2];
        bool[] rooms = p.GetSuspectList().getRooms();
        int index = 0;
        foreach(bool r in rooms)
        {
            if (!r)
            {
                List<Sospechoso> l = 
                    GameManager.instance.IsSomeoneInMyPlace((GameManager.Place)index);

                bool[] sospechosos = p.GetSuspectList().getSuspetcs();
                foreach(Sospechoso s in l)
                {
                    if (!sospechosos[s.getType()])
                    {
                        sol[0] = (DeckManager.DeckElements)index;
                        sol[1] = (DeckManager.DeckElements)s.getType() 
                            + (int)DeckManager.DeckElements.Terraza + 1;
                        return sol;
                    }
                }
            }
            index++;
        }

        //si llega aqui, significa que no hay habitaciones que no tenga con sospechosos que tampoco tenga
        return sol;
    }

    private void moveTo(Player p, DeckManager.DeckElements room)
    {
        bool test = true;
        do
        {
            System.Random rnd = GameManager.instance.getRandomSeed();
            int x = rnd.Next(0, GameManager.instance.getRows());
            int y = rnd.Next(0, GameManager.instance.getCols());
            Position pos = new Position(x, y);
            test = p.Move(pos);
            if(p.getActualCas().getType() != (int)room)
            {
                test = false;
            }
        } while (test);
    }

    private int call(Player p)
    {
        //llama
        int sospechoso = -1;
        do
        {
            sospechoso++;
        } while (p.getMyCards().Contains((DeckManager.DeckElements)(sospechoso + (int)GameManager.Place.Terraza + 1)));

        GameManager.instance.tablero.getSospechosos()[sospechoso].GetComponent<Sospechoso>().CallSuspect();

        return sospechoso + (int)GameManager.Place.Terraza + 1;
    }

    private void chooseWeapon(Player p)
    {
        //arma
        int arma = -1;
        do
        {
            arma++;
        } while (p.getMyCards().Contains((DeckManager.DeckElements)
            (arma + (int)DeckManager.DeckElements.Cnel_Rubio + 1)));

        GameManager.instance.makeAccusation((DeckManager.DeckElements)
            (arma + (int)DeckManager.DeckElements.Cnel_Rubio + 1), 2);
    }
}
