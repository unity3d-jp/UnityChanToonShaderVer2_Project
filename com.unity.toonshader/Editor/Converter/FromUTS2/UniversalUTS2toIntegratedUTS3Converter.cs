using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace UnityEditor.Rendering.Toon
{
    internal sealed class UniversalUTS2toIntegratedUTS3Converter : RenderPipelineConverterContainer
    {
        readonly UTSGUID kOrgShaderGUID = new UTSGUID("766736548846cdf459a9766614dcccab", "Universal Render Pipeline/Toon", false);
        readonly UTSGUID kOrgTessShaderGUID = null;

        public override string name => "Universal Toon Shader";
        public override string info => "This tool converts project materials from Universal Toon Shader to Unity Toon Shader " + UTS3GUI.versionString;
        public override int priority => -9000;

        public override void SetupConverter() 
        {
            SetupConverterCommon(kOrgShaderGUID, kOrgTessShaderGUID);
        }
        public override void Convert() 
        { 
            CommonConvert(); 
            SendAnalyticsEvent();
        }
        public override void PostConverting() { }

        public override int CountErrors(bool addToScrollView) { return 0; }
        public override InstalledStatus CheckSourceShaderInstalled() { return InstalledStatus.NotInstalled; }
    }
}