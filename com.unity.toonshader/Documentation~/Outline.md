# Outline Settings

Outlines are one of the most important elements that  affect the impression of a cell-animated images. You can specify the thickness of the outline not just numerically, but can also change the thickness in detail using a special map. You can also specify  the color not just numerically but blend outline colors with the character body colors. UTS offers two methods to generate outlines: one is to stretch polygons in the normal direction, and the other is to apply a scale value to the mesh.

<img src="images/InspectorOutlineSettings.png" width="573">
<br/><br/>

* [Outline](#outline)
  * [Outline Mode](#outline-mode)
  * [Outline Width](#outline-width)
  * [Outline Color](#outline-color)
  * [Blend Base Color to Outline](#blend-base-color-to-outline)
  * [Outline Width Map](#outline-width-map)
  * [Offset Outline with Camera Z-axis](#offset-outline-with-camera-z-axis)
  * [Camera Distance for Outline Width](#camera-distance-for-outline-width)
    * [Farthest Distance to vanish](#farthest-distance-to-vanish)
    * [Nearest Distance to draw with Outline Width](#nearest-distance-to-draw-with-outline-width)

  * [Outline Color Map](#outline-color-map)
  * [Baked Normal Map](#baked-normal-map)

## Outline 
A checkbox to enable outline.
| Outline Off | Outline On |
| -- | -- | 
| <img src="images/OutlineOff.png" height="256"> | <img src="images/OutlineOn.png" height="256"> |

### Outline Mode
Specifies how the inverted-outline objects spawn.
| Outline Mode | Description |
| -- | -- | 
| Normal Direction | Stretches polygons in the normal direction. |
| Position Scaling | Applies a scale value to the mesh. |



### Outline Width
Specifies the width of the outline. This value relies on the scale when importing the model to Unity

<img src="images/OutlineWidth.gif" height="256">

### Outline Color
Specifies the color of outline.

<img src="images/OutlineColor.gif" height="256">


### Blend Base Color to Outline
Blends **Base Color** into outline color. 


| Blend Base Color to Outline Off | Blend Base Color to Outline  On |
| -- | -- | 
| <img src="images/BlendBaseColorToOutlineOff.png" height="200"> | <img src="images/BlendBaseColorToOutlineOn.png" height="200"> |

### Outline Width Map
Outline Width Map as gray scale Texture : Texture(linear). UTS provides  meticulous control solution for outline thickness as a texture map. Look at the difference of the outlines around the character's eyes and face.

| Outline Width Map Off | Outline Width Map On |
| -- | -- | 
| <img src="images/OutlineWidthMapOff.png" width="320"> | <img src="images/OutlineWidthMapOn.png" width="320"> |

You will notice that the outlines of the white areas appear thicker in the texture map.

| Outline Width Map applied above | Base Map |
| -- | -- |
| <img src="images/utc_all2_outlinesmpler.png" height="256"> | <img src="images/utc_all2_light.png" height="256"> |



### Offset Outline with Camera Z-axis
Offsets the outline in the depth (Z) direction of the camera. UTS outline is an implementation of either extending polygons in the normal direction or applying a scale value. Sometimes, offsetting the position of the generated polygons in the Z (depth) direction can improve their appearance. 

| Without Z-axis Offset | With Z-axis Offset |
| -- | -- |
| <img src="images/OffsetZ02.png" height="256"> | <img src="images/OffsetZ03.png" height="256"> |

### Camera Distance for Outline Width

#### Farthest Distance to vanish
Specify the furthest distance, where the outline width changes with the distance between the camera and the object. The outline will be zero at this distance.

#### Nearest Distance to draw with Outline Width
Specify the closest distance, where the outline width changes with the distance between the camera and the object. At this distance, the outline will be the max width set by Outline_Width.

### Outline Color Map
Apply a texture as outline color map.

### Baked Normal Map
Normal maps with vertex normals  baked in from other models can be loaded as an addition when setting up normal inversion outlines. 