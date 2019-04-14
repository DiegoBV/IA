using System.Collections;
using System.Collections.Generic;

/*
clase posicion, util para las casillas del tablero
 */
namespace Model
{
    public class Position
    {
        private int row;
        private int col;

        public Position(int r, int c) { row = r; col = c; }

        public int getRow() { return row; }
        public int getCol() { return col; }

		public override string ToString ()
		{
			return "(" + row + ", " + col + ")";
		}
    }
}