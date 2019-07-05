using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
 
[Serializable]
[PostProcess(typeof(UTS_SobelColorEdgeDetectionRenderer), PostProcessEvent.AfterStack, "UnityChan/UTS_SobelColorEdgeDetection")]
public sealed class UTS_SobelColorEdgeDetection : PostProcessEffectSettings
{
    [Header("Edge Detection : Sobel Color Filter")]

    [Tooltip("Edge Color")]
    public ColorParameter edgesColor = new ColorParameter { value = new Color(0.5f, 0.5f, 0.5f, 1) };
    [Range(0f, 1f), Tooltip("Color Filter Power")]
    public FloatParameter filterPower = new FloatParameter { value = 0.3f };
    [Range(0f, 1f), Tooltip("Filter Threshold")]
    public FloatParameter threshold = new FloatParameter { value = 0.3f };
}


 
public sealed class UTS_SobelColorEdgeDetectionRenderer : PostProcessEffectRenderer<UTS_SobelColorEdgeDetection>
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