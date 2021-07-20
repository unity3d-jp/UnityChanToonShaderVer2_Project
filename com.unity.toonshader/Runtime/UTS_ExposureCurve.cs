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
    public class UTS_ExposureCurve : MonoBehaviour
    {

        const int k_ExposureCurvePrecision = 128;
        Texture2D m_ExposureCurveTexture;
        const string kExposureAdjustmentPropName = "_ExposureAdjustment";

        [SerializeField]
        public bool m_expssureAdjustmnt = true;
        [SerializeField]
        public float m_ExopsureMultiplier = 0.0f;
        bool m_initialized = false;
        bool m_srpCallbackInitialized = false;
#if UNITY_EDITOR
#pragma warning restore CS0414
        bool m_isCompiling = false;
#endif



        void Update()
        {

            Initialize();

            Shader.SetGlobalInt(kExposureAdjustmentPropName, m_expssureAdjustmnt ? 1 : 0);

#if UNITY_EDITOR
            // handle script recompile
            if (EditorApplication.isCompiling && !m_isCompiling)
            {
                // on compile begin
                m_isCompiling = true;
                Release();
            }
            else if (!EditorApplication.isCompiling && m_isCompiling)
            {
                // on compile end
                m_isCompiling = false;
            }
#endif

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
            Debug.Assert(m_ExposureCurveTexture == null);
            m_ExposureCurveTexture = new Texture2D(k_ExposureCurvePrecision, 1, TextureFormat.RHalf, false, true)
            {
                name = "Exposure Curve",
                filterMode = FilterMode.Bilinear,
                wrapMode = TextureWrapMode.Clamp
            };


            m_initialized = true;
        }


        void Release()
        {
            if (m_initialized)
            {
                 DestroyUnityObject(m_ExposureCurveTexture);
                 m_ExposureCurveTexture = null;
            }

            m_initialized = false;

        }

        public static void DestroyUnityObject(UnityObject obj)
        {
            if (obj != null)
            {
#if UNITY_EDITOR
                if (Application.isPlaying)
                    UnityObject.Destroy(obj);
                else
                    UnityObject.DestroyImmediate(obj);
#else
                UnityObject.Destroy(obj);
#endif
            }
        }
    }


}
