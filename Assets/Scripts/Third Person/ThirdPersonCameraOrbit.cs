using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraOrbit : MonoBehaviour {

	public Transform lookAt;
	//public Transform camTransform;

	public float zDistance = 6f;
	public float currentX = 0f;
	public float currentY = 0f;

	public float xSensitivity = 4f;
	public float ySensitivity = 1f;

	public const float Y_ANGLE_MIN = -60f;
	public const float Y_ANGLE_MAX = 0f;

	public Vector3 lookAtOffset = new Vector3 (0f, 0.5f, 0f);


	private void Start() {

		//camTransform = transform;

	}

	private void Update() {

		currentX += Input.GetAxis("Mouse X") * xSensitivity;
		currentY += Input.GetAxis("Mouse Y") * ySensitivity;

		currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
	}

	private void LateUpdate() {

		Vector3 dir = new Vector3(0f, 0f, -zDistance);
		Quaternion rotation = Quaternion.Euler(-currentY, currentX, 0f);

		Camera.main.transform.position = lookAt.position + rotation * dir;
		Camera.main.transform.LookAt(lookAt.position + lookAtOffset);
	}

}

//public float turnSpeed = 4.0f;
//public Transform player;

//public float yAdd = 1f;
//public Vector3 offset = new Vector3(0f, 2f, -2f);

//private Vector3 yAddVec3;
//private Vector3 newOffset;

//void Start() {

//	yAddVec3 = new Vector3(0f, yAdd, 0f);
//	//offset = new Vector3(offset.x, offset.y, offset.z);
//	//offset = new Vector3(player.position.x, player.position.y + offset.y + yAdd, player.position.z + offset.z);
//}

//void LateUpdate() {
//	newOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
//	transform.position = player.position + newOffset;
//	transform.LookAt(player.position);
//	transform.position = transform.position + yAddVec3;
//}