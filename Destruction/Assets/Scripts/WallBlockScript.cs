using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlockScript : MonoBehaviour {

	public float explRadius = 100.0f;
	public float explPower = 50.0f;
	public float wallDestRadius = 5.0f;
	Rigidbody rb;
	Renderer rend;
	public Material rbAwake;
	public Material rbSleeping;
	public Material defaultMat;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rend = GetComponent<Renderer> ();
		rend.material = defaultMat;
		//transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	void FixedUpdate(){
		if (rb.IsSleeping()) {
			rend.material = rbSleeping;
		} else {
			rend.material = rbAwake;
		}
			
	}

	void OnTriggerEnter(Collider other) {
		
		if (other.tag == "bullet") {
			other.GetComponent<BulletScript> ().createExplosion ();
			//Destroy(other);
		}

	}

	public void freezeRb(){
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX 
			| RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY 
			| RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
	}

	public void UnFreezeRb(){
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = true;
		rb.constraints = RigidbodyConstraints.None;
	}
}
