# Samples
Sample scenes are installable from the package manager.
Please, make sure that render pipeline you want use installed and set up before installing the samples for each.

<img width = "400" src="images/InstallingSamples.png">

Samples for Universal RP require `UTS2URPPipelineAsset` set in `Project Setting` dialog.

<img width = "400" src="images/URP-Asset.png">

Ones for HDRP require `HDRenderPipelineAsset_UTS` asset as well.

<img width = "400" src="images/HDRP-Asset.png">

The **Color Space** in OtherSettig -> Rendering  must be `Linear`.

<img width = "400" src="images/SelectLinearColorSpace.png">

 `Assets\Samples\Unity Toon Shader\0.7.4-preview\Universal render pipeline` folder contains the following scenes.

* ToonShader.unity            ：Settings for an illustration-style shader.  
* ToonShader_CelLook.unity    ：Settings for a cel-style shader.  
* ToonShader_Emissive.unity    ：Settings for a shader with an emissive .  
* ToonShader_Firefly.unity    ：Multiple real-time point lights.  
* AngelRing\AngelRing.unity：`AngelRing` and `ShadingGradeMap` sample.  
* Baked Normal\Cube_HardEdge.unity：Baked Normal reference.  
* BoxProjection\BoxProjection.unity        ：Lighting a dark room using Box Projection.  
* EmissiveAnimation\EmisssiveAnimation.unity：EmissiveAnimation sample.  
* LightAndShadows\LightAndShadows.unity：Comparison between the PBR shader and Unity Toon Shader.  
* MatCapMask\MatCapMask.unity：MatcapMask sample.  
* Mirror\MirrorTest.unity: Sample scene checking for a mirror object  
* NormalMap\NormalMap.unity    ：Tricks for using the normal map with Unity Toon Shader.  
* PointLightTest\PointLightTest.unity：Sample of  cel-style content with point lights.  
* Sample\Sample.unity        ：Introduction to the basic Unity Toon Shader.  
* ShaderBall\ShaderBall.unity：Unity Toon Shader settings on an example shader ball.  



 
Sample scenes for other render pipelines are  in the following folder.  
* for Legacy (Built-in)：`Assets\Samples\Unity Toon Shader\0.7.4-preview\Legacy render pipeline` folder. 
* for HDRP：`Assets\Samples\Unity Toon Shader\0.7.4-preview\High definition render pipeline` folder.