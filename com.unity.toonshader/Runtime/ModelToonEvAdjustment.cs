using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using UnityObject = UnityEngine.Object;
using System.Linq;

namespace Unity.Rendering.Toon
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [UTSHelpURL("ToonEVAdjustment")]


    internal class ModelToonEvAdjustment : MonoBehaviour
    {
        const string kCompensationPorpName = "_ToonEvAdjustmentCompensation";
        const string kExposureAdjustmentPropName = "_ToonEvAdjustmentCurve";
        const string kExposureArrayPropName = "_ToonEvAdjustmentValueArray";
        const string kExposureMinPropName = "_ToonEvAdjustmentValueMin";
        const string kExposureMaxPropName = "_ToonEvAdjustmentValueMax";
        const string kToonLightFilterPropName = "_ToonLightHiCutFilter";

        // flags
        bool m_initialized = false;
        bool m_srpCallbackInitialized = false;

        const int kAdjustmentCurvePrecision = 128;




        [SerializeField]
        internal bool m_ToonLightHiCutFilter = false;
        [SerializeField]
        internal bool m_ExposureAdjustmnt = false;
        [SerializeField]
        internal bool m_IgnorVolumeExposure = false;
        [SerializeField]
        internal AnimationCurve m_AnimationCurve = DefaultAnimationCurve();
        [SerializeField]
        internal float[] m_ExposureArray;
        [SerializeField]
        internal float m_Max, m_Min;
        [SerializeField]
        internal float m_Compensation;

        internal GameObject[] m_Objs;
        [SerializeField]
//        [HideInInspector]
        Renderer[] m_Renderers;
        [SerializeField]
//        [HideInInspector]
        MaterialPropertyBlock[] m_MaterialPropertyBlocks;

#if UNITY_EDITOR
#pragma warning restore CS0414
        bool m_isCompiling = false;
#endif

        void Reset()
        {
            OnDisable();
            OnEnable();
            DefaultAnimationCurve();
        }

        void OnValidate()
        {
            Release();
            Initialize();
        }
        static AnimationCurve DefaultAnimationCurve()
        {
            return AnimationCurve.Linear(-10f, -10f, -1.32f, -1.32f);
        }

        private void Awake()
        {
            Initialize();

        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (m_Renderers == null || m_Renderers.Length == 0)
            {
                return;
            }

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



            int length = m_Renderers.Length;
            for (int ii = 0; ii < length; ii++)
            {
                m_Renderers[ii].GetPropertyBlock(m_MaterialPropertyBlocks[ii]);

                m_MaterialPropertyBlocks[ii].SetFloatArray(kExposureArrayPropName, m_ExposureArray);
                m_MaterialPropertyBlocks[ii].SetFloat(kExposureMinPropName, m_Min);
                m_MaterialPropertyBlocks[ii].SetFloat(kExposureMaxPropName, m_Max);
                m_MaterialPropertyBlocks[ii].SetInt(kExposureAdjustmentPropName, m_ExposureAdjustmnt ? 1 : 0);
                m_MaterialPropertyBlocks[ii].SetInt(kToonLightFilterPropName, m_ToonLightHiCutFilter ? 1 : 0);
                m_MaterialPropertyBlocks[ii].SetFloat(kCompensationPorpName, m_Compensation);

                m_Renderers[ii].SetPropertyBlock(m_MaterialPropertyBlocks[ii]);
            }

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
            // must be put to gameObject model chain.
            if (m_Objs == null || m_Objs.Length == 0)
            {
                m_Objs = new GameObject[1];
                m_Objs[0] = this.gameObject;
            }
            int objCount = m_Objs.Length;
            int rendererCount = 0;

            List<Renderer> rendererList = new List<Renderer>();
            for (int ii = 0; ii < objCount; ii++)
            {
                if (m_Objs[ii] == null )
                {
                    continue;
                }


                var renderer = m_Objs[ii].GetComponent<Renderer>();
                if (renderer != null)
                {
                    rendererCount++;
                    rendererList.Add(renderer);
                }
                GameObject[] childGameObjects = m_Objs[ii].GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToArray();
                int childCount = childGameObjects.Length;
                for (int jj = 0; jj < childCount; jj++)
                {
                    if (m_Objs[ii] == childGameObjects[jj])
                        continue;
                    var modelToonEvAdjustment = childGameObjects[jj].GetComponent<ModelToonEvAdjustment>();
                    if ( modelToonEvAdjustment != null )
                    {

                        break;
                    }
                    renderer = childGameObjects[jj].GetComponent<Renderer>();
                    if ( renderer != null )
                    {
                        rendererList.Add(renderer);
                        rendererCount++;
                    }
                }
            }
            if (rendererCount != 0)
            {
 
                m_MaterialPropertyBlocks = new MaterialPropertyBlock[rendererCount];
                m_Renderers = rendererList.ToArray();


                for (int ii = 0; ii < rendererCount; ii++)
                {
                    m_MaterialPropertyBlocks[ii] = new MaterialPropertyBlock();
                }
            }
            if (m_ExposureArray == null || m_ExposureArray.Length != kAdjustmentCurvePrecision)
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
                if (m_Renderers != null )
                {
                    int length = m_Renderers.Length;
                    for (int ii = 0; ii < length; ii++)
                    {
                        m_Renderers[ii].SetPropertyBlock(null);
                    }
                }
                m_Renderers = null;
                m_MaterialPropertyBlocks = null;
            }

            m_initialized = false;

        }

    }
}