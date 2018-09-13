using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastScript : MonoBehaviour {

	public GameObject bullet;
	public Transform nozzlePos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)) {
			shootBullet();
		}
	}

	void shootRay(){
		RaycastHit hit;
		Ray shootingRay = new Ray (transform.position, Vector3.forward);

		if(Physics.Raycast(shootingRay, out hit)){
			if(hit.collider.tag == "hitObj"){
				//Debug.Log(hit.collider.gameObject);
				GameObject hitObj = hit.collider.gameObject;
				//scaleObject (hitObj);
				shootBullet();
			}
		}
	}

	void scaleObject(GameObject obj){
		obj.transform.localScale += new Vector3 (0.1f, 0.1f, 0.1f);
	}

	void shootBullet(){
		GameObject bulletObj = Object.Instantiate(bullet,nozzlePos.position,nozzlePos.rotation) as GameObject;
		Rigidbody bullRb = bulletObj.GetComponent<Rigidbody> ();
		bullRb.AddForce (Vector3.forward * 1000);
		bulletObj.SetActive (true);
	}
}
