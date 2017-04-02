using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	float timeLeft = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timeLeft > 0) {
			timeLeft -= Time.deltaTime;
		}
		if (timeLeft <= 0) {
			GetComponent<FracturedObject> ().Explode (new Vector3(0.0f,0.0f,0.0f), 10.0f);
		}
	}
}
