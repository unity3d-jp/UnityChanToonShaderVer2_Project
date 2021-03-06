//Unity Toon Shader
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Universal RP/HDRP) 
using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering.Toon;
namespace UnityEditor.Rendering.HighDefinition.Toon
{


    public partial class HDRPToonGUI : UTS_GUIBase
    {
        internal override string srpDefaultLightModeName { get { return "SRPDefaultUnlit"; } }

    }
}