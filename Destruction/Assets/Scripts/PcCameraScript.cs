using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcCameraScript : MonoBehaviour {

	public GameObject vehicle;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = vehicle.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate(){
		Vector3 targetCamPosition = new Vector3(vehicle.transform.position.x - 0.5f, 
			vehicle.transform.position.y + 1f,
			 vehicle.transform.position.z - 0.2f);

		transform.position = rb.position - Vector3.forward * 2;
	}
}
