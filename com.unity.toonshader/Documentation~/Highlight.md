# Highlight Settings

* [Highlight](#highlight)
* [Highlight Power](#highlight-power)
* [Specular Mode](#specular-mode)
  * [Color Blending Mode](#color-blending-mode)
* [Highlight Blending on Shadows](#highlight-blending-on-shadows)
  * [Blending Level](#blending-level)
* [Highlight Mask](#highlight-mask)
* [Highlight Mask Level](#highlight-mask-level)


## Highlight
Highlight : Texture(sRGB) × Color(RGB) Default:White.

<img src="images/Highlight.gif"  height="256">
<br><br>

## Highlight Power
Highlight power factor, pow(x,5) is used inside the shader.

<img src="images/SpecularPower.gif"  height="256">
<br><br>

## Specular Mode
Specular light mode. Hard or Soft.

| Hard | Soft |
| - | - |
| <img src="images/SpecularHard.png" > | <img src="images/SpecularSoft.png" > |


<br><br>
#### Color Blending Mode
Specular color blending mode. Multiply or Add. **Color Blending Mode** is disabled when **Specular Mode** is **Soft**.

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
A grayscale texture which utilises its brightness to control highlight intensity. Applying the highlight mask allows to fine-tune the reflectivity on the material.

 Gray Scale Texture Example | 
| ---- |
|<img src="images/UVCheckGrid.png" height="256">|

| High Light Mask Off | HIgh Light Mask On |
| ---- | ---- |
| <img src="images/HighlightMaskOff.png" height="256"> | <img src="images/HighlightMaskOn.png" height="256"> |




## Highlight Mask Level
Highlight mask texture blending level to highlights.

<img src="images/HighlightMaskLevel.gif" height="256">
