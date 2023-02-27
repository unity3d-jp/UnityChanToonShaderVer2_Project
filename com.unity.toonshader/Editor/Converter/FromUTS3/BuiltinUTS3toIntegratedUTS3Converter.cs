using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityEditor.Rendering.Toon
{
    internal sealed class BuiltinUTS3toIntegratedUTS3Converter : RenderPipelineConverterContainer
    {
        static internal readonly UTSGUID kOrgShaderGUID = new UTSGUID("9baf30ce95c751649b14d96da3a4b4d5", "Toon (Built-in)");
        static internal readonly UTSGUID kOrgTessShaderGUID = new UTSGUID("31dfbecc7cb879847aa8626f0d30ec43", "ToonTessellation (Built-in)", true);

        public override string name => "Unity Toon Shader(Built-in RP) 0.7.x or older";
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