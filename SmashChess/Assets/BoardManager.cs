using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {
	public static BoardManager Instance {set; get;}
	private bool [,] allowedMoves { set; get; }

	//private selection coordinates

	int selectionX = -1;
	int selectionY = -1;
	public static float X_OFFSET = 22.2f;
	public static float Y_OFFSET = 22.2f;
	static float  TILE_SIZE = 5.5f;
	static float  TILE_OFFSET = 2.75f;
	public ChessmanScript[,] Chessmans { set; get; }
	private List<GameObject> activeChessmans;
	private ChessmanScript selectedChessman;
	public Text messageBoard;

	//Huge holders for all chessmans
	public GameObject blackRook;
	public GameObject blackBishop;
	public GameObject blackKnight;
	public GameObject blackPawn;
	public GameObject blackKing;
	public GameObject blackQueen;
	public GameObject whiteRook;
	public GameObject whiteBishop;
	public GameObject whiteKnight;
	public GameObject whitePawn;
	public GameObject whiteKing;
	public GameObject whiteQueen;
	// Use this for initialization
	bool isWhiteTurn = true;
	bool restarting = false;
	float restartTime = 5.0f;
	float restartTimer = 0;
	void Start () {
		Debug.Log ("Started");
		Instance = this;
		setUpBoard ();
	}
	
	// Update is called once per frame
	void Update () {
		if (restarting) {
			isWhiteTurn = true;
			restartTimer += Time.deltaTime;
			if (restartTimer >= restartTime) {
				foreach (GameObject go in activeChessmans) {
					if (go != null && go.GetComponent<ChessmanScript> () != null) {
						//go.GetComponent<ChessmanScript> ().SelfDestory();
						//activeChessmans.Remove (go);
						Destroy (go);
					}
				}
				BoardHighlight.Instance.HideHighlights ();
				setUpBoard ();
			}

			return;
		} else {
			UpdateSelection ();
			if (Input.GetMouseButtonDown (0)) {
				if (selectionX >= 0 && selectionY >= 0) {
					//Debug.Log (selectionX + " : " + selectionY+" : "+selectedChessman);
					if (selectedChessman == null) {
						//select Chessman
						SelectChessman(selectionX, selectionY);
					} else {
						//move Chessman
						MoveChessman(selectionX, selectionY);
					}
				}
			}
		}
	}
	private void UpdateSelection(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100.0f, LayerMask.GetMask ("ChessPlane"))) {
			Vector2 coordinates = translateToBoardIndices (new Vector2 (hit.point.x, hit.point.z));
			selectionX = (int)coordinates.x;
			selectionY = (int)coordinates.y;
			if (selectionX > 7)
				selectionX = 7;
			if (selectionY > 7)
				selectionY = 7;
			//Debug.Log (selectionX+":"+selectionY);
		} else {
			selectionX = -1;
			selectionY = -1;
			//Debug.Log (selectionX+":"+selectionY);
		}
	}
	public void SelectChessman(int x, int y){
		if (Chessmans [x, y] == null)
			return;
		//Debug.Log (Chessmans [x, y].isWhite);
		if (Chessmans [x, y].isWhite != isWhiteTurn)
			return;
		bool hasAtleastOne = false;
		allowedMoves = Chessmans [x, y].possibleMove ();
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8 && !hasAtleastOne; j++) {
				if (allowedMoves [i, j]) {
					hasAtleastOne = true;
					break;
				}
			}
		}
		if (!hasAtleastOne)
			return;
		selectedChessman = Chessmans [x, y];
		BoardHighlight.Instance.highlightAollowedMoves (allowedMoves);
	}
	public void MoveChessman(int x, int y){
		if (allowedMoves[x,y]) {
			ChessmanScript c = Chessmans [x, y];
			if (c != null && c.isWhite != isWhiteTurn) {
				if (c.GetType () == typeof(KingScript)) {
					EndGame ();
					return;
				}
				//somethign to kill
				Chessmans[x,y].becomeDestroyable();
				activeChessmans.Remove (Chessmans [x, y].gameObject);


			}
			Chessmans [selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
			//selectedChessman.SetDestination (translateToWorldCoord (new Vector2 ((float)x,(float)y)), 3);
			//selectedChessman.transform.position = translateToWorldCoord (new Vector2 ((float)x,(float)y));
			selectedChessman.setPosition (x, y);
			selectedChessman.SetDestination(translateToWorldCoord (new Vector2 ((float)x,(float)y)), 2);
			Chessmans [x, y] = selectedChessman;
			isWhiteTurn = !isWhiteTurn;
		}
		BoardHighlight.Instance.HideHighlights ();
		selectedChessman = null;
	}
	/** 
	Function to convert Vector(x,z) from real world indices to board Vector(x,y) left->right, front->back
	v.x = row;
	v.y = col;
	**/
	public static Vector2 translateToBoardIndices(Vector2 v){
		float newX = (int)((v.x + X_OFFSET) / TILE_SIZE);
		float newY = (int)((v.y + Y_OFFSET) / TILE_SIZE);
		Vector2 newVec = new Vector2 (newX, newY);
		return newVec;
	}
	/**
	 * v.x = row;
	 * v.y = col;
	 **/
	public static Vector3 translateToWorldCoord(Vector2 v){
		float worldX = (v.x * TILE_SIZE) - X_OFFSET + TILE_OFFSET;
		float worldZ = (v.y * TILE_SIZE) - Y_OFFSET + TILE_OFFSET;
		return new Vector3 (worldX, 0, worldZ);

	}
	void setUpBoard(){
		Chessmans = new ChessmanScript[8, 8];
		activeChessmans = new List<GameObject> ();
		restartTimer = 0;
		restarting = false;
		messageBoard.enabled = false;
		for (int i = 0; i < 8; i++) {
			GameObject pawnsB = Instantiate (blackPawn);
			pawnsB.transform.position = translateToWorldCoord(new Vector2(i,1));
			pawnsB.SetActive (true);
			GameObject pawnsW = Instantiate (whitePawn);
			pawnsW.transform.position = translateToWorldCoord(new Vector2(i,6));
			pawnsW.SetActive (true);
			Chessmans [i,1] = pawnsB.GetComponent<ChessmanScript>();
			Chessmans [i,1].setPosition (i,1);
			activeChessmans.Add (pawnsB);
			Chessmans [i,6] = pawnsW.GetComponent<ChessmanScript>();
			Chessmans [i,6].setPosition (i,6);
			activeChessmans.Add (pawnsW);
		}
		//black
		GameObject knightB1 = Instantiate (blackKnight);
		knightB1.transform.position = translateToWorldCoord(new Vector2(1,0));
		knightB1.SetActive (true);
		//knightB1.transform.eulerAngles = new Vector3 (0.0f, 90.0f, 0.0f);
		Chessmans [1,0] = knightB1.GetComponent<ChessmanScript>();
		Chessmans [1,0].setPosition (1,0);
		activeChessmans.Add (knightB1);

		GameObject knightB2 = Instantiate (blackKnight);
		knightB2.transform.position = translateToWorldCoord(new Vector2(6,0));
		knightB2.SetActive (true);
		//knightB2.transform.eulerAngles = new Vector3 (0.0f, 90.0f, 0.0f);
		Chessmans [6,0] = knightB2.GetComponent<ChessmanScript>();
		Chessmans [6,0].setPosition (6,0);
		activeChessmans.Add (knightB2);

		GameObject bishopB1 = Instantiate (blackBishop);
		bishopB1.transform.position = translateToWorldCoord(new Vector2(2,0));
		bishopB1.SetActive (true);
		Chessmans [2,0] = bishopB1.GetComponent<ChessmanScript>();
		Chessmans [2,0].setPosition (2,0);
		activeChessmans.Add (bishopB1);

		GameObject bishopB2 = Instantiate (blackBishop);
		bishopB2.transform.position = translateToWorldCoord(new Vector2(5,0));
		bishopB2.SetActive (true);
		Chessmans [5,0] = bishopB2.GetComponent<ChessmanScript>();
		Chessmans [5,0].setPosition (5,0);
		activeChessmans.Add (bishopB2);

		GameObject rookB1 = Instantiate (blackRook);
		rookB1.transform.position = translateToWorldCoord(new Vector2(0,0));
		rookB1.SetActive (true);
		Chessmans [0, 0] = rookB1.GetComponent<ChessmanScript>();
		Chessmans [0, 0].setPosition (0, 0);
		activeChessmans.Add (rookB1);

		GameObject rookB2 = Instantiate (blackRook);
		rookB2.transform.position = translateToWorldCoord(new Vector2(7,0));
		rookB2.SetActive (true);
		Chessmans [7,0] = rookB2.GetComponent<ChessmanScript>();
		Chessmans [7,0].setPosition (7,0);
		activeChessmans.Add (rookB2);

		GameObject kingB = Instantiate (blackKing);
		kingB.transform.position = translateToWorldCoord(new Vector2(4,0));
		kingB.SetActive (true);
		Chessmans [4,0] = kingB.GetComponent<ChessmanScript>();
		Chessmans [4,0].setPosition (4,0);
		activeChessmans.Add (kingB);

		GameObject queenB = Instantiate (blackQueen);
		queenB.transform.position = translateToWorldCoord(new Vector2(3,0));
		queenB.SetActive (true);
		Chessmans [3,0] = queenB.GetComponent<ChessmanScript>();
		Chessmans [3,0].setPosition (3,0);
		activeChessmans.Add (queenB);

		//white
		GameObject knightW1 = Instantiate (whiteKnight);
		knightW1.transform.position = translateToWorldCoord(new Vector2(1,7));
		knightW1.SetActive (true);
		//knightW1.transform.eulerAngles = new Vector3 (0.0f, -90.0f, 0.0f);
		Chessmans [1,7] = knightW1.GetComponent<ChessmanScript>();
		Chessmans [1,7].setPosition (1,7);
		activeChessmans.Add (knightW1);

		GameObject knightW2 = Instantiate (whiteKnight);
		knightW2.transform.position = translateToWorldCoord(new Vector2(6,7));
		knightW2.SetActive (true);
		//knightW2.transform.eulerAngles = new Vector3 (0.0f, -90.0f, 0.0f);
		Chessmans [6,7] = knightW2.GetComponent<ChessmanScript>();
		Chessmans [6,7].setPosition (6,7);
		activeChessmans.Add (knightW2);

		GameObject bishopW1 = Instantiate (whiteBishop);
		bishopW1.transform.position = translateToWorldCoord(new Vector2(2,7));
		bishopW1.SetActive (true);
		Chessmans [2,7] = bishopW1.GetComponent<ChessmanScript>();
		Chessmans [2,7].setPosition (2,7);
		activeChessmans.Add (bishopW1);

		GameObject bishopW2 = Instantiate (whiteBishop);
		bishopW2.transform.position = translateToWorldCoord(new Vector2(5,7));
		bishopW2.SetActive (true);
		Chessmans [5,7] = bishopW2.GetComponent<ChessmanScript>();
		Chessmans [5,7].setPosition (5,7);
		activeChessmans.Add (bishopW2);

		GameObject rookW1 = Instantiate (whiteRook);
		rookW1.transform.position = translateToWorldCoord(new Vector2(0,7));
		rookW1.SetActive (true);
		Chessmans [0,7] = rookW1.GetComponent<ChessmanScript>();
		Chessmans [0,7].setPosition (0,7);
		activeChessmans.Add (rookW1);

		GameObject rookW2 = Instantiate (whiteRook);
		rookW2.transform.position = translateToWorldCoord(new Vector2(7,7));
		rookW2.SetActive (true);
		Chessmans [7, 7] = rookW2.GetComponent<ChessmanScript>();
		Chessmans [7, 7].setPosition (7, 7);
		activeChessmans.Add (rookW2);

		GameObject kingW = Instantiate (whiteKing);
		kingW.transform.position = translateToWorldCoord(new Vector2(4,7));
		kingW.SetActive (true);
		Chessmans [4,7] = kingW.GetComponent<ChessmanScript>();
		Chessmans [4,7].setPosition (4,7);
		activeChessmans.Add (kingW);

		GameObject queenW = Instantiate (whiteQueen);
		queenW.transform.position = translateToWorldCoord(new Vector2(3,7));
		queenW.SetActive (true);
		Chessmans [3,7] = queenW.GetComponent<ChessmanScript>();
		Chessmans [3,7].setPosition (3,7);
		activeChessmans.Add (queenW);

	}

	public void EndGame(){
		if (isWhiteTurn) {
			Debug.Log ("White win");
			messageBoard.text = "White wins!";
			messageBoard.enabled = true;
		} else {
			Debug.Log ("Black win");
			messageBoard.text = "Black wins!";
			messageBoard.enabled = true;
		}
		foreach (GameObject go in activeChessmans) {
			if (go.GetComponent<ChessmanScript> ().isWhite != isWhiteTurn) {
				go.GetComponent<ChessmanScript> ().SelfDestory();
				//activeChessmans.Remove (go);
			}
		}
		restarting = true;

	}
}
