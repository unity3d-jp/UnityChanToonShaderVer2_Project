//Unitychan Toon Shader ver.8.0
//v.8.0.0
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Univerasl RP/HDRP) 
//https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project
//(C)Unity Technologies Japan/UCL
//Everything realated to Rendering per channel is controlled in this file.
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace UnityEditor.Rendering.HDRP.Toon
{
    public partial class HDRPToonGUI : ShaderGUI
    {
        ReorderableList m_ReorderableList;
        GUIContent m_colorPickerContent;
        Texture2D m_texIconVisible;
        Texture2D m_texIconInvisible;


        GUIStyle m_ToggleStyle;
        public enum _ChannelEnum
        {
            BaseColor,
            FirstShade,
            SecondShade,
            Highlight,
            AngleRing,
            Outline,
        };

        class ChannelSetting
        {
            public bool m_drawEnabled;
            public bool m_drawOverride;
            public Color m_maskColor;
            public string m_name;
        };

        List<ChannelSetting> m_channelSettings; 

            

        const string ShaderProp_RenderingPerChannelsMask = "_RenderingPerChannelsMask";

        
        static bool _PerChanelShaderSettings_Foldout = false;

        void RenderingPerChennelsSetting(Material material)
        {
            SetupChannelSettings();

            int flags = material.GetInt(ShaderProp_RenderingPerChannelsMask);
            if (m_ReorderableList != null)
            {
                m_ReorderableList.DoLayoutList();
            }
            /*
            _PerChanelShaderSettings_Foldout = Foldout(_PerChanelShaderSettings_Foldout, "Rendering per Channels Settings");
            if (_PerChanelShaderSettings_Foldout)
            {
                EditorGUI.indentLevel++;
                flags = EditorGUILayout.MaskField("Channels", flags, options);
                
                EditorGUI.indentLevel--;
            }
            */
            material.SetInt(ShaderProp_RenderingPerChannelsMask, flags);
        }

        void SetupChannelSettings()
        {

            if ( m_texIconVisible == null )
            {
                m_texIconVisible = (Texture2D)EditorGUIUtility.Load("d_scenevis_visible_hover@2x");
//                m_texIconVisible = ResizeThumbnailTexture(m_texIconVisible, 20, 20);

            }
            if (m_texIconInvisible == null)
            {
                m_texIconInvisible = (Texture2D)EditorGUIUtility.Load("d_SceneViewVisibility@2x");
//                m_texIconInvisible = ResizeThumbnailTexture(m_texIconInvisible, 20, 20);

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
            if (m_channelSettings == null)
            {
                m_channelSettings = new List<ChannelSetting>();
                m_channelSettings.Add(new ChannelSetting()
                {   
                    m_drawEnabled = true,
                    m_drawOverride = false,
                    m_maskColor = Color.gray,
                    m_name = _ChannelEnum.BaseColor.ToString()
                });
                m_channelSettings.Add(new ChannelSetting()
                {   
                    m_drawEnabled = true,
                    m_drawOverride = false,
                    m_maskColor = Color.cyan,
                    m_name = _ChannelEnum.FirstShade.ToString()
                });
                m_channelSettings.Add(new ChannelSetting()
                {
                    m_drawEnabled = true,
                    m_drawOverride = false,
                    m_maskColor = Color.blue,
                    m_name = _ChannelEnum.SecondShade.ToString()
                });
                m_channelSettings.Add(new ChannelSetting()
                {
                    m_drawEnabled = true,
                    m_drawOverride = false,
                    m_maskColor = Color.yellow,
                    m_name = _ChannelEnum.Highlight.ToString()
                });
                m_channelSettings.Add(new ChannelSetting()
                {
                    m_drawEnabled = true,
                    m_drawOverride = false,
                    m_maskColor = Color.green,
                    m_name = _ChannelEnum.AngleRing.ToString()
                });
                m_channelSettings.Add(new ChannelSetting()
                {
                    m_drawEnabled = true,
                    m_drawOverride = false,
                    m_maskColor = Color.red,
                    m_name = _ChannelEnum.Outline.ToString()
                });
            }
            if (m_colorPickerContent == null )
            {
                m_colorPickerContent = new GUIContent("");
            }
            if (m_ReorderableList == null)
            {
                m_ReorderableList = new ReorderableList(m_channelSettings, typeof(ChannelSetting));
                m_ReorderableList.displayAdd = false;
                m_ReorderableList.displayRemove = false;
                m_ReorderableList.draggable = false;
                m_ReorderableList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Channel Mask Setting");
                m_ReorderableList.drawElementCallback = (rect_, index, isActive, isFocused) =>
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
                    var isVisible = m_channelSettings[index].m_drawEnabled;
                    isVisible = EditorGUI.Toggle(toggleRectVislble, isVisible, m_ToggleStyle);
                    m_channelSettings[index].m_drawEnabled = isVisible;
                    using (new EditorGUI.DisabledScope(isVisible == false))
                    {
                        var toggleOverride = EditorGUI.Toggle(toggleRectOverride, m_channelSettings[index].m_drawOverride);
                        m_channelSettings[index].m_drawOverride = toggleOverride;
                        using (new EditorGUI.DisabledScope(toggleOverride == false))
                        {
                            var color = toggleOverride == false ? m_channelSettings[index].m_maskColor * 0.5f : m_channelSettings[index].m_maskColor;
                            EditorGUI.ColorField(colorPickerRect, m_colorPickerContent, color, false, false, false);
                        }

                        EditorGUI.LabelField(nameRect, m_channelSettings[index].m_name);
                    }
                };
            }
        }
        void ApplyRenderingPerChennelsSetting(Material material)
        {

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
    } // End of UTS2GUI2
}// End of namespace UnityChan