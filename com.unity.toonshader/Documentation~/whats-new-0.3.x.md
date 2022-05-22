<a id="RaytracedShadow"></a>
## HDRP Raytrace Shadow 
<img width = "800" src="images/ShadowmapVariation.gif">

Raytrace Shadow of HDRP is now supported in Unity Toon Shader. This feature is only available for HDRP and only when the DirectX 12 API is selected.
For more information on how to set up Raytrace Shadow, please click [here](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@12.0/manual/Ray-Traced-Shadows.html).


<a id="ToonEvAdjustmentCurve"></a>
## "Scene/Model Toon EV Adjustment" Mono behavior 
<img width = "800" src="images/TooEvAdjustmenEfficiency2.png">


This feature is only available for HDRP, which, when combined with post-effects such as the Exposure Volume Profile, is designed to render optically correct images without collapsing even in bright environments as intense as 130,000 lux. However, because the Toon Shader uses a completely different logic to render toons apart from this optical correctness, the automatic correction built into HDRP is not enough to get the picture the artist wants. This feature supports artists' corrections in three ways.

<img width = "380" src="images/sceneToonEvAdjustment.png">

Over the scene, you can use this feature by creating a GameObject to control it by selecting `GameObject/Toon Shader/Scene Toon EV Adjustment` from the Unity Editor menu. Only one `Scene Toon EV Adjustment Mono Behavior` can be placed in a scene.

<img width = "380" src="images/attachModelToonEvAdjustment.png">

Or, you can apply this feature to a certain model by chosing `GameObject/Toon Shader/Attatch Model Toon EV Adjustment` when the model is selected.

`GameObject/Toon Shader/Scene Toon EV Adjustment`

<img width = "380" src="images/ToonEvAdjustmentCurveScript.png">

| `Functions` | Description |
|:-------------------|:-------------------|
| `Ignore Volume Exposure` | Ignore the automatic corrections built into HDRP. If this checkbox is On, lights brighter than 1 will result in more blown whites and a much more exaggerated Bloom. However, this method is suitable if you are using Light Culling or similar to shine independent lights of 1lux or less on your character.| 
| `Light High Cut  Filter` | Clips up to 1 lux of light hitting objects with Toon Shader materials.| 
| `Toon EV adjustment curve` | The correction is done with an editable curve; since it would be impossible for an artist to draw a curve that controls from 0 lux to 130000 lux, EV is used as the brightness unit in this screen. By default, the curve is set to linearly complement the range from -10 EV to -1.32 EV.| 

