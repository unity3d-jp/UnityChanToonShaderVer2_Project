# Requirements and compatibility

* **Requires Unity 2019.4.21f1 or higher**. 

## Render pipeline compatibility

*  Unity Toon Sahder supports **Legacy**, **Universal RP** and **HDRP**. Please refer to the documentation of each rendering pipeline for supported platforms.

* However, The behavior of the Unity Toon Shader varies slightly depending on the render pipeline. HDRP uses stencil buffers internally, so UTS stencil effects cannot be applied. Please see the [Feature Model documentation](./FeatureModel_en.md) for the different support status of UTS in each render pipeline.
 
* Unity Toon Shader uses **a forward rendering**. Using **a linear color space** is recommended. (A gamma color space can also be used, but this tends to strengthen shadow gradiation. For more details, see [Linear or Gamma Workflow](https://docs.unity3d.com/Manual/LinearRendering-LinearOrGammaWorkflow.html).)

* Due to the pandemic, we are currently unable to test on the consoles, so please bear this in mind.
