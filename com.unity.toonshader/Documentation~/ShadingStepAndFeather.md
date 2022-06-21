# Shading Steps and Feather Settings

The **Unity Toon Shader** allows you to control the position of the boundaries of the area, to clearly demarcate the boundaries, or to blur or blend them.

<img src="images/InspecotrShadingStepsAndFeatherSettings.png" width="573">
<br/><br/>

* [Base Color Step](#base-color-step)
* [Base Shading Feather](#base-shading-feather)
* [Shading Color Step](#shading-color-step)
* [1st/2nd Shading Feather](#1st2nd-shading-feather)
* [Receive Shadows](#receive-shadows)
  * [System Shadow Level](#system-shadow-level)
* [Point Light Settings](#point-light-settings)
  * [Step Offset](#step-offset)
  * [Filter Point Light Highlights](#filter-point-light-highlights)
<br><br>

### Base Color Step
Sets the boundary between the Base Color and the Shade Colors.

<img src="images/ColorStep.gif"  height="384">
<br><br>

## Base Shading Feather
Feathers the boundary between the Base Color and the Shade Colors.

<img src="images/BaseShadingFeather.gif" height="384">
<br><br>

## Shading Color Step
Sets the boundary between the 1st and 2nd Shade Colors. Set this to 0 if  2nd Shade Color is unnecessary.

<img src="images/ShadingColorStep.gif" height="384">
<br><br>


### 1st/2nd Shading Feather
Feathers the boundary between the 1st and 2nd Shade Colors.

<img src="images/1st2ndShadeFeather.gif" height="384">
<br><br>

### Receive Shadows
Determine if the material reflects shadows.

<img src="images/RecieveSystemShadow.gif" height="384">
<br><br>

#### System Shadow Level
Define the appearance of self-shadows and other received shadows that blend with the Too Shader.

<img src="images/SystemShadowLevel.gif" height="384">
<br><br>

## Point Light Settings

### Step Offset
Fine tunes light steps (boundaries) added in the ForwardAdd pass, such as real-time point lights.


<img src="images/PointLightStepOffset.gif" height="384">  
<img src="images/PointLightStepOffset-Ball3.gif" height="384"> 
<br><br>


### Filter Point Light Highlights
Show or hide highlight of point lights.

<img src="images/FilterHilightOnPointLight-2.gif" height="384">
<img src="images/FilterPointLightHighlight-Ball.gif" height="384">
