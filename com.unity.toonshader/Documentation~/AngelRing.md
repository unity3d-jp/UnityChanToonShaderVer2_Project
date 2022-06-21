# Angel Ring Projection Settings

The Angel Ring effect for UTS expresses shine or luster in hair. This effect always appears in a fixed position as seen from the Camera. Angel Ring requires the 2nd UV in the meshes.

<img src="images/AR_Image.png" height="256">
<br/><br/>


<img src="images/InspectorAngelRingSettings.png" width="573">
<br/><br/>


- [Angel Ring Projection](#angel-ring-projection) 
  - [Angel Ring](#angel-ring)
  - [Offset U](#offset-u)
  - [Offset V](#offset-v)
  - [Alpha Channel as Clipping Mask](#alpha-channel-as-clipping-mask)

## Angel Ring Projection
Enable the Angel Ring effect for UTS, which expresses shine or luster in hair.

| Angel Ring Projection Off | Angel Ring Projection On |
| - | - |
| <img src="images/AngelRingProjectionOff.png" height="256"> | <img src="images/AngelRingProjectionOn.png" height="256"> |



### Angel Ring 
Angel Ring : Texture(sRGB) × Color(RGB). Default:Black.
By default, the **Unity Toon Shader** adds the color to the lighting results. Texture alpha channel doesn't affect.
Please refer to [Alpha Channel as Clipping Mask](#alpha-chennel-as-clipping-mask) when alpha clipping is desirable.

| Angel Ring Texture Example | 
| ---- |
|<img src="images/para_height2.png" height="256">|

### Offset U
Adjusts the Angel Ring’s shape in the horizontal direction. The range is from 0.0 to 0.5. The default is 0.
please refer to the image in [Offset V](#offset-v).

### Offset V
Adjusts the Angel Ring’s shape in the vertical direction. The range is from 0.0 to 1.0. The default is 0.3.

<img src="images/AngelRingOffsetUV_1.gif" height="256" >


### Alpha Channel as Clipping Mask

Texture alpha channel is a clipping mask. If disabled, the alpha doesn't affect at all. The color of the **Angel Ring** can directly affects instead of using the additive method.

| Example Texture | Applied the texture with **Alpha Channel as Clipping Mask** |
| - | - |
| <img src="images/ARtexAlpha.png" height="256"> | <img src="images/AlphaChennelAsClippingMask.png" height="256"> |
