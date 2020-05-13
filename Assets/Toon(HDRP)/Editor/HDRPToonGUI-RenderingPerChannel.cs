//Unitychan Toon Shader ver.8.0
//v.8.0.0
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Univerasl RP/HDRP) 
//https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project
//(C)Unity Technologies Japan/UCL
//Everything realated to Rendering per channel is controlled in this file.
using UnityEngine;
using UnityEditor;

namespace UnityEditor.Rendering.HDRP.Toon
{
    public partial class HDRPToonGUI : ShaderGUI
    {
        public enum _ChannelEnum
        {
            Nothing,
            Everyting,
            FirstShade,
            SecondShade,
            HighLight,
            AngelRing,
            Outline,
        }

        static int flags = 0;
        static bool _PerChanelShaderSettings_Foldout = false;
        static string[] options = new string[] { 
            "Base Color", 
            "1st Shade", 
            "2nd Shade",
            "Highlgiht",
            "Angel Ring",
            "Outline"
        };
        void RenderingPerChennelsSetting()
        {

            _PerChanelShaderSettings_Foldout = Foldout(_PerChanelShaderSettings_Foldout, "Rendering per Channels Settings");
            if (_PerChanelShaderSettings_Foldout)
            {
                EditorGUI.indentLevel++;
                flags = EditorGUILayout.MaskField("Player Flags", flags, options);

                EditorGUI.indentLevel--;
            }
        }

    } // End of UTS2GUI2
}// End of namespace UnityChan