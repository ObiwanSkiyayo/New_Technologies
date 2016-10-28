using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
//using ValentijnsAssets.CocainStash;

public class GameController : MonoBehaviour
{
	private Camera cam;
	private Rigidbody rigidBody;

	internal AudioSource audio;

	private Vector3 startPosition;
	private Vector3 currentPosition;

	private Quaternion startRotation;
	private Quaternion inputRotation;
	private Quaternion rot;

	private float rotationZ = 0.0f;
	private float sensitivityZ = 2.0f;

	private float yrotation = 0;
	private Vector2 prevPos = new Vector2 (0, 0);

	public float damping = 1.0f;
	internal float speed = 100.0f;
	public float turningRate = 5.0f;
	public float jumpRate = 5.0f;
	public float angleDamping = 0.5f;

	private float velocityY;

	public GameObject camObj;

	private Vector3 cam0Position, cam1Position;
	private Quaternion cam0Rotation, cam1Rotation;

	public float _multiplier;
	internal float distanceGround;

	internal float camTime = 1f;
	internal float frontalAngle;

	internal GameObject portalObject;
	internal Vector3 portalPosition;

	void Start()
	{
   
		cam = Camera.main;
		startPosition = transform.position;
		startRotation = transform.rotation;
		rigidBody = GetComponent<Rigidbody> ();
		prevPos  = (Vector2)Camera.main.ScreenToViewportPoint (Input.mousePosition);

		// Camera Settings [ Cam 0 , Cam 1 ]
		cam0Position = camObj.transform.localPosition;
		cam0Rotation = camObj.transform.localRotation;
		cam1Position = new Vector3 (-2.0f, 0.0f, 0.0f);
		cam1Rotation = Quaternion.Euler (0, 90, 0);
		camTime = 1f;

		// RigidBody Settings..
		rigidBody.angularDrag = 175;
		rigidBody.drag = 2;
		rigidBody.mass = 8;
		rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // Constraints true code..

		audio = GetComponent<AudioSource> ();
        _multiplier = 1.0f;

	}

    void FixedUpdate()
    {
        // Set the default multiplier.
        if (Input.GetKeyUp(KeyCode.W))
		{
			_multiplier = 1.0f;
		}

		// Pressing 'R' resets all the game.
		if (Input.GetKeyDown ("r")) {
			MainGameManager.Instance.Restart ();
		}

		rigidBody.useGravity = true;
		velocityY = rigidBody.velocity.y;

		// Lerp the Camera between the first-person and third-person position and rotation.
		cam.transform.localPosition = Vector3.Lerp (cam0Position, cam1Position, camTime);
		cam.transform.localRotation = Quaternion.Lerp (cam0Rotation, cam1Rotation, camTime);

		// Lerp the Camera FoV between Camera 0 and 1
		cam.fieldOfView = Mathf.Lerp (80, 100, camTime);
		
		// Lerp between 0 and 1 when the right-mouse button is being hold.
		if (Input.GetMouseButton (1)) {
			if (camTime < 1)
				camTime += 0.03f;
		} else {
			if (camTime > 0)
				camTime -= 0.03f;
		}

		// Pressing the "W" key doubles the speed of the AddForce. 
		if (Input.GetKey ("w") && _multiplier != 0.0f )
		{ 
			_multiplier = 2.0f;	

		}
  
		rigidBody.AddForce (transform.right  * speed * _multiplier, ForceMode.Force);

		bool isGrounded = true;  // Player grounded or not? Variable set.

		// Raycast to the ground, to check if jumping is allowed.
		RaycastHit hit;
		Physics.Raycast (transform.position, transform.up * -1, out hit);

        distanceGround = hit.distance;      // Variable for the distance to the ground.
        isGrounded = hit.distance < 0.5f;

        // Raycast to the surface to get the object and use the the normal data to rotate
        RaycastHit hitFront;
        Physics.Raycast(transform.position, transform.right - new Vector3(0, 0.3f, 0), out hitFront);

        // Rotation Controller, defined with Quaternions.
        if (isGrounded)
        {
            // When grounded, follow the angle of the normals.
            rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
        else if (hitFront.distance < 28.0f)
        {
            //follow the normals of the object in front.
            rot = Quaternion.FromToRotation(Vector3.up, hitFront.normal);
        }
        else
        {
            rot = Quaternion.Euler(0, 0, 0); // When in mid-air, but not close to the ground,
            // it should reset it's orientation to the player can see where he is going.
        }

        // Slerp between current rotation and the new rotation.
        transform.rotation = Quaternion.Slerp (transform.rotation, rot, Time.deltaTime * angleDamping);

		// Jumping is possible when grounded. (inactive for now!)
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			velocityY = 0; // Reset the velocity in the y-direction when jumping creates the thrust look.
			rigidBody.AddForce (Vector3.up * jumpRate /2 );
		}

		// Calculate Mouse Position.
		Vector2 mpos = (Vector2)Camera.main.ScreenToViewportPoint (Input.mousePosition);
		// Previous frame position - current frame position.
		Vector2 change = prevPos - mpos;

		// Add the difference to the rotation.
		yrotation += -change.x * turningRate * 0.7f;

		// Rotate the player according to the Camera.
		transform.Rotate (0, yrotation, 0);
		prevPos = mpos;

    }

	void Update()
    {
        // Alter the gravity applied to all rigid bodies.
        if (_multiplier != 0) 
		{
			Physics.gravity = new Vector3 (0, -9.81f, 0);
		}
		else
		{
			Physics.gravity = new Vector3 (0, 0, 0);
		}
	}

}
