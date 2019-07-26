# About &lt;Unitychan ToonShader2&gt;
**Unitychan ToonShader 2.0.7.5 (UTS2)** is a toon shader for images and video that is designed to meet the needs of creators working on cel-shaded 3DCG animations. Unlike other pre-render toon shaders, all features can be adjusted in real time on Unity, which is the greatest feature of UTS2.  

UTS2 has great power and makes a wide variety of character designs possible, from cel-shaded to light novel illustration styles.  

For more information, see [README.md](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/master/README.md).


# Installing &lt;Unitychan ToonShader2&gt;
To install this package, follow the instructions in the [Package Manager documentation](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@latest/index.html). 

If you installed this package, UTS2 files are installed into **Toon** folder under **Assets** folder in your Unity project.  

To get the sample scenes of UTS2, download whole project from [here](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/archive/master.zip).  
The project comes with sample scenes where you can learn various setting examples of UTS2.  


<a name="UsingUnitychanToonShader2"></a>
# Using &lt;Unitychan ToonShader2&gt;
To learn how to use UTS2, see [Users' Manual](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/master/Manual/UTS2_Manual_en.md). Users' manual is a document with plentiful knowledge of toon style.  

You can open Users' Manual from the custom inspector **English Manual** button of UTS2, see [here](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/master/Manual/UTS2_Manual_en.md#1-basic-shader-settings-menu-for-uts2).  


# Technical details
## Requirements
* Requires Unity 5.6.x or higher. The operation check from latest Unity 2019.3.x to Unity 2017.4.x LTS has been completed.  
* UTS2 uses a forward rendering environment. Using a linear color space is recommended. (A gamma color space can also be used, but this tends to strengthen shadow gradiation. For more details, see [Linear or Gamma Workflow](https://docs.unity3d.com/Manual/LinearRendering-LinearOrGammaWorkflow.html).)
* Target Platforms are **Windows, MacOS, iOS, Android, PlayStation4, Xbox One, Nintendo Switch**. Tessellation version is only supported for environments where DX11/DX12 works properly.  

## Known limitations

* UTS2 runs under legacy rendering pipeline.  


## Package contents

The following table indicates the &lt;describe the breakdown you used here&gt;:

|Folder Location|Description|
|---|---|
|`<Toon>`|Contains UTS2 shader files.|


## Document revision history
The release history of UTS2 is [here](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/master/Manual/HISTORY_en.md).  
