# UTS/UniversalToon Properties
***Read this document in [日本語版](Props_ja.md)***  

The following table is an explanation of the properties found in each UTS2 shader, in order by feature block.  
For all UTS2 shaders, if the property name is the same, then they also function the same.  

<img width = "400" src="Images_jpg/UI_Toon_DoubleShadeWithFeather.jpg">

---

## 1. Stencil, Culling, and Clipping Properties
<img width = "500" src="Images_jpg/Property_UTS2_01.jpg">


| `Property`                     | Function                                                     |
| ------------------------------ | ------------------------------------------------------------ |
| `Stencil No`                   | Used by  `StencilMask`　/　`StencilOut` shaders. Designates a stencil reference number between 0 - 255 (note that in some cases 255 carries a special significance). Matches the number for the cutting material and the material to be cut. |
| `Cull　Mode`                   | Designates which side of a polygon will not be drawn (culling). Available options are: `OFF` (both sides drawn) / `FRONT` (front side culling) / `BACK` (back side culling). `Back` is selected by default. In some cases, selecting `OFF` can cause the normal map and lighting to display strangely. |
| `ClippingMask`                 | Used by `Clipping` / `TransClipping` shaders. Designates the grayscale clipping mask. White indicates “none”. If no settings are chosen, the clipping feature will be inactive. |
| `IsBaseMapAlphaAsClippingMask` | A property only found in `TransClipping` shaders. Checking this property will use the A channel, including the `BaseMap`, as a clipping mask. Designating a `ClippingMask` is not required. |
| `Inverse_Clipping`             | Inverts the clipping mask.                                   |
| `Clipping_Level`               | Designates the strength of the clipping mask.                |
| `Tweak_transparency`           | Used by `TransClipping` shaders. Adjusts the transparency level by treating the clipping mask grayscale level as an α value. |


---

## 2. “The Three Basic Colors (Base Color, 1st Shade Color, 2nd Shade Color)”, Their Settings, and Properties

<img width = "500" src="Images_jpg/Property_UTS2_02.jpg">

| `Property`                        | Function                                                     |
| --------------------------------- | ------------------------------------------------------------ |
| `BaseMap`                         | Designates the Base Color texture.                           |
| `BaseColor`                       | The color which is multiplied by the `BaseMap`. If there is no designated texture, this color will be set as the Base Color. |
| `Is_LightColor_Base`              | Applies the light color to the Base Color. **※ Tip: Be sure to check when using `SceneLights Hi-Cut_Filter`.**                  |
| `1st_ShadeMap`                    | Designates the 1st Shade Color texture.                      |
| `Use BaseMap as 1stShade_Map`     | When set to ON, applies the `1st_ShadeMap` to the texture designated as the `BaseMap`. |
| `1st_ShadeColor`                  | The color which is multiplied by the `1st_ShaderMap`. If there is no designated texture, this color will be used as the 1st Shade Color. |
| `Is_LightColor_1st_Shade`         | Applies the light color to the 1st Shade Color. **※ Tip: Be sure to check when using `SceneLights Hi-Cut_Filter`.**             |
| `2nd_ShadeMap`                    | Designates the 2nd Shade Color texture.                      |
| `Use 1stShade_Map as 2ndShade_Map` | When set to ON, applies the `1st_ShadeMap` texture to the `2nd_ShadeMap`. If `Use BaseMap as 1stShade_Map` is also ON, the `BaseMap` will also be applied to the `2nd_ShadeMap`. |
| `2nd_ShadeColor`                  | The color which is multiplied by the `2nd_ShaderMap`. If there is no designated texture, this color will be used as the 2nd Shade Color. |
| `Is_LightColor_2nd_Shade`         | Applies the light color to the 2nd Shade Color. **※ Tip: Be sure to check when using `SceneLights Hi-Cut_Filter`.**             |

**※ Hint: Turning off any color’s `Is_LightColor_color name` switch will cause that color’s Light Intensity to be set to 1 with a light color of white, regardless of the strength of other lights in the environment. This switch should generally only be used when there is only one Directional LIght in the environment. **.  

---

## 3. “Normal Map” Properties

<img width = "500" src="Images_jpg/Property_UTS2_03.jpg">

| `Property`           | Function                                                     |
| -------------------- | ------------------------------------------------------------ |
| `NormalMap`          | Designates the Normal Map.                                   |
| `Is_NormalMapToBase` | Check when you want the Normal Map to be reflected in the colors. If not checked, the object’s geometry will be reflected. |

---

## 4. The Basic Settings for Cel and Illustration Styles (Step and Feather Intensity)

## 4-1. DoubleShadeWithFeather Shaders

There are common properties among DoubleShadeWithFeather shaders, the standard shaders in UST2.  

<img width = "500" src="Images_jpg/Property_UTS2_04_1.jpg">

| `Property`                                   | Function                                                     |
| -------------------------------------------- | ------------------------------------------------------------ |
| `Set_SystemShadowsToBase`                    | Use Unity’s shadow system. This option must be selected to use  ReceiveShadow. (`ReceiveShadow` must also be selected under Mesh Renderer) |
| `Tweak_SystemShadowsLevel`                   | This property is active when `Set_SystemShadowsToBase` is set to ON. It controls Unity’s system shadow levels. The default is 0, and the levels can be adjusted to ±0.5. After setting the step with  `BaseColor_Step`/`1st_ShadeColor_Step`, this property allows for even finer tuning. It can also be used to fine tune other things, like how a self shadow’s ReceiveShadow  displays. |
| `BaseColor_Step`                             | Sets the boundary between the Base Color and the Shade Colors. |
| `Base/Shade_Feather`                         | Feathers the boundary between the Base Color and the Shade Colors. |
| `ShadeColor_Step`                            | Sets the boundary between the 1st and 2nd Shade Colors. Set this to 0 if no 2nd Shade Color is used. |
| `1st/2nd_Shades_Feather`                     | Feathers the boundary between the 1st and 2nd Shade Colors.  |
| `Step_Offset (ForwardAdd Only)`              | Fine tunes light steps (boundaries) added in the ForwardAdd pass, such as real-time point lights. This property is not available in the Mobile/Light version. |
| `PointLights HiCut_Filter (ForwardAdd Only)` | Cuts unnecessary highlights from the Base Color area of lights added during the ForwardAdd pass, such as  real-time point lights. This is particularly useful for cel-shaded styles, where there is little to no feathering. This property is not available in the Mobile/Light version. |
| `Set_1st_ShadePosition`                      | Uses a Position Map to force the 1st Shade Color’s position, independent of the lighting. Indicates areas that must have a shadow in black. |
| `Set_2nd_ShadePosition`                      | Uses a Position Map to force the 2nd Shade Color’s position, independent of the lighting. Indicate areas that must have a shadow in black (also affects the 1st Shade Color’s Position Map). |

---

## 4-2. ShadingGradeMap Shaders

These properties are common among UTS2’s high spec ShadingGradeMap shaders.  

<img width = "500" src="Images_jpg/Property_UTS2_04_2.jpg">

| `Property`                                   | Function                                                     |
| -------------------------------------------- | ------------------------------------------------------------ |
| `Set_SystemShadowsToBase`                    | Specified when the Unity shadow system will be used. Be sure to check this option when ReceiveShadow is required (also be sure to check `ReceiveShadow` on the Mesh Renderer). |
| `Tweak_SystemShadowsLevel`                   | Option that is enabled when `Set_SystemShadowsToBase` is set to ON. This property also controls Unity’s system shadow level. The default is 0, and it can be adjusted in a range of ±0.5. This property is useful when you want to do further fine tuning after setting the step level with `BaseColor_Step`/`1st_ShadeColor_Step` or adjust how the self shadow’s ReceiveShadow looks. |
| `1st_ShadeColor_Step`                        | Sets the step between the Base color and 1st Shade Color, the same as the `BaseColor_Step` property. |
| `1st_ShadeColor_Feather`                     | Feathers the boundary between the Base Color and the 1st Shade Color, the same as the `Base/Shade_Feather` property. |
| `2nd_ShadeColor_Step`                        | Sets the step between the 1st and 2nd Shade Colors, the same as the `ShadeColor_Step` property. |
| `2nd_ShadeColor_Feather`                     | Feathers the boundary between the 1st and 2nd Shade Colors, the same as the `1st/2nd_Shades_Feather` properties. |
| `Step_Offset (ForwardAdd Only)`              | Controls the step of lights added during the ForwardAdd pass, such as real-time point lights. This property is not available in the Mobile/Light versions. |
| `PointLights HiCut_Filter (ForwardAdd Only)` | Removes unnecessary lights added during the ForwardAdd pass, such as real-time point lights, from the Base Color area. This is especially useful for cel-shaded styles that don’t use feathering. This property is not available in the Mobile/Light versions. |
| `ShadingGradeMap`                            | Designates the Shading Grade Map as grayscale. Be sure to set `SRGB (Color Texture)` to `OFF` in the texture import settings for Shading Grade Map.|
| `Tweak_ShadingGradeMapLevel` | Level correction of the gray scale value of Shading Grade Map. The default is 0, and it can be adjusted in a range of ±0.5. |
| `Blur Level of ShadingGradeMap` | Blur the Shading Grade Map using the Mip Map function. To enable Mip Map, set Advanced> `Generate Mip Maps` to` ON` in the texture import settings. The default is 0 (no blur). |

---

## 5. Properties of High color (Highlights, Specular Lighting)  

<img width = "500" src="Images_jpg/Property_UTS2_05.jpg">


| Property                       | Function                                                     |
| ------------------------------ | ------------------------------------------------------------ |
| `HighColor`                    | Specifies the high color. If you are not using it, set it to Black (0,0,0). High color moves according to the direction of light. |
| `HighColor_Tex`                | Specifies the color texture of the ‘HighColor’. By using this, you can use complex colors. It will be added to the HighColor’s properties, so if you only want the texture’s color, set ‘HighColor’ to white (1,1,1). If you do not need it, you do not have to touch the settings. |
| `Is_LightColor_HighColor`      | Allows the light color to affect the High Color.             |
| `Is_NormalMapToHighColor`      | Allows the normal map to influence the high color. If not turned on, the object’s geometry will influence it. |
| `HighColor_Power`              | Adjust the high color’s range (In specular lighting terms, this will be the “power”) |
| `Is_SpecularToHighColor`       | Render the high color range as speculum lighting (gloss). If turned off it the boundaries of the high color range will be drawn as circles. |
| `Is_BlendAddToHiColor`         | Adds to the High Color (It becomes brighter). Specular can only be used with the Add mode. |
| `Is_UseTweakHighColorOnShadow` | Turns the  `TweakHighColorOnShadow` slider on.               |
| `TweakHighColorOnShadow`       | Adjusts the power of the high color range in shadows.        |
| `Set_HighColorMask`            | It masks the high color based on the UV coordinates. 100% with white, and black does not show it at all. If you do not need it, you do not need to adjust it. |
| `Tweak_HighColorMaskLevel`     | It adjust the mask level of the high color. The default is 0. |

---

## 6.Properties of RimLight

<img width = "500" src="Images_jpg/Property_UTS2_06.jpg">


| Property                         | Function                                                     |
| -------------------------------- | ------------------------------------------------------------ |
| `RimLight`                       | Turns the ‘RimLight’ on.                                     |
| `RimLightColor`                  | Specifies the RimLight’s color.                              |
| `Is_LightColor_RimLight`         | Turns the light color on in relation to the RimLight.        |
| `Is_NormalMapToRimLight`         | Turn it on when you want the normal map to influence the RimLight. If you do not turn it on the object’s geometry will influence it. |
| `RimLight_Power`                 | Adjusts the RimLight’s power.                                |
| `RimLight_InsideMask`            | Adjusts the power of the inside masking of the RimLight.     |
| `RimLight_FeatherOff`            | Cuts the RimLight’s blurring.                                |
| `LightDirection_MaskOn`          | Only shows RimLight in the light’s direction.                |
| `Tweak_LightDirection_MaskLevel` | Adjusts the rim mask level in the light’s direction.         |
| `Add_Antipodean_RimLight`        | Shows RimLight (AP RimLight) in the opposite direction of the light’s direction. |
| `Ap_RimLightColor`               | Specifies the AP RimLight’s color.                           |
| `Is_LightColor_Ap_RimLight`      | Turns the light color on in relation to AP Rim Color.        |
| `Ap_RimLight_Power`              | Specifies the power of the AP Rim Light.                     |
| `Ap_RimLight_FeatherOff`         | Cuts the AP Rim Light’s blurring.                            |
| `Set_RimLightMask`               | Masks the RimLight according to the UV coordinates. Set to 100% for white, and the rim light does not show when set to black. If you do not need it, you do not have to adjust it. |
| `Tweak_RimLightMaskLevel`        | Adjusts the RimLight mask’s level. The default is 0.         |


---

## 7. Properties of MatCap

<img width = "500" src="Images_jpg/Property_UTS2_07.jpg">


| Property                             | Function                                                     |
| ------------------------------------ | ------------------------------------------------------------ |
| `MatCap`                             | Turns MatCap on.                                             |
| `MatCap_Sampler`                     | Set which texture to use for MatCap.                         |
| `Blur Level of MatCap_Sampler` | Blur MatCap_Sampler using the Mip Map function. To enable Mip Map, set Advanced> `Generate Mip Maps` to` ON` in the texture import settings. The default is 0 (no blur). |
| `MatCapColor`                        | Color that will be added to MatCap_Sampler. If you set a grayscale image on MatCap_Sampler, you can add color to the MatCap with MatCapColor. |
| `Is_LightColor_MatCap`               | Turn the light color on in relations to MatCap.              |
| `Is_BlendAddToMatCap`                | If you turn this on, the MatCap blend will be set to Adding Mode. (It makes it brighter.) If you don’t turn it on it will be blend with Multiplication Mode (It makes it darker.) |
| `Tweak_MatCapUV`                     | You can adjust the MatCap’s range by adjusting the UV of the MatCap_Sampler from the center to a circle. |
| `Rotate_MatCapUV`                    | Rotates the MatCap_Sampler’s UV based on the center.         |
| `Activate CameraRolling_Stabillizer` | By turning it ON, it prevents MatCap from rotating for camera rolling (Rotation with the depth direction as the axis). |
| `Is_NormalMapForMatCap`              | Gives a normal map specifically for MatCap. If you are using MatCap as speculum lighting, you can use this to mask it. |
| `NormalMapForMatCap`                 | Adjust the settings of the normal map especially for the MatCap. |
| `Rotate_NormalMapForMatCapUV`        | Rotates the UV of the MatCap’s normal map based on the center. |
| `Is_UseTweakMatCapOnShadow`          | Turns the Tweak MatCapOnShadow slider on.                    |
| `Tweak MatCapOnShadow`               | Adjusts the power of the Matcap’s range in shadows.          |
| `Set_MatcapMask`                     | By setting a grayscale mask for MatCap, you can adjust how MatCap is shown. The MatcapMask is placed based on the UV coordinates of the mesh that the MatCap will be projected on. Mask with black and unmask with white. |
| `Tweak_MatcapMaskLevel`              | Adjusts the power of the MatcapMask. When the value is 1, MatCap is displayed 100% irrespective of whether or not there is a mask. When the value is -1, MatCap will not be displayed at all and MatCap will be the same as in the off state. The default value is 0. |
| `Inverse_MatcapMaskLevel` | By turning it ON, the MatcapMask is inverted. |
| `Orthographic Projection for MatCap` | Enable this when the camera projection used in the scene is orthographic. When using perspective camera, turn it off. By doing this, you can correct the distortions caused by the camera.  TIP: By turning this on, the perspective camera behaves like the one in UTS2 v.2.0.4, which means the distortion will not be corrected. |

---

## 8.Angel Ring Properties

<img width = "500" src="Images_jpg/Property_UTS2_08.jpg">


| Property            | Function                                                     |
| ------------------- | ------------------------------------------------------------ |
| `AngelRing`         | Turns Angel Ring on.                                         |
| `AngelRing_Sampler` | Specifies the texture of the Angel Ring.                     |
| `AngelRing_Color`   | Specifies the color that will be multiplied to the Angel Ring. |
| `AR_OffsetU`        | Adjusts the Angel Ring’s shape in the horizontal direction.      |
| `AR_OffsetV`        | Adjusts the Angel Ring’s shape in the vertical direction.       |
| `Is_LightColor_AR`  | Allows the light color to affect the Angel Ring.             |
| `ARSampler_AlphaOn` | By turning this on, you can use the α channel that is included in the Angel Ring’s texture as a clipping mask. |


---

## 9. Properties of Emissive

<img width = "500" src="Images_jpg/Property_UTS2_09.jpg">


| Property         | Function                                                     |
| ---------------- | ------------------------------------------------------------ |
| `EMISSIVE MODE` | By making it `ANIMATION`, you can animate the RGB channel part of the texture specified by` Emissive` in various ways. **Alpha channel is a mask, so it can not be animated.** |
| `Emissive_Tex`   | Specifies the texture for Emissive. You can also create  a mask texture with grayscale and make it emit light with Emissive_Color. If you do not want it to emit light on top of other parts, set it to Black (RGB: 0,0,0) |
| `Emissive_Color` | Color that will be multiplied to each pixel color in ‘Emissive_Tex’. In most cases, set **[HDR Color](https://docs.unity3d.com/ja/current/Manual/HDRColorPicker.html)** |
| `The αchannel of Emissive Texure` | As of v. 2.0.7, the alpha channel can be used as a mask for emissive textures. Emissive is displayed at the position where the alpha channel is set to white (RGB = (1, 1, 1)) on UV basis. When black (RGB = (0, 0, 0)), the emissive disappears. In order to enable the alpha channel, in the case of an image format that can have an alpha channel such as Targa format, set `Alpha Source` to` Input Texture Alpha` in Import Settings of each texture. In the case of PNG format, since it is not possible to have an alpha channel directly in the image specification, after importing an alpha channel as a selection range in Photoshop, specify "Layer mask> Mask outside selection range" and save in PNG format You Then import it into Unity, and in Import Settings, set `Alpha Source` to` Input Texture Alpha` and `Alpha Is Transparency` to` ON`. |
| `Base_Speed` | Specifies the base update speed of the animation. If the value is 1, it will be updated in 1 second. Specifying a value of 2 results in twice the speed of a value of 1, so it will be updated in 0.5 seconds. |
| `Scroll_EmissiveU` | Specifies how much the Emissive texture is to be scrolled in the U direction (direction of the X axis) when updating the animation. Specify in the range -1 to 1 and the default is 0. Scroll animation is ultimately determined as the result of `Base Speed (Time)` x `Scroll U Direction` x` Scroll V Direction`. |
| `Scroll_EmissiveV` | Specifies how much the Emissive texture is to be scrolled in the V direction (direction of the Y axis) to update the animation. Specify in the range -1 to 1 and the default is 0. |
| `Rotate_EmissiveUV` | Specifies how much the Emissive texture should be rotated around the center of the UV coordinates (UV = (0.5, 0.5)) as an animation update. When Base Speed = 1, turns 1 clockwise with a value of 1. When combined with scrolling, it will rotate after scrolling. |
| `Is_PingPong_Base` | By setting it to `ON`, you can set PingPong (back and forth) in the direction of the animation. |
| `Activate ColorShift` | By setting it to `ON`, the color to be multiplied to the Emissive texture is changed between` Destination Color`. **When using this function, it is better to set the Emissive texture to grayscale and design the each colors on the color side to be multiplied.** |
| `Destination Color` | This is the color to change to when color shifting. It can be specified in HDR. |
| `ColorShift_Speed` | Set the standard speed for color shift. When the value is 1, change of one cycle should be approximately 6 seconds as a guide. |
| `Activate_ViewShift` | `ON` shifts the color relative to the camera's viewing angle to view the object. When viewed from the front of the surface of the object, the normal Emissive color is displayed, and the color changes to the shifted color as the view angle gradually inclines. |
| `ViewShift` | This is the color to change to when shifting views. Specify in HDR. |
| `Is_ViewCoord_Scroll` | Specifies the coordinate system to use for scrolling. In the case of `OFF`, scrolling is performed based on the UV coordinates of Emissive_Tex. In the case of `ON`, it scrolls based on the same view coordinates as MatCap. Scrolling in the view coordinate system is very useful because it does not take into account the UV coordinates of the texture, but it is often the case that objects with flat faces like cubes can not be displayed well. On the other hand, the view coordinate system can be used very conveniently for objects with many surfaces such as characters. |

---

## 10.Properties of Outline  

<img width = "500" src="Images_jpg/Property_UTS2_10.jpg">


| Property                  | Function                                                     |
| ------------------------- | ------------------------------------------------------------ |
| `OUTLINE MODE`            | Specifies how the inverted-outline object will be spawned.  You can choose between `NML`（normal inverted method） / `POS`（position scaling method. In most cases, NML is used but if it is a mesh that is only made of hard edges (such as cubes), POS will prevent the outline from being disconnected. It will be good to use  POS for simple shapes and use NML for characters and things that have complicated outlines. |
| `Outline_Width`           | Specifies the width of the outline. **NOTICE: This value relies on the scale when the model was imported to Unity** which means that you have to be careful if the scale is not 1. |
| `Farthest_Distance`       | The width of the outline will change depending on the distance between the camera and the object. Specifies the farthest distance. The farthest distance will be when the outline becomes 0. |
| `Nearest_Distance`        | The width of the outline will change depending on the distance between the camera and the object. Specifies the closest distance. The closest distance will be when the ‘Outline_Width” is the width that was set as the maximum. |
| `Outline_Sampler`         | When you want to see the “start” and “end” of your outlines, or when you only want to outline certain parts, use the outline sampler (texture). The thickest width with white and the least thickest with white. |
| `Outline_Color`           | Specifies the color of the outline.                          |
| `Is_BlendBaseColor`       | Select this when you want to blend the color of the outline with the object’s base color. |
| `Is_LightColor_Outline`  | Applies the light color to the Outline Color. The contribution of the light color to the outline is: When "OFF", the color set for the outline color is displayed as it is. When "Active with 1 realtime directional light in the scene", the outline color responds to the color and brightness of the realtime directional light. When "There is no real-time directional light in the scene at the time of Active", the outline color responds to the color and brightness of Color in the Source of Environment Lighting. **At this time, please note that the value of Color is referenced even if you are using Skybox. In addition, please be careful as it does not react to ambient light other than real-time point light and color.** |
| `Is_OutlineTex`           | Turn it on when you want to paste texture to the inverted-outline object. |
| `OutlineTex`              | Use this when you want the outline to have special textures. By changing the textures, you can give the outlines patterns, or make the outline unique by changing the texture of the inverted object which will be front-face culled. |
| `Offset_Camera_Z`         | Offsets the outlines in direction Z. The outline will be less visible for the spikey parts in spikey hair if you input a positive value. For most cases, just set this to 0. |
| `Is_BakedNormal`          | By turning this on, you can turn on `BakedNormal for Outline`. |
| `BakedNormal for Outline` | Reads normal maps that have the vertices normal from other models baked into it as “added” when setting the inverted outlines. For more, look below. |


---

## 11.  Using Light Probes, and the properties of functions that are useful for Shader Built-in Light and VRChat  

<img width = "500" src="Images_jpg/Property_UTS2_11.jpg">

| `Property`                                     | Function                                                     |
| ---------------------------------------------- | ------------------------------------------------------------ |
| `GI_Intensity`                                 | By setting `GI_Intensity` to 0 or higher, it will deal with the GI system within Unity’s Lighting window, especially [Light Probe](https://docs.unity3d.com/ja/current/Manual/LightProbes.html).  When `GI_Intensity` is 1, the GI intensity will be 100%. **This function is improved with v.2.0.6, if you want to use GI including light probes, first set 1 (almost the same brightness as Standard Shader). and then adjust as necessary**. |
| `Unlit_Intensity`                              | When there is no real-time directional light in the scene, the scene’s brightness and color will be determined by[Environment Lighting’s Source Settings ](<https://docs.unity3d.com/ja/current/Manual/GlobalIllumination.html>),boost it by `Unlit_Intensity` and use it as a light source. (This is called **Ambient Blending**) The default is 1 and 0 is to turn it off completely. This is used when you want to have the environment color blend with the material color, **but if you want it to be a darker blend, set it to 0.5～1 and if you want it to be a lighter blend, set it to 1.5～2.** (From v.2.0.6, the maximum value has become 4.) |
| `VRChat : SceneLights Hi-Cut_Filter`           | This will minimize overexposure when the light intensity is too high, or when there are multiple real-time directional lights, or multiple real-time point lights. By turning this one you can maintain the light colors and its attenuations while only cutting the intensity of the material color to avoid overexposure. The default is `OFF`. When using this function, please make sure that `Is_LightColor_` system such as` Is_LightColor_Base` check is `ON`. **We recommend VRChat users to turn this on**. Hint: If overexposure still occurs even when this is turned on, please check the post-effect bloom settings. (In particular, when Bloom’s threshold value is under 1, it is easier to happen.) |
| `Advanced : Activate Built-in Light Direction` | For experienced users, you can activate the Built-in Light Direction’s vector (the vector of the virtual lights in the shader). When this is activated, the intensity and color of the light will follow the real-time directional light’s values within the scene. If there aren’t lights like that, the values for ambient blending will be used. |
| ` Offset X-Axis (Built-in Light Direction)`    | Moves the virtual lights left and right that are spawned by the built-in light direction vector left and right . |
| ` Offset Y-Axis (Built-in Light Direction)`    | Moves the virtual lights that are spawned by the built-in light direction vector up and down. |
| ` Inverse Z-Axis (Built-in Light Direction)`   | Moves the virtual lights that are spawned by the built-in light direction vector back and forwards. |

---

## 12.Properties of Tessellation  

<img width = "500" src="Images_jpg/Property_UTS2_12.jpg">


| `Property`            | Function                                                     |
| --------------------- | ------------------------------------------------------------ |
| `DX11 Tess : Edge Length`    | Divides the tessellation according to the camera’s distance. The smaller the value, the smaller the tiles become. The default is 5. |
| `DX11 Tess : Phong Strengh`  | Adjusts the pulling strength of the surfaces divided by tessellation. The default is 0.5. |
| `DX11 Tess : Extrusion Amount` | Scale the expanded parts due to tessellation. The default is 0. |

