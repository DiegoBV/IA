using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class Board : MonoBehaviour {

    private Matrix matriz;
    private Casilla[,] tablero; //en vez de game objects, deberia der de casillas
	public Casilla casillaPrefab;
    private GameManager manager;

    public void Initialize(GameManager manager)
    {
        this.manager = manager;
        matriz = new Matrix();
        matriz.Initialize();
        tablero = new Casilla[matriz.getRows(), matriz.getCols()];
        GenerateBoard();
    }

    void GenerateBoard()
    {
        int rows = matriz.getRows();
        int cols = matriz.getCols();

        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
				Casilla cas = Instantiate(casillaPrefab, new Vector3(j * casillaPrefab.gameObject.transform.localScale.x , 0, i * casillaPrefab.gameObject.transform.localScale.z), Quaternion.identity);
				tablero [i, j] = cas;
				cas.Initialize (this, new Position(i, j));
				cas.setType (matriz [i, j]);
                //hay que colocarlos benne equisde
				/*tablero[i,j].GetComponent<Casilla>.
				Casilla casComp = casillaPrefab.GetComponent<Casilla>();
				casComp.setPosition (new Position (i, j));
				tablero[i,j].GetComponent<Renderer>().material.SetColor("_Color", Color.blue);*/
            }
        }
    }

	public Casilla this[int k1, int k2]{
		get{
			return tablero [k1, k2];
		}
		set{
			tablero [k1, k2] = (Casilla)value;
		}
	}
}
