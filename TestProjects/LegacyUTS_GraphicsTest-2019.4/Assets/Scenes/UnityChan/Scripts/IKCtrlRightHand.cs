//
//IKCtrlRightHand.cs
//
//Sample script for IK Control of Unity-Chan's right hand.
//
//2014/06/20 N.Kobayashi
//
using UnityEngine;
using System.Collections;

namespace UnityEngine.Rendering.Toon.Samples
{
	[RequireComponent(typeof(Animator))]
	public class IKCtrlRightHand : MonoBehaviour
	{

		private Animator anim;
		public Transform targetObj = null;
		public bool isIkActive = false;
		public float mixWeight = 1.0f;

		void Awake ()
		{
			anim = GetComponent<Animator> ();
		}

		void Update ()
		{
			//Kobayashi
			if (mixWeight >= 1.0f)
				mixWeight = 1.0f;
			else if (mixWeight <= 0.0f)
				mixWeight = 0.0f;
		}

		void OnAnimatorIK (int layerIndex)
		{
			if (isIkActive) {
				anim.SetIKPositionWeight (AvatarIKGoal.RightHand, mixWeight);
				anim.SetIKRotationWeight (AvatarIKGoal.RightHand, mixWeight);
				anim.SetIKPosition (AvatarIKGoal.RightHand, targetObj.position);
				anim.SetIKRotation (AvatarIKGoal.RightHand, targetObj.rotation);
			}
		}

		void OnGUI ()
		{
			Rect rect1 = new Rect (10, Screen.height - 20, 400, 30);
			isIkActive = GUI.Toggle (rect1, isIkActive, "IK Active");
		}

	}
}