using UnityEngine;
using System.Collections;

public class KingScript : ChessmanScript
{
	public override bool[,] possibleMove ()
	{
		bool[,] r = new bool[8, 8];
		ChessmanScript c;
		int i, j;
		i = CurrentX-1;
		j = CurrentY+1;
		//top side
		if (CurrentY != 7) {
			for (int k = 0; k < 3; k++) {
				if (i >= 0 || i < 8) {
					c = BoardManager.Instance.Chessmans [i, j];
					if (c == null || isWhite != c.isWhite) {
						r [i, j] = true;
					}
				}
				i++;
			}
		}

		i = CurrentX-1;
		j = CurrentY-1;
		//down side
		if (CurrentY != 0) {
			for (int k = 0; k < 3; k++) {
				if (i >= 0 || i < 8) {
					c = BoardManager.Instance.Chessmans [i, j];
					if (c == null || isWhite != c.isWhite) {
						r [i, j] = true;
					}
				}
				i++;
			}
		}
		//left
		if (CurrentX != 0) {
			c = BoardManager.Instance.Chessmans [CurrentX - 1, CurrentY];
			if (c == null || c.isWhite != isWhite) {
				r [CurrentX - 1, CurrentY] = true;
			}
		}

		//right
		if (CurrentX != 7) {
			c = BoardManager.Instance.Chessmans [CurrentX + 1, CurrentY];
			if (c == null || c.isWhite != isWhite) {
				r [CurrentX + 1, CurrentY] = true;
			}
		}
		return r;
	}
}

