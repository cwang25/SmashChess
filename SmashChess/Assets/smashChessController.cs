using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateFracturing;

public class smashChessController : MonoBehaviour {
	public GameObject        ObjectToShoot      = null;                 // In ShootObjects mode, the object to instance when shooting
	public float             InitialObjectSpeed = 1.0f;                 // In ShootObjects mode, the initial speed of the object
	public float             ObjectScale        = 1.0f;                 // In ShootObjects mode, the object's scale
	public float             ObjectMass         = 1.0f;                 // In ShootObjects mode, the object's mass
	public float             ObjectLife         = 10.0f;                // In ShootObjects mode, the object's life time (seconds until it deletes itself)

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				GameObject newObject = GameObject.Instantiate(ObjectToShoot) as GameObject;
				newObject.transform.position = ray.GetPoint (0);
				newObject.transform.localScale = new Vector3(ObjectScale, ObjectScale, ObjectScale);
				newObject.GetComponent<Rigidbody>().mass       = ObjectMass;
				newObject.GetComponent<Rigidbody>().solverIterations = 255;
				newObject.GetComponent<Rigidbody>().AddForce((hit.point - ray.GetPoint(0)) * InitialObjectSpeed, ForceMode.VelocityChange);

				DieTimer dieTimer = newObject.AddComponent<DieTimer>() as DieTimer;
				dieTimer.SecondsToDie = ObjectLife;
			}
		} else if (Input.GetMouseButton (1)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		}
	}
}
