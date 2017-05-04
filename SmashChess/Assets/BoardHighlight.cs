using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardHighlight : MonoBehaviour
{
	public static BoardHighlight Instance{ set; get;}
	public GameObject HighlightPrefab;
	private List<GameObject> highlights;
	private void Start(){
		Instance = this;
		highlights = new List<GameObject> ();
	}

	private GameObject getHighLightObject (){
		GameObject go = highlights.Find (g => !g.activeSelf);
		if (go == null) {
			go = Instantiate (HighlightPrefab);
			highlights.Add (go);
		}
		return go;
	} 

	public void highlightAollowedMoves(bool [,] moves){
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if (moves [i, j]) {
					GameObject go = getHighLightObject ();
					go.SetActive (true);
					Vector3 v3 = BoardManager.translateToWorldCoord (new Vector2 ((float)i, (float)j));
					v3.y = 0.1f;
					go.transform.position = v3;
				}
			}
		}
	}

	public void HideHighlights(){
		foreach (GameObject go in  highlights) {
			go.SetActive (false);
		}
	}
}

