//
// SetShadowResolution.cs
// シャドウマップにカスタムレゾリューションを設定するスクリプト.
// メインライトにアタッチすること. 
//
using UnityEngine;

namespace UnityEngine.Rendering.Toon.HDRP.Samples
{
	internal class SetShadowResolution : MonoBehaviour {
		public int resolution;
		
		void Update () {
				GetComponent<Light>().shadowCustomResolution = resolution;
		}
	}
}