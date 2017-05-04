using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolveDemo : MonoBehaviour {
	public GameObject evolveTo;
	// Use this for initialization
	void Start () {
		GetComponent<ChessmanScript> ().Evolve (evolveTo);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
