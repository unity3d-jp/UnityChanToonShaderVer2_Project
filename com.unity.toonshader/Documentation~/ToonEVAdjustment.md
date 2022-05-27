# Toon EV Adjustment

<img width = "800" src="images/TooEvAdjustmenEfficiency2.png">

**Toon EV Adjustment** is only available for HDRP. When combined with post-effects, such as the Exposure Volume Profile, is designed to render optically correct images without collapsing even in bright environments as intense as 130,000 lux. However, because the **Unity Toon Shader** uses a totally different logic to render toons, the automatic correction built in HDRP is not enough to get the picture the artist wants. **Toon EV Adjustment** supports artists' corrections in two ways.

## Scene Toon EV Adjustment
<img width = "380" src="images/sceneToonEvAdjustment.png">

Over the scene, **Scene Toon EV Adjustment** can control exposure level by applying EV curve for **Unity Toon Shader**. 

### he way to enable **Scene Toon EV Adjustment**
1. `GameObject/Toon Shader/Scene Toon EV Adjustment` from the Unity Editor menu to create  **Scene Toon EV Adjustment Component** component.
2. Put models you want to contorl exposure to its inspector.

Only one **Scene Toon EV Adjustment Component** can be placed in a scene.


## Model Toon EV Adjustment
**Toon EV Adjustment** can be applied to a certain model by chosing `GameObject/Toon Shader/Attatch Model Toon EV Adjustment` when the model is selected.

<img width = "380" src="images/attachModelToonEvAdjustment.png">


## Functions

<img width = "380" src="images/ToonEvAdjustmentCurveScript.png">

| `Functions` | Description |
|:-------------------|:-------------------|
| `Ignore Volume Exposure` | Ignore the automatic corrections built into HDRP. If this checkbox is On, lights brighter than 1 will result in more blown whites and a much more exaggerated Bloom. However, this method is suitable if you are using Light Culling or similar to shine independent lights of 1 lux or less on your character.| 
| `Light High Cut  Filter` | Clips up to 1 lux of light hitting objects with Toon Shader materials.| 
| `Toon EV adjustment curve` | The correction is done with an editable curve; since it would be impossible for an artist to draw a curve that controls from 0 lux to 130000 lux, EV is used as the brightness unit in this screen. By default, the curve is set to linearly complement the range from -10 EV to -1.32 EV.| 
