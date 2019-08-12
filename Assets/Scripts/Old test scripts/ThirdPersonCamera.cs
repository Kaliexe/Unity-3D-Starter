 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	//https://www.youtube.com/watch?v=Ta7v27yySKs
	public Transform lookAt;
	public Transform camTransform;

	private Camera camera;

	public float AmbientSpeed = 100.0f;

	public float RotationSpeed = 200.0f;

	public Rigidbody rigidbody;

	private float distance = 6f;
	private float currentX = 0.0f;
	private float currentY = 0.0f;
	private float currentZ = 0.0f;
	private float sensitivityX = 10.0f;
	private float sensitivityY = 10.0f;

	private float mouseSpeed = 7f;
	private float zSpeed = 4f;

	private float angleX = 13.7f;

	private const float zoomMin = 2.0f;
	private const float zoomMax = 16.0f;

	private const float fixMin = -16.0f;
	private const float fixMax = 2f;

	private const float X_ANGLE_MIN = -80.0f;
	private const float X_ANGLE_MAX = 78.0f;

	private float fixUp = 0.0f;

	// Use this for initialization
	void Start () {
		camTransform = transform;
		rigidbody = GetComponent<Rigidbody> ();
		//camera = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		currentX -= Input.GetAxis ("Mouse Y") * mouseSpeed;
		currentY += Input.GetAxis ("Mouse X") * mouseSpeed;
		//currentZ += Input.GetAxis ("Roll") * zSpeed;

		currentX = Mathf.Clamp (currentX, X_ANGLE_MIN, X_ANGLE_MAX);

		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			distance -= 2f;
			fixUp += 2f;
		}

		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			distance += 2f;
			fixUp -= 2f;
		}﻿

		if (distance < zoomMin) {
			distance = zoomMin;

		}

		if (distance > zoomMax) {
			distance = zoomMax;
		}

		if (fixUp < fixMin) {
			fixUp = fixMin;
		}

		if (fixUp > fixMax) {
			fixUp = fixMax;
		}

		//Quaternion AddRot = Quaternion.identity;
		//float roll = 0;
		//roll = Input.GetAxis("Roll") * (Time.deltaTime * RotationSpeed);
		//roll = Input.GetAxis("Roll") * zSpeed;
		//AddRot.eulerAngles = new Vector3(0, 0, -roll);
		//rigidbody.rotation *= AddRot;

		Vector3 dir = new Vector3 (0, 4f, -distance-4f);
		//Quaternion rotation = Quaternion.Euler(currentX-10.7f-fixUp, currentY, -roll);
		//Quaternion rotation = Quaternion.Euler(currentX-10.7f-fixUp, currentY, currentZ);
		Quaternion rotation = Quaternion.Euler(currentX-10.7f-fixUp, currentY, 0);
		//Quaternion rotation = Quaternion.Euler(currentX-10.7f, currentY, 0);
		camTransform.position = lookAt.position + rotation * dir;
		camTransform.LookAt (lookAt.position);

		//Vector3 AddPos = Vector3.forward;
		//AddPos = rigidbody.rotation * AddPos;
		//rigidbody.velocity = AddPos * (Time.deltaTime * AmbientSpeed);
	}

	private void LateUpdate() {


	}
}


