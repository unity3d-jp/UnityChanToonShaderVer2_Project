
using UnityEngine;

namespace Unity.Rendering.Toon {

internal static class ToonConstants {
    
    private const string SHADER_KEYWORD_IS_CLIPPING_MATTE = "_IS_CLIPPING_MATTE";
   
    
    private static readonly int SHADER_PROPERTY_CLIPPING_MATTE_MODE = Shader.PropertyToID("_ClippingMatteMode");    
    
    private static readonly int SHADER_PROPERTY_BASE_COLOR_VISIBLE   = Shader.PropertyToID("_BaseColorVisible");
    private static readonly int SHADER_PROPERTY_FIRST_SHADE_VISIBLE  = Shader.PropertyToID("_FirstShadeVisible");
    private static readonly int SHADER_PROPERTY_SECOND_SHADE_VISIBLE = Shader.PropertyToID("_SecondShadeVisible");
    private static readonly int SHADER_PROPERTY_HIGHLIGHT_VISIBLE    = Shader.PropertyToID("_HighlightVisible");
    private static readonly int SHADER_PROPERTY_ANGEL_RING_VISIBLE   = Shader.PropertyToID("_AngelRingVisible");
    private static readonly int SHADER_PROPERTY_RIM_LIGHT_VISIBLE    = Shader.PropertyToID("_RimLightVisible");
    private static readonly int SHADER_PROPERTY_OUTLINE_VISIBLE      = Shader.PropertyToID("_OutlineVisible");

    private static readonly int SHADER_PROPERTY_COMPOSER_MASK_MODE = Shader.PropertyToID("_ComposerMaskMode");
    
    private static readonly int SHADER_PROPERTY_BASE_COLOR_MASK_COLOR   = Shader.PropertyToID("_BaseColorMaskColor");
    private static readonly int SHADER_PROPERTY_FIRST_SHADE_MASK_COLOR  = Shader.PropertyToID("_FirstShadeMaskColor");
    private static readonly int SHADER_PROPERTY_SECOND_SHADE_MASK_COLOR = Shader.PropertyToID("_SecondShadeMaskColor");
    private static readonly int SHADER_PROPERTY_HIGHLIGHT_MASK_COLOR    = Shader.PropertyToID("_HighlightMaskColor");
    private static readonly int SHADER_PROPERTY_ANGEL_RING_MASK_COLOR   = Shader.PropertyToID("_AngelRingMaskColor");
    private static readonly int SHADER_PROPERTY_RIM_LIGHT_MASK_COLOR    = Shader.PropertyToID("_RimLightMaskColor");
    private static readonly int SHADER_PROPERTY_OUTLINE_MASK_COLOR      = Shader.PropertyToID("_OutlineMaskColor");
    
}



} //end namespace