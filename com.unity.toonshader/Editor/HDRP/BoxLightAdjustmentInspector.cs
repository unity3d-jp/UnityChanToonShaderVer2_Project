using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


#if HDRP_IS_INSTALLED_FOR_UTS
using Unity.Rendering.HighDefinition.Toon;
using UnityEngine.Rendering.HighDefinition;


namespace UnityEditor.Rendering.HighDefinition.Toon
{
    [CustomEditor(typeof(BoxLightAdjustment))]

    public class BoxLightAdjustmentInspector : Editor
    {
        private SerializedProperty m_GameObjectsProperties;

        List<string> m_layerNames;

        private void OnEnable()
        {
            m_GameObjectsProperties = serializedObject.FindProperty("m_GameObjects");
        }

        public override void OnInspectorGUI()
        {
            const string labelBoxLight = "Box Light";
            const string labelFollowPostion = "Follow Position";
            const string labelFollowRotation = "Follow Rotation";
            const string labelLightLeyer = "Light Layer";

            bool isChanged = false;

            var obj = target as BoxLightAdjustment;
            serializedObject.Update();
            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.LabelField(labelBoxLight);
            HDAdditionalLightData targetLight = EditorGUILayout.ObjectField(obj.m_targetBoxLight, typeof(HDAdditionalLightData), true) as HDAdditionalLightData;
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed the target box lihgt");
                obj.m_targetBoxLight = targetLight;
                isChanged = true;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginDisabledGroup(targetLight == null);
            {
                EditorGUI.indentLevel++;


                if (m_layerNames == null )
                {
                    m_layerNames = new List<string>();
                    m_layerNames.Add(LightLayerEnum.LightLayerDefault.ToString());
                    m_layerNames.Add(LightLayerEnum.LightLayer1.ToString());
                    m_layerNames.Add(LightLayerEnum.LightLayer2.ToString());
                    m_layerNames.Add(LightLayerEnum.LightLayer3.ToString());
                    m_layerNames.Add(LightLayerEnum.LightLayer4.ToString());
                    m_layerNames.Add(LightLayerEnum.LightLayer5.ToString());
                    m_layerNames.Add(LightLayerEnum.LightLayer6.ToString());
                    m_layerNames.Add(LightLayerEnum.LightLayer7.ToString());
                }

                HDAdditionalLightData lightData = targetLight != null ? targetLight.GetComponent<HDAdditionalLightData>() : null;
                LightLayerEnum lightLayers = lightData != null ? lightData.lightlayersMask :  LightLayerEnum.LightLayerDefault;
                EditorGUI.BeginChangeCheck();
                lightLayers = (LightLayerEnum)EditorGUILayout.MaskField(labelLightLeyer, (int)lightLayers, m_layerNames.ToArray());
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Changed Light Layer");
                    lightData.lightlayersMask = lightLayers;
                    isChanged = true;
                }

                EditorGUI.BeginChangeCheck();

                bool followPosition = EditorGUILayout.Toggle(labelFollowPostion, obj.m_FollowGameObjectPosition);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Changed Light Hi Cut Filter");
                    obj.m_FollowGameObjectPosition = followPosition;
                    isChanged = true;
                }

                EditorGUI.BeginChangeCheck();
                bool followRotation = EditorGUILayout.Toggle(labelFollowRotation, obj.m_FollowGameObjectRotation);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Changed Expsure Adjustment");
                    obj.m_FollowGameObjectRotation = followRotation;
                    isChanged = true;
                }

                m_GameObjectsProperties.arraySize = EditorGUILayout.IntField("Size", m_GameObjectsProperties.arraySize);
                for (int ii = 0; ii < m_GameObjectsProperties.arraySize; ii++)
                {
                    var dialogue = m_GameObjectsProperties.GetArrayElementAtIndex(ii);
                    EditorGUILayout.PropertyField(dialogue, new GUIContent("GameObject " + ii + ":"), true);
                }
                EditorGUI.indentLevel--;

            }
            serializedObject.ApplyModifiedProperties();

            if (isChanged)
            {
                if (target != null)
                {
                    var boxlightAdjustment = target as BoxLightAdjustment;
                    boxlightAdjustment.OnValidate();
                }
                // at leaset 2020.3.12f1, not neccessary. but, from which version??
                EditorApplication.QueuePlayerLoopUpdate();
            }

        }


        float ConvertFromEV100(float EV100)
        {

            float val = Mathf.Pow(2, EV100) * 2.5f;
            return val;

        }

        float ConvertToEV100(float val)
        {

            return Mathf.Log(val * 0.4f, 2.0f);

        }





        [MenuItem("GameObject/Toon Shader/Create Box Light", false, 9999)]
        static void CreateBoxLight()
        {


            var lightGo = BoxLightAdjustment.CreateBoxLight(Selection.gameObjects);

        }

    }
}
#endif