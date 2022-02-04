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
    [RequireComponent(typeof(Camera))]
    internal class DebugShadowmap : MonoBehaviour
    {
        // flags
        bool m_initialized = false;
        bool m_srpCallbackInitialized = false;
        [SerializeField]
        internal bool m_enableShadowmapDebugging = false;
        [SerializeField]
        internal bool m_enableSelfShadowDebugging = false;
        [SerializeField]
        internal bool m_enableBinalization = false;
        [SerializeField]
        internal bool m_enableOutlineDebugging = false;


  
        const string kDebugShadowmapDefine = "UTS_DEBUG_SHADOWMAP";
        const string kDebugSelfShadowDefine = "UTS_DEBUG_SELFSHADOW";
        const string kDebugDefineNoOutline = "UTS_DEBUG_SHADOWMAP_NO_OUTLINE";
        const string kDebugDefineBinalization = "UTS_DEBUG_SHADOWMAP_BINALIZATION";
        private static DebugShadowmap instance;
#if UNITY_EDITOR
#pragma warning restore CS0414
        bool m_isCompiling = false;
#endif


        void Awake()
        {
            if (instance == null)
            {
                instance = this as DebugShadowmap;
                return;
            }
            else if (instance == this)
            {
                return ;
            }
            Debug.LogError("There is DebugShadowmap instance in hierarchy.");
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
        }


        void Update()
        {

            Initialize();

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
            ApplyDebuggingFlag();
        }

        void EnableSelfShadowKeyword()
        {
            Shader.EnableKeyword(kDebugSelfShadowDefine);
        }

        void DisableSelfShadowKeyword()
        {
            Shader.DisableKeyword(kDebugSelfShadowDefine);
        }


        void EnableShadowmapKeyword()
        {
            Shader.EnableKeyword(kDebugShadowmapDefine);
        }
        void DisableShadowmapKeyword()
        {
            Shader.DisableKeyword(kDebugShadowmapDefine);
        }

        void EnableOutlineKeyword()
        {
            Shader.EnableKeyword(kDebugDefineNoOutline);
        }
        void DisableOutlineKeyword()
        {
            Shader.DisableKeyword(kDebugDefineNoOutline);
        }

        void EnableBinalizationKeyword()
        {
            Shader.EnableKeyword(kDebugDefineBinalization);
        }
        void DisableBinalizationKeyword()
        {
            Shader.DisableKeyword(kDebugDefineBinalization);
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

        void OnValidate()
        {
            ApplyDebuggingFlag();
        }

        void ApplyDebuggingFlag()
        {
            if (! this.enabled)
            {
                return;
            }
            if (m_enableShadowmapDebugging)
            {
                EnableShadowmapKeyword();
            }
            else
            {
                DisableShadowmapKeyword();
            }
            if (m_enableSelfShadowDebugging)
            {
                EnableSelfShadowKeyword();
            }
            else
            {
                DisableSelfShadowKeyword();
            }

            
            if (m_enableOutlineDebugging)
            {
                EnableOutlineKeyword();
            }
            else
            {
                DisableOutlineKeyword();
            }
            if (m_enableBinalization)
            {
                EnableBinalizationKeyword();
            }
            else
            {
                DisableBinalizationKeyword();
            }
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

            m_initialized = true;
        }


        void Release()
        {
            if (m_initialized)
            {
                DisableShadowmapKeyword();
            }

            m_initialized = false;

        }

    }


}
