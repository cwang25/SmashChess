using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
	int selectionX = -1;
	int selectionY = -1;

	float TILE_SIZE = 1.0f;
	float TILE_OFFSET = 0.5f;
	// Use this for initialization
	void Start () {
		Debug.Log ("Started");
	}
	
	// Update is called once per frame
	void Update () {
		
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100.0f, LayerMask.GetMask("ChessPlane"))) {
			Debug.Log (hit.point);
		}
	}
}
