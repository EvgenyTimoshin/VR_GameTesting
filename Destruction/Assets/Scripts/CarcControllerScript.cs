using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarcControllerScript : MonoBehaviour {

	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steeringAngle;

	public WheelCollider frontLeftW, frontRightW;
	public WheelCollider rearLeftW, rearRightW;
	public Transform frontLeftT, frontRightT;
	public Transform rearLeftT, rearRightT;
	public float maxSteerAngle = 30;
	public float motorForce = 50;

	public void GetInput(){
		m_horizontalInput = Input.GetAxis ("Horizontal");
		m_verticalInput = Input.GetAxis ("Vertical");
	}

	private void Steer(){
		m_steeringAngle = maxSteerAngle * m_horizontalInput;
		frontLeftW.steerAngle = frontRightW.steerAngle = m_steeringAngle;
	}

	private void Accelerate(){
		//frontLeftW.motorTorque = frontRightW.motorTorque = m_verticalInput * motorForce;
		rearLeftW.motorTorque = rearRightW.motorTorque = m_verticalInput * motorForce;
	}

	private void UpdateWheelPositions(){
		UpdateWheelPosition (frontLeftW,frontLeftT);
		UpdateWheelPosition (frontRightW,frontRightT);
		UpdateWheelPosition (rearLeftW,rearLeftT);
		UpdateWheelPosition (rearRightW,rearRightT);
	}

	private void UpdateWheelPosition(WheelCollider _collider, Transform _transform){
		Vector3 _pos = transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose (out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}

	private void FixedUpdate(){
		GetInput ();
		Steer ();
		Accelerate ();
		UpdateWheelPositions ();
	}
		
}
