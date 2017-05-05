using UnityEngine;
using System.Collections;

public class RookScript : ChessmanScript
{
	public override bool[,] possibleMove(){
		bool[,] r = new bool[8, 8];
		ChessmanScript c;
		int i;
		i = CurrentX;
		//right
		while (true) {
			i++;
			if (i >= 8)
				break;
			c = BoardManager.Instance.Chessmans [i, CurrentY];
			if (c == null) {
				r [i, CurrentY] = true;
			} else {
				if (c.isWhite != isWhite) {
					r [i, CurrentY] = true;
				}
				break;
			}

		}

		i = CurrentX;
		//left
		while (true) {
			i--;
			if (i < 0)
				break;
			c = BoardManager.Instance.Chessmans [i, CurrentY];
			if (c == null) {
				r [i, CurrentY] = true;
			} else {
				if (c.isWhite != isWhite) {
					r [i, CurrentY] = true;
				}
				break;
			}

		}

		i = CurrentY;
		//up
		while (true) {
			i++;
			if (i >= 8)
				break;
			c = BoardManager.Instance.Chessmans [CurrentX, i];
			if (c == null) {
				r [CurrentX, i] = true;
			} else {
				if (c.isWhite != isWhite) {
					r [CurrentX, i] = true;
				}
				break;
			}

		}

		i = CurrentY;
		//down
		while (true) {
			i--;
			if (i < 0)
				break;
			c = BoardManager.Instance.Chessmans [CurrentX, i];
			if (c == null) {
				r [CurrentX, i] = true;
			} else {
				if (c.isWhite != isWhite) {
					r [CurrentX, i] = true;
				}
				break;
			}

		}
		return r;
	}
}

