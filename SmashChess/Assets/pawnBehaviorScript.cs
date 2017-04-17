using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pawnBehaviorScript : MonoBehaviour {
	bool destroyed = false;
	public GameObject wholePiece = null;
	Vector3 startPosition;
	Vector3 target;
	Vector3 moveStep;
	float timeToReachTarget;
	float t;
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		target = new Vector3 (-13.75f,0.0f, -13.75f);
		timeToReachTarget = 1.0f;
		moveStep = (target - startPosition)/timeToReachTarget;
		t = 0;
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		//transform.position = Vector3.Lerp(startPosition, target, t);
		if (t < timeToReachTarget) {
			transform.Translate(moveStep*Time.deltaTime);
		}
	}

	public void OnCollisionEnter(Collision collision){
		if(!collision.gameObject.tag.Equals("chessboard")){
			destroyed = true;
			//GetComponent<FracturedObject> ().CollapseChunks();
		}
	}

	public void SetDestination(Vector3 destination, float time)
	{
		t = 0;
		startPosition = transform.position;
		timeToReachTarget = time;
		target = destination; 
	}

}
