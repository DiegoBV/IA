using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class SmartIA : MonoBehaviour
{

    public void Act(Player p)
    {
        int sos = 0, wep= 0;

        if (!p.getMyCards().Contains((DeckManager.DeckElements)(p.getActualCas().getType())))
        {
            //Si no tienes la sala actual llamas a un sospechoso que no tengas
            sos = call(p) ;
        }
        else
        {
            //mueve a una que no tenga (preferiblemente con gente)
            //mueve
            DeckManager.DeckElements[] sol = checkOptions(p);
                moveTo(p, sol[0]);
            if(sol[0] != sol[1])
            {
                sos = (int)sol[1];
            }
            else
            {
                List<Sospechoso> l =
                    GameManager.instance.IsSomeoneInMyPlace((GameManager.Place)sol[0]);
                //ACUSAR A JUGADOR EN MAZO INICIAL


                if (l.Count > 0)
                {
                    bool found = false;
                    foreach(Sospechoso Sosp in l) //Miro si tengo alguno en mi lista inicial
                    {
                        if (p.GetSuspectList().getInitSuspetcs()[Sosp.getType()])
                        {
                            sos = Sosp.getType() + (int)DeckManager.DeckElements.Terraza + 1;
                            found = true;
                            break;
                        }
                    }
                    if(!found) //
                        sos = l[0].getType() + (int)DeckManager.DeckElements.Terraza + 1;

                }
                else return;
            }
        }
        
        //ELIGES ARMA
        wep = chooseWeapon(p);
        //acuse
        GameManager.instance.makeAccusation((DeckManager.DeckElements)(sos), 1);
        GameManager.instance.makeAccusation((DeckManager.DeckElements) wep, 2);

        bool b = false;
        GameManager.instance.showCard.text = this.gameObject.name + " suggests: " + (DeckManager.DeckElements)GameManager.instance.getPlayerActive().getActualCas().getType() + " " +
            (DeckManager.DeckElements)sos + " " + (DeckManager.DeckElements)wep + " ";
        GameManager.instance.Suggest(out b);
        if (b) { GameManager.instance.Accuse(); }
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

                sol[0] = (DeckManager.DeckElements)index;
                sol[1] = (DeckManager.DeckElements)index;
                bool[] sospechosos = p.GetSuspectList().getSuspetcs();
                foreach(Sospechoso s in l)
                {
                    if (!sospechosos[s.getType()])
                    {
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
        /*bool test = true;
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
        } while (test);*/

        List<Casilla> lst = GameManager.instance.tablero.getCasillasOfType((int)room);
        
        foreach(Casilla cas in lst)
        {
            if (!cas.getOcupada())
            {
                p.Move(cas.getPosition());
                return;
            }
        }
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

    private int chooseWeapon(Player p)
    {
        //arma
        int arma = -1;
        do
        {
            arma++;
        } while (p.getMyCards().Contains((DeckManager.DeckElements)
            (arma + (int)DeckManager.DeckElements.Cnel_Rubio + 1)));

        return (arma + (int)DeckManager.DeckElements.Cnel_Rubio + 1);
    }
}
