//
// Set the Shadow Distance/Shadow Cascades/Cascade splits from the scene in QualitySettings/Shadows.
//
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Rendering.Toon.Samples
{
	[ExecuteInEditMode]
	public class SetShadowQuality : MonoBehaviour {

			public enum CascadeMode {
				Zero = 0,
				Two = 2,
				Four = 4,	
			}

		public ShadowProjection shadowProjection = ShadowProjection.CloseFit;
		public float shadowDistance  = 150.0f;
		public CascadeMode cascadeMode = CascadeMode.Four;
		public float twoCascadeSetting = 33.3f;
		public Vector3 fourCascadeSetting = new Vector3(6.7f,13.3f,26.7f);
		private Vector3 settingFor4Cascades;
		
		// Use this for initialization 
		void Start () {
		}
		
		void Update()
		{
			settingFor4Cascades = new Vector3((fourCascadeSetting.x)/100f, 
										(fourCascadeSetting.x + fourCascadeSetting.y)/100f, 
										(fourCascadeSetting.x + fourCascadeSetting.y + fourCascadeSetting.z)/100f);
		}


		void LateUpdate () {
			QualitySettings.shadowProjection = shadowProjection;
			QualitySettings.shadowDistance = shadowDistance;
			QualitySettings.shadowCascades = (int)cascadeMode;
			QualitySettings.shadowCascade2Split = twoCascadeSetting/100f;
			QualitySettings.shadowCascade4Split = settingFor4Cascades;
		}
	}
}
