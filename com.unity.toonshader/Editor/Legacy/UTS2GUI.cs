//Unity Toon Shader
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Universal RP/HDRP) 

using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering.Toon;
namespace UnityEditor.Rendering.Builtin.Toon
{


    public partial class UTS2GUI : UTS_GUIBase
    {
        internal override string srpDefaultLightModeName { get { return "Always"; } }
        internal override void TessellationSetting(Material materal) { }
        internal override void RenderingPerChennelsSetting(Material materal) { }
        internal override void ApplyTessellation(Material materal) { }
        internal override void ApplyRenderingPerChennelsSetting(Material materal) { }
    } 

}