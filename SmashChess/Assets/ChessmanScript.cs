using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessmanScript : MonoBehaviour {

	bool destroyed = false;
	Vector3 startPosition;
	public Vector3 target;
	Vector3 moveStep;
	public float timeToReachTarget;
	public bool StartWithMoving = false;
	bool moving = false;
	float t;
	bool ignoringChessBoard = false;
	//from youtube
	public int CurrentX {set;get;}
	public int CurrentY { set; get; }
	public bool isWhite;
	// Use this for initialization
	void Start () {
		if (StartWithMoving) {
			startPosition = transform.position;
			//target = new Vector3 (-2.7f,0.0f, -2.3f);
			//timeToReachTarget = 1.0f;
			moveStep = (target - startPosition)/timeToReachTarget;
			moving = true;
			t = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		if (moving) {
			t += Time.deltaTime;
			//transform.position = Vector3.Lerp(startPosition, target, t);
			if (t < timeToReachTarget) {
				moving = true;
				transform.Translate (moveStep * Time.deltaTime);
			} else {
				if (t >= timeToReachTarget + 1) {
					//Debug.Log ("ID: " + name + ": " + moving);
					moving = false;
//					if(GetComponent<Collider>() != null)
//						GetComponent<Collider> ().enabled = false;
//					if (GetComponent<Rigidbody> () != null)
//						Destroy (this.gameObject.GetComponent<Rigidbody> ());
					GetComponent<Rigidbody> ().mass = 1;
					GetComponent<Rigidbody> ().useGravity = false;
					if(GetComponent<Collider> () != null)
						GetComponent<Collider> ().isTrigger = true;
				}
			}
		} else if (ignoringChessBoard) {
			foreach(Transform child in transform)
			{
				Collider childCollider = child.GetComponent<Collider>();
				// do stuff with the collider
				Physics.IgnoreCollision (childCollider, GetComponent<Collider>(), false);
			}
			GetComponent<Rigidbody> ().useGravity = true;
			ignoringChessBoard = false;
		}
	}

	public virtual bool[,] possibleMove(){
		bool [,] r = new bool[8,8];
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				r [i, j] = true;
			}
		}
		return r;
	}
	public void setPosition(int newX, int newY){
		CurrentX = newX;
		CurrentY = newY;
	}

	public void OnCollisionEnter(Collision collision){
		if(!collision.gameObject.tag.Equals("chessboard")){
			//Debug.Log ("Detected Collision");

			if (collision.gameObject.GetComponent<ChessmanScript>() != null &&
				CurrentX == collision.gameObject.GetComponent<ChessmanScript>().CurrentX &&
				CurrentY == collision.gameObject.GetComponent<ChessmanScript>().CurrentY && !moving) {
				destroyed = true;
				Destroy (GetComponent<Collider> ());
				GetComponent<FracturedObject> ().CollapseChunks();

			}
		}
	}

	public void SetDestination(Vector3 destination, float time)
	{
		t = 0;
		moving = true;
		startPosition = transform.position;
		timeToReachTarget = time;
		target = destination; 
		moveStep = (target - startPosition)/timeToReachTarget;
		GetComponent<Rigidbody> ().mass = 1000;
		GetComponent<Rigidbody> ().useGravity = true;
		if(GetComponent<Collider> () != null)
			GetComponent<Collider> ().isTrigger = false;
//		Rigidbody gameObjectsRigidBody = this.gameObject.AddComponent<Rigidbody>(); // Add the rigidbody.
//		gameObjectsRigidBody.mass = 100; // Set the GO's mass to 5 via the Rigidbody.
//		gameObjectsRigidBody.useGravity = false;
//		if(GetComponent<Collider> () != null)
//			GetComponent<Collider> ().enabled = true;
	}

	public void becomeDestroyable(){
		GetComponent<Rigidbody> ().mass = 1;
		GetComponent<Rigidbody> ().useGravity = true;
		if(GetComponent<Collider> () != null)
			GetComponent<Collider> ().isTrigger = false;
	}
	public void IgnoreChessBoardCollision()
	{
		foreach(Transform child in transform)
		{
			Collider childCollider = child.GetComponent<Collider>();
			// do stuff with the collider
			Physics.IgnoreCollision (childCollider, GetComponent<Collider>());
		}
		//Collider cb = GameObject.FindWithTag ("chessboard").GetComponent<Collider> ();
		ignoringChessBoard = true;
	}
	public void Evolve(GameObject pawn)
	{
		GameObject newPawn = Instantiate (pawn);
		newPawn.transform.position = new Vector3 (this.transform.position.x,this.transform.position.y + 15,this.transform.position.z);
		newPawn.GetComponent<PawnScript> ().IgnoreChessBoardCollision ();
		newPawn.GetComponent<PawnScript> ().SetDestination (this.transform.position, 4);
//		newPawn.GetComponent<Rigidbody> ().useGravity = false;
//		newPawn.GetComponent<Rigidbody> ().isKinematic = false;
		Rigidbody rb = newPawn.GetComponent<Rigidbody>();
		rb.useGravity = false;
	}
}
