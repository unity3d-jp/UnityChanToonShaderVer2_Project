using System;
using UnityEngine;

namespace UnityChan.ImageEffects
{
    [ExecuteInEditMode]
    [RequireComponent (typeof (Camera))]
    [AddComponentMenu ("UnityChan/Image Effects/UTS Edge Detection")]
    public class UTS_EdgeDetection : PostEffectsBase
    {
        public enum EdgeDetectMode
        {
            SobelDepth = 0,
            SobelDepthThin = 1,
            RobertsCrossDepthNormals = 2,
            SobelColor = 3,
        }


        public EdgeDetectMode mode = EdgeDetectMode.SobelColor;
        public float sensitivityDepth = 1.0f;
        public float sensitivityNormals = 1.0f;
        public float edgeExp = 1.0f;
        public float sampleDist = 1.0f;
        public Color edgesColor = Color.grey;
        public float filterPower = 0.1f;
        public float threshold = 0.5f;
        public float edgesOnly = 0.0f;
//Attach UTS_EdgeDetect.shader
        public Shader edgeDetectShader;
        private Material edgeDetectMaterial = null;
        private EdgeDetectMode oldMode = EdgeDetectMode.SobelColor; 

        public override bool CheckResources ()
		{
            CheckSupport (true);

            edgeDetectMaterial = CheckShaderAndCreateMaterial (edgeDetectShader,edgeDetectMaterial);
            if (mode != oldMode)
                SetCameraFlag ();

            oldMode = mode;

            if (!isSupported)
                ReportAutoDisable ();
            return isSupported;
        }

        new void Start ()
		{
            oldMode	= mode;
        }

        void SetCameraFlag ()
		{
            if(mode == EdgeDetectMode.SobelDepth || mode == EdgeDetectMode.SobelDepthThin){ 
                GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
            }
            else if (mode == EdgeDetectMode.RobertsCrossDepthNormals){
                GetComponent<Camera>().depthTextureMode = DepthTextureMode.DepthNormals;
            }    
        }

        void OnEnable ()
		{
            SetCameraFlag();
        }

        [ImageEffectOpaque]
        void OnRenderImage (RenderTexture source, RenderTexture destination)
		{
             if (CheckResources () == false)
			 {
                 Graphics.Blit (source, destination);
                 return;
             }

            edgeDetectMaterial.SetVector ("_EdgesColor", edgesColor);
            edgeDetectMaterial.SetFloat ("_Exponent", edgeExp);
            edgeDetectMaterial.SetFloat ("_SampleDistance", sampleDist);
            edgeDetectMaterial.SetFloat ("_FilterPower", filterPower);
            edgeDetectMaterial.SetFloat ("_Threshold", threshold);
            edgeDetectMaterial.SetFloat ("_BgFade", edgesOnly);

            Vector2 sensitivity = new Vector2 (sensitivityDepth, sensitivityNormals);
            edgeDetectMaterial.SetVector ("_Sensitivity", new Vector4 (sensitivity.x, sensitivity.y, 1.0f, sensitivity.y));

            Graphics.Blit (source, destination, edgeDetectMaterial, (int) mode);

        }
    }
}
