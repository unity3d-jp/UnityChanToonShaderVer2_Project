# Scene Light Effectiveness Settings

<img src="images/InspectorSceneLightEffectivenessSettings.png" width="573">
<br/><br/>

* [Light Color Effectiveness Settings](#light-color-effectiveness)
  * [Base Color](#base-color)
  * [1st Shading Color](#1st-shading-color)
  * [2nd Shading Color](#2nd-shading-color)
  * [Highlight](#highlight)
  * [Rim Light](#rim-light)
  * [Inversed Light Direction Rim Light](#inversed-light-direction-rim-light)
  * [Angel Ring](#angel-ring)
  * [MatCap](#matcap)
  * [Outline](#outline)


 * [Light Probe Intensity](#light-probe-intensity)
 * [Limit Light Intensity](#limit-light-intensity)

## Light Color Effectiveness

If the setting for each color is Off, the color is always illuminated with a light intensity of 1 and a light color of white regardless of the intensity of the lights in the scene.


### Base Color
Light color effect in  the base color areas.

### 1st Shading Color
Light color effect in  the 1st shading color areas.

### 2nd Shading Color
Light color effect in  the 2nd shading color areas.

### Highlight
Light color effect in  high lit areas.

### Rim Light
Light color effect in  rim lit areas.

### Inversed Light Direction Rim Light
Light color effect in inverted direction rim lit areas.

### Angel Ring
Light color effect in angel ring area. Angel Ring is only available in **With Additional Control Maps** mode.

### MatCap
Light color effect in  MatCap areas.

### Outline
Light color effect in outlines.

Example: A red light on the face. Unchecked areas on the face material are not affected by the light color.
|Light Color Effectiveness Off | Light Color Effectiveness On |
| - | - |
| <img src="images/SceneLightColorEffectivenessOn.png" height="256"> | <img src="images/SceneLightColorEffectivenessOff.png" height="256"> |
|<img src="images/SceneLightColorEffectiveness1.png" height="70">|<img src="images/SceneLightColorEffectiveness0.png" height="70">|



## Light Probe Intensity
The light probe color is added to the material color according to the **Light Probe Intensity** value.

<img src="images/LightProbeIntensity.gif" height="256">
<br><br>

## Limit Light Intensity
Limit the brightness of the light to 1 to avoid white-out.

Example: A intensive light on the character.

| Limit Light Intensity Off | Limit Light Intensity On |
| - | - |
| <img src="images/LimitLightIntensityOff.png" height="256"> | <img src="images/LimitLightIntensityOn.png" height="256"> |
