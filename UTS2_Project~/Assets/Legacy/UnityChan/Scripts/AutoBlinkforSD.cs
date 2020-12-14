//
//AutoBlinkforSD.cs
//Auto Blink Script for SD unity-chan model
//2014/12/10 N.Kobayashi
//
using UnityEngine;
using System.Collections;

namespace UnityEngine.Rendering.Toon.Samples
{
	public class AutoBlinkforSD : MonoBehaviour
	{

		public bool isActive = true;				//Activate Auto Blink
		public SkinnedMeshRenderer ref_face;	//ref for _face
		public float ratio_Close = 85.0f;			//Closed Eye blendshape ratio
		public float ratio_HalfClose = 20.0f;		//Half-Closed Eye blendshape ratio
		public int index_EYE_blk = 0;			//Morph index for eye-blink
		public int index_EYE_sml = 1;			//Morph index not to Blink
		public int index_EYE_dmg = 15;			//Morph index not to Blink


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

		///Reset Timer
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
			ref_face.SetBlendShapeWeight (index_EYE_blk, ratio_Close);
		}

		void SetHalfCloseEyes ()
		{
			ref_face.SetBlendShapeWeight (index_EYE_blk, ratio_HalfClose);
		}

		void SetOpenEyes ()
		{
			ref_face.SetBlendShapeWeight (index_EYE_blk, ratio_Open);
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
						//Skip when the morphs do not want to blink.
						if(ref_face.GetBlendShapeWeight(index_EYE_sml)==0.0f && ref_face.GetBlendShapeWeight(index_EYE_dmg)==0.0f){
							isBlink = true;
						}
					}
				}
				// Put an interval until the next decision
				yield return new WaitForSeconds (interval);
			}
		}
	}
}