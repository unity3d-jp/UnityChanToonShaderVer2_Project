//
//AutoBlink.cs
//Auto Blink Script
//2014/06/23 N.Kobayashi
//
using UnityEngine;
using System.Collections;

namespace UnityEngine.Rendering.Toon.Samples
{
	public class AutoBlink : MonoBehaviour
	{

		public bool isActive = true;				//Activate Auto Blink
		public SkinnedMeshRenderer ref_SMR_EYE_DEF;	//Ref for EYE_DEF
		public SkinnedMeshRenderer ref_SMR_EL_DEF;	//Ref for EL_DEF
		public float ratio_Close = 85.0f;			//Closed Eye blendshape ratio
		public float ratio_HalfClose = 20.0f;		//Half-Closed Eye blendshape ratio
		[HideInInspector]
		public float
			ratio_Open = 0.0f;
		private bool timerStarted = false;			//for Start timer
		private bool isBlink = false;				//For Blink management

		public float timeBlink = 0.4f;				//Time for Blink
		private float timeRemining = 0.0f;			//Timer Remaining Time

		public float threshold = 0.3f;				//Random Decision Threshold
		public float interval = 3.0f;				//Random Decision Interval



		enum Status
		{
			Close,
			HalfClose,
			Open	//Status of Blink
		}


		private Status eyeStatus;	//Current Blink status

		void Awake ()
		{
			//ref_SMR_EYE_DEF = GameObject.Find("EYE_DEF").GetComponent<SkinnedMeshRenderer>();
			//ref_SMR_EL_DEF = GameObject.Find("EL_DEF").GetComponent<SkinnedMeshRenderer>();
		}



		// Use this for initialization
		void Start ()
		{
			ResetTimer ();
			// Start a function for random determination
			StartCoroutine ("RandomChange");
		}

		//Reset Timer
		void ResetTimer ()
		{
			timeRemining = timeBlink;
			timerStarted = false;
		}

		// Update is called once per frame
		void Update ()
		{
			if (!timerStarted) {
				eyeStatus = Status.Close;
				timerStarted = true;
			}
			if (timerStarted) {
				timeRemining -= Time.deltaTime;
				if (timeRemining <= 0.0f) {
					eyeStatus = Status.Open;
					ResetTimer ();
				} else if (timeRemining <= timeBlink * 0.3f) {
					eyeStatus = Status.HalfClose;
				}
			}
		}

		void LateUpdate ()
		{
			if (isActive) {
				if (isBlink) {
					switch (eyeStatus) {
					case Status.Close:
						SetCloseEyes ();
						break;
					case Status.HalfClose:
						SetHalfCloseEyes ();
						break;
					case Status.Open:
						SetOpenEyes ();
						isBlink = false;
						break;
					}
					//Debug.Log(eyeStatus);
				}
			}
		}

		void SetCloseEyes ()
		{
			ref_SMR_EYE_DEF.SetBlendShapeWeight (6, ratio_Close);
			ref_SMR_EL_DEF.SetBlendShapeWeight (6, ratio_Close);
		}

		void SetHalfCloseEyes ()
		{
			ref_SMR_EYE_DEF.SetBlendShapeWeight (6, ratio_HalfClose);
			ref_SMR_EL_DEF.SetBlendShapeWeight (6, ratio_HalfClose);
		}

		void SetOpenEyes ()
		{
			ref_SMR_EYE_DEF.SetBlendShapeWeight (6, ratio_Open);
			ref_SMR_EL_DEF.SetBlendShapeWeight (6, ratio_Open);
		}
		
		// Function for Random Decision
		IEnumerator RandomChange ()
		{
			// Start infinite loop
			while (true) {
				//Seed generation for random decisions
				float _seed = Random.Range (0.0f, 1.0f);
				if (!isBlink) {
					if (_seed > threshold) {
						isBlink = true;
					}
				}
				// Put an interval until the next decision
				yield return new WaitForSeconds (interval);
			}
		}
	}
}