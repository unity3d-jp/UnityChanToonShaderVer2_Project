# Material Capture(MatCap) Settings

MatCap is a method of light expression using pre-rendered images. This technique uses a picture of a sphere that represents the material and light to simulate lighting.

<img src="images/MatCap.gif" height="256">
<br/><br/>

<img src="images/InspectorMatcapSettings.png" width="573">
<br/><br/>

* [MatCap Map](#matcap-map)
* [MatCap Blur Level](#matcap-blur-level)
* [Color Blending Mode](#color-blending-mode)
* [Scale MatCap UV](#scale-matcap-uv)
* [Rotate MatCap UV](#rotate-matcap-uv)
* [Stabilize Camera rolling](#stabilize-camera-rolling)
* [Normal Map Specular Mask for MatCap](#normal-map-specular-mask-for-matcap)
  * [Normal Map](#normal-map)
  * [Rotate Normal Map UV](#rotate-normal-map-uv)
* [MatCap Blending on Shadows](#matcap-blending-on-shadows)
  * [Blending Level](#blending-level)
* [MatCap Camera Mode](#matcap-camera-mode)
* [MatCap Mask](#matcap-mask)
  * [MatCap Mask Level](#matcap-mask-level)
  * [Invert MatCap Mask](#invert-matcap-mask)

## MatCap Map
MatCap Color : Texture(sRGB) × Color(RGB) Default:White

| MatCap Map Texture Example | 
| -- |
| <img src="images/HiLight_Matcap.png" height="256">|



| MatCap Map Off  | MatCap Map On |
| -- | --|
| <img src="images/WithOutMatCap.gif" height="256">| <img src="images/WithMatCap.gif" height="256"> 

## MatCap Blur Level
Blur MatCap Map using the Mip Map feature; to enable Mip Map, activate Advanced > Generate Mip Maps in the [Texture Import Settings](https://docs.unity3d.com/Manual/class-TextureImporter.html). Default is 0 (no blur)

<img src="images/MatCapBlurLebel.gif" height="256">

## Color Blending Mode
MatCap color blending mode. Multiply or Add.



## Scale MatCap UV
Scaling UV of MatCap Map.

<img src="images/ScaleMatCapUV.gif" height="256">


## Rotate MatCap UV
Rotating UV of MatCap Map.

<img src="images/RotateMatCapUV.gif" height="256">

## Stabilize Camera Rolling
Stabilize Camera rolling when capturing materials with camera.

| Stabilize Camera Rolling Off  | Stabilize Camera Rolling On |
| -- | --|
| <img src="images/StabilizerOff.gif" height="256">| <img src="images/StabilizerOn.gif" height="256"> |


## Normal Map Specular Mask for MatCap
If enabled, gives a normal map specifically for MatCap. If you are using MatCap as speculum lighting, you can use this to mask it.

| Normal Map Specular Mask Off  | Normal Map Specular Mask On |
| -- | --|
| <img src="images/MatCapNormalMapOff.png" height="256">| <img src="images/MatCapNormalMapOn.png" height="256"> |


### Normal Map
A texture that dictates the bumpiness of the material.

| Normal Map Texture Example | 
| -- |
| <img src="images/HairNormalMask.png" height="256">|

### Rotate Normal Map UV
Rotates the MatCap normal map UV based on its center.

<img src="images/RotateMatCapNormalMapUV.gif" height="256">

## MatCap Blending on Shadows
Enables the blending rate of the MatCap range in shadows.
| MatCap Blending on Shadows Off  | MatCap Blending on Shadows On |
| -- | --|
| <img src="images/MatCapBlendingOnShadowOff.png" height="256">| <img src="images/MatCapBlendingOnShadowOn.png" height="256"> |

## Blending Level
Adjusts the intensity of MatCap applied to shadow areas.

<img src="images/MatCapOnShadowLevel.gif" height="256">

## MatCap Camera Mode
Control how render the MatCap Map based on the camera type.
## MatCap Mask
The MatCap mask is positioned correspond with the UV coordinates of the mesh onto which the MatCap is projected, and the pixels on black areas are hidden.

<img src="images/MatCapMaskSample.png" height="256">

## MatCap Mask Level
Adjusts the level of the MatCap Mask. When the value is 1, MatCap represents 100% irrespective of mask. When the value is -1, MatCap won't be displayed at all and MatCap will be the same as in the off state.

<img src="images/MatCapMaskLevel.gif" height="256">



## Invert MatCap Mask
When enabled, inverts **MatCap Mask** Texture colors.

<img src="images/InvertMatCapMask.png" height="256">

