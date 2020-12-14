//
//RandomWind.cs for unity-chan!
//
//Original Script is here:
//ricopin / RandomWind.cs
//Rocket Jump : http://rocketjump.skr.jp/unity3d/109/
//https://twitter.com/ricopin416
//
//Modification 2014/12/20
//Add directional change/gravity influence on the wind.
//

using UnityEngine;
using System.Collections;

namespace UnityEngine.Rendering.Toon.Samples
{
	public class RandomWind : MonoBehaviour
	{
		private SpringBone[] springBones;
		public bool isWindActive = false;

		private bool isMinus = false;				//For wind direction reversal.
		public float threshold = 0.5f;				// Random Decision Threshold.
		public float interval = 5.0f;				// Random Decision Interval.
		public float windPower = 1.0f;				//Wind strength.
		public float gravity = 0.98f;				//Strength of gravity.
        public bool isGUI = true;


		// Use this for initialization
		void Start ()
		{
			springBones = GetComponent<SpringManager> ().springBones;
			StartCoroutine ("RandomChange");
		}





		// Update is called once per frame
		void Update ()
		{

			Vector3 force = Vector3.zero;
			if (isWindActive) {
				if(isMinus){
					force = new Vector3 (Mathf.PerlinNoise (Time.time, 0.0f) * windPower * -0.001f , gravity * -0.001f , 0);
				}else{
					force = new Vector3 (Mathf.PerlinNoise (Time.time, 0.0f) * windPower * 0.001f, gravity * -0.001f, 0);
				}

				for (int i = 0; i < springBones.Length; i++) {
					springBones [i].springForce = force;
				}
			
			}
		}

		void OnGUI ()
		{
            if (isGUI)
            {
                Rect rect1 = new Rect(10, Screen.height - 40, 400, 30);
                isWindActive = GUI.Toggle(rect1, isWindActive, "Random Wind");
            }
		}

		// Function for random decision.
		IEnumerator RandomChange ()
		{
			// Infinite loop start.
			while (true) {
				//Seed generation for random decisions.
				float _seed = Random.Range (0.0f, 1.0f);

				if (_seed > threshold) {
					//Invert the sign when the _seed is greater than threshold.
					isMinus = true;
				}else{
					isMinus = false;
				}

				// Put an interval until the next decision.
				yield return new WaitForSeconds (interval);
			}
		}


	}
}