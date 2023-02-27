using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace UnityEditor.Rendering.Toon
{
    internal sealed class UniversalUTS3toIntegratedUTS3Converter : RenderPipelineConverterContainer
    {
        readonly UTSGUID kOrgShaderGUID = new UTSGUID("766736548846cdf459a9766614dcccab", "Universal Render Pipeline/Toon");
        readonly UTSGUID kOrgTessShaderGUID = null;

        public override string name => "Unity Toon Shader(URP) 0.7.x or older";
        public override string info => "This tool materials project elements from Unity Toon Shader 0.7.x or older to Unity Toon Shader " + UTS3GUI.versionString;
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