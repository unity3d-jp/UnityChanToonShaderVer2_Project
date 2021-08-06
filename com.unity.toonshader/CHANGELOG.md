# Changelog
## [0.2.0-preview] - 2021-08-06
* HDRP: Toon EV adjutment curve
* Legacy, Universal and HDRP: Some texture samplers,such as  _MainTex,  _NormalMap, _1st_ShadeMap and  _2nd_ShadeMap, are shared by 1 sampler, sampler_MainTex, in order to avoid sampler number exceeding errors. 
* Integrated Textures feature is removed from all the render pipeline versions instead of above.
* HDRP and URP are compatible with SRP Batcher.
* Legacy, Universal and HDRP: Added image comparison tests with Vulkan API.

## [0.1.0-preview] - 2021-07-07
* HDRP: Emission started to work.
* HDRP: Improved exposure.
* HDRP: Implemented tessellation to the outline pass.
* deleted ValidationConfig.json.

## [0.0.7-preview] - 2021-06-14
* Made samples installable.

## [0.0.6-preview] - 2021-06-04
* Modifed documents

## [0.0.5-preview] - 2021-05-12
* Channel Mask rendering feature put the results into alpha channel.

## [0.0.4-preview] - 2021-05-09
* Uniersal RP shaders are compatible with SRP 12.

## [0.0.3-preview] - 2021-05-05
* Uniersal RP and HDRP shaders are compatible with SRP 10.
* HDRP shaders' DepthOnlyp path is replaced to DepthForwardOnly path.
* Internal tests are compatible with not only 2019.4 but also 2020.x now.

## [0.0.2-preview] - 2021-03-30
* Legacy shaders are integrated into two shaders.
* The number of textures is reduced experimental
* Added Clipping Matte feature for HDRP.
* Removed almost all the warrnings when compiling shaders.
* Grafted graphics tests from Scriptable Render Pipelines.
* Reorganized documents.
* Reorganized repository folder structure.
* Added feature model list FeatureModel_en.md

## [0.0.1-preview] - 2021-03-11

* Reorganized Unitychan/Universal Toon Shader as Unity Toon shader.

