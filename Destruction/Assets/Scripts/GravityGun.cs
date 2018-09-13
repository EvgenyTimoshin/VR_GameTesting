using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour {

	bool gunActivated = false;
	bool gotObjects = false;
	float pickUpRadius = 5.0f;
	float attractionStrenght = 5.0f;
	Vector3 objectPickUpArea;
	Vector3 objectHoverPos;
	Collider[] gravPullObjects;
	public GameObject pullArea;

	// Use this for initialization
	void Start () {
		//objectPickUpArea = transform.position + Vector3.forward * 10;
		objectPickUpArea = pullArea.transform.position;
		gravPullObjects = null;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.Space)) {
			GravityActive ();
		} else {
			if (gravPullObjects != null) {
				DropObjects ();	
			}
		}

		/* Use for when have external controller input
		if (gunActivated) {
			GravityActive ();
		} else if(gravPullObjects != null){
			DropObjects ();
		}
		*/
	}

	void FixedUpdate(){
		objectPickUpArea = pullArea.transform.position;
		objectHoverPos = transform.position + Vector3.forward * 3;
	}

	public void ActivateGun(){
		gunActivated = true;
	}

	public void DeactivateGun(){
		gunActivated = false;
	}

	void GravityActive(){
		
		if (!gotObjects) {
			GetObjectsInRadius ();
			gotObjects = true;
			Debug.Log ("Got Objects to pull");
		}
			
		if (gravPullObjects != null) {

			foreach (Collider col in gravPullObjects) {
			
				GameObject go = col.transform.gameObject;

				WallBlockScript cubeScript = go.GetComponent<WallBlockScript> ();

				//Calculates position of the object that is affected
				Vector3 posOfHitObj = col.gameObject.transform.position;

				//Calculates the direction in which object will be pulled
				Vector3 directionOfPull = objectHoverPos - posOfHitObj;

				//Unfreezes the rigibody of the cube + unfreezes it
				Rigidbody rb = col.GetComponent<Rigidbody> ();

				if (rb != null && cubeScript != null && rb.tag != "gravPullArea") {
					Debug.Log ("Pulling Objects");
					cubeScript.UnFreezeRb ();
					rb.AddForce (attractionStrenght * directionOfPull);
					rb.useGravity = false;
				}
			}
		}
		
	}

	void DropObjects(){
		
		foreach (Collider col in gravPullObjects) {
			Debug.Log ("Dropped Objects");
			//Drops Objects in gravity field
			Rigidbody rb = col.GetComponent<Rigidbody> ();

			if (rb != null && rb.tag != "gravPullArea") {
				rb.useGravity = true;
			}

		}

		gotObjects = false;
		gravPullObjects = null;
		
	}

	void GetObjectsInRadius(){
		Vector3 pickUpPos = objectPickUpArea;
		gravPullObjects = Physics.OverlapSphere (pickUpPos, pickUpRadius);
	}

}
