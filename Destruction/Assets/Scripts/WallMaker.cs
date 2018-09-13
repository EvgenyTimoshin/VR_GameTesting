using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMaker : MonoBehaviour {

	public Material materialRef;
	public int wallSizeX = 15;
	public int wallSizeY = 15;

	// Use this for initialization
	void Start () {
		
		for (int x = 0; x < wallSizeX; x++) {
			for(int y = 0; y < wallSizeY; y++){
				//Creates the cube and puts in postion + attaches it to the parent object (this)(Wall)
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.parent = transform;
				cube.transform.position = new Vector3(x, y, 0);

				//Adds a WallBlockScript and Rigidbody to the cube
				cube.AddComponent<Rigidbody> ();
				WallBlockScript cubeScript = cube.AddComponent<WallBlockScript> ();

				cubeScript.freezeRb ();

				//Sets the default material to cube
				cube.GetComponent<Renderer> ().material = materialRef;
			}

		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*
	public void detachChildren(){
		foreach (Transform child in transform) {
			Rigidbody rb = child.GetComponent<Rigidbody> ();
			rb.useGravity = true;
			rb.freezeRotation = false;
			Debug.Log ("Changed children rbs");
			//child.transform.parent = null;

			//rb.isKinematic = false;
		}
		transform.DetachChildren ();
	}
	*/

	/*
	public void detachChildrenInRadius(float radius, Vector3 explPosition){
		Collider[] objects = Physics.OverlapSphere (explPosition, radius);
		foreach (Collider col in objects) {
			
			//Detaches individual wall blocks from parent wall
			col.transform.parent = null;
			GameObject go = col.transform.gameObject;

			//Gets and UnFreezes the rigidbody (Individual Block)
			Rigidbody rb = go.GetComponent<Rigidbody> ();
			WallBlockScript cubeScript = go.GetComponent<WallBlockScript> ();
			if (cubeScript != null) {
				//UnFreezeRb (rb);
				cubeScript.UnFreezeRb();
			}
		}	
	}
	*/
}
