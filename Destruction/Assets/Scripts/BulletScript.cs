using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public float explRadius = 20.0f;
	public float explPower = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime * 10);
	}

	public void createExplosion(){
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, explRadius);
		WallBlockScript blockScript;
		foreach (Collider hit in colliders) {
			blockScript = hit.transform.gameObject.GetComponent<WallBlockScript> ();

			if (blockScript != null) {
				blockScript.UnFreezeRb ();	
			}

			Rigidbody rb = hit.GetComponent<Rigidbody> ();

			if (rb != null) {
				rb.AddExplosionForce (explPower, explosionPos, explRadius);
			}
		}
	}

	public float GetExplRadius(){
		return explRadius;
	}
}
