using System.Collections;
using System.Collections.Generic;
using System;

namespace Model
{
	public class CasillaMatrix
	{

		private bool[,] matrix;
		private int rows;
		private int cols;

		public CasillaMatrix(int r = 3, int c = 3) { rows = r; cols = c; }

		public void Initialize() //generacion aleatoria de la matriz
		{
			matrix = new bool[rows, cols]; //defecto a false
		}

		public int getRows() { return rows; }

		public int getCols() { return cols; }
	}
}
