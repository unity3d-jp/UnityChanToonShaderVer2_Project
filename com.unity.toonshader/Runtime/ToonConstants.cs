
using UnityEngine;

namespace Unity.Rendering.Toon {

internal static class ToonConstants {
    
    internal const string SHADER_KEYWORD_IS_CLIPPING_MATTE = "_IS_CLIPPING_MATTE";
    
    internal static readonly int SHADER_PROPERTY_CLIPPING_MATTE_MODE = Shader.PropertyToID("_ClippingMatteMode");    
    
    internal static readonly int SHADER_PROPERTY_BASE_COLOR_VISIBLE   = Shader.PropertyToID("_BaseColorVisible");
    internal static readonly int SHADER_PROPERTY_FIRST_SHADE_VISIBLE  = Shader.PropertyToID("_FirstShadeVisible");
    internal static readonly int SHADER_PROPERTY_SECOND_SHADE_VISIBLE = Shader.PropertyToID("_SecondShadeVisible");
    internal static readonly int SHADER_PROPERTY_HIGHLIGHT_VISIBLE    = Shader.PropertyToID("_HighlightVisible");
    internal static readonly int SHADER_PROPERTY_ANGEL_RING_VISIBLE   = Shader.PropertyToID("_AngelRingVisible");
    internal static readonly int SHADER_PROPERTY_RIM_LIGHT_VISIBLE    = Shader.PropertyToID("_RimLightVisible");
    internal static readonly int SHADER_PROPERTY_OUTLINE_VISIBLE      = Shader.PropertyToID("_OutlineVisible");

    internal static readonly int SHADER_PROPERTY_COMPOSER_MASK_MODE = Shader.PropertyToID("_ComposerMaskMode");
    
    internal static readonly int SHADER_PROPERTY_BASE_COLOR_MASK_COLOR   = Shader.PropertyToID("_BaseColorMaskColor");
    internal static readonly int SHADER_PROPERTY_FIRST_SHADE_MASK_COLOR  = Shader.PropertyToID("_FirstShadeMaskColor");
    internal static readonly int SHADER_PROPERTY_SECOND_SHADE_MASK_COLOR = Shader.PropertyToID("_SecondShadeMaskColor");
    internal static readonly int SHADER_PROPERTY_HIGHLIGHT_MASK_COLOR    = Shader.PropertyToID("_HighlightMaskColor");
    internal static readonly int SHADER_PROPERTY_ANGEL_RING_MASK_COLOR   = Shader.PropertyToID("_AngelRingMaskColor");
    internal static readonly int SHADER_PROPERTY_RIM_LIGHT_MASK_COLOR    = Shader.PropertyToID("_RimLightMaskColor");
    internal static readonly int SHADER_PROPERTY_OUTLINE_MASK_COLOR      = Shader.PropertyToID("_OutlineMaskColor");

    internal const string GBUFFER_PASS_NAME = "GBuffer";
    
}



} //end namespace