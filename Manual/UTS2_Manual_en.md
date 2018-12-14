# Unity-Chan Toon Shader 2.0 v.2.0.5 Manual

### 2018/11/22 Nobuyuki Kobayashi (Unity Technologies Japan)

---

## 【IMPORTANT】Notice for those upgrading to v.2.0.5 from v.2.0.4.3p1

* The BaseMap may be removed from pre-existing materials due to changes to internal parameter names. In such cases, remember to reattach the BaseMap.   

* The HiColor_Power slider’s sensitivity has been adjusted. The HiColor_Power value will need to be readjusted when using Is_SpecularToHighColor=OFF/Is_BlendAddToHiColor=0FF. This is not necessary for Is_SpecularToHighColor=ON.  

* Latest updates and version history can be found here **[README.md](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/master/README.md)**  

---

<img width = "400" src="Images_jpg/CellLook.jpg">

<img width = "800" src="Images_jpg/CRS01.jpg">

<img width = "800" src="Images_jpg/CRS02.jpg">

<img width = "800" src="Images_jpg/CRS03.jpg">

[![](https://img.youtube.com/vi/Pobi_MPaQEc/0.jpg)](https://www.youtube.com/watch?v=Pobi_MPaQEc)

<img width = "800" src="Images_jpg/TPK_04.jpg">

<img width = "800" src="Images_jpg/HiUni01.jpg">

# Introduction to Unity-Chan Toon Shader 2.0 

Unity-Chan Toon Shader (UTS) is a toon shader for images and video that is designed to meet the needs of creators working on cel-shaded 3DCG animations.  

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_10.jpg">

This Toon shader is configured to easily produce all shadows essential to character design, such as those that accentuate the form of various parts of a character model, without having to worry about the position and intensity of light sources. Shadow color settings can also be used  to make it easy for the person responsible for color design to create shadows.   

One of this shader’s particularly powerful features is the ability to adjust shadows using sliders within the shader itself, eliminating the need for multiple light sources.   

<img width = "800" src="Images_jpg/0713-06_01.jpg">

Performance was greatly enhanced in Unity-Chan Toon Shader Ver. 2.0; the same rendering capabilities as Ver. 1.0 were retained while also allowing for an even more sophisticated look.

<img width = "800" src="Images_jpg/SS_SampleScene.jpg">

In addition to the 3 basic layers of **Base Color**, **1st Shade Color**, and **2nd Shade Color**, colors and textures can also accept a wide variety of customization options, such as **High Color**, **Rim Light**, **MatCap** (sphere mapping), and **Emissive** (light emission).  

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_11.jpg">

The level of gradation between colors can also be adjusted in Unity in real-time.   

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_13.jpg">

This shader also has 2 options for creating fixed shadows necessary to the design: the **Position Map**, which assigns a set casting point to each shadow, and the **Shading Grade Map**, which can adjust shadow intensity based on the lighting.  

There are several other convenient tools for things like **how visible eyes and eyebrows are through bangs**, which can further emphasize an anime-style look.   

In short, Unity-Chan Toon Shader 2.0 (UTS2) makes a wide variety of character designs possible, from cel-shaded to light novel illustration styles.   

Of course, it also supports Unity’s system shadow feature.   

<img width = "800" src="Images_jpg/Comp_ST_UTS2.jpg">

Adding post effects allows UTS2 to use non-photorealistic rendering (NPR) to create any picture-like element that can be made with a standard shader that supports physics-based rendering (PBR).  

Several techniques have been implemented to beautifully display characters in a variety of lighting environments, thanks to recent feedback from VRChat users.  

See for yourself by coloring your best character model with Unity-Chan Toon Shader 2.0 (UTS2).  

You may be surprised to see your character looking better than ever before.  

This manual focuses on the newest version of Unity-Chan Toon Shader Ver. 2.0: **UTS2 v.2.0.5 Release**.  

## 【Development Environment】

Unity-Chan Toon Shader Ver. 2.0 is developed based on Unity 5.6.3p1, and verification of compatibility with the latest versions of Unity 2018.x is ongoing.   

(However, as version 5.6.3p1 is in the legacy pipeline, it is not currently compatible with SRP)  

## 【Target Environment】

Unity 5.6.x or later is required. This shader is confirmed to be compatible with Unity 2017.4 15f1 LTS. Unity 2018.1.0f2 and later versions can also be used.   

This package is developed in Unity 5.6.3p1.  

This package uses a forward rendering environment. Using a linear color space is recommended.  

(A gamma color space can also be used, but this tends to strengthen shadow gradiation. For more details, see [Linear or Gamma Workflow]. (https://docs.unity3d.com/ja/current/Manual/LinearRendering-LinearOrGammaWorkflow.html))  

## 【Installation】

1. Extract the project included with Unity-Chan Toon Shader 2.0  and search directly under the folder for the `UTS_ShaderOnly_v(version name).unitypackage` file. 

   The image below uses version `2.0.4.3_Release_p1`.   

<img width = "600" src="Images_jpg/Explorer01.jpg">

2. Install Unity-Chan Toon Shader 2.0 and open a Unity project.  

3. Open the Assets folder in the Unity Project window.   

4. Drag and drop `UTS_ShaderOnly_v(version name).unitypackage` from the OS’ Explorer or Finder window into the the Assets folder in the Unity Project window.  

<img width = "800" src="Images_jpg/DandD01.jpg">

5. When the Import Unity Package window opens, import all files.   

<img width = "600" src="Images_jpg/ImportWindow01.jpg">

6. This will create a Toon folder, where Unity-Chan Toon Shader 2.0 is installed, under Assets.  

<img width = "400" src="Images_jpg/ProjectWindow01.jpg">

7. Create a new material, and if a section called UnityChanToonShader appears in the Shader drop down menu, installation has completed successfully.   

<img width = "400" src="Images_jpg/NewShader01.jpg">

## 【Basic UTS2 Settings】

Consult the following movie for an example of creating a skin-toned material on a shader ball.   

[![](https://img.youtube.com/vi/z7Pr39NW5Dk/0.jpg)](https://www.youtube.com/watch?v=z7Pr39NW5Dk)

For those completely new to UTS2, practicing “making a picture with Step and Feathering using only the Base Color and 1st Shade Color” is recommended, rather than immediately attempting to use all the available features.   

It is important to first become familiar with the basics of UTS2, then gradually add rim lights and a 2nd Shade Color if necessary.   

Feel free to use the following video for reference while practicing.   

[![](https://img.youtube.com/vi/qDAo5gqIezw/0.jpg)](https://www.youtube.com/watch?v=qDAo5gqIezw)

# Using the Different Shaders in UTS2

Opening the shader class (UnityChanToonShader) installed by Unity-Chan Toon Shader 2.0 (UTS2) shows that it contains a variety of shader files.  

At this point, most users would simply close this; instead take a closer look at the various name blocks in the menu. These name blocks (`Toon`, `DoubleShadeWithFeather`, `Clipping`, `StencilMask`, etc.) are actually one of the most basic features of UTS2. This structure ensures that shaders that have the same name block as one shown in the menu will have the same features. 

Now, let's take a look at each name block and their features.     

## ●The Shaders in the UnityChanToonShader Root Folder

<img width = "480" src="Images_jpg/UTS2_Standard.jpg">

UTS2 shaders fall into two main categories.  

* `DoubleShadeWithFeather` : The standard shader for UTS2. Allows for 2 shade colors (Double Shade Colors) and  gradation between colors (Feathering).   

* `ShadingGradeMap`: A more advanced UTS2 shader. In addition to the DoubleShadeWithFeather features, this shader can also hold a special map called a ShadingGradeMap.   

<img width = "800" src="Images_jpg/Comp_UTS2_Shaders.jpg">

Both types have the same basic features, so the same look can be achieved with either type by matching the color (`_Step`) and gradation (`_Feather`) values.    

Choosing which shader to use is a matter of personal taste, but generally `DoubleShadeWithFeather` is more suited to cel styles that need sharp, well defined colors, while `ShadingGradeMap` may be better for illustrated styles where the colors are more blurred together.   

Additionally, having `Toon` at the beginning of the shader name means that the shader **can create outlines using the object inversion formula**.  

Outlines in UTS2 have a lot of customization options: the strength of outlines made with dedicated textures, the level of blending with the Base Color, camera base offset, and more.   

Shader names also have name blocks such as `Clipping` at the end. These indicate the following kinds of features.   

* `Clipping`: Shader contains a clipping mask, a kind of  “texture omitter” capable of things like cut outs, dissolves, etc.   

* `TransClipping`: Also indicates a clipping mask, but takes the mask’s αTransparency into account when omitting a texture. This allows for better omission, but creates a greater load than `Clipping`.  

* `StencilMask`: Uses the stencil buffer to designate how visible certain parts are through others. For example, for an anime style character it may be desirable to always have the eyebrows visible instead of covered by the character’s bangs. This shader must always be used with a `StencilOut` type shader.  

* `StencilOut` : Used together with `StencilMask` type shaders. In the above example, this shader would be set on the “bangs” part to make them transparent so that the “eyebrows” parts are visible.  

<img width = "800" src="Images_jpg/1230-11_10.jpg">

## ●The Shaders in the UnityChanToonShader/NoOutline Folder

<img width = "800" src="Images_jpg/UTS2_NoOutline.jpg">

The shaders in the `NoOutline` folder have `ToonColor` at the front of their names, but here this indicates that the shader **does not have the outline feature**.  

Parts without the outline function will have one less drawing pass, so these shaders are ideal for designs that do not require outlines or when using high accuracy toon line shaders like [PSOFT Pencil+ 4 Line for Unity](https://www.psoft.co.jp/jp/product/pencil/unity/).  

<img width = "600" src="Images_jpg/ToonColor_Transparent.jpg">

Some of the `NoOutline` shaders have the `Transparent` name block at the end of their names.   

These are **special transparency shaders**. They are useful for parts meant to have a “blush” look, or for glass and glass-like objects.   

## ●The Shaders in the UnityChanToonShader/AngelRing Folder

<img width = "800" src="Images_jpg/UTS2_AngelRing.jpg">

The `AngelRing` folder contains shaders with an “angel ring” feature.   

The “angel ring” is a highlight effect, as demonstrated in the image below. They have a fixed position from the camera’s perspective.  

<img width = "800" src="Images_jpg/AR_Image.jpg">

Only UTS2’s hi-spec `ShadingGradeMap` shaders and their variant `ShadingGradeMap_TransClipping` shaders have this “angel ring” feature.   

These shaders are primarily used in “hair” parts, which puts them in a similar category to `StencilOut` shaders, which are omitted by stencils.   

## ●The Shaders in the UnityChanToonShader/Mobile Folder

<img width = "800" src="Images_jpg/UTS2_Mobile.jpg">

Lightweight versions of shaders, intended for Mobile and VR content,  that generally won’t change the look of objects can be found in the  `Mobile` folder.   

The following features are restricted in order to make these shaders lighter for mobile platforms.   

* **Only one real-time directional light** may be used (**multiple lights and real-time point lights are also unsupported**).  

* Point lights are supported by using baked point lights and [light probes](<https://docs.unity3d.com/ja/current/Manual/LightProbes-MovingObjects.html>). This may require certain adjustments to `GI_Intensity`.   

Mobile shader properties are compatible with normal `Toon_DoubleShadeWithFeather` shaders and  `Toon_ShadingGradeMap` shaders, so if the above features are acceptable, mobile shaders can be substituted for their standard versions in order to improve rendering performance.    

The `Mobile/AngelRing` folder contains mobile shaders that support the “angel ring” feature.   

The basic features of each shader are identical to their standard version counterparts.   

## ●The Shaders in the UnityChanToonShader/Tessellation Folder

<img width = "800" src="Images_jpg/UTS2_Tess.jpg">

UTS2 shaders that support DirectX 11 [Phong Tessellation](<https://docs.unity3d.com/ja/current/Manual/SL-SurfaceShaderTessellation.html>) can be found in the `Tessellation` folder.   

Phong Tessellation corrects the position of subdivided surfaces so that they will be more closely aligned with the mesh’s normal vectors. It’s an effective method for smoothing low-poly meshes.   

UTS2 can only use Phong Tessellation with DirectX 11 in a Windows environment.   

The `Tessellation/Light` folder contains lightweight versions of the Tessellation shaders with the same restrictions as the `Mobile` shaders.  

The rest of the folders also contain versions of the previously described shaders, with added support for Phong Tessellation.   

<img width = "800" src="Images_jpg/DX11Tess.jpg">

Adding Phong Tessellation noticeably improves the quality of outlines and other small details such as the lips.   

These shaders are primarily intended for pre-rendered content, however they also have applications in high-end VR character content with requirements such as having characters get very close to the user.   

## ●The shaders in the UnityChanToonShader/Helper Folder

<img width = "800" src="Images_jpg/UTS2_Helper.jpg">

The `Helper` folder contains shaders that are only used to show the outline object.   

The outline object can be applied to a character as a multi-material.   

<img width = "800" src="Images_jpg/OutlineHelper.jpg">

Add outlines by going to the mesh’s `Skinned Mesh Renderer` > `Materials` and increasing `Size` by 1, then register the desired outline material.   

**Warning: The outline will be added on top of the other materials, which will increase the PC burden. Keep this in mind when using these shaders.**  

# Sample Scenes

The following sample scenes can be found by opening a project and opening the \Assets\Sample Scenes folder.  

* BoxProjection.unity        ：Lighting a dark room using Box Projection  

* ToonShader.unity            ：Settings for an illustration-style shader  

* ToonShader_CelLook.unity    ：Settings for a cel-style shader  

* ToonShader_Emissive.unity    ：Settings for a shader with an emissive   

* ToonShader_Firefly.unity    ：Multiple real-time point lights  

* Baked Normal\Cube_HardEdge.unity：Baked Normal reference  

* Sample\Sample.unity        ：Introduction to the basic UTS2 shaders  

* ShaderBall\ShaderBall.unity：UTS2 settings on an example shader ball  

* PointLightTest\PointLightTest.unity：Sample of  cel-style content with point lights  

* SSAO Test\SSAO.unity        ：Test for SSAO in PPS  

* NormalMap\NormalMap.unity    ：Tricks for using the normal map with UTS2  

* LightAndShadows\LightAndShadows.unity：Comparison between the standard shader and UTS2  

* AngelRing\AngelRing.unity：”angel ring” sample  

* MatCapMask\MatCapMask.unity：MatcapMask sample  

Each scene is intended as a reference for the relevant shader and lighting settings.   

They should come in handy as an example when creating your own scenes.   

# Initial Project Settings

In File>Build Settings>Player Settings...   

* Rendering Path⇒`Forward`  

* Color Space⇒`Linear`  

is recommended. 

<img width = "400" src="Images_jpg/0801-12_07.jpg">

# The Various Properties of UTS2 Shaders 

The following table is an explanation of the properties found in each UTS2 shader, in order by feature block.  

For all UTS2 shaders, if the property name is the same, then they also function the same.  

<img width = "400" src="Images_jpg/UI_Toon_DoubleShadeWithFeather.jpg">

---

## 1. Stencil, Culling, and Clipping Properties

<img width = "500" src="Images_jpg/Property_UTS2_01.jpg">

These properties do things like set the stencil buffer reference number, set the culling formula, and decide what kind of mask will be used for each clipping shader.  

**※ Hint: The culling formula applies to every shader, but stencil and clipping settings only apply to shaders which use those settings. **  

| `Property`                     | Function                                                     |
| ------------------------------ | ------------------------------------------------------------ |
| `Stencil No`                   | Used by  `StencilMask`　/　`StencilOut` shaders. Designates a stencil reference number between 0 - 255 (note that in some cases 255 carries a special significance). Matches the number for the cutting material and the material to be cut. |
| `Cull　Mode`                   | Designates which side of a polygon will not be drawn (culling). Available options are: `OFF` (both sides drawn) / `FRONT` (front side culling) / `BACK` (back side culling). `Back` is selected by default. In some cases, selecting `OFF` can cause the normal map and lighting to display strangely. |
| `ClippingMask`                 | Used by `Clipping` / `TransClipping` shaders. Designates the grayscale clipping mask. White indicates “none”. If no settings are chosen, the clipping feature will be inactive. |
| `IsBaseMapAlphaAsClippingMask` | A property only found in `TransClipping` shaders. Checking this property will use the A channel, including the `BaseMap`, as a clipping mask. Designating a `ClippingMask` is not required. |
| `Inverse_Clipping`             | Inverts the clipping mask.                                   |
| `Clipping_Level`               | Designates the strength of the clipping mask.                |
| `Tweak_transparency`           | Used by `TransClipping` shaders. Adjusts the transparency level by treating the clipping mask grayscale level as an α value. |

<img width = "800" src="Images_jpg/0102-06_02.jpg">

`TransClipping` shaders have the same general features as `Clipping` shaders, but can also use the clipping mask grayscale level as an α value.  

These shaders are often used to cut the ends of long, straight hair using a mask which references the alpha, or for things like strands of hair that stick up out of the top of a character’s head.   

Adjust the transparency level using the `Tweak_transparency` slider.   

---

## 2. “The Three Basic Colors (Base Color, 1st Shade Color, 2nd Shade Color)”, Their Settings, and Properties

<img width = "500" src="Images_jpg/Property_UTS2_02.jpg">

This block defines the basic colors used by UTS2: the Base Color, 1st Shade Color, and 2nd Shade Color.   

These colors are arranged **in order from the light source’s direction, Base Color⇒ 1st Shade Color ⇒ 2nd Shade Color**.  

Each color is determined by multiplying each pixel in the texture by each color, then multiplying by the light color.   

**※ Hint: The Shade Colors do not have to be darker than the Base Color, and there’s nothing wrong with making the 2nd Shade Color lighter than the 1st Shade Color. In fact, doing so can give the impression of reflecting light from the environment.**  

**※ Hint: The design of the content determines whether the 2nd Shade Color is used or not. If it is not used, there is also no need to designate one.**  

| `Property`                        | Function                                                     |
| --------------------------------- | ------------------------------------------------------------ |
| `BaseMap`                         | Designates the Base Color texture.                           |
| `BaseColor`                       | The color which is multiplied by the `BaseMap`. If there is no designated texture, this color will be set as the Base Color. |
| `Is_LightColor_Base`              | Applies the light color to the Base Color.                   |
| `1st_ShadeMap`                    | Designates the 1st Shade Color texture.                      |
| `Use BaseMap as 1stShade_Map`     | When set to ON, applies the `1st_ShadeMap` to the texture designated as the `BaseMap`. |
| `1st_ShadeColor`                  | The color which is multiplied by the `1st_ShaderMap`. If there is no designated texture, this color will be used as the 1st Shade Color. |
| `Is_LightColor_1st_Shade`         | Applies the light color to the 1st Shade Color.              |
| `2nd_ShadeMap`                    | Designates the 2nd Shade Color texture.                      |
| `Use 1stShade_Map as 2ndShade_Map | When set to ON, applies the `1st_ShadeMap` texture to the `2nd_ShadeMap`. If `Use BaseMap as 1stShade_Map` is also ON, the `BaseMap` will also be applied to the `2nd_ShadeMap`. |
| `2nd_ShadeColor`                  | The color which is multiplied by the `2nd_ShaderMap`. If there is no designated texture, this color will be used as the 2nd Shade Color. |
| `Is_LightColor_2nd_Shade`         | Applies the light color to the 2nd Shade Color.              |

**※ Hint: Turning off any color’s `Is_LightColor_color name` switch will cause that color’s Light Intensity to be set to 1 with a light color of white, regardless of the strength of other lights in the environment. This switch should generally only be used when there is only one Directional LIght in the environment. **  

---

## 3. “Normal Map” Properties

<img width = "500" src="Images_jpg/Property_UTS2_03.jpg">

This block is where Normal Map settings are performed.   

**The Normal Map is generally used in UTS2 for Shade Color gradation.**  

Using the Normal Map along with standard shading allows for more complex gradation effects.   

| `Property`           | Function                                                     |
| -------------------- | ------------------------------------------------------------ |
| `NormalMap`          | Designates the Normal Map.                                   |
| `Is_NormalMapToBase` | Check when you want the Normal Map to be reflected in the colors. If not checked, the object’s geometry will be reflected. |

<img width = "600" src="Images_jpg/Is_NormalToBase.jpg">

As with the colors, each effect also has a checkbox for allowing influence from the Normal Map.   

If unchecked, the geometry’s vertex normals will be used.   

[![](https://img.youtube.com/vi/Hdyp8f7l0VI/0.jpg)](https://www.youtube.com/watch?v=Hdyp8f7l0VI)

**※ Hint:** The Normal Map can also be used for pseudo-solid effects like bumps. However, the Normal Map will not actually make the surface of the object’s geometry bumpy and instead will cause the lighting to give the appearance of bumps. Therefore it is necessary to **set the Base Color/1st Shade Color/2nd Shade color step so that it is easier for the light to bring out this effect**. [In the example above](<https://twitter.com/nyaa_toraneko/status/1051359237631164417>), the Base Color step is set to 0.8, the Shade Color step is set to 0.5, and a slightly darker Hi-Color is used to emphasize the object as solid.   

---

## 4. The Basic Settings for Cel and Illustration Styles (Step and Feather Intensity)

This block is where the Base Color/1st Shade Color/2nd Shade Color Step and the Feathering Intensity are set. In addition to the real-time directional light settings, these are the most important settings in UTS2. **These settings will determine the basic look of your content**.  

These property settings can be checked repeatedly in real-time in Unity.  

There is no need to render and confirm the effects of every single property change, making it easier to deliberately design and assemble content.  

Adjust the Step and Feather parameters to create totally different looks, without changing the direction of the light source.  

Next, let’s take a look at special maps for designating shadows, like the Position Map and the Shading Grade Map.  

## 4-1. DoubleShadeWithFeather Shaders

There are common properties among DoubleShadeWithFeather shaders, the standard shaders in UST2.  

These shaders can have 2 **Position Maps**, a special feature that allows the 1st and 2nd Shade Colors to be fixed to a model regardless of lighting.  

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

### 【The Basics of Using the Step/Feather Sliders】

[![](https://img.youtube.com/vi/eM3iwE67ICM/0.jpg)](https://www.youtube.com/watch?v=eM3iwE67ICM)

This covers the basics of using the **Step slider, which controls the color levels**, and the **Feather slider, which controls the gradation at the boundaries between colors**  

---

### 【What is the Position Map?】

<img width = "800" src="Images_jpg/0906-18_03.jpg">

The Position Map designates shadows that you want to cast regardless of the lighting.  

It can be added to a material in addition to the lighting, for times when you want a particular shadow to show in each scene or when there are directorial reasons to add shadows.   

**※ Hint: It might be easier to draw those shadows directly using a 3D painter like Substance Painter, etc. **  

### 【How the 1st and 2nd Shade Color Position Maps Interact】

<img width = "800" src="Images_jpg/0102-22_03.jpg">

In order to **display the 2nd Shade Color** independent of lighting, make sure to **fill in the places where the 1st and 2nd Shade Color Position Maps will overlap**.   

This way, even if shadows from other lighting fall on the 2nd Shade Color area, it will continue to show.   

On the other hand, **for areas where the 2nd Shade Color doesn’t show** (areas designated by the 2nd Shade Color Position Map, but not the 1st Shade Color Position Map), the 2nd Shade Color will only show when covered by shadows created by the lighting.   

---

## 4-2. ShadingGradeMap Shaders

These properties are common among UTS2’s high spec ShadingGradeMap shaders. 

Shaders with a **Shading Grade Map** can control the sharpness and intensity of shadows in relation to the lighting.   

These maps allow you to set shadows of any shape and in any place you like, regardless of geometry or vectors.   

Compared to Point Maps, in addition to placing shadows, Shading Grade Maps can also adjust how shadows look depending on the way the light hits them.   

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
| `ShadingGradeMap`                            | Designates the Shading Grade Map as grayscale.               |

---

### 【What is the Shading Grade Map?】

`Toon_DoubleShadeWithFeather.shader` is the basic shader in UTS2, and that shader is based on the shading grade map, which is a shading gray scale map. The `Toon_ShadingGradeMap` shader is further expanded to use the UV points to control shadows.  

<img width = "800" src="Images_jpg/0122-06_04.jpg">

Adding the `Shading Grade Map` to the normal toon shader allows the 1st and/or 2nd Shade Colors to be controlled at the UV point level.   

This map’s fine level of control makes effects like “hiding the wrinkles in clothes **when the light hits them**” possible.   

The black portions of an image are handled by the 2nd Shade Color of the `Shading Grade Map`, and the way shadows fall changes based the gray portions, which depend on the density of the black portions. 

The denser the gray, the more easily shadows fall, so shadows can also fall between two gray areas.  

Applying **a shading map like the Ambient Occlusion map** to the shading grade map makes it easier for shadows to fall depending on the lighting. This is useful for things like creating shadows that follow hair bangs or the concave parts of clothing.   

---

### 【Adjusting the different colors of point lights ：Step_Offset、PointLights HiCut_Filter】

[![](https://img.youtube.com/vi/fJX8uQKzWhc/0.jpg)](https://www.youtube.com/watch?v=fJX8uQKzWhc)

In UTS2 v.2.0.5 we have improved the reaction of  Real-time Point Light in cel-styles that doesn’t use  feathering.  As a result, we can create a cel-shaded look with just point lighting. 

This is done by adjusting the Step slider of the Base Color /1st Shade Color, 1st Shade Color / 2nd Shade Color. With point lighting, the changes in shadows are more obvious when moving, compared to directional lighting. 

To make it less obvious, use “Step_Offset” to make finer adjustments. 

[![](https://img.youtube.com/vi/WkJId-e2TKk/0.jpg)](https://www.youtube.com/watch?v=WkJId-e2TKk)

By using “Step_Offset”, you can adjust the Realtime light steps (the level of gradation) like point lighting that will be added to the ForwardAdd path. 

The adjustments on “BaseColor_Step” will determine the main light’s gradation but you can also use it to adjust the point lighting settings. 

By using “Step_Offset” at the same time, you can adjust the finer details of point lighting. In particular, this is useful when expressing highlights for machine-related art.   

The brightness of the lighting depends on how close an object is, so the highlight might be too bright in some cases, especially for the base color (bright color). 

When this happens, you can turn on “Point Lights HiCut_Filter” to make the highlights dimmer, and make it blend in more with the cel-look. 

If you want the highlights to be brighter, turn off “PointLights HiCut_Filter”. 

---

## 5. Properties of High color (Highlights, Specular Lighting) 

<img width = "500" src="Images_jpg/Property_UTS2_05.jpg">

**High Color** is also known as **Highlights or Speculum Lighting**. 

It is used to reflect the main directional light. When the light moves the reflection also moves accordingly. In UTS2, you can adjust the high color rendering.  

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

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_rev_16.jpg">

By using HighColorMask, you can dim light (on skin etc.) that reflects too much depending on the angle. 

This is useful when highlighting skin color on the cheeks and chest. 

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_31.jpg">

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_32.jpg">

You can also use HighColorMask as a specular lighting map for reflective surfaces. So it can also be use for metallic textures as well. 

As seen in Akatsuki Yuki’s (@AkatsukiWorks) work, by using HighColorMask and RimLightMask, you can create art that looks like an illustration but also render the texture of each material. 

---

## 6.Properties of RimLight

<img width = "500" src="Images_jpg/Property_UTS2_06.jpg">

In realistic styles, **RimLight**  is a technique in which light is set to shine on the rims of the object. 

In non-photorealistic styles that includes Toon Shader, highlights are also placed on the edges of objects to make it more visible, and it is also called RimLight.  

You can use these RimLight options in UTS2. 

| Property                         | Function                                                     |
| -------------------------------- | ------------------------------------------------------------ |
| `RimLight`                       | Turns the ‘RimLight’ on.                                     |
| `RimLightColor`                  | Specifies the RimLight’s color.                              |
| `Is_LightColor_RimColor`         | Turns the light color on in relation to the Rimcolor.        |
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

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_14.jpg">

The RimLight is generally shown around the objects edges from the camera’s perspective. In UTS2, you can adjust where the rim light is shown in relation to where the main light is. (‘LightDirection_MaskOn’)

You can also set RimLight in the opposite direction of the light source. You can also render “light reflection” with ‘Add_Antipodean_RimLight’.

Specify the RimLight’s color of the light direction as Black (0,0,0) if you only want the rim light to be shown on the opposite direction of the light source and cut the rim light in the direction of the light source. 

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_15.jpg">

RimLight can also be too bright like HighColor depending on the camera’s angles. 

In UTS2, you can adjust RimLight’s mask to make it dimmer. 

In the image above, the RimLight’s color in the light source’s direction and the light reflection’s direction is changed. The rim light is also masked around the underarms to prevent unnecessary highlights. 

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_33.jpg">

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_34.jpg">

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_35.jpg">

By masking the RimLight, you can also emphasize the metallic textures in comparison to other materials, or adjust the incidental light on clothes to create wrinkles on velvet. 

---

## 7. Properties of MatCap

<img width = "500" src="Images_jpg/Property_UTS2_07.jpg">

In UTS2, you can add Matcap textures in addition to multiplying it. 

MatCap is a camera based sphere map that you can paste on to objects.

It is used for ZBrush texture rendering. 

When you google image search for Matcap, you can find a lot of examples. It was used to create metallic reflections before physics-based shaders were common. You can use Matcap to create all kinds of textures in addition to metallic textures.

**※ HINT: UTS2 v.2.0.5 and later, appropriate adjustments will be made to distortions caused by the camera. (<https://twitter.com/kanihira/status/1061448868221480960>) So the Matcap will not be distorted even when the object is on at the edge of the camera’s perspective. **

| Property                             | Function                                                     |
| ------------------------------------ | ------------------------------------------------------------ |
| `MatCap`                             | Turns MatCap on.                                             |
| `MatCap_Sampler`                     | Set which texture to use for MatCap.                         |
| `MatCapColor`                        | Color that will be added to MatCap_Sampler. If you set a grayscale image on MatCap_Sampler, you can add color to the MatCap with MatCapColor. |
| `Is_LightColor_MatCap`               | Turn the light color on in relations to MatCap.              |
| `Is_BlendAddToMatCap`                | If you turn this on, the MatCap blend will be set to Adding Mode. (It makes it brighter.) If you don’t turn it on it will be blend with Multiplication Mode (It makes it darker.) |
| `Tweak_MatCapUV`                     | You can adjust the MatCap’s range by adjusting the UV of the MatCap_Sampler from the center to a circle. |
| `Rotate_MatCapUV`                    | Rotates the MatCap_Sampler’s UV based on the center.         |
| `Is_NormalMapForMatCap`              | Gives a normal map specifically for MatCap. If you are using MatCap as speculum lighting, you can use this to mask it. |
| `NormalMapForMatCap`                 | Adjust the settings of the normal map especially for the MatCap. |
| `Rotate_NormalMapForMatCapUV`        | Rotates the UV of the MatCap’s normal map based on the center. |
| `Is_UseTweakMatCapOnShadow`          | Turns the Tweak MatCapOnShadow slider on.                    |
| `Tweak MatCapOnShadow`               | Adjusts the power of the Matcap’s range in shadows.          |
| `Set_MatcapMask`                     | By setting a grayscale mask for MatCap, you can adjust how MatCap is shown. The MatcapMask is placed based on the UV coordinates of the mesh that the MatCap will be projected on. Mask with black and unmask with white. |
| `Tweak_MatcapMaskLevel`              | Adjusts the power of the MatcapMask. The default is 0.       |
| `Orthographic Projection for MatCap` | Enable this when the camera projection used in the scene is orthographic. When using perspective camera, turn it off. By doing this, you can correct the distortions caused by the camera.  TIP: By turning this on, the perspective camera behaves like the one in UTS2 v.2.0.4, which means the distortion will not be corrected. |

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_36.jpg">

In the example above, Matcap is used as a similar environment map. 

By using MatCap, you can create light reflections of smooth surfaces while keeping the illustration style.

<img width = "800" src="Images_jpg/MatcapTips.jpg"> 

In the example above, MatCap, NormalMapForMatCap and MatcapMask is used to express the light reflections on hair.

* MatCap_Sampler :  Used for the light circles that is multiplied.

* NormalMapForMatCap : If only MatCap is used, it will be rendered as is, but by repeatedly using NormalMapForMatCap, the crescent-shaped light reflections are created. This is known as Speculum Masking. The normal map used here is not used for bumpy textures.

* MatcapMask : Adjusts the range where MatCap is shown. By setting the gradation mask vertically, and adjusting the Tweak_MatcapMaskLevel slider, you can easily control the range of the MatCap shown. 

<img width = "800" src="Images_jpg/MatcapMask.jpg">

You can also express light cookies like this with MatcapMask. 

---

## 8.Angel Ring Properties

<img width = "500" src="Images_jpg/Property_UTS2_08.jpg">

Angel Ring is a highlight that is always shown in a fixed place from the camera’s perspective. It is used on highlights for hair. Shaders that have the Angel Ring function can be found in the AngelRing folder.

<img width = "500" src="Images_jpg/AngelRing.jpg">

Angel Ring is influenced by the UV2 of the mesh that it will be projected on. So you have to set the UV2 of the DCC tools like Maya, 3ds Max, Blender, etc. beforehand.  

| Property            | Function                                                     |
| ------------------- | ------------------------------------------------------------ |
| `AngelRing`         | Turns Angel Ring on.                                         |
| `AngelRing_Sampler` | Specifies the texture of the Angel Ring.                     |
| `AngelRing_Color`   | Specifies the color that will be multiplied to the Angel Ring. |
| `AR_OffsetU`        | Adjusts the Angel Ring’s shape in the length direction.      |
| `AR_OffsetV`        | Adjusts the Angel Ring’s shape in the cross direction.       |
| `Is_LightColor_AR`  | Allows the light color to affect the Angel Ring.             |
| `ARSampler_AlphaOn` | By turning this on, you can use the α channel that is included in the Angel Ring’s texture as a clipping mask. |

---

### ●Making materials for Angel Ring.

First, set a second UV that will be applied to the Angel Ring function of the hair’s mesh. 

 

The UV for AngelRing is separate from the UV for the hair’s texture and is created orthographically from the character’s front. 

<img width = "800" src="Images_jpg/HairModel.jpg">

**The steps from here, including creating UV2, are done with DCC tools like Maya, 3ds Max, Blender etc.**  

By using the AngelWing’s UV as a guide, draw the texture of the highlights. The highlight’s color will be added to the original color. Register the created texture as AngelRing_Sampler. You can draw the highlight with white and add color in AngelRing_Color too. 

<img width = "800" src="Images_jpg/Hair_UV1.jpg">

By turning ARSampler_AlphaOn on, you can use the α channel that is included in the AngelRing’s texture as a clipping mask, as shown below. 

You can set the color of the AngelRing directly instead of adding it.

<img width = "800" src="Images_jpg/0609-04_13.jpg">

---

## 9. Properties of Emissive

<img width = "500" src="Images_jpg/Property_UTS2_09.jpg">

**Emissive** means that objects emit light. 

**By defining the HDR color for ‘Emissive_Color’, you can create parts that are brighter than the other colors. By using it with post-effects that are attached to the camera, like Bloom (https://docs.unity3d.com/ja/current/Manual/PostProcessing-Bloom.html) in [Post Processing Stack](<https://docs.unity3d.com/ja/current/Manual/PostProcessing-Stack.html>) you can make parts emit light effectively. **

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_17.jpg">

| Property         | Function                                                     |
| ---------------- | ------------------------------------------------------------ |
| `Emissive_Tex`   | Specifies the texture for Emissive. You can also create  a mask texture with grayscale and make it emit light with Emissive_Color. If you do not want it to emit light on top of other parts, set it to Black (RGB: 0,0,0) |
| `Emissive_Color` | Color that will be multiplied to each pixel color in ‘Emissive_Tex’. In most cases, set **[HDR Color](https://docs.unity3d.com/ja/current/Manual/HDRColorPicker.html)** |

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_42.jpg">

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_43.jpg">

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_48.jpg">

This is an example from @einz_zwei, in which emissive parts are used very effectively. By combining color maps and emissive maps, details are added according to the light’s brightness. 

---

## 10.Properties of Outline 

<img width = "500" src="Images_jpg/Property_UTS2_10.jpg">

 

We are using the **inverted outline method for material-based objects** for the outline functions. 

This method, to put it simply, spawns the inverted surface normal slightly bigger than the original object with a shader. 

The object spawned for the object’s outline is drawn with front culling, so when it is overwritten by the original object, the parts that do not fit becomes the outline. 

This method is light and easy to adjust, and it had been used for outlines in games for a long time. 

 

Please be aware that **actual lines are not drawn around the objects**. 

**Reference: You can draw lines around the objects too, but that is mostly used as a post-process (post-effects) outline. ** 

The type of outline used in post-process affects the speed and quality. In games, the inverted-object method and light post-process methods are used to make adjustments. 

 

| Property                  | Function                                                     |
| ------------------------- | ------------------------------------------------------------ |
| `OUTLINE MODE`            | Specifies how the inverted-outline object will be spawned.  You can choose between `NML`（normal inverted method） / `POS`（position scaling method. In most cases, NML is used but if it is a mesh that is only made of hard edges (such as cubes), POS will prevent the outline from being disconnected. It will be good to use  POS for simple shapes and use NML for characters and things that have complicated outlines. |
| `Outline_Width`           | Specifies the width of the outline. **NOTICE: This value relies on the scale when the model was imported to Unity** which means that you have to be careful if the scale is not 1. |
| `Farthest_Distance`       | The width of the outline will change depending on the distance between the camera and the object. Specifies the farthest distance. The farthest distance will be when the outline becomes 0. |
| `Nearest_Distance`        | The width of the outline will change depending on the distance between the camera and the object. Specifies the closest distance. The closest distance will be when the ‘Outline_Width” is the width that was set as the maximum. |
| `Outline_Sampler`         | When you want to see the “start” and “end” of your outlines, or when you only want to outline certain parts, use the outline sampler (texture). The thickest width with white and the least thickest with white. |
| `Outline_Color`           | Specifies the color of the outline.                          |
| `Is_BlendBaseColor`       | Select this when you want to blend the color of the outline with the object’s base color. |
| `Is_OutlineTex`           | Turn it on when you want to paste texture to the inverted-outline object. |
| `OutlineTex`              | Use this when you want the outline to have special textures. By changing the textures, you can give the outlines patterns, or make the outline unique by changing the texture of the inverted object which will be front-face culled. |
| `Offset_Camera_Z`         | Offsets the outlines in direction Z. The outline will be less visible for the spikey parts in spikey hair if you input a positive value. For most cases, just set this to 0. |
| `Is_BakedNormal`          | By turning this on, you can turn on `BakedNormal for Outline`. |
| `BakedNormal for Outline` | Reads normal maps that have the vertices normal from other models baked into it as “added” when setting the inverted outlines. For more, look below. |

---

### ●Adjusting the strength of outlines：**Outline_Sampler**

<img width = "800" src="Images_jpg/0906-18_01.jpg">

Black means “no lines” and white means that the width is at 100%. 

**Tips：Tips: When you are using Outline_Sampler for multiple characters, by unifying the UV position of each character’s parts, you can adjust the the start and end of outlines easier. **

---

### ●Supplementing the inverted outlines of objects：**UTS_EdgeDetection**

The inverted object outline that is used in UTS2 is a technique that has been used for a long time but it is still used in games that is real-time sensitive. 

On the other hand, machine power is considerably better today so it is also used with material-based outlines and post-process effects outlines that are attached to the camera. 

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_23.jpg">

UTS2 also has a post-effect called  **UTS_EdgeDetection** that is used with the inverted object outline. By attaching UTS_EdgeDetection to the main camera, UTS2’s inverted object outline looks better. 

<img width = "800" src="Images_jpg/UTS_EdgeDitectionV1.jpg">

UTS_EdgeDetection is provided at UTS2’s project route, **UTS_EdgeDetection.unitypackage**.

You can install this package by drag and dropping it to Unity.

`ToonShader_CelLook.unity` is the sample scene so please check the UTS_EdgeDetection component that is attached to the scene’s main camera.  

### ●UTS_EdgeDetection.unitypackage  

<img width = "400" src="Images_jpg/UTS_EdgeDetectWindow.jpg"> 

This is a post-effect edge extraction filter. In addition to the three filters that were made by modifying Unity’s standard assets (<https://docs.unity3d.com/ja/540/Manual/script-EdgeDetectEffectNormals.html>), **Sobel Color Filter**has been added. By using Sobel Color Filter, you can emphasize toon line edges effectively and reproduce the effects of color tresses of cel drawings. 

---

### ●Tracing baked vertices normals ：**Baked Normal for Outline**   

You can now read normal maps that have vertices normals baked on to them additionally when setting the inverted outlines. By using this you can give hard-edged objects soft-edged outlines according to the baked normal maps. 

When using baked normal maps, set UTS2’s outline setting properties to: 

1. OUTLINE MODE as **"NML"**
2. Is_BakedNormal as   **"ON"**
3. Apply the map that you want to use to  Baked Normal for Outline

Normal maps that can be used as Baked Normal for Outline are as follows: 

1. Does not overlap with the UV of the object that it will be applied to. This means that it is **essential that UV expansion and all normals maps will not overlap**.
2. Normal maps are based on OpenGL, as Unity is. 
3. The texture settings of normal maps are as follows:

​    Set Texture Type to  **"Default"** . **Note: Do not set this to “Normal Map”**

​    Make sure that sRGB (Color Texture) is turned **"OFF"** 

For more please check the assets in the Baked Normal folder of the sample project. 

**Note: This kind of vertex normal adjusting is done by the vertex shader, so it will depend on the  number of vertices that it will be applied to.** This means that it does not correct the vertex normals like pixel shader, so please be careful. 

---

### ●Moving the outline away from the camera：**Offset_Camera_Z**

<img width = "800" src="Images_jpg/0205-11_01.jpg">

By inputting a value in `Offset_Camera_Z` , you can offset the outline in Z direction. You can use this when your model has spikey hair to adjust the outlines of the spikes. In normal situations, please set this to 0. 

---

## 11.  Using Light Probes, and the properties of functions that are useful for Shader Built-in Light and VRChat. 

<img width = "500" src="Images_jpg/Property_UTS2_11.jpg">

| `Property`                                     | Function                                                     |
| ---------------------------------------------- | ------------------------------------------------------------ |
| `GI_Intensity`                                 | By setting `GI_Intensity` to 0 or higher, it will deal with the GI system within Unity’s Lighting window, especially [Light Probe](https://docs.unity3d.com/ja/current/Manual/LightProbes.html).  When `GI_Intensity` is 1, the GI intensity will be 100% but in Toon Shader, this is too high. The default is 0, and although it depends on your tastes, **when you are using light probes, 0.3 is suitable**. |
| `Unlit_Intensity`                              | When there is no real-time directional light in the scene, the scene’s brightness and color will be determined by[Environment Lighting’s Source Settings ](<https://docs.unity3d.com/ja/current/Manual/GlobalIllumination.html>),boost it by `Unlit_Intensity` and use it as a light source. (This is called **Ambient Blending**) The default is 1 and 0 is to turn it off completely. This is used when you want to have the environment color blend with the material color, **but if you want it to be a darker blend, set it to 0.5~1 and if you want it to be a lighter blend, set it to 1.5~2**. |
| `VRChat : SceneLights Hi-Cut_Filter`           | This will minimize overexposure when the light intensity is too high, or when there are multiple real-time directional lights, or multiple real-time point lights. By turning this one you can maintain the light colors and its attenuations while only cutting the intensity of the material color to avoid overexposure. The default is `OFF`. **We recommend VRChat users to turn this on**. Hint: If overexposure still occurs even when this is turned on, please check the post-effect bloom settings. (In particular, when Bloom’s threshold value is under 1, it is easier to happen.) |
| `Advanced : Activate Built-in Light Direction` | For experienced users, you can activate the Built-in Light Direction’s vector (the vector of the virtual lights in the shader). When this is activated, the intensity and color of the light will follow the real-time directional light’s values within the scene. If there aren’t lights like that, the values for ambient blending will be used. |
| ` Offset X-Axis (Built-in Light Direction)`    | Moves the virtual lights left and right that are spawned by the built-in light direction vector left and right . |
| ` Offset Y-Axis (Built-in Light Direction)`    | Moves the virtual lights that are spawned by the built-in light direction vector up and down. |
| ` Inverse Z-Axis (Built-in Light Direction)`   | Moves the virtual lights that are spawned by the built-in light direction vector back and forwards. |

---

### ●Decide the light probe’s brightness ：GI_Intensity

<img width = "800" src="Images_jpg/GI_Intensity.jpg">

**↑ Left：GI_Intensity = 0、Right：GI_Intensity = 0.3 approx. When the value of GI_Intensity goes up, the light probe’s color will be added to the material color. **  

<img width = "800" src="Images_jpg/LightProbe.jpg">

**↑ This is an example of a baked point light and a light probe that is positioned on a stage. There is no problem for baked lights overlapping in each range. Line light probes from the top to bottom of Unity-chan. **  

By setting  ’GI_Intensity’ to 0 and above, it will be compatible with GI systems like light probes.

Light probes that are baked in a scene with other baked lights will be added to the material color as a complementary color. When is at 1, the color that is baked into light probes is added 100%. When it is set to 0, it is the color of the material color. 

This depends on your tastes but when is high, it looks too bright so **setting it to 0.3~0.4 max** is recommended.    

 

<img width = "800" src="Images_jpg/GI_IntensityOFF.jpg">

**↑ GI_Intensity = 0**  

<img width = "800" src="Images_jpg/GI_IntensityON.jpg">

**↑ GI_Intensity = approx. 0.3**  

---

### ●Adjusting the ambient blending：Unlit_Intensity  

The ambient light settings are now reflected in the light colors. The minimum intensity of the directional light is the setting of the scene`s ambient light.  

In VRChat, the brightness of ambient light can be adjusted with the Unlit_Intensity slider. 

Unlit_Intensity boosts the brightness of the ambient light. 

The default is 1. 

If there is no directional light in the scene, the default light that is included in the shader is used, and the direction is always according to where the camera is. As a result,lighting is always good where the camera is pointed at. This light will function when the ambient light is blended.  

This is a video that explains the Unlit_Intensity function and ambient blending. 

[![](https://img.youtube.com/vi/7-k6m69JQ2g/0.jpg)](https://www.youtube.com/watch?v=7-k6m69JQ2g)

---

### ●Preventing overexposure when there are multiple bright lights in a scene ：SceneLights HiCut_Filter  

**SceneLights HiCut_Filter**is a very useful function for VRChat users. 

This is a video that explains it in detail. It also explains in brief how to set the tone mapper with PPS. 

[![](https://img.youtube.com/vi/FM8TomuNwnI/0.jpg)](https://www.youtube.com/watch?v=FM8TomuNwnI)

---

### ●Adding Built-in Light Direction as an advanced feature 

As an advanced feature for experienced users, you can now set the light direction vector that is built into the shader. Materials that have Built-in Light Direction activated can have their own light direction vector for shading purposes which is independent of the mesh object’s coordinates. This means that it will have the same effect as having an exclusive fixed light.  

The drop shadow of this part will be used for the directional light in the scene so you can also change how shading and drop shadows look. To set the light color of Built-in Light Direction, use the main directional light settings of the scene. 

Please watch this video about how to use Built-in Light Direction. 

[![](https://img.youtube.com/vi/IFAPrbAGfmw/0.jpg)](https://www.youtube.com/watch?v=IFAPrbAGfmw)

---

## 12.Properties of Tessellation 

<img width = "500" src="Images_jpg/Property_UTS2_12.jpg">

You can only use Tessellation on UTS2 with Windows/DX11

| `Property`            | Function                                                     |
| --------------------- | ------------------------------------------------------------ |
| `Tess Edge Length`    | Divides the tessellation according to the camera’s distance. The smaller the value, the smaller the tiles become. The default is 5. |
| `Tess Phong Strengh`  | Adjusts the pulling strength of the surfaces divided by tessellation. The default is 0.5. |
| `TessExtrusionAmount` | Scale the expanded parts due to tessellation. The default is 0. |

We used Nora’s https://github.com/Stereoarts/UnityChanToonShaderVer2_Tess  as reference for the code that deals with this. 

Tessellation is not available on every platform and also requires a good PC environment, so please take this into account. It can be used with Windows10/DX11 machines that have powerful GPUs for visuals and VR. 

The Light version is a version that is lightened by only having one directional light. 

# Using it for visuals（pre-render）

If you are using it pre-render, disable Anti_Aliasing in Image Effects and minimize things that were output with 4K frame capture to the suitable size. It will look better.  (This is practically super sampling)

**Frame Capture** is provided here:

https://github.com/unity3d-jp/FrameCapturer 

You can use what you have output from frame capture in NUKE and AfterEffects.  

You can also use **Alembic Importer/Exporter**  for videos. 

https://github.com/unity3d-jp/AlembicImporter  

# About Licensing 

Unity-chan Toon Shader Ver.2.0 is provided as **UCL2.0（Unity-chan License2.0）**/

For more details on Unity-chan’s license, see here: 

http://unity-chan.com/contents/guideline/  

<img width = "140" src="Images_jpg/Light_Silhouette.jpg">

**HINT:** This is a frequently asked question, but you can redistribute your own 3D models (for both commercial use and non-commercial use) that includes the shader file (.shader) and the included file (.cginc) of UTS2 that is distributed by UCL2.0. You can do this regardless of the 3D model or content (including adult content).

For the convenience of users, we would like to ask you to state things such as “UTS2 v.2.0.5 was used” to make it easier for later versions but other than that, you are not required to state anything. We would also like to ask you not to remove the header that states the  UCL2.0 license in each file. 

**※If you decide to use it for your work ** : When  you have finished your model or content that used UTS2, we would love to hear from you!!! Please tweet at  [Unity Technologies Japan](https://twitter.com/unity_japan) ! We look forward to hearing from you and seeing your amazing work!

# Additional Notes 

Tips on using UTS2

## １．Tips: Minimizing the artifacts that appear at the edge of each color when using system shadow.  

<img width = "800" src="Images_jpg/0105-22_01.jpg">

Here is how to minimize the artifacts that appear at the edges of Unity’s system shadow and custom lighting as shown above. 

### 【１：Changing the linear color space】

<img width = "800" src="Images_jpg/0105-22_012.jpg">

If the current color space is set to gamma color space, change it to **linear color space** . 

The gradation of artifacts is less harsh with linear color space. 

### 【２：Increase mesh density】

<img width = "800" src="Images_jpg/0105-22_05.jpg">

 

If the lighting, shader parameters are all the same, increase **mesh density**.

By doing this, most of the artifacts will disappear. 

### 【３：Make artifacts disappear by adjusting BaseColor_Step/Tweak_SystemShadowsLevel 】

<img width = "800" src="Images_jpg/0105-22_04.jpg">

 

At the shadow’s edges in Image 1, the shadows made by Unity’s system shadow and custom lighting are almost identical. 

This means that even if you untick `Set_SystemShadowsToBase`  at this point, the edges of the shadow will not change its place. 

<img width = "800" src="Images_jpg/0105-22_07.jpg"> 

When it looks like Image 1, artifacts appear if you move the `Base/Shade_Feather` slider to the right. (Image 2) 

This is because **the custom lighting’s shadow is within Unity’s system shadow**. 

<img width = "800" src="Images_jpg/0105-22_08.jpg">

When this happens, **move the `BaseColor_Step`slider to the right to increase custom lighting’s shadow**. 

By doing this, the artifacts will disappear and the edges will become blurry as well. 

By using the `Tweak_SystemShadowsLevel` slider, you can also adjust the system shadow’s level and make the artifacts disappear. 

### 【４：Increasing the bias of the directional light】

<img width = "800" src="Images_jpg/0105-23_02.jpg">

 

You can also increase the bias of the directional light that works as a key light that is directed at the ball. By doing this you can change the position of the system shadow. However, if you increase it too much, the shadow itself might be too far away from the object, so please be careful.

---

## ２．Tips: Minimizing light slips when using multiple real-time point lights

 

If there is somewhere that more than four real-time point light ranges overlap, the light that is on the model that has adapted to UTS2 might flip.

This is because the maximum number of real-time point lights is four for forward rendering in Unity. UTS2 also follows this. 

This video explains how to unflip the lights. You can also use this for standard shaders. 

[![](https://img.youtube.com/vi/G5-alxDO0bs/0.jpg)](https://www.youtube.com/watch?v=G5-alxDO0bs)