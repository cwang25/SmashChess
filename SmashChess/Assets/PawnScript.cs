using UnityEngine;
using System.Collections;

public class PawnScript : ChessmanScript
{

	public override bool[,] possibleMove(){
		bool[,] r = new bool[8, 8];
		ChessmanScript c, c2;
		Debug.Log ("Current pos: " + CurrentX + "," + CurrentY);
		if (!isWhite) {
			//diagonal left
			if (CurrentX != 0 && CurrentY != 7) {
				c = BoardManager.Instance.Chessmans [CurrentX - 1, CurrentY + 1];
				if (c != null && c.isWhite) {
					r [CurrentX - 1, CurrentY + 1] = true;
				}
			}
			//diagonal right
			if (CurrentX != 7 && CurrentY != 7) {
				c = BoardManager.Instance.Chessmans [CurrentX + 1, CurrentY + 1];
				if (c != null && c.isWhite) {
					r [CurrentX + 1, CurrentY + 1] = true;
				}
			}
			//middle
			if (CurrentY != 7){
				c = BoardManager.Instance.Chessmans [CurrentX, CurrentY + 1];
				if (c == null) {
					r [CurrentX, CurrentY + 1] = true;
				}
			}
			//middle first
			if(CurrentY == 1){
				c = BoardManager.Instance.Chessmans [CurrentX, CurrentY + 1];
				c2 = BoardManager.Instance.Chessmans [CurrentX, CurrentY + 2];
				if (c == null && c2 == null) {
					r [CurrentX, CurrentY + 2] = true;
				}
			}
		} else {
			//diagonal left
			if (CurrentX != 0 && CurrentY != 0) {
				c = BoardManager.Instance.Chessmans [CurrentX - 1, CurrentY - 1];
				if (c != null && !c.isWhite) {
					r [CurrentX - 1, CurrentY + 1] = true;
				}
			}
			//diagonal right
			if (CurrentX != 7 && CurrentY != 0) {
				c = BoardManager.Instance.Chessmans [CurrentX + 1, CurrentY - 1];
				if (c != null && !c.isWhite) {
					r [CurrentX + 1, CurrentY - 1] = true;
				}
			}
			//middle
			if (CurrentY != 0){
				c = BoardManager.Instance.Chessmans [CurrentX, CurrentY - 1];
				if (c == null) {
					r [CurrentX, CurrentY - 1] = true;
				}
			}
			//middle first
			if(CurrentY == 6){
				c = BoardManager.Instance.Chessmans [CurrentX, CurrentY - 1];
				c2 = BoardManager.Instance.Chessmans [CurrentX, CurrentY - 2];
				if (c == null && c2 == null) {
					r [CurrentX, CurrentY - 2] = true;
				}
			}
		}



		return r;


	}
}

