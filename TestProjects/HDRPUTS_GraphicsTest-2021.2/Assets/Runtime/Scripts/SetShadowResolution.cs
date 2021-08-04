//
// SetShadowResolution.cs
// シャドウマップにカスタムレゾリューションを設定するスクリプト.
// メインライトにアタッチすること. 
//
using UnityEngine;

namespace UnityEngine.Rendering.Toon.HDRP.Samples
{
	public class SetShadowResolution : MonoBehaviour {
		public int resolution;
		
		void Update () {
				GetComponent<Light>().shadowCustomResolution = resolution;
		}
	}
}