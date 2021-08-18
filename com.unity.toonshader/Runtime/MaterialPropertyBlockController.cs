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
    public class MaterialPropertyBlockController : MonoBehaviour
    {
        const string kExposureAdjustmentPorpName = "_ToonEvAdjustmentCurve";
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
        public int m_HighCutFilter = 1000000;
        [SerializeField]
        internal AnimationCurve m_AnimationCurve = DefaultAnimationCurve();
        [SerializeField]
        internal float[] m_ExposureArray;
        [SerializeField]
        internal float m_Max, m_Min;

        public GameObject[] objs;
        [SerializeField]
        Renderer[] renderers;
        [SerializeField]
        MaterialPropertyBlock[] materialPropertyBlocks;

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

        static AnimationCurve DefaultAnimationCurve()
        {
            return AnimationCurve.Linear(-10f, -10f, -1.32f, -1.32f);
        }

        private void Awake()
        {
            if (objs == null || objs.Length == 0)
            {
                return;
            }
            int objCount = objs.Length;
            int rendererCount = 0;
            for (int ii = 0; ii < objCount; ii++)
            {
                var renderer = objs[ii].GetComponent<Renderer>();
                if (renderer == null)
                {
                    continue;
                }
                rendererCount++;

            }
            if (rendererCount != 0)
            {
                renderers = new Renderer[rendererCount];
                materialPropertyBlocks = new MaterialPropertyBlock[rendererCount];
                int rendererCount2 = 0;
                for (int ii = 0; ii < objCount; ii++)
                {
                    var renderer = objs[ii].GetComponent<Renderer>();
                    if (renderer == null)
                    {
                        continue;
                    }
                    renderers[rendererCount2] = renderer;
                    materialPropertyBlocks[rendererCount2] = new MaterialPropertyBlock();
                    rendererCount2++;

                }
                Debug.Assert(rendererCount2 == rendererCount);
            }

        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (renderers == null || renderers.Length == 0)
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



            Color col = Color.green;
            int length = renderers.Length;
            for (int ii = 0; ii < length; ii++)
            {
                renderers[ii].GetPropertyBlock(materialPropertyBlocks[ii]);

//                materialPropertyBlocks[ii].SetColor("_UnlitColor", col);
                materialPropertyBlocks[ii].SetFloatArray(kExposureArrayPropName, m_ExposureArray);
                materialPropertyBlocks[ii].SetFloat(kExposureMinPropName, m_Min);
                materialPropertyBlocks[ii].SetFloat(kExposureMaxPropName, m_Max);
                materialPropertyBlocks[ii].SetInt(kExposureAdjustmentPorpName, m_ExposureAdjustmnt ? 1 : 0);
                materialPropertyBlocks[ii].SetInt(kToonLightFilterPropName, m_ToonLightHiCutFilter ? 1 : 0);


                renderers[ii].SetPropertyBlock(materialPropertyBlocks[ii]);
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
                Shader.SetGlobalInt(kExposureAdjustmentPorpName, 0);
                Shader.SetGlobalInt(kToonLightFilterPropName, 0);
            }

            m_initialized = false;

        }

    }
}