# Highlight Settings

The ability to display specular highlights in a cel-animation-like manner is essential for Toon Shaders. The **Unity Toon Shader** provides a wide variety of expressions with controllability to illuminate the area independently of light color and intensity for impressive cel-shading.

<img src="images/InspectorHighlightSettings.png" width="573">
<br/><br/>

* [Highlight](#highlight)
* [Highlight Power](#highlight-power)
* [Specular Mode](#specular-mode)
  * [Color Blending Mode](#color-blending-mode)
* [Highlight Blending on Shadows](#highlight-blending-on-shadows)
  * [Blending Level](#blending-level)
* [Highlight Mask](#highlight-mask)
* [Highlight Mask Level](#highlight-mask-level)


## Highlight
Highlight : Texture(sRGB) × Color(RGB) Default:White. Pattern and color of specularly illuminated area.

<br><br>
| Default Color | Applied different light color |
| - | - |
| <img src="images/Highlight0.png"  height="256"> | <img src="images/Highlight1.png"  height="256"> | 

## Highlight Power

The size of the Highlight controlled through the High light power slider. The size increase with the formula: pow(x,5).

<img src="images/SpecularPower.gif"  height="256">
<br><br>

## Specular Mode

UTS provides two modes for the highlight for different occasions and effect. The hard mode provides a crisp and solid edge to the highlight while the soft mode provides a blended blurred effect.

| Hard | Soft |
| - | - |
| <img src="images/SpecularHard.png" > | <img src="images/SpecularSoft.png" > |


<br><br>

### Color Blending Mode
Specular color blending mode allows the user to control the hardness of the colour applied to the highlight. Users have two options: Multiply or Add. Note that **Color Blending Mode** is disabled when **Specular** Mode is  **Soft**.

| Multiply | Add |
| - | - |
| <img src="images/SpecularMultiply.png" > | <img src="images/SpecularAdd.png" > |

## Highlight Blending on Shadows
Control the blending for the highlights in shadows. Please refer to the image at [Blending Level](#blending-level).

### Blending Level
Adjusts the intensity of highlight applied to shadow areas.

<img src="images/HighlightBlendingLevel.gif" >
<br><br>

## Highlight Mask
A gray scale texture which utilises its brightness to control highlight intensity. Applying the highlight mask allows to fine-tune the reflectivity on the material.

 Gray Scale Texture Example | 
| ---- |
|<img src="images/UVCheckGrid.png" height="256">|

| High Light Mask Off | HIgh Light Mask On |
| ---- | ---- |
| <img src="images/HighlightMaskOff.png" height="256"> | <img src="images/HighlightMaskOn.png" height="256"> |




## Highlight Mask Level
Highlight mask texture blending level to highlights.

<img src="images/HighlightMaskLevel.gif" height="256">
