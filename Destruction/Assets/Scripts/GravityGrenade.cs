using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGrenade : MonoBehaviour {

	public float radius = 10.0f;
	public float attractionStrenght = 1.0f;
	//public float explRadius = 20.0f;
	public float explPower = 10.0f;
	Collider[] colliders;
	bool released = false;
	bool grenadeActivated = false;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		colliders = null;
		rb = gameObject.AddComponent<Rigidbody> ();
		rb.AddForce (Vector3.forward * 350);
		released = true;

	}
	
	// Update is called once per frame
	void Update () {

		//When grenade is released (if statement placeholder)
		if(released){
			StartCoroutine (GrenadeCooked ());
			released = false;
		}
		
	}

	void FixedUpdate(){
		//Activated grenade pulling
		if (grenadeActivated) {
			ActivateGrenade ();
		}
		
	}

	//Runs Co-routine when grenade is released
	IEnumerator GrenadeCooked(){
		yield return new WaitForSeconds (5);
		grenadeActivated = true;
		yield return new WaitForSeconds (5);
		ExplosionEffect ();
		Destroy (gameObject);
	}

	//Activates pulling effect of grenade
	void ActivateGrenade(){
		
		colliders = Physics.OverlapSphere (transform.position, radius);
		foreach (Collider hit in colliders) {

			//Gets Gameobject of collider and its components
			GameObject go = hit.transform.gameObject;

			WallBlockScript cubeScript = go.GetComponent<WallBlockScript> ();

			//Calculates position of the object that is affected
			Vector3 posOfHitObj = hit.gameObject.transform.position;

			//Calculates the direction in which object will be pulled
			Vector3 directionOfPull = transform.position - posOfHitObj;

			//Unfreezes the rigibody of the cube + unfreezes it
			Rigidbody rb = hit.GetComponent<Rigidbody> ();
			if (rb != null && cubeScript != null) {
				rb.AddForce (attractionStrenght * directionOfPull);
				cubeScript.UnFreezeRb ();
				rb.useGravity = false;
			}
		}
	}

	void GrenadeEffect(){
	}

	void ExplosionEffect(){
		//Where explosion happens
		Vector3 explosionPos = transform.position;

		//Adds explosion forces to all objects in grenade radius
		if(colliders != null){
			foreach (Collider hit in colliders) {
				Rigidbody rb = hit.GetComponent<Rigidbody> ();

				if (rb != null) {
					rb.useGravity = true;
					rb.AddExplosionForce (explPower, explosionPos, radius);
				}
			}

			colliders = null;
		}
	}
}
