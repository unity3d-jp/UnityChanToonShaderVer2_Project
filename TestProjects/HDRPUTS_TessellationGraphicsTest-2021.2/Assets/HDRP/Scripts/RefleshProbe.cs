using UnityEngine;
using System.Collections;
namespace UnityEngine.Rendering.Toon.HDRP.Samples
{
	public class RefleshProbe : MonoBehaviour
	{

		bool isReflesh = false;
		bool isButtonActive = true;
		public ReflectionProbe probeComponent;
		private int renderID;




		// Update is called once per frame
		void Update()
		{
			if (isReflesh)
			{
				renderID = probeComponent.RenderProbe();
				//isButtonActive = false;
				isReflesh = false;
			}
			if (probeComponent.IsFinishedRendering(renderID))
			{
				isButtonActive = true;
			}
		}


		void OnGUI()
		{
			GUI.Box(new Rect(Screen.width - 110, Screen.height - 65, 100, 50), "ReflectionProbe");
			if (isButtonActive)
			{
				if (GUI.Button(new Rect(Screen.width - 100, Screen.height - 40, 80, 20), "Reflesh"))
				{
					isReflesh = true;
					isButtonActive = false;
				}
			}
		}
	}
}