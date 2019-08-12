using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//simplified player Control using Rigidbody as mover
//attach to player container/object
[RequireComponent(typeof(Rigidbody))]
public class playerScriptCtrl : MonoBehaviour {

	public enum CameraRelationship {
		TransformFree, CameraRelative,
	}


	public CameraRelationship cameraRelationship;
	
	public float speed = 10f;
	public float turnSmoothing = 0.2f;

	private Rigidbody rigidbody;

	private Vector3 direction;
	//private float percent;

	private void Awake() {

		rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate() {

		float xVal = Input.GetAxis("Horizontal");
		float yVal = Input.GetAxis("Vertical");

		direction = new Vector3(xVal, 0f, yVal);

		//if have moving keys
		if (xVal != 0f || yVal != 0f) {

			//new position
			Vector3 relativeCameraDirection = Camera.main.transform.TransformDirection(direction);
			relativeCameraDirection = new Vector3(relativeCameraDirection.x, 0f, relativeCameraDirection.z);
			rigidbody.MovePosition(transform.position + (relativeCameraDirection * speed * Time.deltaTime));

			if (cameraRelationship == CameraRelationship.TransformFree) {
				//new direction.  transform looks at its own direction
				Quaternion desiredRotation = Quaternion.LookRotation(relativeCameraDirection);
				Quaternion newRotation = Quaternion.Slerp(rigidbody.rotation, desiredRotation, turnSmoothing);
				rigidbody.MoveRotation(newRotation);
			} else {
				
				//new direction.  transform looks at camera direction
				Vector3 desiredVector = Camera.main.transform.rotation.eulerAngles;

				//zero out x rotation so transform doesn't lean forward or backward relative to camera
				desiredVector = new Vector3(0f, desiredVector.y, desiredVector.z);
				Quaternion desiredRotation = Quaternion.Euler(desiredVector);
				Quaternion newRotation = Quaternion.Slerp(rigidbody.rotation, desiredRotation, turnSmoothing);
				rigidbody.MoveRotation(newRotation);
			}

			
		}
	}

	#region old movement
	//old
	////public static Vector3 accessPlayerPos;
	////public Animator animator; 
	//public float xSpeed = 1.0f;
	//public float ySpeed = 1.0f;

	//public float sensitivity = 5.0f;


	//// Use this for initialization
	//void Start () {

	//}

	//// Update is called once per frame
	////https://www.youtube.com/watch?v=YgaLKrSApWM
	////https://forum.unity3d.com/threads/move-character-relative-to-camera-angle.474375/
	////https://forum.unity3d.com/threads/difference-between-getbutton-getkey-and-getmousebutton.167567/
	//void Update () {


	//	float xVal = Input.GetAxis ("Horizontal") * xSpeed;
	//	float yVal = Input.GetAxis ("Vertical") * ySpeed;


	//	//camera forward and right vectors:
	//	Vector3 forward = Camera.main.transform.forward;
	//	Vector3 right = Camera.main.transform.right;
	//	forward.Normalize ();
	//	right.Normalize ();


	//	//this is the direction in the world space we want to move:
	//	//Vector3 desiredMoveDirection = (forward * yVal) + (right * xVal) + (udVal * updown);
	//	Vector3 desiredMoveDirection = (forward * yVal) + (right * xVal);
	//	transform.position += desiredMoveDirection * Time.deltaTime * sensitivity;

	//	//transform.rotation = new Quaternion()

	//}

	#endregion
}
