README_en

Unity-Chan Toon Shader Ver.2.0.4

Unity-Chan Toon Shader is a toon shader for video and images that is designed to meet your needs when creating cel-shaded 3DCG animations.

We have greatly enhanced the performance and features in Unity-Chan Toon Shader Ver. 2.0.

It still has the same rendering capabilities as Ver. 1.0, but now you can give your creations an even more sophisticated look.


【Target Environment】

Requires Unity 5.6.x or later.
This pack was created in Unity 5.6.5p1.

【Use with iOS/OSX METAL】

When using with iOS / OSX METAL, objects may not display correctly when CullMode = OFF (double-sided drawing).
To correct this, place meshes on both sides of the object and set materials to CullMode = Back (back-face culling) / CullMode = Front (front-face culling) on each side.

【License】

Unity-Chan Toon Shader 2.0 is provided under the Unity-Chan License 2.0 terms.

Please refer to the following link for information regarding the Unity-Chan License.
http://unity-chan.com/contents/guideline_en/

【Installation】

When installing for the first time, simply drag and drop this package into Unity to begin the installation process.

When over-writing a previous version, the code will be completely revised, so please take the following precautions:
1. Back-up all previous projects.
2. When opening a project in Unity, create a new scene beforehand.
3. Erase the folder containing previous versions of the toon shader (Assets/Toon/Shader) from within Unity.
4. Drag and drop this pack into Unity. 

We recommend first erasing the previous shader then installing the new shader, to preserve existing links between materials. 

Please contact us if you have any issues. 

【Version】
2018/03/26
UTS_EdgeDetection.unitypackage
This is an edge extraction filter of post effect type.
This package includes three modified filters modified from Unity's Standard Assets,and a newly created Sobel Color Filter.
By using Sobel Color Filter, you can effectively emphasize the toon line edge and give out the color tress-like atmosphere of the cell picture era.
Attach this post effect before the post-effect-stack.

2018/02/09: 2.0.4: Officially set Unity5.6.x and later versions as the target environment. (Unity2018.1 is also supported)
In addition to overall code revisions and bug fixes, the following has been added to this new version. 

● Phong Tessellation support
The supporting code referenced Nora's work https://github.com/Stereoarts/UnityChanToonShaderVer2_Tess
Please be aware that only certain platforms support tessalation and it requires a considereably powerful PC environment.
It is intended for use in video, images, and VR on powerful Windows10/DX11 machines.
The "light version" uses lighter weight variation instead of limiting the directional lighting to 1 source. 

●Base outlines for Position Scaling 
In previous versions, outlines would be broken by the vector inversion formula, now you can cleanly outline models like cubes

●Support for clipping shaders with outlines that don't have an alpha
When paired with textures that have an alpha, the outline polygons will be removed according to the alpha, even when viewed from behind. 

●Outline textures (Outline_Tex)
●HiColor textures (HiColor_Tex)
●Assuming this shader will be used together with PostProcessingStack, Emissive Color (Emissive_Color) and Emissive Textures (Emissive_Tex)
HDR values can be set on the Emissive Color side, allowing the Emissive portion to shine when combined with PostProcessingStack's bloom effect.
※As always, using these new textures is not required. 

【Version Update History】

2017/06/25: 2.0.3: Manual updated, added 【Use with iOS/OSX METAL】.
2017/06/19: 2.0.3: Added Set_HighColorMask and Set_RimLightMask, as a result of these improvements Set_HighColorPosition was removed.
2017/06/09: 2.0.2: Official support for Nintendo Switch and PlayStation 4. Added lightweight version for mobile. Various other improvements
2017/05/20: 2.0.1: Modified the blend methods for TransClipping shaders and added rim light regulation function. 
In addition to the above modifications, added 2 transparent shaders (ToonColor_DoubleShadeWithFeather_Transparent, ToonColor_ShadingGradeMap_Transparent)
2017/05/07: 2.0.0: Initial version 

Latest Version: 2.0.4
Update: 2018.02.09
Category: 3D
File format: zip