# Changelog
**Universal Toon Shader [a.k.a. Unity-Chan Toon Shader]**
## [2.2.3] - 2021/05/09
### Fixed
- A compile error on Unity 2020.2 when ShadingGradeMap is selected.
- Made the shaders compatible with 2020.2, 2021,1, 2021.2.

## [2.2.2] - 2021/02/22
### Fixed
- A compile error on Unity 2020.2 on some materials which are converted from Universal RP/Lit.

## [2.2.1] - 2021/02/12
### Fixed
- A compile error on Unity 2020.2.

## [2.2.0] - 2020/07/07
The first UTS/UniversalToon for URP official release.

### Added
- RTHS (Realtime Raytraced Hard Shadow) support. Require the environment where DXR(DirectX Raytracing) works properly. 
- Feature parity with UTS2 legacy v2.0.7.5

### Changed
- Changed directory format to support Package Manager distribution.

### Fixed
- Self-shadowing was not applied during the ShadingGradeMap workflow.


---
# UTS2(Legacy) Changelog
The following changelog is the history of UTS2 shader in legacy pipeline.
## [2.0.7 release 5] - 2019/05/22
### Added
- Added "_Is_LightColor_Outline" to make light color react to outline color. From the custom GUI, the function can be turned on by setting "Outline" button to "Active" from "LightColor Contribution to Materials" menu. In the case of the outline, there are some specification restrictions on the reaction to the light color, so please refer to the manual for details.  

### Fixed
- Fixed Version 4 has been discarded and a new "the problem of incorrect display of the image outline reflected in the mirror object when Z-Offset is specified in the outline" has been corrected.  

## [2.0.7 release 4] - 2019/05/15
### Changed
- When Z-Offset is specified for the outline, the problem of incorrect display of the outline of the image reflected in the mirror object has been alleviated.  

## [2.0.7 release 3] - 2019/05/10
### Added
- Added a new sample scene for testing(Mirror/MirrorTest.unity).  

### Fixed
- Fixed sign of _Offset_Z in UCTS_Outline.cginc for OpenGL.  

## [2.0.7 release 2] - 2019/04/17
### Changed
- Made Updating to UTS2 v.2.0.7 easier than with v.2.0.4.3p1 or earlier.  

## [2.0.7 release 1] - 2019/03/28
### Added
- Added 'Remove Unused Keywords/Properties from Material` button to Option Menu in Basic Shader Settings.  By executing this function, unnecessary shader keywords and unused property values in the UTS2 material can be removed.  Applying this when publishing a project, especially just before uploading avatars to VRChat, can reduce the unnecessary load on the system.  This function was developed based on the Issue #18 of ACiiL TwitterID: @__aciil's .

## [2.0.7 release 0] - 2019/03/23
### Added
- Added Emissive Animation function.  
- Add Vulkan support. (note:UTS2 with DX11 Tessellation doesn't work with Vulkan.)
- Add Angel Ring Camera Rolling Stabilizer. (Always on) 

### Fixed
- MatCap image in the mirror and the Camera Rolling Stabilizer now work properly with all Unity versions. 

---
## [2.0.6 release 1] - 2019/02/28
### Changed
- Refactored custom inspector.
- Improved documentation.

## [2.0.6 release 0] - 2019/02/21
### Added
- Added following bug fixes and new features.  
- Added custom inspector.
- The new feature "Activate Camera Rolling_Stabilizer" which suppresses rotation of MatCap against the rolling of the camera was carried. This function was realized by Contribution of TwitterID: @ShowBuyS.  
- The new feature "Blur Level of MatCap_Sampler" based on Mip Map has been added to MatCap_Sampler.  
- The new function "Inverse_MatcapMask" to invert texture for MatcapMask was added.  
- The ShadingGradeMap function has been enhanced. In addition to adding "Tweak_ShadingGradeMapLevel" for level correction, "Blur Level of ShadingGradeMap" based on Mip Map has been added.  
- Bump scale added to normal map.  
- Depth buffer write processing has been added to Transparent shaders.  

### Changed
- Improved documentation with custom inspector instructions.
- Enhanced the Tweak_SystemShadowsLevel function to adapt to receive shadows and shading with system shadows.  
- When "PointLights HiCut_Filter (ForwardAdd Only) = OFF", high color is added to real time point light.  
- In addition to making the properties of tessellation shaders easy to use, "DX 11 Tess: Extrusion Amount" was made slidable for easy adjustment.  
- Improved the GI_Intensity to set the strength of GI. If you want GI to reflect on UTS2 material, please first set "GI_Intensity = 1". It is reflected at almost the same brightness as Standard Shader.  
- The maximum value of Unlit_Intensity has been changed from 2 to 4.

### Fixed
- Fixed that the rimlight was not reflected when the blend mode of MatCap was multiplying (Is_BlendAddToMatCap = OFF).  
- When the color blend mode of MatCap is multiplication (Is_BlendAddToMatCap = OFF), the order of shadow masks was incorrect was corrected.  
- Fine adjustment of surface judgment of outline mode / position scaling method.  

---
### Pre-2.0.5 Updates
2019/01/07: 2.0.5 Release: Fixed illegal termination when opening the project on macOS / Unity 2018.3.0f2.  
2018/11/22: 2.0.5 Release: Release version.  
2018/11/16: 2.0.5 Test07: Bugfix.  
2018/11/16: 2.0.5 Test06: SceneLights Hi-Cut_Filter. VR Chat ready!  
2018/11/11: 2.0.5 Test05: MatCap with camera skew correction.  
2018/11/08: 2.0.5 Test04: Cel-look quality when using real-time point lights has improved.  
2018/11/06: 2.0.5 Test 03: Added some features.  
2018/10/31: 2.0.5 Test 02: Added some features.  
2018/10/06: 2.0.5 Test: The internal variable name of BaseMap had been changed. Add Built-in Light Direction.  
2018/09/10: 2.0.4.3 Release Patch 1: Bugfix.  
2018/09/05: 2.0.4.3 Release: Added useful features for VRChat users. Ambient light blending.  
2018/08/21: 2.0.4.2 Release: Bugfix.  
2018/08/16: 2.0.4.2 Release: Added MatcapMask.  
2018/07/04: 2.0.4.1 Release: Added Unlit_Intensity property.  
2018/05/04: 2.0.4 Release: Set Unity5.6.x and later versions as the target environment. DX11 Phong Tessellation support. Started VR Chat support.  
2017/06/19: 2.0.3: Added Set_HighColorMask and Set_RimLightMask.  
2017/06/09: 2.0.2: Official support for Nintendo Switch and PlayStation 4.  
2017/05/20: 2.0.1: Added 2 transparent shaders.  
2017/05/07: 2.0.0: Initial version   
