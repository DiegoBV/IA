﻿using System.Collections;
using System.Collections.Generic;
using System;

/*
Clase matrix, almacena una matriz de dos dimensiones de las dimensiones
elegidas al construirla. Se puede iniciar a un valor por defecto
o a valores aleatorios no repetidos.
 */
namespace Model
{
    public class Matrix
    {

        private int[,] matrix;
        private int rows;
        private int cols;

        private System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());

        public Matrix(int r = 3, int c = 3) { rows = r; cols = c; }

        public void Initialize() //generacion aleatoria de la matriz
        {
            matrix = new int[rows, cols];
            List<int> listNumbers = new List<int>();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int number = 0;

                    do
                    {
                        number = rnd.Next(0, rows * cols);
                    } while (listNumbers.Contains(number));

                    listNumbers.Add(number);
                    matrix[i, j] = number;
                }
            }
        }

        public void Initialize(int value = 0) //generacion a partir de un valor por defecto
        {
            matrix = new int[rows, cols];
            
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    matrix[i, j] = value;
                } 
            }
        }

        //gets, sets, operador [,]
        public int getRows() { return rows; }
        public int getCols() { return cols; }

		public int this[int k1, int k2]{
			get{
				return matrix [k1, k2];
			}
			set{
				matrix [k1, k2] = value;
			}
		}
    }
}
