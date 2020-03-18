using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace UnityEngine.Rendering.Universal.Toon
{
    [Serializable]
    [PostProcess(typeof(SobelColorEdgeDetectionRenderer), PostProcessEvent.AfterStack, "UnityChan/UTS_SobelColorEdgeDetection")]
    public sealed class SobelColorEdgeDetection : PostProcessEffectSettings
    {
        [Header("Edge Detection : Sobel Color Filter")]

        [Tooltip("Edge Color")]
        public UnityEngine.Rendering.PostProcessing.ColorParameter edgesColor = new UnityEngine.Rendering.PostProcessing.ColorParameter { value = new Color(0.5f, 0.5f, 0.5f, 1) };
        [Range(0f, 1f), Tooltip("Color Filter Power")]
        public UnityEngine.Rendering.PostProcessing.FloatParameter filterPower = new UnityEngine.Rendering.PostProcessing.FloatParameter { value = 0.3f };
        [Range(0f, 1f), Tooltip("Filter Threshold")]
        public UnityEngine.Rendering.PostProcessing.FloatParameter threshold = new UnityEngine.Rendering.PostProcessing.FloatParameter { value = 0.3f };
    }



    public sealed class SobelColorEdgeDetectionRenderer : PostProcessEffectRenderer<SobelColorEdgeDetection>
    {
        public override void Render(PostProcessRenderContext context)
        {
            var sheet = context.propertySheets.Get(Shader.Find("Hidden/UnityChan/UTS_SobelColorEdgeDetection"));
            sheet.properties.SetColor("_EdgesColor", settings.edgesColor);
            sheet.properties.SetFloat("_FilterPower", settings.filterPower);
            sheet.properties.SetFloat("_Threshold", settings.threshold);

            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
    }
}
