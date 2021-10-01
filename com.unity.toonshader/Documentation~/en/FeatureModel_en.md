| Feature Description 	| Feature Model 	| Legacy 	| URP 	| HDRP 	| Note 	|
|-	|-	|-	|-	|-	|-	|
| **UTS Feature Model 3.1** 	|  	|  	|  	|  	| as of 0.4.0-preview	|
| ***1. Basic shader features*** 	|  	|  	|  	|  	|  	|
| Double Shade With Feather Workflow 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Shading Grade Map Workflow 	| 2.0 	| OK 	| OK	| OK 	|  	|
| Switch Culling Mode 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Stencil Mask function 	| 2.0 	| OK 	| OK 	| N.A. 	|  	|
| Stencil Out function 	| 2.0 	| OK 	| OK 	| N.A. 	|  	|
| Custom stencil number 	| 2.0 	| OK 	| OK 	| N.A. 	|  	|
| Clipping mask function. Tweak Clipping level 	| 2.0 	| OK 	| OK 	| OK	|  	|
| TransClipping function. Tweak TransClipping level 	| 2.0 	| OK 	|OK	| OK 	|  	|
| Inverse Clipping Mask 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Use the alpha channel of Basemap as a Clipping mask 	| 2.0 	| OK 	|OK	| OK	|  	|
| Transparent shader function. Tweak Transparency Level 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Switch Current UI type 	| 2.0 	| OK 	| OK	| OK 	|  	|
| Game recommendation button 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Remove Unused Keyword button 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| ***2. Basic Three Colors and Shadow Control Maps features*** 	|  	|  	|  	|  	|  	|
| Set Base Map and Color.  Sharing the map with 1st Shade Map 	| 2.0 	| OK 	| OK	| OK 	|  	|
| Set 1st Shade Map and Color.  Sharing the map with 2nd Shade Map 	| 2.0 	| OK 	| OK 	| OK	|  	|
| Set 2nd Shade Map and Color 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Normal Map function. Tweak Bump scale, Tiling and Offset 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Tweak Normal map effectiveness to three basic colors, high color, or rim light 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Set 1st and 2nd Position Maps for shadow control 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Set Shading Grade Map for shadow control. Tweak gray level and blur the map 	| 2.0 	| OK 	| OK	| OK 	|  	|
| ***3. Basic Lookdevs features***	|  	|  	|  	|  	|  	|
| Tweak the level of step and feather between the base and 1st shade colors 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Tweak the level of step and feather between the 1st and 2nd shade colors 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Switch system shadows receiving. Tweak the level of system shadows 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Offset the step value of Point lights 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| PointLights Hi-Cut Filter function 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| ***4. HighColor features*** 	|  	|  	|  	|  	|  	|
| Set High Color and map and Tweak HighColor Power 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Switch Specular Mode 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Switch Color Blend Mode either Multiply or Additive 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Mask High Color zone with system shadows and Tweak the masking power 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Set HighColor Mask and Tweak the level of the mask 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| ***5. RimLight features*** 	|  	|  	|  	|  	|  	|
| Add RimLight on the surface and Set the color and power of it 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Tweak the power of RimLight Inside Mask and Cut the feather off on the edge of the mask 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Light Direction Mask function. Tweak the level of the mask 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Antipodean(Ap)_RimLight function. <br>Ap_RimLight appears on the opposite surface of the light direction. <br>Set the color of the Ap_RimLight, tweak the power of it and switch whether cutting the feather off 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| RimLight Mask function. Tweak the level of the mask 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| ***6. MatCap features*** 	|  	|  	|  	|  	|  	|
| Add MatCap on the surface and Set the MatCap sampler and color 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Blur the MatCap sampler and Tweak Tiling and Offset 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Switch Color Blend Mode either Multiply or Additive 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Tweak the Scale and Rotate of the UV Coordinate of MatCap Sampler 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| CameraRolling Stabilizer function, freezing the rotation of MatCap <br>projection following by camera rolls 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Normal Map for MatCap function.  Tweak Bump scale, Tiling, Offset and UV Rotation 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Mask MatCap projection with system shadows and Tweak the masking power 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Switch the projection camera of MatCap, avoiding lens distortion 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Set MatCap Mask, Tweak the level of the mask, or Inverse it. 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| ***7. AngelRing Projection features*** 	|  	|  	|  	|  	|  	|
| Add AngelRing Projection on the surface and <br>Set the AngelRing Sampler and color 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Offset the position of AngelRing with  the U or V axis 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Use the alpha channel of AngelRing sampler as a Clipping mask 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| ***8. Emissive features*** 	|  	|  	|  	|  	|  	|
| Set Emissive Map and Color and Tweak Tiling and Offset 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Use the alpha channel of Emissive Map as a Clipping mask 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Emissive Animation function 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Set Base Speed of updating 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Switch scroll coordinate either UV or View 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Set scroll amounts of Emissive Map between U/X and V/Y directions 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Set rotate amounts of Emissive Map around the center of UV Coordinate 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Set PingPong like movement for scroll 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| ColorShift function. Set the speed of shift and the destination color 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| ViewShift function. Set the destination color to view shift. 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| ***9. Outline features*** 	|  	|  	|  	|  	|  	|
| Switch the method of Outline Mode either Normal Direction or Position Scaling 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Set the width and color of the outline 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Blend the color of the outline with the surface's base color 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Control the width of the outline's stroke with Outline Sampler 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Offset the outline along with the Z-axis of the camera coordinate 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Control the width of the outline with the distance from the camera. Set the threshold values of the farthest and nearest distances 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Use Outline Texture for the outline's color 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Use Baked NormalMap for Outline in the Normal Direction method 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| ***10. Tessellation feature*** 	|  	|  	|  	|  	|  	|
| Tessellation function. Tweak the value of Edge Length, Phong Strength, or Extrusion Amount 	| 2.0 	| DX11 	| N.A. 	| DX11/Vulkan/Metal 	|  	|
| ***11. LightColor Contribution feature*** 	|  	|  	|  	|  	|  	|
| Turn on/off Realtime LightColor Contribution to each color:<br> i.e., BaseColor, 1st Shade Color, 2nd Shade Color, HighColor, RimLight, Ap_RimLight, MatCap, AngelRing, or Outline 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| ***12. Environmental Lighting Contributions features*** 	|  	|  	|  	|  	|  	|
| Tweak the value of GI Intensity toward material from light probes 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Tweak the value of Unlit Intensity of material in the scene where is no realtime lighting source 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| SceneLights Hi-Cut Filter function. Adjust the high intensity from lights avoiding the overshoot of material's colors 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| Built-in Light Direction function. <br>Activate the virtual light for each material and Tweak the offset value of each axis of the light's direction 	| 2.0 	| OK 	| OK 	| OK 	|  	|
| **UTS Feature Model 2.1** 	|  	|  	|  	|  	|  	|
| 1. Support RTHS (Realtime Raytraced Hard Shadow) as the shadow map, getting precise cel-shaded images (requires a working environment for DXR) 	|  	|  	|  	|  	|  	|
| Activate Raytraced Hard Shadow function (requiring ShadowRaytracer component on the camera) 	| 2.1 	| OK 	| OK 	| OK 	|  	|
| **UTS Feature Model 2.2** 	|  	|  	|  	|  	|  	|
| ***1. Integrate two workflows and shader variations as an Uber Shader*** 	|  	|  	|  	|  	|  	|
| UniversalToon / Uber shader custom user interface 	| 2.2 	| OK	| OK 	| OK 	|  	|
| Switch Workflow Mode either DoubleShadeWithFeather or ShadingGradeMap 	| 2.2 	| OK	| OK 	| OK 	|  	|
| Select Auto Queue or Custom Render Queue 	| 2.2 	| OK	| OK 	| OK 	|  	|
| Switch Transparent Mode 	| 2.2 	| OK	| OK 	| OK 	|  	|
| Switch Stencil Mode either Off, StencilOut or StencilMask 	| 2.2 	| OK	| OK 	| N.A. 	|  	|
| Switch Clipping Mode 	| 2.2 	| OK	| OK 	| OK 	|  	|
| Switch TransClipping Mode 	| 2.2 	| OK	| OK 	| OK 	|  	|
| Activate Outline function 	| 2.2 	| OK	| OK 	| OK 	|  	|
| ***2. Rendering per Channels feature*** 	|  	|  	|  	|  	|  	|
| Set the color and visibility of each channel:<br> i.e., BaseColor, 1st Shade, 2nd Shade, HighColor, AngelRing, RimLight, or Outline 	| 2.2 	| N.A. 	| N.A. 	| OK 	|  	|
| **UTS Feature Model 3.0** 	|  	|  	|  	|  	|  	|
| ***1. Integrate some textures into one automatically (experimental)***	|  	|  	|  	|  	|  	|
| _MainTexSynthesized, _ShadowControlSynthesized, _HighColor_TexSynthesized<br>and _Outline_SamplerSynthesized 	| 3.0	| deprecated	| deprecated	| deprecated	|  	|
| **UTS Feature Model 3.1** 	|  	|  	|  	|  	|  	|
| ***1. More than 16 textures*** 	|  	|  	|  	|  	|  	|
| Some texture samplers are shared 	| 3.1	| OK	| OK	| OK	|  	|
| ***2. EV Adjustment*** 	|  	|  	|  	|  	|  	|
| EV Adjustment in high intensity light scenes	 	| 3.1	| N.A	| N.A.	| OK	|  	|
| ***3. Render pipeline built-in raytraced shadows*** 	|  	|  	|  	|  	|  	|
| DXR shadow supported in render pipelines 	| 3.1	| N.A.	| N.A.	| OK	|  	|
| ***4. Box Light*** 	|  	|  	|  	|  	|  	|
| Substitute for directional light 	| 3.1	| N.A.	| N.A.	| OK	| to avoid the limitation that unable to have multiple directional light casting shadows |


