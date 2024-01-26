# 【Unity-Chan Toon Shader 2.0 (UTS2) Ver.2.0.9】
---
<img width = "800" src="Manual/Images_jpg/UTS2_TopImage00.jpg">

***Read this document in other languages: [日本語版](./README_ja.md)***  


## 2024/01/26: Information
We appreciate your interest in Unity-Chan Toon Shader 2.0.
It's important to note that this package has been succeeded by [Unity Toon Shader](https://docs.unity3d.com/Packages/com.unity.toonshader@0.9/manual/index.html) and is provided as is, without any maintenance or release plan.
Therefore, we are unable to monitor bug reports, accept feature requests, or review pull requests for this package.

However, we understand that users may want to continue using and improving Unity-Chan Toon Shader 2.0, instead of upgrading to Unity Toon Shader.
In that case, we recommend that you fork the repository. This will allow you to make changes and enhancements as you see fit.



## 2022/06/14: 2.0.9 Release: new features added.    
* Changed release environment to Unity 2019.4.31f1, tested with Unity 2020.3.x LTS.  
* Single Pass Instanced rendering (also known as Stereo Instancing), support. See [Unity Manual](https://docs.unity3d.com/2019.4/Documentation/Manual/SinglePassInstancing.html) for supported platforms.  
* Note that the UnityPackages for UTS2 extra image effects has been removed as unsupported from this release.  
* Improved blending of extended outline objects with environmental lighting in environments without real-time directional lighting.  

## 【Overview : What is UTS2?】

<img width = "800" src="Manual/Images_jpg/TPK_04.jpg">

**Unity-Chan Toon Shader 2.0 (UTS2)** is a toon shader for images and video that is designed to meet the needs of creators working on cel-shaded 3DCG animations. Unlike other pre-render toon shaders, **all features can be adjusted in real time on Unity, which is the greatest feature of UTS2**.  

[![](https://img.youtube.com/vi/3yajmhc5A08/0.jpg)](https://www.youtube.com/watch?v=3yajmhc5A08)

<img width = "800" src="Manual/Images_jpg/IllustSample_UTS2.jpg">

UTS2 has great power and makes a wide variety of character designs possible, **from cel-shaded to light novel illustration styles**.  

<img width = "800" src="Manual/Images_jpg/UTS2_TopImage02.gif">

UTS2 has the 3 basic layers of **Base Color**, **1st Shade Color**, and **2nd Shade Color**, colors and textures can also accept a wide variety of customization options, such as **High Color**, **Rim Light**, **MatCap** (sphere mapping), and **Emissive** (light emission).  

<img width = "800" src="Manual/Images_jpg/UTS2_TopImage05.jpg">

What colors will you select as **accent colors**? The accent color is the color which is set at the opposite side of light direction.  

In UTS2, you can use **2nd shade color and Ap-RimLight** as accent color. Of course, these accent colors also change dynamically to the light.  

<img width = "480" src="Manual/Images_jpg/UTS2_TopImage03.gif">

**The level of gradation (feather) between colors can also be adjusted in Unity in real-time**.  

<img width = "800" src="Manual/Images_jpg/UTS2_TopImage04.jpg">

In animation production, color design is made for each part in each scene unit. It is common to have specialists who make these color designs. UTS2 is suitable for such pipelines.  

In Animation movies, shadows are used not only to represent light directions but also to clarify shapes of characters. It’s not just shadow, but a vital part of character design.  

<img width = "350" src="Manual/Images_jpg/UTS2_TopImage06.gif">

For this purpose, UTS2 also has 2 options for creating fixed shadows necessary to the design: the **Position Map**, which assigns a set casting point to each shadow, and the **Shading Grade Map**, which can adjust shadow intensity based on the lighting. The movie above is a sample of the features of **Shading Grade Map and AngelRing**.  

<img width = "800" src="Manual/Images_jpg/UTS2_TopImage07.jpg">

These two images are comparison between **Standard Shader** and **UTS2 v.2.0.7.5** under the same lighting conditions.  

Although there is a difference between Photo-realistic and Non-photo-realistic images, you can understand all surface reflections to real-time lights are seen in the same areas. **It means UTS2 can be used as same as Standard Shader under various lighting conditions.**  

UTS2 is very useful if you want to decorate your game scene with beautiful lightings.

<img width = "500" src="Manual/Images_jpg/VRChatUser00.jpg">

Finally, several techniques have been implemented to beautifully display characters in a variety of lighting environments, thanks to recent feedback from VRChat users.  

-----
## 【Users' Manual】
**[English manual for v.2.0.9](Manual/UTS2_Manual_en.md) is available now.**  

Users' manual is a document with plentiful knowledge of toon style.  
The iteration cycle between reading the manual and using UTS2 actually is the best way to learn the beautiful toon style.  

-----
## 【Target Environment】
* UTS2 shader itself and UTS2 materials are compatible with Unity 5.6.7f1 or later. (Unity 2019.4.31f1 or later is recommended)  
* Unity 2019.4.31f1 or later is required to properly play the sample scenes.  
* Unity 2019.4.31f1 through Unity 2020.3.34f1, Unity 2021.3.3f1, and Unity 2022.1.1f1 have been tested.  
* This package was created with Unity 2019.4.31f1.  

This package uses a forward rendering environment. Using a linear color space is recommended.  
(A gamma color space can also be used, but this tends to strengthen shadow gradiation. For more details, see [Linear or Gamma Workflow](https://docs.unity3d.com/ja/current/Manual/LinearRendering-LinearOrGammaWorkflow.html). )  

-----
## 【Target Platforms】
Windows, MacOS, iOS, Android, PlayStation4, Xbox One, Nintendo Switch  

* Tessellation version is only supported for environments where DX11 works properly.  

-----
## 【License】
Unity-Chan Toon Shader 2.0 is provided under the Unity-Chan License 2.0 terms.  
Please refer to the following link for information regarding the Unity-Chan License.  
https://unity-chan.com/contents/guideline_en/

-----
## 【Download whole project】
### [UnityChanToonShaderVer2_Project (Zip)](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/archive/refs/heads/release/legacy/2.0.zip)  

The project comes with sample scenes where you can learn various setting examples of UTS2.  

-----
## 【Shader Installation】
### [UTS2_ShaderOnly_v2.0.9_Release.unitypackage](./UTS2_ShaderOnly_v2.0.9_Release.unitypackage)  
When installing for the first time, simply drag and drop this package into Unity to begin the installation process.  
When over-writing a previous version, there is no problem with the same process, but if you want to pay close attention, so please take the following precautions:  
1. Back-up all previous projects.  
2. When opening a project in Unity, create a new scene beforehand.  
3. Erase the folder containing previous versions of the toon shader (Assets/Toon/Shader) from within Unity.  
4. Drag and drop this pack into Unity.  

Be sure to check the [manual](Manual/UTS2_Manual_en.md) after installation.  
The manual explains how to use UTS2 in detail.  

Please contact us if you have any issues.  

-----
## 【Release History】  
The release history of UTS2 is [here.](Manual/HISTORY_en.md)  

-----
## 【Information】  
Latest Version: 2.0.9 Release  
Update: 2022/06/14  
Category: 3D  
File format: zip/unitypackage  

-----
**README.md 2022/06/14**  

