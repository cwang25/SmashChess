using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript : MonoBehaviour {

	bool destroyed = false;
	Vector3 startPosition;
	public Vector3 target;
	Vector3 moveStep;
	public float timeToReachTarget;
	bool moving = false;
	float t;
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		//target = new Vector3 (-2.7f,0.0f, -2.3f);
		//timeToReachTarget = 1.0f;
		moveStep = (target - startPosition)/timeToReachTarget;
		t = 0;
	}

	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		//transform.position = Vector3.Lerp(startPosition, target, t);
		if (t < timeToReachTarget) {
			moving = true;
			transform.Translate (moveStep * Time.deltaTime);
		} else {
			if (t >= timeToReachTarget+1) {
				//Debug.Log ("ID: " + name + ": " + moving);
				moving = false;
			}
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
}
