using UnityEngine;
using System.Collections;

namespace UnityEngine.Rendering.Toon.Samples
{
//
// Script to switch the loop animation with the UP/DOWN keys (with random switching) Ver.3
// 2014/04/03 N.Kobayashi
//

// Require these components when using this script
	[RequireComponent(typeof(Animator))]



	public class IdleChanger : MonoBehaviour
	{
	
		private Animator anim;						// ref to Animator
		private AnimatorStateInfo currentState;		// Save the current state state reference
		private AnimatorStateInfo previousState;	// Save the previous state reference
		public bool _random = false;				// Random Decision Start Switch
		public float _threshold = 0.5f;				// Random Decision Threshold
		public float _interval = 10f;               // Random Decision Interval
        //private float _seed = 0.0f;					// Seeds for random decisions
        public bool isGUI = true;
	


		// Use this for initialization
		void Start ()
		{
			// Initialization of each reference
			anim = GetComponent<Animator> ();
			currentState = anim.GetCurrentAnimatorStateInfo (0);
			previousState = currentState;
			// Start a function for random decisions
			StartCoroutine ("RandomChange");
		}
	
		// Update is called once per frame
		void  Update ()
		{
			// Send state to the next process when the UP key or space(JUMP) is pressed
			if (Input.GetKeyDown ("up") || Input.GetButton ("Jump")) {
				// Set the Boolean Next to true
				anim.SetBool ("Next", true);
			}
		
			// The DOWN key is pressed, the process of bringing the state back
			if (Input.GetKeyDown ("down")) {
				// Set the Boolean Back to true
				anim.SetBool ("Back", true);
			}
		
			// Process when the "Next" flag is true
			if (anim.GetBool ("Next")) {
				// Check the current state and set the boolean Next to false if the state name is different
				currentState = anim.GetCurrentAnimatorStateInfo (0);
				if (previousState.fullPathHash != currentState.fullPathHash) {
					anim.SetBool ("Next", false);
					previousState = currentState;				
				}
			}
		
			// Process when the "Back" flag is true
			if (anim.GetBool ("Back")) {
				// Check the current state and set the boolean Back to false if the state name is different
				currentState = anim.GetCurrentAnimatorStateInfo (0);
				if (previousState.fullPathHash != currentState.fullPathHash) {
					anim.SetBool ("Back", false);
					previousState = currentState;
				}
			}
		}

		void OnGUI ()
		{
            if (isGUI)
            {
                GUI.Box(new Rect(Screen.width - 110, 10, 100, 90), "Change Motion");
                if (GUI.Button(new Rect(Screen.width - 100, 40, 80, 20), "Next"))
                    anim.SetBool("Next", true);
                if (GUI.Button(new Rect(Screen.width - 100, 70, 80, 20), "Back"))
                    anim.SetBool("Back", true);
            }
		}


		// Functions for Random Decision
		IEnumerator RandomChange ()
		{
			// Infinite Loop Start
			while (true) {
				//IF Random Decision Switch On
				if (_random) {
					// Extract a random seed and set the flag by its size
					float _seed = Random.Range (0.0f, 1.0f);
					if (_seed < _threshold) {
						anim.SetBool ("Back", true);
					} else if (_seed >= _threshold) {
						anim.SetBool ("Next", true);
					}
				}
				// Put an interval until the next decision
				yield return new WaitForSeconds (_interval);
			}

		}

	}
}
