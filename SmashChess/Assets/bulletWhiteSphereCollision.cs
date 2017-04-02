using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletWhiteSphereCollision : MonoBehaviour {
	bool destroyCountDown = false;
	float timeToLive = 4;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (destroyCountDown) {
			timeToLive -= Time.deltaTime;
			if (timeToLive <= 0) {
				Destroy (gameObject);
			}
		}
	}

	void OnCollisionEnter (Collision col)
	{
		destroyCountDown = true;
	}
}
