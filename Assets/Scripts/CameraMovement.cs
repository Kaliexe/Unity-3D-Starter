using UnityEngine;
using System.Collections;


public class CameraMovement : MonoBehaviour {

	public float speed = 2.0f;
	public float zoomSpeed = 2.0f;

	public float minX = -360.0f;
	public float maxX = 360.0f;
	
	public float minY = -45.0f;
	public float maxY = 45.0f;

	public float sensX = 100.0f;
	public float sensY = 100.0f;
	
	float rotationY = 0.0f;
	float rotationX = 0.0f;

	//InputDevice device = InputManager.ActiveDevice;
	//InputControl control = device.GetControl(InputControlType.Action1);

	void Update () {

		//float scroll = device.GetControl(InputControlType.Action1);
		//transform.Translate(0, 0, scroll * zoomSpeed, Space.Self);

        
        float xVal = Input.GetAxis("Horizontal");
        float yVal = Input.GetAxis("Vertical");

        
        //camera forward and right vectors:
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward.Normalize();
        right.Normalize();

        //transform up and down calcs
        float udVal = Input.GetAxis("UpDown");
        Vector3 updown = transform.up;
        updown.Normalize();

        //this is the direction in the world space we want to move:
        Vector3 desiredMoveDirection = (forward * yVal) + (right * xVal) + (udVal * updown);
        transform.position += desiredMoveDirection * speed * Time.deltaTime;
        //movement(xVal, yVal, desiredMoveDirection);

        if (Input.GetMouseButton (1)) {
			rotationX += Input.GetAxis ("Mouse X") * sensX * Time.deltaTime;
			rotationY += Input.GetAxis ("Mouse Y") * sensY * Time.deltaTime;
			rotationY = Mathf.Clamp (rotationY, minY, maxY);
			transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
		}
	}

    //https://forum.unity3d.com/threads/moving-character-relative-to-camera.383086/
    private void movement(float x, float y, Vector3 desiredDir) {
       // animator.SetFloat("velX", x);
        //animator.SetFloat("velY", y);

        transform.position += desiredDir * speed * Time.deltaTime;
        //transform.forward = camera.transform.forward;

        //transform.eulerAngles.y = camera.transform.eulerAngles.y;

        //transform.position = (transform.position + camera.transform.forward) * x * Time.deltaTime;
        //transform.position = (transform.position + camera.transform.right) * y * Time.deltaTime;

    }
}
