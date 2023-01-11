# Changelog

## [0.8.3-preview] - 2023-01-11
### Fixed:
* URP Shader doesn't work with Unity 2022.2.2f1.
* Warrnings in HDRP shaders.

## [0.8.2-preview] - 2022-09-09
### Updated:
* Updated some documents.

### Fixed:
* Deleted some warinings.
* The converter ignores custom render queue in Unitychan Toon Shader ver 2 materials.
* The converter sometimes misidentified materials older than 0.7.x in Built-in UTS3 as older materials in Unitychan Toon Shader Ver 2.0.7.
* URP shader was not working for WebGL/GLES 3.0.


## [0.8.1-preview] - 2022-08-24
### Updated:
* Updated some docs.

### Fixed:
* TOC didn't include the link to Material Converter page.
* A image in index.md wasn't displayed in doc web site.

## [0.8.0-preview] - 2022-08-02
### Updated:
* All the render pipeline shaders are integrated.
* Added material converter to above which is capable of Unity-Chan Toon Shader V2, Universal Toon and UTS3 older than 0.7.x.
* The package is compatible with 2020.3, 2021.3, 2022.1 and 2022.2. 2019.4 is no longer supported.


## [0.7.5-preview] - 2022-06-27
* Improved the logic for blending outlines into a scene when there are no directional lights in the scene.

## [0.7.4-preview] - 2022-06-25
### Fixed:
* HDRP:Metaverse light was not working properly.

### Updated:
* Added `Getting Started` to the documentation.
* Added more description to Outline doc.
* Added more description to RimLight doc.
* Mended Meteverse doc to use property, insted of indented menus.
* Mended SceneLightSetting doc to use property instead of indented menus.
* Re-wrote the doc to install samples.
* Updated some images in docs.
* Improved some wording in documentation.

## [0.7.3-preview] - 2022-06-15
### Updated:
* HDRP shader is compatible with 2022.2.0 a16.
* Attribute class for Mono-behaviors for UTS3.
* Mac Graphics Tests use rsync instead of scp.
* New retry count to several commands in Mac graphics tests.
* 3 basic color is now Three basic colors.
* `NormalMap Settings` is now an independent block.
* Updated docs.

## [0.7.2-preview] - 2022-05-30
### Fixed:
* Dead links for Toon EV adjustment HELP URL Buttons.
* Some classes are accessible from outside.

## [0.7.1-preview] - 2022-05-26
### Updated:
* Polished docs.

## [0.7.0-preview] - 2022-05-23
### Updated:
* Shader version properties in all the render pipeline shaders.
* Updated installation.md.
* The inspector window is significantly reorganized.
* Renamed `Double Shade with Feather` to `Standard`.
* Renamed `Shading Grade map` to `With Additional Control Maps`.
* Replaced toggle buttons Off/Active UI to Unity standard toggles.
* Renamed `Basic Shader Settings` to `Shader Setting`.
* Replaced some toggle buttons, such as `Emissive UV Coord`, `Specular Mode`, to dropdown menus
* Specular mode name are not just `Off`/`On` but `Soft`/`Hard`.
* Made everything in the inspector hidden when disabled to be shown but grayed out.
* Folder headers use Unity standard style in the inspector window when SRP core newer than 12 is not installed.
* Folder headers use SRP style in the inspector window when SRP core newer than 12 is installed.
* Renamed `【DX11 Phong Tessellation Settings】` to `Legacy Pipeline: Phong Tessellation Settings` as it works on Mac too.
* Renamed term, `Technique`, to `Mode`.
* Renamed term, `Contribution`, to `Effectiveness`.
* Renamed foldout name `Light Color Contribution` to `Scene Light Effectiveness Settings`
* Moved `Gi Intensity` and `Scene Light hi cut filter` to bellow it.
* Renamed `Scene  Hi-cut filter` to  `Limit Light Intensity` .
* Renamed term, `High Color`, to `Highlight`.
* Renamed `Settings for PointLights in ForwardAdd Pass` to `Point Light Settings`.
* Renamed `Point Light High Cut Filter` to `Filter Point Light Hight Light`.
* Re-organized Stencil and Clipping settings in the inspector window.
* Renamed `Stencil Out` to `Draw If Not Equal to`.
* Renamed `Stencil Mask` to `Replace Stencil Buffer with`.
* Renamed `Stencil No.` to `Stencil Value`.
* The above used to be a number field. Now it is editable with IntSlider.
* Renamed `Inverse MatCap Mask` to `Invert MatCap Mask`.
* Renamed `Trans Clipping Mode` to `Clip Transparency`.
* Renamed `Unlit Intensity` to `Metaverse Light Intensity` and `Buiilt-in light settings` are moved into `Metaverse Settings` foldout header in the inspector so that users can understand  setting them is not necessary for usual uses.The setting works only when no directional lights are on the scenes like some VRChat ones.
* _Unlit_Intensity ranges defined in all the shaders are from 0 to 4. originally they were from 0.001 to 4.
* Changed built-in light default brightness from 1 to 0.
* Renamed `Basic Look Dev` to `Shading Step and Feather Settings`.
* `Advanced settings` in Outline foldout header are always shown.
* Renamed `Outline Sampler` in Ontline foldout header to `Outline Width Map`.
* Renamed `Outline-tex` in Ontline foldout header to `Outline Color Map`.
* Renamed Culling mode itemes from `CullingOff`, `FrontCulling` and `BackCulling` to `Off`, `Frontface` and `Backface`.
* Renamed `Receive System Shadows` to `Receive Shadows`.
* Renamed `Shadow Mask on High Color` to `Highlight Blending on Shadows`.
* Renamed `Highlight Power on Shadows` to `Blending Level`.
* Renamed `Camera Rolling Stabilizer` to `Stabilize Camera Rolling`.
* Renamed `ViewShift of Color` to `Color Shifting with View Angle`.
* Renamed `ViewShift` to `Shifting Target Color`.
* Renamed `Use Outline Texture` to `Outline Color Map`.
* Renamed `Use Baked Normal for Outline` to `Baked Normalmap`.
* Renamed `HighColor Power on Shadow` to `Brightness on Shadows`.
* Renamed `MatCap Power on Shadow` to `MatCap Blending on Shadows`.
* Renamed `Color Shift Speed` to `Color Shifting Speed`.
* Renamed `Blur Level of ShadingGradeMap` to `ShadingGradeMap Blur Level`.
* Renamed `MatCap Sampler` to `MatCap Map`.
* Renamed `Blur Level of MatCap Sampler` to `MatCap Blur Level`.
* Renamed `Rim Light Power` to `Rim Light Level`.
* Renamed `RimLight Inside Mask` to `Adjust Rim Light Area`.
* Renamed `Light Direction Mask` under Rim Light Settings to `Light Dreiction Mode`.
* Renamed `Light Direction Mask Level` to `Light Direction Rim Light Level`.
* Renamed `Antipodean(Ap)_RimLight` to `Inversed Direciton Rim Light`.
* Renamed `AP_RimLgiht Power` to `Inversed Rim Light Level`.
* Renamed `Color Blend Mode` to `Color Bleinding Mode`.
* Renamed `GI Intesity` to `Light Probe Intensity`
.
### Added:
* Legacy: Compatibility with Single Pass Stereo Rendering.
* Legacy: Graphics Test 2022.2.
* Universal RP: Graphics Test 2022.2.
* HDRP: Graphics Test 2022.2.
* Project Settings, but quite simple yet.
* Help buttons to foldout headers in the inspector window, which work newer than Unity 2021.1.
* Unity-Chan Toon Shader 2 Converter window opens when old shaders are in projects on start up or right after the package is installed.
* Dependency to srp core take advantege of its help system.
* Added Tips to Culling Mode popup menu in the inspector window.
* Added Tips to GUI Toggles in the inspector window.
* Added Tips to Range Properties in the inspector window.

### Removed:
* Removed   `● Additional Settings` in the Inspector window.
* Removed Simple UI.
* Removed Japanese and English manual link buttons.
* Removed Game Recommendation window and changed the default values of shader variables.
* Removed 【】in the Inspector window.
* Removed  ● in the Inspector window.
* Removed unnecessary labels in the inspector such as `System Shadows:`
* `RTHS(Realtime Hard Shadow` is deprecated now as it will not be Unity official package.
* RTHS is not shown unless `Show deprecated features in the inspector` is checked in the `Project Settings Window`.

### Fixed:
* Undo/Redo was not working on some items in the inspector window.
* Fixed Indent in NoramMap Settings in the inspector window.
* `Point Light High Cut Filter` Side Effects. It just shows or hides the point lights' highlight.
* Unify the notation in the converter with Unity-Chan Toon Shader 2.
* Fixed some popups in the inspector window not to use connected names without spaces. Each name is separated by a space.
* `Rimlight Mask` on URP and HDRP version were not working well.
* Single pass stereo rendering was not working with legacy(built-in) tessellation shaders.
* URP shader errors when used with Unity 2022.2.

## [0.6.1-preview] - 2022-02-24
### Fixed:
* typo in inspector.
* removed .sample.json under Samples~ folder
* Some unnecessary classes were public.
* Unnatural expression `Multiply or Additive` to `Multiply or Add`.

## [0.6.0-preview] - 2022-02-22
### Updated:
* Replaced test VM to use gtx1080
* Updated README.md
* Added LICENSE.md under the project folder.
* Shader version properties in all the render pipeline shaders.

### Fixed:
* Some unnecessary classes were public.
* URP: a depth output issue later then 10.0.x. (Thanks to riina)
* Typos in README.md
* promotion test issues.
* no .sample.json files under each render-pileline sample folder.

## [0.5.0-preview] - 2022-01-20
### Updated:
* Updated documentation and folder structure in order to make the package structure friendly to Unity official package.
* Tentatively deleted Japanese documentation before making the package ready for Unity standard translation system.
* HDRP: made UTS compatible with HDRP AOV. Capable of outputting Albedo, Normal and so on using AOV Image Sequence Recorder newer than 3.0.
* More effective and strict internal tests.

### Fixed:
* HDRP:reduced shader variants by disabling some debug pragmas.
* URP:UTS materials don't receive shadows when using newer than Unity 2021.1.
* Legacy:Unable to disable Outline.
* AutoRenderQueue is disabled when material inspector is unfocused.


### Known Issues:
* HDRP: When outputting AOV images, UTS Outline is also put into the images. In such cases, please disable OUTLINE in Material Inspectors.

## [0.4.1-preview] - 2021-10-20
### Added
* HDRP:Something similar to HDRP exposure compensation.

### Fixed:
* typo in documents.
* missing mono behaviors in sample scenes.
* HDRP:BoxLightAdjustment script. some flags are not updated properly when some checkboxes are clicked.
* HDRP:Shader compile errors when used with HDRP 12.1

## [0.4.0-preview] - 2021-10-13
### Added
* HDRP: Compatibility with Box Light, a spot light variation, as main lights.
* HDRP: BoxLightAdjustment mono-behavior

### Fixed:
* URP: Unstable SRP Compatibility. Thanks to tangx246.
* URP: Outline pass is not compatible with VR when  Single Pass Instanced rendering is chosen. Thanks to tangx246 again.
* URP: Some warnings.

### Updated:
* Documentation~/en/FeatureModel_en.md. Thanks to riina.
* Platforms in documentation.

## [0.3.2-preview] - 2021-09-28
### Added
* doc: HDRP: description for ray-trace shadow.

### Fixed
* HDRP: fix Gaps between self-shadow and ray-trace shadow.

## [0.3.1-preview] - 2021-09-27
* description is above.

## [0.3.0-preview] - 2021-09-16
* HDRP: Fixed weird steps when more than 3 point lights are in a scene.
* HDRP: Added Toon EV Adjustment per Model.
* HDRP: Compatible with Raytraced Hardshadow when DX12 is chosen as API.
* Legacy: Applied a fix for outline flicker in VR chat.

## [0.2.2-preview] - 2021-08-24
* Modified Toon EV Adjustment Curve inspector.
* Excluded unnecessary files from release zip.
* Improved folder structure in order not to cause long file name errors when included in other packages.
* Added documentation for Toon EV Adjustment Curve.

## [0.2.1-preview] - 2021-08-18
* HDRP: Fixed: Multiple instances of Toon EV adjustment curve can exist in one scene.

## [0.2.0-preview] - 2021-08-17
* HDRP: Toon EV adjustment curve as a Mono-behavior.
* Legacy, Universal and HDRP: Some texture samplers,such as  _MainTex,  _NormalMap, _1st_ShadeMap and  _2nd_ShadeMap, are shared by 1 sampler, sampler_MainTex, in order to avoid sampler number exceeding errors.
* Integrated Textures feature is removed from all the render pipeline versions instead of above.
* HDRP and URP are compatible with SRP Batcher.
* Legacy, Universal and HDRP: Added image comparison tests for Windows Vulkan API.
* HDRP: Added light probe compatibility.
* Added a material converter from Unity-chan Toon Shader newer than 2.0.7  to Unity Toon Shader. (Experimental)

## [0.1.0-preview] - 2021-07-07
* HDRP: Emission started to work.
* HDRP: Improved exposure.
* HDRP: Implemented tessellation to the outline pass.
* deleted ValidationConfig.json.

## [0.0.7-preview] - 2021-06-14
* Made samples installable.

## [0.0.6-preview] - 2021-06-04
* Modified documents

## [0.0.5-preview] - 2021-05-12
* Channel Mask rendering feature put the results into alpha channel.

## [0.0.4-preview] - 2021-05-09
* Universal RP shaders are compatible with SRP 12.

## [0.0.3-preview] - 2021-05-05
* Universal RP and HDRP shaders are compatible with SRP 10.
* HDRP shaders' DepthOnly path is replaced to DepthForwardOnly path.
* Internal tests are compatible with not only 2019.4 but also 2020.x now.

## [0.0.2-preview] - 2021-03-30
* Legacy shaders are integrated into two shaders.
* The number of textures is reduced experimental
* Added Clipping Matte feature for HDRP.
* Removed almost all the warnings when compiling shaders.
* Grafted graphics tests from Scriptable Render Pipelines.
* Reorganized documents.
* Reorganized repository folder structure.
* Added feature model list FeatureModel_en.md

## [0.0.1-preview] - 2021-03-11

* Reorganized Unity-chan/Universal Toon Shader as Unity Toon shader.

