using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class desturcibleObjBehaviourScript : MonoBehaviour {
	public GameObject parent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		//parent.GetComponent<ChessmanScript>().OnCollisionEnter(collision);
	}
}
