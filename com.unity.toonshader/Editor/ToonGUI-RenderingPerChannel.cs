//Unity Toon Shader
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Universal RP/HDRP) 

using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace UnityEditor.Rendering.Toon
{
    internal partial class UTS3GUI : UnityEditor.ShaderGUI
    {

        public enum _ChannelEnum
        {
            BaseColor,
            FirstShade,
            SecondShade,
            Highlight,
            AngelRing,
            RimLight,
            Outline,
            Max,
        };


        const string m_strCompositoerMaskSetting = "Compositor mask setting:";
        int m_selectedIndex = 0;
        ReorderableList m_channelMaskReorderableList;

        List<string> m_channelNames;
        GUIContent m_colorPickerContent;
        Texture2D m_texIconVisible;
        Texture2D m_texIconInvisible;

        GUIStyle  m_ToggleStyle;


        List<string> m_clippingMatte;
        string[] m_clippingMatteStringArray;



        static bool _PerChanelShaderSettings_Foldout = false;

        internal  void RenderingPerChennelsSettingHDRP(Material material)
        {
            _PerChanelShaderSettings_Foldout = Foldout(_PerChanelShaderSettings_Foldout, Styles.maskRenderingFoldout);
            if (!_PerChanelShaderSettings_Foldout)
            {
                return;
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("  Clipping Matte");
            if (m_clippingMatteStringArray == null && m_clippingMatte != null)
            {
                m_clippingMatteStringArray = m_clippingMatte.ToArray();
            }
            if (m_clippingMatteStringArray != null)
            {
                m_selectedIndex = EditorGUILayout.Popup(m_selectedIndex, m_clippingMatteStringArray);
            }
            EditorGUILayout.EndHorizontal();

            if (m_channelMaskReorderableList != null)
            {
                m_channelMaskReorderableList.DoLayoutList();
            }
            SetuClippingMatte(material);
            SetupChannelSettings(material);
        }

        void SetuClippingMatte(Material material)
        {

            material.DisableKeyword(ShaderDefineIS_CLIPPING_MATTE);
            if (m_selectedIndex != 0 )
            {

                material.EnableKeyword(ShaderDefineIS_CLIPPING_MATTE);
            }
            material.SetInt("_ClippingMatteMode", m_selectedIndex);
        }

        string GetAppropriateIcon(string str)
        {
            if (EditorGUIUtility.pixelsPerPoint > 1.0f)
            {
                return str + "@2x";
            }
            return str;
        }

        void SetupChannelSettings(Material material)
        {

            if (m_texIconVisible == null)
            {
                m_texIconVisible = (Texture2D)EditorGUIUtility.Load(
                        EditorGUIUtility.isProSkin ? GetAppropriateIcon("d_scenevis_visible_hover") : GetAppropriateIcon("scenevis_visible_hover"));
            }
            if (m_texIconInvisible == null)
            {
                m_texIconInvisible = (Texture2D)EditorGUIUtility.Load(
                        EditorGUIUtility.isProSkin ? GetAppropriateIcon("d_SceneViewVisibility") : GetAppropriateIcon("scenevis_hidden_hover"));
            }

            if (m_ToggleStyle == null)
            {
                m_ToggleStyle = new GUIStyle(EditorStyles.toggle);


                m_ToggleStyle.normal.background = m_texIconInvisible;
                m_ToggleStyle.normal.scaledBackgrounds = new Texture2D[] { m_texIconInvisible };
                m_ToggleStyle.onNormal.background = m_texIconVisible;
                m_ToggleStyle.onNormal.scaledBackgrounds = new Texture2D[] { m_texIconVisible };

                m_ToggleStyle.active.background = m_texIconInvisible;
                m_ToggleStyle.active.scaledBackgrounds = new Texture2D[] { m_texIconInvisible };
                m_ToggleStyle.onActive.background = m_texIconVisible;
                m_ToggleStyle.onActive.scaledBackgrounds = new Texture2D[] { m_texIconVisible };

                m_ToggleStyle.focused.background = m_texIconInvisible;
                m_ToggleStyle.focused.scaledBackgrounds = new Texture2D[] { m_texIconInvisible };
                m_ToggleStyle.onFocused.background = m_texIconVisible;
                m_ToggleStyle.onFocused.scaledBackgrounds = new Texture2D[] { m_texIconVisible };

                m_ToggleStyle.hover.background = m_texIconInvisible;
                m_ToggleStyle.hover.scaledBackgrounds = new Texture2D[] { m_texIconInvisible };
                m_ToggleStyle.onHover.background = m_texIconVisible;
                m_ToggleStyle.onHover.scaledBackgrounds = new Texture2D[] { m_texIconVisible };
            }
            if (m_channelNames == null)
            {
                m_channelNames = new List<string>();
                m_channelNames.Add(_ChannelEnum.BaseColor.ToString());
                m_channelNames.Add(_ChannelEnum.FirstShade.ToString());
                m_channelNames.Add(_ChannelEnum.SecondShade.ToString());
                m_channelNames.Add(_ChannelEnum.Highlight.ToString());
                m_channelNames.Add(_ChannelEnum.AngelRing.ToString());
                m_channelNames.Add(_ChannelEnum.RimLight.ToString());
                m_channelNames.Add(_ChannelEnum.Outline.ToString());
            }
            if (m_clippingMatte == null)
            {
                m_clippingMatte = new List<string>();
                m_clippingMatte.Add("None");
                m_clippingMatte.Add(_ChannelEnum.BaseColor.ToString());
                m_clippingMatte.Add(_ChannelEnum.FirstShade.ToString());
                m_clippingMatte.Add(_ChannelEnum.SecondShade.ToString());
                m_clippingMatte.Add(_ChannelEnum.Highlight.ToString());
                m_clippingMatte.Add(_ChannelEnum.AngelRing.ToString());


            }
            if (m_colorPickerContent == null)
            {
                m_colorPickerContent = new GUIContent(string.Empty);
            }

            const int toggleWholeWidth = 170;
            const int toggleWidth = 20;

            if (m_channelMaskReorderableList == null)
            {
                m_channelMaskReorderableList = new ReorderableList(m_channelNames, typeof(string));
                m_channelMaskReorderableList.displayAdd = false;
                m_channelMaskReorderableList.displayRemove = false;
                m_channelMaskReorderableList.draggable = false;
                m_channelMaskReorderableList.drawHeaderCallback = rect =>
                {
                    using (new EditorGUI.DisabledScope(m_selectedIndex != 0))
                    {


                        Rect fieldRect = rect;
                        Rect toggleRect = rect;
                        fieldRect.width = rect.width - toggleWholeWidth;
                        EditorGUI.LabelField(fieldRect, "Channel Mask Setting");
                        const string propCompositorMaskMode = "_ComposerMaskMode";

                        toggleRect.width = toggleWholeWidth;
                        toggleRect.x += fieldRect.width;
                        bool isVisible = material.GetFloat(propCompositorMaskMode) > 0.0f;
                        EditorGUI.BeginChangeCheck();
                        var store = EditorGUIUtility.labelWidth;
                        EditorGUIUtility.labelWidth = toggleWholeWidth - toggleWidth;
                        isVisible = EditorGUI.Toggle(toggleRect, m_strCompositoerMaskSetting, isVisible);
                        EditorGUIUtility.labelWidth = store;
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(material, "Compositor mask setting is changed.");
                            material.SetFloat(propCompositorMaskMode, isVisible ? 1.0f : 0.0f);
                        }
                    }
                };
                m_channelMaskReorderableList.drawElementCallback = (rect_, index, isActive, isFocused) =>
                {
                    Rect toggleRectVislble = new Rect(rect_)
                    {
                        height = 16,
                        width = 16,
                        x = rect_.x + 6,
                        y = rect_.y
                    };
                    Rect toggleRectOverride = new Rect(rect_)
                    {
                        height = 16,
                        width = 16,
                        x = rect_.x + 6 + 22,
                        y = rect_.y
                    };
                    Rect colorPickerRect = new Rect(rect_)
                    {
                        height = 16,
                        width = 16,
                        x = rect_.x + 6 + 22 * 2,
                        y = rect_.y
                    };
                    Rect nameRect = new Rect(rect_)
                    {
                        height = 16,
                        width = 120,
                        x = rect_.x + 6 + 22 * 3,
                        y = rect_.y
                    };

                    using (new EditorGUI.DisabledScope(m_selectedIndex != 0))
                    {
                        string propNameVisible = "_" + m_channelNames[index].ToString() + "Visible";
                        string propNameOverriden = "_" + m_channelNames[index].ToString() + "Overridden";
                        string propNameColor = "_" + m_channelNames[index].ToString() + "MaskColor";
                        bool isVisible = material.GetFloat(propNameVisible) > 0.0f;
                        EditorGUI.BeginChangeCheck();
                        isVisible = EditorGUI.Toggle(toggleRectVislble, isVisible, m_ToggleStyle);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(material, "Layer visiblity is changed");
                            material.SetFloat(propNameVisible, isVisible ? 1.0f : 0.0f);
                        }

                        using (new EditorGUI.DisabledScope(isVisible == false))
                        {
                            EditorGUI.BeginChangeCheck();
                            bool toggleOverride = material.GetFloat(propNameOverriden) > 0.0f;
                            toggleOverride = EditorGUI.Toggle(toggleRectOverride, toggleOverride);
                            if (EditorGUI.EndChangeCheck())
                            {
                                Undo.RecordObject(material, "Layer mask is changed");
                                material.SetFloat(propNameOverriden, toggleOverride ? 1.0f : 0.0f);
                            }


                            Color color = material.GetColor(propNameColor);
                            //color *= toggleOverride == false ? 0.5f : 1.0f;
                            if (m_selectedIndex == 0)
                            {
                                EditorGUI.BeginChangeCheck();

                                color = EditorGUI.ColorField(colorPickerRect, m_colorPickerContent, color, false, true, false);

                                if (EditorGUI.EndChangeCheck())
                                {
                                    Undo.RecordObject(material, "Layer mask color is changed");
                                    material.SetColor(propNameColor, color);
                                }
                            }
                            EditorGUI.LabelField(nameRect, m_channelNames[index]);
                        }
                    }
                };
            }

        }


        Texture2D ResizeThumbnailTexture(Texture2D tex, int sizX, int sizY)
        {
            var rt = RenderTexture.GetTemporary(sizX, sizY);
            var previous = RenderTexture.active;

            RenderTexture.active = rt;
            Graphics.Blit(tex, rt);

            var newTexture = new Texture2D(sizX, sizY);
            newTexture.ReadPixels(new Rect(0, 0, sizX, sizY), 0, 0);
            newTexture.Apply();
            RenderTexture.active = previous;

            RenderTexture.ReleaseTemporary(rt);
            return newTexture;

        }
    } // End of UTS3GUI
}