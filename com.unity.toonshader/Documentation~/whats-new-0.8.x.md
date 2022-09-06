# What's new for Unity Toon Shader 0.8.x-preview

## Three render pipeline integrated shader.

* From 0.8.0-preview on, The **Unity Toon Shader** handles all the render pipelines such as Built-in, URP, and HDRP, with two shaders, **Toon** and **Toon(Tessellation)**. The Unity Toon Shader doesn't include shaders solely for single render pipeline.any more.
0.8.x-preview is compatible with 2020.3, 2021.3, 2022.1 and 2022.2. This version doesn't support 2019.4 anymore as it utilizes Shader Package Requirement feature.
* All the materials created with older than 0.7.x-preview must be converted. 
* The converter is capable of converting from **Unity-chan Toon Shader 2.0.7** and **Universal Toon Shader** materials.

Please, refer to [Unity Toon Shader Material Converter](MaterialConverter.md) for detail.

