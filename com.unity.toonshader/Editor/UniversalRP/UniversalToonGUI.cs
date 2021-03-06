//Unity Toon Shader
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Universal RP/HDRP) 

using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering.Toon;
namespace UnityEditor.Rendering.Universal.Toon.ShaderGUI
{


    public class UniversalToonGUI : UTS_GUIBase
    {
        internal override string srpDefaultLightModeName { get { return "SRPDefaultUnlit"; } }

        internal override void TessellationSetting(Material materal) { }
        internal override void RenderingPerChennelsSetting(Material materal) { }
        internal override void ApplyTessellation(Material materal) { }
        internal override void ApplyRenderingPerChennelsSetting(Material materal) { }
    } 

}