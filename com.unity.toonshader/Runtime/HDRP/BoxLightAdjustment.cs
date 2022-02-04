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

#if HDRP_IS_INSTALLED_FOR_UTS
using UnityEngine.Rendering.HighDefinition;

namespace Unity.Rendering.HighDefinition.Toon
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Light))]
    internal class BoxLightAdjustment : MonoBehaviour
    {


        // flags
        bool m_initialized = false;
        bool m_srpCallbackInitialized = false;

        [SerializeField]
        GameObject[] m_GameObjects;

        [SerializeField]
        Renderer[] m_Renderers;


        [SerializeField]
        internal HDAdditionalLightData m_targetBoxLight;

        [SerializeField]
        internal bool  m_FollowGameObjectPosition = false;

        [SerializeField]
        internal bool m_FollowGameObjectRotation = false;
        [SerializeField]
        internal Vector3 m_PositionOffset;
        [SerializeField]
        internal Quaternion m_RotationOffset;
#if UNITY_EDITOR
#pragma warning restore CS0414
        bool m_isCompiling = false;
#endif

        void Reset()
        {
            OnDisable();
            OnEnable();

        }

        internal void OnValidate()
        {
            Release();
            Initialize();
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
            if (m_Renderers == null)
            {
                return;
            }
            if (m_targetBoxLight == null)
            {
                return;
            }

            for (int ii = 0; ii < m_Renderers.Length; ii++)
            {
                m_Renderers[ii].renderingLayerMask &= 0xffffff00;
                m_Renderers[ii].renderingLayerMask |= (uint)m_targetBoxLight.lightlayersMask;
            }
            if ( /* m_targetBoxLight != null && */ m_GameObjects != null && m_GameObjects.Length > 0 && m_GameObjects[0] != null )
            {
                if (m_FollowGameObjectPosition )
                {
                    m_targetBoxLight.transform.position = m_GameObjects[0].transform.position + m_PositionOffset;
                }
                if (m_FollowGameObjectRotation )
                {
                    m_targetBoxLight.transform.rotation = m_GameObjects[0].transform.rotation * m_RotationOffset;
                }
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

        void UpdateObjectLightLayers()
        {
            Initialize();

        }

        internal static GameObject  CreateBoxLight(GameObject[] gameObjects)
        {
            if (gameObjects == null || gameObjects[0] == null )
            {
                Debug.LogError("Please, select a GameObject you want a Box Light to follow.");
                return null;
            }
            var gameObjectName = "Box Light for " + gameObjects[0].name;
            GameObject lightGameObject = new GameObject(gameObjectName);
#if UNITY_EDITOR
            Undo.RegisterCreatedObjectUndo(lightGameObject, "Created Boxlight adjustment");
#endif
            HDAdditionalLightData hdLightData = lightGameObject.AddHDLight(HDLightTypeAndShape.BoxSpot);
            // light size
            hdLightData.SetBoxSpotSize(new Vector2(10.0f, 10.0f)); // Size should be culculated with more acculacy?
            BoxLightAdjustment boxLightAdjustment = lightGameObject.GetComponent<BoxLightAdjustment>();
            if (boxLightAdjustment == null)
            {
#if UNITY_EDITOR
                boxLightAdjustment = Undo.AddComponent<BoxLightAdjustment>(lightGameObject);
#else
                boxLightAdjustment = lightGameObject.AddComponent<BoxLightAdjustment>();
#endif
            }



#if UNITY_EDITOR
            Undo.RecordObject(boxLightAdjustment, "target " + boxLightAdjustment.name);
#endif
            boxLightAdjustment.m_targetBoxLight = hdLightData;
#if UNITY_EDITOR
            Undo.RecordObject(lightGameObject.transform, "Position " + lightGameObject.transform.name);
#endif
            // position and rotation
            var goPos = lightGameObject.transform.position;
            goPos.y += 10.0f;
            lightGameObject.transform.position = goPos;

            var goRot = lightGameObject.transform.rotation;
            goRot.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
#if UNITY_EDITOR
            Undo.RecordObject(lightGameObject.transform, "Rotation " + lightGameObject.transform.name);
#endif
            hdLightData.gameObject.transform.rotation = goRot;

            // must be put to gameObject model chain.
            boxLightAdjustment.m_GameObjects = gameObjects;


            return lightGameObject;
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
            if (m_GameObjects == null )
            {
                return;
            }
            int objCount = m_GameObjects.Length;
            int rendererCount = 0;

            List<Renderer> rendererList = new List<Renderer>();
            for (int ii = 0; ii < objCount; ii++)
            {
                if (m_GameObjects[ii] == null )
                {
                    continue;
                }


                var renderer = m_GameObjects[ii].GetComponent<Renderer>();
                if (renderer != null)
                {
                    rendererCount++;
                    rendererList.Add(renderer);
                }
                GameObject[] childGameObjects = m_GameObjects[ii].GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToArray();
                int childCount = childGameObjects.Length;
                for (int jj = 0; jj < childCount; jj++)
                {
                    if (m_GameObjects[ii] == childGameObjects[jj])
                        continue;
                    var modelToonEvAdjustment = childGameObjects[jj].GetComponent<BoxLightAdjustment>();
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
                if (rendererCount != 0)
                {


                    m_Renderers = rendererList.ToArray();

                }
            }
            if (m_targetBoxLight != null  && objCount > 0 )
            {
                m_PositionOffset = m_targetBoxLight.transform.position - m_GameObjects[0].transform.position;
                m_RotationOffset = Quaternion.Inverse(m_GameObjects[0].transform.rotation) * m_targetBoxLight.transform.rotation;
            }
            
            m_initialized = true;
        }


        void Release()
        {
            if (m_initialized)
            {
                m_Renderers = null;
            }

            m_initialized = false;

        }

    }
}
#endif  // HDRP_IS_INSTALLED_FOR_UTS