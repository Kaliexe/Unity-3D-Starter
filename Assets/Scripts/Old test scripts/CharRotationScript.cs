//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Unity;
//using InControl;
//using UnityEngine;
//using System;

//using Cinemachine;

//public class PlayerControlScript : MonoBehaviour {

//	//public CharacterController charController;
//	public GameObject playerModel;
//	public Animator playerAnimator;

//	Rigidbody rigidbody;
//	Rigidbody playerModelRB;

//	Collider collider;
//	public Collider playerModelCol;

//	float colliderYExtent;
//	//Bounds colliderBounds;
//	public float speed = 12.0f;
//	public float flightSpeed = 24.0f;
//	float appliedSpeed = 0f;
//	public float jumpHeight = 20f;
//	public float jumpCooldownTime = 5f;
//	public float jumpValFloor = 0.01f;
//	//public float zoomSpeed = 12.0f;

//	//public float minX = -360.0f;
//	//public float maxX = 360.0f;

//	//public float minY = -45.0f;
//	//public float maxY = 45.0f;

//	//public float sensX = 1000.0f;
//	//public float sensY = 1000.0f;

//	float rotationY = 0.0f;
//	float rotationX = 0.0f;

//	public float turnSmoothing = 0.2f;
//	public float transformSmoothing = 0.1f;
//	public float sprintFactor = 1.5f;

//	public float flyStandstill = 1f;
//	public AnimationCurve flyStandCruve;

//	public OutOfBattleActionSet playerActions;
//	string saveData;

//	public bool flying = false;
//	bool sprintBurst = false;
//	public bool jumpAvailable = true;

//	public CinemachineFreeLookZoom CFLZ;

//	void OnEnable() {
//		// See PlayerActions.cs for this setup.
//		playerActions = OutOfBattleActionSet.CreateWithDefaultBindings();
//		//playerActions.Move.OnLastInputTypeChanged += ( lastInputType ) => Debug.Log( lastInputType );

//		//LoadBindings();
//	}


//	void OnDisable() {
//		// This properly disposes of the action set and unsubscribes it from
//		// update events so that it doesn't do additional processing unnecessarily.
//		playerActions.Destroy();
//	}


//	void Start() {
//		//transform.rotation = Quaternion.Euler(new Vector3(15.43f, 180f, 0f));
//		rigidbody = GetComponent<Rigidbody>();
//		playerModelRB = playerModel.GetComponent<Rigidbody>();
//		collider = GetComponent<Collider>();
//		//playerModelCol = playerModel.GetComponent<Collider>();
//		flying = false;
//		sprintBurst = false;
//		jumpAvailable = true;
//		appliedSpeed = 12f;
//		//colliderBounds = GetComponent<Collider>().bounds;
//		colliderYExtent = GetComponent<Collider>().bounds.extents.y;
//		//StartCoroutine(jumpCooldown());
//		//playerActions.Reset();
//		//charController.enabled = true;

//	}

//	private void Update() {



//	}
//	void FixedUpdate() {

//		//float xVal = Input.GetAxis("Horizontal");
//		//float yVal = Input.GetAxis("Vertical");

//		float xVal = playerActions.LR.Value;
//		float yVal = playerActions.FB.Value;
//		Debug.Log("xVal " + xVal + " yVal " + yVal);
//		//Debug.Log(xVal);
//		//Debug.Log(yVal);
//		//if there is movement


//		//jump mechanic.  for some reason I am double jumping
//		//if not flying, and therefore either on ground on in air while jumping >
//		if (!flying) {


//			//if on ground and jump pressed and available, you jump into air
//			if (playerActions.Jump.WasPressed && jumpAvailable) {
//				jumpAvailable = false;
//				Debug.Log("on ground jumping, first condition");
//				StopAllCoroutines();
//				rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
//				//jumpAvailable = false;

//				StartCoroutine(jumpCooldown());
//				//if you pressed jump button again but are InAir and Jump unavailable, you fly
//			} else if (playerActions.Jump.WasPressed && !jumpAvailable) {
//				Debug.Log("in air jumping, second condition");



//				rigidbody.AddForce(Vector3.up * jumpHeight / 1.5f, ForceMode.VelocityChange);
//				//rigidbody.MovePosition(transform.position + Vector3.up * appliedSpeed * Time.deltaTime * jumpHeight/2f);

//				//switch colliders, turn off character collider and turn on local collider
//				//collider.enabled = false;
//				//playerModelCol.enabled = true;

//				StopAllCoroutines();
//				jumpAvailable = false;
//				flying = true;
//				setFlying();
//				//cancel fly
//				playerAnimator.SetFloat("Flying", 1f);
//				playerAnimator.SetFloat("Walking", 0f);

//				//rigidbody.constraints = RigidbodyConstraints.None;
//				//CMMS.executeChangeGrToFl();


//				CFLZ.executeZoom();
//			}

//		} else {

//			//if flying and you pressed jump button again, you fall to ground resetting things
//			if (playerActions.Jump.WasPressed) {
//				Debug.Log("flying but pressed space, falling third condition");

//				//switch colliders, turn off character collider and turn on local collider
//				//collider.enabled = true;
//				//playerModelCol.enabled = false;

//				StopAllCoroutines();
//				flying = false;
//				jumpAvailable = false;
//				setFlying();
//				StartCoroutine(jumpCooldown());
//				//CMMS.executeChangeFlToGr();
//				//reset rotation
//				//transform.rotation = Quaternion.Euler(Vector3.zero);
//				//rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
//				CFLZ.executeZoom2();
//			}
//		}
//		//Debug.Log("air velocity y " + Math.Abs(rigidbody.velocity.y));

//		//If moving
//		if (xVal != 0f || yVal != 0f) {


//			//camera forward and right vectors:
//			//Vector3 forward = new Vector3(transform.forward.x, 0f, transform.forward.z);
//			//Vector3 right = new Vector3(transform.right.x, 0f, transform.right.z);
//			Vector3 desiredMoveDirection;
//			if (flying) {
//				Vector3 forward = Camera.main.transform.forward;
//				Vector3 right = Camera.main.transform.right;
//				forward.Normalize();
//				right.Normalize();

//				appliedSpeed = flightSpeed;
//				////transform up and down calcs
//				//float udVal = Input.GetAxis("UpDown");
//				//Vector3 updown = transform.up;
//				//updown.Normalize();

//				//this is the direction in the world space we want to move:
//				//Vector3 desiredMoveDirection = (forward * yVal) + (right * xVal) + (udVal * updown);
//				desiredMoveDirection = (forward * yVal) + (right * xVal);
//				//Vector3 newPosition = transform.position;
//				//newPosition += desiredMoveDirection * speed * Time.deltaTime;
//				//transform.position += desiredMoveDirection * speed * Time.deltaTime;
//				playerAnimator.SetFloat("Flying", 1f);
//				playerAnimator.SetFloat("Fly Idle", 0f);
//				playerAnimator.SetFloat("Walking", 0f);
//			} else {
//				//you are walking here

//				Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
//				Vector3 right = new Vector3(forward.z, 0f, -forward.x);
//				//Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
//				//Vector3 right = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z);
//				//Vector3.Scale(forward, Vector3.forward);
//				//Vector3.Scale(right, Vector3.right);
//				forward.Normalize();
//				right.Normalize();

//				appliedSpeed = speed;
//				////transform up and down calcs
//				//float udVal = Input.GetAxis("UpDown");
//				//Vector3 updown = transform.up;
//				//updown.Normalize();

//				//this is the direction in the world space we want to move:
//				//Vector3 desiredMoveDirection = (forward * yVal) + (right * xVal) + (udVal * updown);
//				desiredMoveDirection = (forward * yVal) + (right * xVal);
//				//Vector3 newPosition = transform.position;
//				//newPosition += desiredMoveDirection * speed * Time.deltaTime;
//				//transform.position += desiredMoveDirection * speed * Time.deltaTime;
//				playerAnimator.SetFloat("Walking", 1f);
//				playerAnimator.SetFloat("Flying", 0f);
//				playerAnimator.SetFloat("Fly Idle", 0f);
//			}

//			//speed checks.  

//			//if (playerActions.Sprint.HasChanged && !sprintBurst) {
//			//	Debug.Log("burst sprint");
//			//	sprintBurst = true;

//			//	rigidbody.MovePosition(transform.position + (desiredMoveDirection * speed * Time.deltaTime * sprintFactor *20f));
//			//	Quaternion desiredRotation = Quaternion.LookRotation(-desiredMoveDirection);
//			//	Quaternion newRotation = Quaternion.Slerp(playerModelRB.rotation, desiredRotation, turnSmoothing * sprintFactor * 20f);
//			//	playerModelRB.MoveRotation(newRotation);
//			//	Invoke("coolDownBurst", 5f);
//			//}



//			if (playerActions.Sprint.IsPressed) {
//				//Debug.Log("Sprinting");
//				//Sprint Speed
//				rigidbody.MovePosition(transform.position + (desiredMoveDirection * appliedSpeed * Time.deltaTime * sprintFactor));
//				Quaternion desiredRotation = Quaternion.LookRotation(-desiredMoveDirection);
//				//Quaternion newRotation = Quaternion.Slerp(rigidbody.rotation, desiredRotation, turnSmoothing * sprintFactor);
//				Quaternion newRotation = Quaternion.Slerp(playerModelRB.rotation, desiredRotation, turnSmoothing);
//				//rigidbody.MoveRotation(newRotation);
//				playerModelRB.MoveRotation(newRotation);

//			} else {
//				//Walking Speed
//				//playerAnimator.SetFloat("Walking", 1f);
//				rigidbody.MovePosition(transform.position + (desiredMoveDirection * appliedSpeed * Time.deltaTime));
//				Quaternion desiredRotation = Quaternion.LookRotation(-desiredMoveDirection);
//				//Quaternion newRotation = Quaternion.Slerp(rigidbody.rotation, desiredRotation, turnSmoothing);
//				Quaternion newRotation = Quaternion.Slerp(playerModelRB.rotation, desiredRotation, turnSmoothing);
//				//rigidbody.MoveRotation(newRotation);
//				playerModelRB.MoveRotation(newRotation);
//			}

//			//playerModel.transform.rotation = desiredRotation;
//			//rigidbody.MoveRotation(desiredRotation);

//		} else {
//			//playerAnimator.SetFloat("Walking", 0f);
//			//is idling either in air or ground
//			if (!flying) {
//				playerAnimator.SetFloat("Flying", 0f);
//				playerAnimator.SetFloat("Fly Idle", 0f);
//				playerAnimator.SetFloat("Walking", 0f);
//			} else {

//				//is idle in air
//				playerAnimator.SetFloat("Flying", 1f);
//				playerAnimator.SetFloat("Fly Idle", 1f);
//				playerAnimator.SetFloat("Walking", 0f);

//				if (playerModelRB.transform.up.y != 1f) {

//					//Quaternion targetRotation = Quaternion.LookRotation(lastDirection);
//					//Quaternion newRotation = Quaternion.Slerp(rBody.rotation, targetRotation, turnSmoothing);
//					//playerModelRB.MoveRotation(newRotation);
//				}
//			}
//		}

//		/*// Get camera forward direction, without vertical component.
//		Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);

//		// Player is moving on ground, Y component of camera facing is not relevant.
//		forward.y = 0.0f;
//		forward = forward.normalized;

//		// Calculate target direction based on camera forward and direction key.
//		Vector3 right = new Vector3(forward.z, 0, -forward.x);
//		Vector3 targetDirection;
//		targetDirection = forward * vertical + right * horizontal;

//		// Lerp current direction to calculated target direction.
//		if((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
//		{
//			Quaternion targetRotation = Quaternion.LookRotation (targetDirection);

//			Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);
//			behaviourManager.GetRigidBody.MoveRotation (newRotation);
//			behaviourManager.SetLastDirection(targetDirection);
//		}
//		*/

//		/*
//		 * Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);
//		// Camera forward Y component is relevant when flying.
//		forward = forward.normalized;

//		Vector3 right = new Vector3(forward.z, 0, -forward.x);

//		// Calculate target direction based on camera forward and direction key.
//		Vector3 targetDirection = forward * vertical + right * horizontal;

//		// Rotate the player to the correct fly position.
//		if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
//		{
//			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

//			Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);

//			behaviourManager.GetRigidBody.MoveRotation(newRotation);
//			behaviourManager.SetLastDirection(targetDirection);
//		}
//		*/
//	}

//	//https://forum.unity3d.com/threads/moving-character-relative-to-camera.383086/
//	private void movement(float x, float y, Vector3 desiredDir) {
//		// animator.SetFloat("velX", x);
//		//animator.SetFloat("velY", y);

//		transform.position += desiredDir * speed * Time.deltaTime;
//		//transform.forward = camera.transform.forward;

//		//transform.eulerAngles.y = camera.transform.eulerAngles.y;

//		//transform.position = (transform.position + camera.transform.forward) * x * Time.deltaTime;
//		//transform.position = (transform.position + camera.transform.right) * y * Time.deltaTime;

//	}

	
//}
