using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using UnityObject = UnityEngine.Object;
using System;

#if SRPCORE_IS_INSTALLED_FOR_UTS
namespace Unity.Rendering.Toon
{
    /// <summary>
    /// A volume component that holds settings for the Toon Ev Adjustment Curve.
    /// </summary>
    [Serializable, VolumeComponentMenu("Toon/EV Adjustment Curve")]
    public sealed class ToonEvAdjustmentCurve : VolumeComponent
    {
        // flags
        bool m_initialized = false;
        bool m_srpCallbackInitialized = false;

        const int kAdjustmentCurvePrecision = 128;

  
        const string kExposureAdjustmentPorpName = "_ToonEvAdjustmentCurve";
        const string kExposureArrayPropName = "_ToonEvAdjustmentValueArray";
        const string kExposureMinPropName   = "_ToonEvAdjustmentValueMin";
        const string kExposureMaxPropName   = "_ToonEvAdjustmentValueMax";
        const string kToonLightFilterPropName = "_ToonLightHiCutFilter";



        [SerializeField]
        internal bool m_ExposureAdjustmnt = false;


        [SerializeField]
        internal float[] m_ExposureArray;
        [SerializeField]
        internal float m_Max, m_Min;
        [SerializeField]
        internal bool m_DebugUI;

#if UNITY_EDITOR
#pragma warning restore CS0414
        bool m_isCompiling = false;
#endif

        /// <summary>
        /// Specifies the method that Toon Shader uses hiCutFilter.
        /// This parameter is only used when <see cref="hiCutFilter"/> is set.
        /// </summary>
        [Tooltip("Specifies the method that Toon Shader uses hiCutFilter.")]
        public BoolParameter hiCutFilter = new BoolParameter(false);

        /// <summary>
        /// Specifies a curve that remaps the Toon exposure on the x-axis to the EV you want on the y-axis.
        /// This parameter is only used when <see cref="curveRemapping"/> is set.
        /// </summary>
        [Tooltip("Specifies a curve that remaps the Toon EV on the x-axis to the EV you want on the y-axis.")]
        public AnimationCurveParameter curveMap = new AnimationCurveParameter(AnimationCurve.Linear(-10f, -10f, -1.32f, -1.32f)); // TODO: Use TextureCurve instead?



 

        void Update()
        {
            if (!m_initialized)
            {
                return;
            }



            // Fail safe in case the curve is deleted / has 0 point
            var curve = curveMap.value;
            

            if (curve == null || curve.length == 0)
            {
                m_Min = 0f;
                m_Max = 0f;

                for (int i = 0; i < kAdjustmentCurvePrecision; i++)
                    m_ExposureArray[i] = 0.0f;
            }
            else
            {
                m_Min = curve[0].time;
                m_Max = curve[curve.length - 1].time;
                float step = (m_Max - m_Min) / (kAdjustmentCurvePrecision - 1f);

                for (int i = 0; i < kAdjustmentCurvePrecision; i++)
                    m_ExposureArray[i] = curve.Evaluate(m_Min + step * i);
            }


#if UNITY_EDITOR
            // handle script recompile
            if (EditorApplication.isCompiling && !m_isCompiling)
            {
                // on compile begin
                m_isCompiling = true;
                ReleaseToonEvAdjustmentCurve();
            }
            else if (!EditorApplication.isCompiling && m_isCompiling)
            {
                // on compile end
                m_isCompiling = false;
            }
#endif

            Shader.SetGlobalFloatArray(kExposureArrayPropName, m_ExposureArray);
            Shader.SetGlobalFloat(kExposureMinPropName, m_Min);
            Shader.SetGlobalFloat(kExposureMaxPropName, m_Max);
            Shader.SetGlobalInt(kExposureAdjustmentPorpName, curveMap.overrideState ? 1 : 0);
            Shader.SetGlobalInt(kToonLightFilterPropName, hiCutFilter.overrideState && hiCutFilter.value ? 1 : 0);


        }

        void EnableSrpCallbacks()
        {

            if (!m_srpCallbackInitialized)
            {
                RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
                RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
                m_srpCallbackInitialized = true;
            }
        }
        void DisableSrpCallbacks()
        {
            if (m_srpCallbackInitialized)
            {
                m_srpCallbackInitialized = false;
                RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
                RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
                m_srpCallbackInitialized = false;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            Initialize();

            EnableSrpCallbacks();

        }

        protected override void OnDisable()
        {
            DisableSrpCallbacks();

            ReleaseToonEvAdjustmentCurve();
            base.OnDisable();
        }

        void Initialize()
        {
            if (m_initialized)
            {
                return;
            }
#if UNITY_EDITOR
            // initializing renderer can interfere GI baking. so wait until it is completed.

            if (EditorApplication.isCompiling)
                return;
#endif

            if (m_ExposureArray == null || m_ExposureArray.Length != kAdjustmentCurvePrecision )
            {
                m_ExposureArray = new float[kAdjustmentCurvePrecision];
            }
            m_initialized = true;
        }


        void ReleaseToonEvAdjustmentCurve()
        {

            if (m_initialized)
            {
                m_ExposureArray = null;
                Shader.SetGlobalInt(kExposureAdjustmentPorpName, 0);
                Shader.SetGlobalInt(kToonLightFilterPropName, 0);

            }

            m_initialized = false;
            base.Release();
        }

        void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            Update();
        }

        void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            //    Finish();
        }
    }




    /// <summary>
    /// Methods that HDRP uses to change the exposure when the Camera moves from dark to light and vice versa.
    /// </summary>
    /// <seealso cref="Exposure.adjustmentMode"/>
    public enum ToonEVAdjustmentMode
    {
        /// <summary>
        /// No Adjustment
        /// </summary>
        NoAdjustment,

        /// <summary>
        /// The EV changes correspond with the curve.
        /// </summary>
        CurveAdjustment
    }

    /// <summary>
    /// Methods that HDRP uses to change the exposure when the Camera moves from dark to light and vice versa.
    /// </summary>
    /// <seealso cref="Exposure.adjustmentMode"/>
    public enum ToonHiCutFilter
    {
        /// <summary>
        /// No Adjustment
        /// </summary>
        Disable,

        /// <summary>
        /// The EV changes correspond with the curve.
        /// </summary>
        Enable
    }
    
    /// <summary>
    /// A <see cref="VolumeParameter"/> that holds a <see cref="ToonEVAdjustmentMode"/> value.
    /// </summary>
    [Serializable]
    public sealed class ToonEVAdjustmentModeParamater : VolumeParameter<ToonEVAdjustmentMode>
    {
        /// <summary>
        /// Creates a new <see cref="ToonEVAdjustmentModeParamater"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public ToonEVAdjustmentModeParamater(ToonEVAdjustmentMode value, bool overrideState = false) : base(value, overrideState) { }
    }
    /// <summary>
    /// A <see cref="VolumeParameter"/> that holds a <see cref="ToonHiCutFilter"/> value.
    /// </summary>
    [Serializable]
    public sealed class ToonHiCutFiltereParamater : VolumeParameter<ToonHiCutFilter>
    {
        /// <summary>
        /// Creates a new <see cref="ToonEVAdjustmentModeParamater"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public ToonHiCutFiltereParamater(ToonHiCutFilter value, bool overrideState = false) : base(value, overrideState) { }
    }
}
#endif //SRPCORE_IS_INSTALLED_FOR_UTS