using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Model;

public class Board : MonoBehaviour {

    private Matrix matriz;
    private Casilla[,] tablero; //en vez de game objects, deberia der de casillas
	public Casilla casillaPrefab;
    private GameManager manager;
	public GameObject[] personajesPrefab;
	private GameObject[] sospechosos;  //el de arriba es de prefab, no se toca
	private int rows;
	private int cols;

    public void Initialize(GameManager manager)
    {
        this.manager = manager;
        matriz = new Matrix();
        matriz.Initialize();
		sospechosos = new GameObject[personajesPrefab.GetLength (0)];
		tablero = new Casilla[matriz.getRows()*matriz.getRows(), matriz.getCols()*matriz.getCols()];
		rows = tablero.GetLength (0);
		cols = tablero.GetLength (1);
        GenerateBoard();
		GenerateCharacters ();
    }

    void GenerateBoard()
    {
        int rows = matriz.getRows();
        int cols = matriz.getCols();

        for(int i = 0; i < rows*rows; i++)
        {
            for(int j = 0; j < cols*cols; j++)
            {
				Casilla cas = Instantiate(casillaPrefab, new Vector3(j * casillaPrefab.gameObject.transform.localScale.x*1.1f , 0, -i * casillaPrefab.gameObject.transform.localScale.z*1.1f), Quaternion.identity);
				tablero [i, j] = cas;
				cas.Initialize (this, new Position(i, j));
				cas.setType (matriz[i/matriz.getRows(), j/matriz.getCols()]);
            }
        }
    }

	void GenerateCharacters()
	{
		System.Random rnd = matriz.getRandomSeed ();

		for (int i = 0; i < personajesPrefab.GetLength (0); i++) {
			int k1 = 0;
			int k2 = 0;

			do{
				k1 = rnd.Next (0, rows);
				k2 = rnd.Next (0, cols);
			}while(tablero[k1, k2].getOcupada());

			Casilla cas = tablero [k1, k2];
			cas.setOcupada (true);
			GameObject o = Instantiate (personajesPrefab [i]);
			o.transform.position = new Vector3 (cas.transform.position.x, cas.transform.position.y + cas.transform.localScale.y/2, cas.transform.position.z);
			sospechosos [i] = o;
		}
	}

	public Casilla this[int k1, int k2]{
		get{
			return tablero [k1, k2];
		}
		set{
			tablero [k1, k2] = value;
		}
	}
}
