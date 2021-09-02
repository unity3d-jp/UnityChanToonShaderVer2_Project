//
// SetShadowResolution.cs
// This script to set up a custom resolution to the shadow map.
// Attaching to the main light. 
//
using UnityEngine;

namespace UnityEngine.Rendering.Toon.Samples
{
	public class SetShadowResolution : MonoBehaviour {
		public int resolution;
		
		void Update () {
				GetComponent<Light>().shadowCustomResolution = resolution;
		}
	}
}