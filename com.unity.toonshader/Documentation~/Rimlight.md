# Rim Light Settings

The Rim Light  highlight the edges of meshes. Rim Light is based on the angle between surface normal and view direction. The **Unity Toon Shader** provides variety of options for Rim Light.

<img src="images/InspectorRimLightSettings.png" width="573">
<br/><br/>

* [Rim Light](#rim-light)
  * [Rim Light Color](#rim-light-color)
  * [Rim Light Level](#rim-light-level)
  * [Adjust Rim Light Area](#adjust-rim-light-area)
  * [Rim Light Feather Off](#rim-light-feather-off)
  * [Light Direction](#light-direction)
  * [Light Direction Rim Light Level](#light-direction-rim-light-level)
  * [Inverted Light Direction Rim Light](#inverted-light-direction-rim-light)
    * [Inverted Rim Light Color](#inverted-light-direction-rim-light)
    * [Inverted Rim Light Level](#inverted-rim-light-level)
    * [Inverted Rim Light Feather Off](#inverted-rim-light-level)

  * [Rim Light Mask](#rim-light-mask)
  * [Rim Light Mask Level](#rim-light-mask-level)

## Rim Light
A Check box to enable **Rim Light** that hits the 3D model from behind and emphasizes the contours of the model from the front.
|  Rim Light Off | Rim Light On | 
| ---- | ---- |
| <img src="images/RimLightOff.png" height="256"> | <img src="images/RimLightOn.png" height="256"> |
 
<br>

### Rim Light Color
Specifies the color of **Rim Light**.

<img src="images/RimLightColor.gif" height="256">
<br>

### Rim Light Level
Specifies **Rim Light** intensity.

<img src="images/RimLightLevel.gif" height="256">
<br>

### Adjust Rim Light Area
Increasing this value narrows the area of influence of **Rim Light**.

<img src="images/AdjustRimLightArea.gif" height="256">
<br>

### Rim Light Feather Off
A check box to disable **Rim Light** feather.

|  Rim Light with feather | Rim light feather disabled | 
| ---- | ---- |
| <img src="images/RimLightFeatherOn.png" height="256"> | <img src="images/RimLightFeatherOff.png" height="256"> |
<br>

### Light Direction
A Checkbox to enable light direction. When Enabled, generates **Rim Light** in the direction of the light source.
|  Rim Light Direction Off | Rim Light Direction On | 
| ---- | ---- |
| <img src="images/RimLightDirectionOff.png" height="256"> | <img src="images/RimLightDirectionOn.png" height="256"> |
<br>

### Light Direction Rim Light Level
Specifies intensity of **Rim Light** in the light source direction.

 <img src="images/LightDirectionRimLightLevel.gif" height="256">
<br>

### Inverted Light Direction Rim Light
Light color effectiveness to inverted direction rim lit areas.
|  Inverted Light Direction Rim Light Off | Inverted Light Direction Rim Light On | 
| ---- | ---- |
| <img src="images/InversedLightDirectionRimLightOff.png" height="256"> | <img src="images/InversedLightDirectionRimLightOn.png" height="256"> |
<br>

#### Inverted Rim Light Color
Specifies the color of inverted/antipodean **Rim Light**.

 <img src="images/InversedRimLightColor.gif" height="256">
<br>


#### Inverted Rim Light Level
Specifies Inverted/Antipodean **Rim Light** Level.

 <img src="images/InversedRimLightLevel.gif" height="256">
<br>

#### Inverted Rim Light Feather Off
Disable Inverted **Rim Light** feather.

|  Inverted Rim Light Feather On | Inverted Rim Light Feather Off | 
| ---- | ---- |
| <img src="images/InversedRimLightFeathterOn.png" height="256"> | <img src="images/InversedRimLightFeathterOff.png" height="256"> |

### Rim Light Mask
Rim Light Mask : a gray scale texture(linear). The white part of the texture represents **Rim Light**, and the black part masks.
 Gray Scale Texture Example | 
| ---- |
|<img src="images/UVCheckGrid.png" height="256">|

| Rim Light Mask Off | Rim Light Mask On |
| ---- | ---- |
| <img src="images/RimLightMaskOff.png" height="256"> | <img src="images/RimLightMaskOn.png" height="256"> |



### Rim Light Mask Level
-1 gives 0% for the Rim Light effect, 0 gives 100% for the Rim Light and Mask effect, 1 gives 100% for the Rim Light and 0% for the Mask effect.

 <img src="images/RimLightMaskLevel.gif" height="256">
<br>