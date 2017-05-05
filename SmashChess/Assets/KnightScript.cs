using UnityEngine;
using System.Collections;

public class KnightScript : ChessmanScript
{
	public override bool[,] possibleMove(){
		bool[,] r = new bool[8, 8];
		knightMoveValid (CurrentX - 1, CurrentY + 2,  ref r); 
		knightMoveValid (CurrentX + 1, CurrentY + 2,  ref r); 
		knightMoveValid (CurrentX + 2, CurrentY + 1,  ref r); 
		knightMoveValid (CurrentX + 2, CurrentY - 1,  ref r); 
		knightMoveValid (CurrentX - 1, CurrentY - 2,  ref r); 
		knightMoveValid (CurrentX + 1, CurrentY - 2,  ref r); 
		knightMoveValid (CurrentX - 2, CurrentY + 1,  ref r); 
		knightMoveValid (CurrentX - 2, CurrentY - 1,  ref r); 

		return r;
	}

	public void knightMoveValid(int x, int y, ref bool[,] r){
		ChessmanScript c;
		if (x < 8 && x >= 0 && y < 8 && y >= 0) {
			c = BoardManager.Instance.Chessmans [x, y];
			if (c == null || isWhite != c.isWhite)
				r [x, y] = true;
		}
	}
}

