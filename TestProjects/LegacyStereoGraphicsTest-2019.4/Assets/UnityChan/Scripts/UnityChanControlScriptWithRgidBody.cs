//
// Controller sample with Rigidbody when the animation data of Mecanim does not move at the origin.
// 
// 2014/03/13 N.Kobyasahi
//
using UnityEngine;
using System.Collections;

namespace UnityEngine.Rendering.Toon.Samples
{
// List of required components
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Rigidbody))]

	public class UnityChanControlScriptWithRgidBody : MonoBehaviour
	{

		public float animSpeed = 1.5f;				// Animation Playback Speed Settings
		public float lookSmoother = 3.0f;			// a smoothing setting for camera motion
		public bool useCurves = true;				// Using or setting the curve adjustment in Mecanim
		// If this switch is not turned on, the curve will not be used
		public float useCurvesHeight = 0.5f;		// The effective height of the curve correction (to increase when it is easier to slip through the ground)

		// Parameters for character controllers below
		// Forward Speed
		public float forwardSpeed = 7.0f;
		// Backward Speed
		public float backwardSpeed = 2.0f;
		// Spin Speed
		public float rotateSpeed = 2.0f;
		// Jump power
		public float jumpPower = 3.0f; 
		// Character Controller (Capsule Collider) references
		private CapsuleCollider col;
		private Rigidbody rb;
		// The amount of movement of the character controller (capsule collider)
		private Vector3 velocity;
		// The variables that keep the initial value of the collider Heiht, Center, which is set in CapsuleCollider
		private float orgColHight;
		private Vector3 orgVectColCenter;
		private Animator anim;							// Reference to the animator to be attached to the character
		private AnimatorStateInfo currentBaseState;			// Reference to the current state of the animator, used in the base layer

		private GameObject cameraObject;	// Reference to the main camera
		
		// Animator reference to each state
		static int idleState = Animator.StringToHash ("Base Layer.Idle");
		static int locoState = Animator.StringToHash ("Base Layer.Locomotion");
		static int jumpState = Animator.StringToHash ("Base Layer.Jump");
		static int restState = Animator.StringToHash ("Base Layer.Rest");

		// Initialization
		void Start ()
		{
			// Get the Animator component
			anim = GetComponent<Animator> ();
			// Get the CapsuleCollider component (capsule collision)
			col = GetComponent<CapsuleCollider> ();
			rb = GetComponent<Rigidbody> ();
			// Get the Main Camera
			cameraObject = GameObject.FindWithTag ("MainCamera");
			// Save the initial value of the CapsuleCollider component's Height and Center
			orgColHight = col.height;
			orgVectColCenter = col.center;
		}
	
	
		// The following is the main process. Handling within FixedUpdate as it involves rigid bodies.
		void FixedUpdate ()
		{
			float h = Input.GetAxis ("Horizontal");				// Define the horizontal axis of the input device with h
			float v = Input.GetAxis ("Vertical");				// Define the vertical axis of the input device in v
			anim.SetFloat ("Speed", v);							// Pass v to the "Speed" parameter set in the Animator side.
			anim.SetFloat ("Direction", h); 						// Pass "h" to the "Direction" parameter set in the Animator side.
			anim.speed = animSpeed;								// Set animSpeed to Animator motion playback speed
			currentBaseState = anim.GetCurrentAnimatorStateInfo (0);	// Set the current state of Base Layer (0) in a state variable for reference
			rb.useGravity = true;// Cutting gravity during a jump, so that it is otherwise subject to the effects of gravity
		
		
		
			// The following character movement process
			velocity = new Vector3 (0, 0, v);		// Get the amount of movement in the Z-axis direction from up and down key input
			// Transformation in the direction of the character's local space
			velocity = transform.TransformDirection (velocity);
			//The following v thresholds to adjust together with transitions on the Mecanim side
			if (v > 0.1) {
				velocity *= forwardSpeed;		// Multiply the movement speed
			} else if (v < -0.1) {
				velocity *= backwardSpeed;	// Multiply the movement speed
			}
		
			if (Input.GetButtonDown ("Jump")) {	// After entering the spacebar

				//Animation states can only jump during Locomotion
				if (currentBaseState.fullPathHash == locoState) {
					//You can jump if you are not in state transition
					if (!anim.IsInTransition (0)) {
						rb.AddForce (Vector3.up * jumpPower, ForceMode.VelocityChange);
						anim.SetBool ("Jump", true);		// Send the flag to switch to jump to Animator
					}
				}
			}
		

			// Up and down keystrokes to move the character
			transform.localPosition += velocity * Time.fixedDeltaTime;

			// Left and right keystrokes make the character swivel in the Y-axis
			transform.Rotate (0, h * rotateSpeed, 0);	
	

			// The following is the process in each state of Animator
			// in Locomotion
			// When the current base layer is in the locoState
			if (currentBaseState.fullPathHash == locoState) {
				//When you're making a collider adjustment on a curve, reset it just in case
				if (useCurves) {
					resetCollider ();
				}
			}
		// Process during JUMP
		// When the current base layer is a jumpState
		else if (currentBaseState.fullPathHash == jumpState) {
				cameraObject.SendMessage ("setCameraPositionJumpView");	// Change the camera for during the jump
				// If the state is not in transition
				if (!anim.IsInTransition (0)) {
				
					// Process if you want to adjust the curve below
					if (useCurves) {
						// Curve JumpHeight and GravityControl on the following JUMP00 animation
						// JumpHeight:Jump height at JUMP00 (0 to 1)
						// GravityControl:1⇒During the jump (gravity disabled), 0⇒Gravity Effective
						float jumpHeight = anim.GetFloat ("JumpHeight");
						float gravityControl = anim.GetFloat ("GravityControl"); 
						if (gravityControl > 0)
							rb.useGravity = false;	//Cut the effect of gravity during a jump
										
						// Drop the ray cast from the center of the character
						Ray ray = new Ray (transform.position + Vector3.up, -Vector3.up);
						RaycastHit hitInfo = new RaycastHit ();
						// Adjust the collider height and center with the curve attached to the JUMP00 animation only when the height is greater than useCurvesHeight
						if (Physics.Raycast (ray, out hitInfo)) {
							if (hitInfo.distance > useCurvesHeight) {
								col.height = orgColHight - jumpHeight;			// Adjusted collider height
								float adjCenterY = orgVectColCenter.y + jumpHeight;
								col.center = new Vector3 (0, adjCenterY, 0);	// Adjusted the center of collider
							} else {
								// Restore the initial value when it is lower than the threshold (just in case)					
								resetCollider ();
							}
						}
					}
					// Reset the jump bool value (to prevent looping)				
					anim.SetBool ("Jump", false);
				}
			}
		// Process during IDLE
		// When the current base layer is idleState
		else if (currentBaseState.fullPathHash == idleState) {
				//When you're making a collider adjustment on a curve, reset it just in case
				if (useCurves) {
					resetCollider ();
				}
				// Enter the spacebar and you will be in the Rest state
				if (Input.GetButtonDown ("Jump")) {
					anim.SetBool ("Rest", true);
				}
			}
		// Process during REST
		// When the current base layer is restState
		else if (currentBaseState.fullPathHash == restState) {
				//cameraObject.SendMessage("setCameraPositionFrontView");		// Switch the camera to the front
				// If the state is not in transition, reset the Rest bool value (to prevent looping)
				if (!anim.IsInTransition (0)) {
					anim.SetBool ("Rest", false);
				}
			}
		}

		void OnGUI ()
		{
			GUI.Box (new Rect (Screen.width - 260, 10, 250, 150), "Interaction");
			GUI.Label (new Rect (Screen.width - 245, 30, 250, 30), "Up/Down Arrow : Go Forwald/Go Back");
			GUI.Label (new Rect (Screen.width - 245, 50, 250, 30), "Left/Right Arrow : Turn Left/Turn Right");
			GUI.Label (new Rect (Screen.width - 245, 70, 250, 30), "Hit Space key while Running : Jump");
			GUI.Label (new Rect (Screen.width - 245, 90, 250, 30), "Hit Spase key while Stopping : Rest");
			GUI.Label (new Rect (Screen.width - 245, 110, 250, 30), "Left Control : Front Camera");
			GUI.Label (new Rect (Screen.width - 245, 130, 250, 30), "Alt : LookAt Camera");
		}


		// Reset function for the character's collider size
		void resetCollider ()
		{
			// Restore the initial value of the component Height and Center
			col.height = orgColHight;
			col.center = orgVectColCenter;
		}
	}
}