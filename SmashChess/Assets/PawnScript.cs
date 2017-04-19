using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript : MonoBehaviour {

	bool destroyed = false;
	Vector3 startPosition;
	public Vector3 target;
	Vector3 moveStep;
	public float timeToReachTarget;
	public bool StartWithMoving = false;
	bool moving = false;
	float t;
	bool ignoringChessBoard = false;
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


	public void OnCollisionEnter(Collision collision){
		if(!collision.gameObject.tag.Equals("chessboard")){
			if (!moving) {
				destroyed = true;
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
