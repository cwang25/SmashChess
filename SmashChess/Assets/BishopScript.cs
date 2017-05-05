﻿using UnityEngine;
using System.Collections;

public class BishopScript : ChessmanScript
{
	public override bool[,] possibleMove ()
	{
		bool[,] r = new bool[8, 8];
		ChessmanScript c;
		int i, j;

		//top left
		i = CurrentX;
		j = CurrentY;
		while (true) {
			i--;
			j++;
			if (i < 0 || j >= 8)
				break;
			c = BoardManager.Instance.Chessmans [i, j];
			if (c == null) {
				r [i, j] = true;
			} else {
				if (c.isWhite != isWhite) {
					r [i, j] = true;
				}
				break;
			}
		}

		//top right
		i = CurrentX;
		j = CurrentY;
		while (true) {
			i++;
			j++;
			if (i >= 8 || j >= 8)
				break;
			c = BoardManager.Instance.Chessmans [i, j];
			if (c == null) {
				r [i, j] = true;
			} else {
				if (c.isWhite != isWhite) {
					r [i, j] = true;
				}
				break;
			}
		}

		//down left
		i = CurrentX;
		j = CurrentY;
		while (true) {
			i--;
			j--;
			if (i < 0 || j < 0)
				break;
			c = BoardManager.Instance.Chessmans [i, j];
			if (c == null) {
				r [i, j] = true;
			} else {
				if (c.isWhite != isWhite) {
					r [i, j] = true;
				}
				break;
			}
		}

		//down right
		i = CurrentX;
		j = CurrentY;
		while (true) {
			i++;
			j--;
			if (i >= 8|| j<0)
				break;
			c = BoardManager.Instance.Chessmans [i, j];
			if (c == null) {
				r [i, j] = true;
			} else {
				if (c.isWhite != isWhite) {
					r [i, j] = true;
				}
				break;
			}
		}

		return r;
	}
}

