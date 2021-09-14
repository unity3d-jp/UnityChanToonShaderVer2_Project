//
// Third-person camera for Unity-Chan
// 
// 2013/06/07 N.Kobyasahi
//
using UnityEngine;
using System.Collections;

namespace UnityEngine.Rendering.Toon.Samples
{
	public class ThirdPersonCamera : MonoBehaviour
	{
		public float smooth = 3f;		// Variables for smoothing out camera motion
		Transform standardPos;			// the usual position for the camera, specified by a transform in the game
		Transform frontPos;			// Front Camera locater
		Transform jumpPos;			// Jump Camera locater
	
		// A boolean flag for when there is no smooth connection (quick changeover)
		bool bQuickSwitch = false;	//Change Camera Position Quickly
	
	
		void Start ()
		{
			// Initialization of each reference
			standardPos = GameObject.Find ("CamPos").transform;
		
			if (GameObject.Find ("FrontPos"))
				frontPos = GameObject.Find ("FrontPos").transform;

			if (GameObject.Find ("JumpPos"))
				jumpPos = GameObject.Find ("JumpPos").transform;

			//Start the camera
			transform.position = standardPos.position;	
			transform.forward = standardPos.forward;	
		}
	
		void FixedUpdate ()	// This camera switching must be in FixedUpdate() to work properly
		{
		
			if (Input.GetButton ("Fire1")) {	// left Ctlr	
				// Change Front Camera
				setCameraPositionFrontView ();
			} else if (Input.GetButton ("Fire2")) {	//Alt	
				// Change Jump Camera
				setCameraPositionJumpView ();
			} else {	
				// return the camera to standard position and direction
				setCameraPositionNormalView ();
			}
		}

		void setCameraPositionNormalView ()
		{
			if (bQuickSwitch == false) {
				// the camera to standard position and direction
				transform.position = Vector3.Lerp (transform.position, standardPos.position, Time.fixedDeltaTime * smooth);	
				transform.forward = Vector3.Lerp (transform.forward, standardPos.forward, Time.fixedDeltaTime * smooth);
			} else {
				// the camera to standard position and direction / Quick Change
				transform.position = standardPos.position;	
				transform.forward = standardPos.forward;
				bQuickSwitch = false;
			}
		}
	
		void setCameraPositionFrontView ()
		{
			// Change Front Camera
			bQuickSwitch = true;
			transform.position = frontPos.position;	
			transform.forward = frontPos.forward;
		}

		void setCameraPositionJumpView ()
		{
			// Change Jump Camera
			bQuickSwitch = false;
			transform.position = Vector3.Lerp (transform.position, jumpPos.position, Time.fixedDeltaTime * smooth);	
			transform.forward = Vector3.Lerp (transform.forward, jumpPos.forward, Time.fixedDeltaTime * smooth);		
		}
	}
}