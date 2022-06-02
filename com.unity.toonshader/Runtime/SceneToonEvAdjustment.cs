using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using UnityObject = UnityEngine.Object;
namespace Unity.Rendering.Toon
{

     
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [UTSHelpURL("ToonEVAdjustment")]

    internal class SceneToonEvAdjustment : MonoBehaviour
    {
        // flags
        bool m_initialized = false;
        bool m_srpCallbackInitialized = false;

        const int kAdjustmentCurvePrecision = 128;

        const string kCompensationPorpName = "_ToonEvAdjustmentCompensation";
        const string kExposureAdjustmentPropName = "_ToonEvAdjustmentCurve";
        const string kExposureArrayPropName = "_ToonEvAdjustmentValueArray";
        const string kExposureMinPropName   = "_ToonEvAdjustmentValueMin";
        const string kExposureMaxPropName   = "_ToonEvAdjustmentValueMax";
        const string kToonLightFilterPropName = "_ToonLightHiCutFilter";
        const string kIgonoreVolumeExposurePropName = "_ToonIgnoreExposureMultiplier";

        [SerializeField]
        internal bool m_ToonLightHiCutFilter= false;
        [SerializeField]
        internal bool m_ExposureAdjustmnt = false;
        [SerializeField]
        internal bool m_IgnorVolumeExposure= false;
        [SerializeField]
        internal AnimationCurve m_AnimationCurve = DefaultAnimationCurve();
        [SerializeField]
        internal float[] m_ExposureArray;
        [SerializeField]
        internal float m_Max, m_Min;
        [SerializeField]
        internal float m_Compensation;


        private static SceneToonEvAdjustment instance;
#if UNITY_EDITOR
#pragma warning restore CS0414
        bool m_isCompiling = false;
#endif


        void Awake()
        {
            if (instance == null)
            {
                instance = this as SceneToonEvAdjustment;
                return;
            }
            else if (instance == this)
            {
                return ;
            }
            Debug.LogError("There is ToonEvAdjustmentCurve instance in hierarchy.");
#if UNITY_EDITOR
            DestroyImmediate(this);
            Selection.activeGameObject = instance.gameObject;
#else
            Destroy(this);
#endif
        }

        void Reset()
        {
            OnDisable();
            OnEnable();
            DefaultAnimationCurve();
        }

        internal static AnimationCurve DefaultAnimationCurve()
        {
            return AnimationCurve.Linear(-10f, -10f, -1.32f, -1.32f);
        }
        void Update()
        {

            Initialize();



            // Fail safe in case the curve is deleted / has 0 point
            var curve = m_AnimationCurve;
            

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
                //                Release(); no need
                return; // 
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
            Shader.SetGlobalInt(kExposureAdjustmentPropName, m_ExposureAdjustmnt ? 1 : 0);
            Shader.SetGlobalInt(kToonLightFilterPropName, m_ToonLightHiCutFilter ? 1 : 0);
            Shader.SetGlobalInt(kIgonoreVolumeExposurePropName, m_IgnorVolumeExposure ? 1: 0);
            Shader.SetGlobalFloat(kCompensationPorpName, m_Compensation);
        }

        void EnableSrpCallbacks()
        {

            if (!m_srpCallbackInitialized)
            {
                m_srpCallbackInitialized = true;
            }
        }
        void DisableSrpCallbacks()
        {
            if (m_srpCallbackInitialized)
            {
                m_srpCallbackInitialized = false;
            }
        }

        void OnEnable()
        {

            Initialize();

            EnableSrpCallbacks();

        }

        void OnDisable()
        {
            DisableSrpCallbacks();

            Release();
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


        void Release()
        {
            if (m_initialized)
            {
                m_ExposureArray = null;
                Shader.SetGlobalInt(kExposureAdjustmentPropName, 0);
                Shader.SetGlobalInt(kToonLightFilterPropName, 0);
            }

            m_initialized = false;

        }

    }


}
