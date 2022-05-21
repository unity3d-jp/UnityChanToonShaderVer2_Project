//CameraController.cs for UnityChan
//Original Script is here:
//TAK-EMI / CameraController.cs
//https://gist.github.com/TAK-EMI/d67a13b6f73bed32075d
//https://twitter.com/TAK_EMI
//
//Revised by N.Kobayashi 2014/5/15 
//Change : To prevent rotation flips on XY plane, use Quaternion in cameraRotate()
//Change : Add the instrustion window
//Change : Add the operation for Mac
//




using UnityEngine;
using System.Collections;

namespace UnityEngine.Rendering.Toon.Samples
{

	//[ExecuteAlways]
	public class CameraController2 : MonoBehaviour
	{

		[SerializeField]
		private GameObject focusObj = null;
		[SerializeField]
		private Vector3 startPosition = new Vector3(0.1909977f, 1.075639f, 0.5440816f);
		[SerializeField]
		private Vector3 startRotationEuler = new Vector3(19.803f, 196.295f, 0);

		[SerializeField]
		private Vector3 endPosition = new Vector3(0, 0.84f, 0.64f);
		[SerializeField]
		private Vector3 endRotationEuler = new Vector3(0, 180, 0);

		[SerializeField]
		private Vector3 endRotationEuler2 = new Vector3(0, 180, -45);
		

		[SerializeField]
		int framesBetween = 240;
		[SerializeField]
		int frameBeforeStart = 60;
		[SerializeField]
		int frameAfterEend = 60;
		[SerializeField]
		AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0.0f, 1.0f, 1.0f);

		[SerializeField]
		bool stabilizeCameraMode = false;
		int currentFrame = 0;



		private Vector3 oldPos;

        private void OnEnable()
        {
			Camera.main.transform.position = startPosition;
			Camera.main.transform.rotation = Quaternion.Euler(startRotationEuler);
			currentFrame = 0;
		}


		void LateUpdate ()
		{
			currentFrame = currentFrame % (framesBetween + frameBeforeStart + frameAfterEend);
			float currentRate = (float)(currentFrame - frameBeforeStart) / (float)(framesBetween);
			if (currentFrame < frameBeforeStart)
			{
				currentRate = 0;
			}
			if (currentFrame >= frameBeforeStart + framesBetween)
			{
				currentRate = 1.0f;
			}

			currentRate = animationCurve.Evaluate(currentRate);

			Quaternion qt = Quaternion.Lerp(Quaternion.Euler(startRotationEuler), Quaternion.Euler(endRotationEuler), currentRate);

			Vector3 position = Vector3.Lerp(startPosition, endPosition, currentRate);

			if ( stabilizeCameraMode )
            {
				qt = Quaternion.Lerp(Quaternion.Euler(endRotationEuler), Quaternion.Euler(endRotationEuler2), currentRate);
				position = endPosition;

			}

			Camera.main.transform.position = position;
			Camera.main.transform.rotation = qt;
			currentFrame++;

		}


	}
}