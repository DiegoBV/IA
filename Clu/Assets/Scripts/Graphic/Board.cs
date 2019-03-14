using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class Board : MonoBehaviour {

    private Matrix matriz;
    private GameObject[,] tablero; //en vez de game objects, deberia der de casillas
    public /*Casilla*/ GameObject casillaPrefab;
    private GameManager manager;

	// Use this for initialization
	void Start () {
        
      //colocar casillas
	}

    public void Initialize(GameManager manager)
    {
        this.manager = manager;
        matriz = new Matrix();
        matriz.Initialize();
        tablero = new GameObject[matriz.getRows(), matriz.getCols()];
        Debug.Log("hey");

        GenerateBoard();
        //hacer
    }

    void GenerateBoard()
    {
        int rows = matriz.getRows();
        int cols = matriz.getCols();

        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                tablero[i, j] = Instantiate(casillaPrefab, new Vector3(j * casillaPrefab.transform.localScale.x * 2, 0, i * casillaPrefab.transform.localScale.z * 2), Quaternion.identity);
                //hay que colocarlos benne equisde
                //casilla.Position = i, j
                //casilla.Tipo = matrix[i, j]
            }
        }
    }
}
