//Unitychan Toon Shader ver.8.0
//v.8.0.0
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Univerasl RP/HDRP) 
//https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project
//(C)Unity Technologies Japan/UCL
//Everything realated to Rendering per channel is controlled in this file.
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Specialized;

namespace UnityEditor.Rendering.HDRP.Toon
{
    public partial class HDRPToonGUI : ShaderGUI
    {
        public enum _ChannelEnum
        {
            BaseColor,
            FirstShade,
            SecondShade,
        }

        const string ShaderProp_RenderingPerChannelsMask = "_RenderingPerChannelsMask";

        
        static bool _PerChanelShaderSettings_Foldout = false;
        static string[] options = new string[] {
            _ChannelEnum.BaseColor.ToString(),
            _ChannelEnum.FirstShade.ToString(),
            _ChannelEnum.SecondShade.ToString(),
        };
        void RenderingPerChennelsSetting(Material material)
        {
            int flags = material.GetInt(ShaderProp_RenderingPerChannelsMask);
            _PerChanelShaderSettings_Foldout = Foldout(_PerChanelShaderSettings_Foldout, "Rendering per Channels Settings");
            if (_PerChanelShaderSettings_Foldout)
            {
                EditorGUI.indentLevel++;
                flags = EditorGUILayout.MaskField("Channels", flags, options);
                material.SetInt(ShaderProp_RenderingPerChannelsMask,flags);
                EditorGUI.indentLevel--;
            }
        }
        void ApplyRenderingPerChennelsSetting(Material material)
        {
            Shader.SetGlobalVector("_BaseColor_Mixer", new Vector4(0.0f,0.0f,0.0f,1.0f));
            Shader.SetGlobalVector("_1st_ShadeColor_Mixer", new Vector4(0.0f, 0.0f, 0.0f, 1.0f));
            Shader.SetGlobalVector("_2nd_ShadeColor_Mixer", new Vector4(0.0f, 0.0f, 1.0f, 0.0f));

            // set in according to above, need to reset by default.
            Shader.SetGlobalVector("_BaseColor_Mixer_Color", Color.clear);
            Shader.SetGlobalVector("_1st_ShadeColor_Mixer_Color", Color.clear);
            Shader.SetGlobalVector("_2nd_ShadeColor_Mixer_Color", Color.clear);
        }

    } // End of UTS2GUI2
}// End of namespace UnityChan