using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerScript : MonoBehaviour {

	public Camera mainCam;
	public GameObject leftFrontWheel;
	public GameObject rightFrontWheel;
	public GameObject steeringWheel;
	Rigidbody rb;
	public float speed;
	bool decelerating = false;
	Vector3 currentAngle;
	Vector3 eulerAngleVelocityRight;
	Vector3 eulerAngleVelocityLeft;
	//public Transform turningPointLeft;
	//public Transform turningPointRight;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		currentAngle = rb.transform.eulerAngles;
		speed = 0;
		eulerAngleVelocityRight = new Vector3 (0, 50, 0);
		eulerAngleVelocityLeft = new Vector3 (0, -50, 0);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate(){
		Move ();

		if (Input.GetKey (KeyCode.UpArrow)) {
			decelerating = false;
			Accelerate ();
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			decelerating = false;
			Reverse ();
		} else {
			decelerating = true;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			TurnLeft ();
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			TurnRight ();
		}

		//Adjusts the rotation of the vehicle
		//rb.transform.eulerAngles = currentAngle;
		//transform.rotation = rb.transform.rotation;
			
		if (decelerating) {
			if (speed > 0 || speed < 0) {
				Decelerate ();
			}
		}
	}

	void Accelerate(){
		Debug.Log ("Accelerating");
		speed += 0.5f;
	}

	void Reverse(){
		Debug.Log ("Reversing");
		speed -= 0.5f;
	}

	void Move(){

		Debug.Log ("Moving");
		rb.velocity = transform.forward * Time.deltaTime * speed;
		//rb.MovePosition(transform.position + (transform.forward * (Time.deltaTime * speed)));
		/*
		Vector3 targetForward = transform.position + Vector3.forward * 2;
		Vector3 direction = (targetForward - transform.position).normalized;
		rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
		*/

	}

	void Decelerate(){
		Debug.Log ("Decelerating");
		speed = Mathf.Lerp (speed, 0, Time.deltaTime);
		/*
		if (speed > 0) {
			speed -= 0.1f;
		}
		*/

	}

	void TurnLeft(){
		Debug.Log ("Turning Left");
		/*
		currentAngle = new Vector3 (
			transform.rotation.x,
			transform.rotation.y - 1,
			transform.rotation.z
		);
		*/

		//rb.transform.Rotate (0, -1, 0);
		Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocityLeft * Time.deltaTime);
		rb.MoveRotation (rb.rotation * deltaRotation);
	}

	void TurnRight(){
		Debug.Log ("Turning Right");
		/*
		currentAngle = new Vector3 (
			transform.rotation.x,
			transform.rotation.y + 1,
			transform.rotation.z
		);
		*/

		//rb.transform.Rotate (0, +1, 0);

		Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocityRight * Time.deltaTime);
		rb.MoveRotation (rb.rotation * deltaRotation);
	}
}
