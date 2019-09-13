# 【Unity-Chan Toon Shader 2.0 (UTS2) Ver.2.0.7】
---
<img width = "800" src="images/UTS2_TopImage00.png?raw=true">

***Read this document in other languages: [日本語版](ja/README_ja.md)***  

## 【Overview : What is UTS2?】

<img width = "800" src="images/TPK_04.png?raw=true">

**Unity-Chan Toon Shader 2.0 (UTS2)** is a toon shader for images and video that is designed to meet the needs of creators working on cel-shaded 3DCG animations. Unlike other pre-render toon shaders, **all features can be adjusted in real time on Unity, which is the greatest feature of UTS2**.  

[![](https://img.youtube.com/vi/3yajmhc5A08/0.png)](https://www.youtube.com/watch?v=3yajmhc5A08)

<img width = "800" src="images/IllustSample_UTS2.png?raw=true">

UTS2 has great power and makes a wide variety of character designs possible, **from cel-shaded to light novel illustration styles**.  

<img width = "800" src="images/UTS2_TopImage02.gif?raw=true">

UTS2 has the 3 basic layers of **Base Color**, **1st Shade Color**, and **2nd Shade Color**, colors and textures can also accept a wide variety of customization options, such as **High Color**, **Rim Light**, **MatCap** (sphere mapping), and **Emissive** (light emission).  

<img width = "800" src="images/UTS2_TopImage05.png?raw=true">

What colors will you select as **accent colors**? The accent color is the color which is set at the opposite side of light direction.  

In UTS2, you can use **2nd shade color and Ap-RimLight** as accent color. Of course, these accent colors also change dynamically to the light.  

<img width = "480" src="images/UTS2_TopImage03.gif?raw=true">

**The level of gradation (feather) between colors can also be adjusted in Unity in real-time**.  

<img width = "800" src="images/UTS2_TopImage04.png?raw=true">

In animation production, color design is made for each part in each scene unit. It is common to have specialists who make these color designs. UTS2 is suitable for such pipelines.  

In Animation movies, shadows are used not only to represent light directions but also to clarify shapes of characters. It’s not just shadow, but a vital part of character design.  

<img width = "350" src="images/UTS2_TopImage06.gif?raw=true">

For this purpose, UTS2 also has 2 options for creating fixed shadows necessary to the design: the **Position Map**, which assigns a set casting point to each shadow, and the **Shading Grade Map**, which can adjust shadow intensity based on the lighting. The movie above is a sample of the features of **Shading Grade Map and AngelRing**.  

<img width = "800" src="images/UTS2_TopImage07.png?raw=true">

These two images are comparison between **Standard Shader** and **UTS2 v.2.0.7.5** under the same lighting conditions.  

Although there is a difference between Photo-realistic and Non-photo-realistic images, you can understand all surface reflections to real-time lights are seen in the same areas. **It means UTS2 can be used as same as Standard Shader under various lighting conditions.**  

UTS2 is very useful if you want to decorate your game scene with beautiful lightings.

<img width = "500" src="images/VRChatUser00.png?raw=true">

Finally, several techniques have been implemented to beautifully display characters in a variety of lighting environments, thanks to recent feedback from VRChat users.  

## 【Installing Unitychan ToonShader2】
To install this package, follow the instructions in the [Package Manager documentation](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@latest/index.html). 

If you installed this package, UTS2 files are installed into **Unitychan ToonShader2** folder under **Packages** folder in your Unity project.  

To get the sample scenes of UTS2, download whole project from [here](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/archive/package/sample-project.zip).  

The project comes with sample scenes where you can learn various setting examples of UTS2.  


<a name="UsingUnitychanToonShader2"></a>
## 【Using Unitychan ToonShader2】
To learn how to use UTS2, see [Users' Manual](UTS2_Manual_en.md).  
Users' manual is a document with plentiful knowledge of toon style.  
The iteration cycle between reading the manual and using UTS2 actually is the best way to learn the beautiful toon style.  

You can open Users' Manual from the custom inspector **English Manual** button of UTS2, 
see [here](UTS2_Manual_en.md#1-basic-shader-settings-menu-for-uts2).  



-----
## 【Target Environment】
* **Requires Unity 5.6.x or higher**. The operation check from latest Unity 2019.3.x to Unity 2017.4.x LTS has been completed.  
* UTS2 uses **a forward rendering environment**. Using **a linear color space** is recommended. (A gamma color space can also be used, but this tends to strengthen shadow gradiation. For more details, see [Linear or Gamma Workflow](https://docs.unity3d.com/Manual/LinearRendering-LinearOrGammaWorkflow.html).)
* Target Platforms are **Windows, MacOS, iOS, Android, PlayStation4, Xbox One, Nintendo Switch**. Tessellation version is only supported for environments where DX11/DX12 works properly.  
* This Package Manager version is equivalent to UTS2 v.2.0.7.5 in the Original Github repository.  

-----
## 【Target Platforms】
Windows, MacOS, iOS, Android, PlayStation4, Xbox One, Nintendo Switch  

* Tessellation version is only supported for environments where DX11 works properly.  

-----
## 【Known limitations】

* UTS2 runs under legacy rendering pipeline.  

## 【Package contents】

The following table indicates the directory of Unitychan ToonShader2:

|Folder Location|Description|
|---|---|
|`Runtime\Shader`|Contains UTS2 shader files.|
|`Editor`|Contains UTS2 Custom Inspector and other utilities.|

-----
## 【License】

Licensed under the Unity Companion License for 
Unity-dependent projects--see [Unity Companion License](http://www.unity3d.com/legal/licenses/Unity_Companion_License). 

Unless expressly provided otherwise, the Software under this license is made available strictly on an “AS IS” BASIS WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED. Please review the license for details on these and other terms and conditions.

-----
## 【Download whole project】
### [UnityChanToonShaderVer2_Project (Zip)](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/archive/master.zip)  

The project comes with sample scenes where you can learn various setting examples of UTS2.  

-----
## 【Shader Installation】
### [UTS2_ShaderOnly_v2.0.7_Release.unitypackage](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/master/UTS2_ShaderOnly_v2.0.7_Release.unitypackage)  
When installing for the first time, simply drag and drop this package into Unity to begin the installation process.  
When over-writing a previous version, there is no problem with the same process, but if you want to pay close attention, so please take the following precautions:  
1. Back-up all previous projects.  
2. When opening a project in Unity, create a new scene beforehand.  
3. Erase the folder containing previous versions of the toon shader (Assets/Toon/Shader) from within Unity.  
4. Drag and drop this pack into Unity.  

Be sure to check the [manual](UTS2_Manual_en.md) after installation.  
The manual explains how to use UTS2 in detail.  

Please contact us if you have any issues.  

-----
## 【Release History】  
The release history of UTS2 is [here.](HISTORY_en.md)  

-----
## 【Information】  
Latest Version: 2.0.7 Release: Fixed release version 5  
Update: 2019/05/25  
Category: 3D  
File format: zip/unitypackage  

-----
**README.md 2019/06/10**  

