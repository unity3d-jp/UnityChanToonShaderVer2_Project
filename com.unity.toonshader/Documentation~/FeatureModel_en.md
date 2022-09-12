## Functional Differences in Each Render Pipeline

| Function	|  Legacy 	| URP 	| HDRP 	| Note 	|
|-	|-	|-	|-	|-	|
| 	|  	|  	|  	| as of 0.8.3-preview	|
| ***1. Modes*** 	|  	|  	|  	|  	|
| Standard	| OK 	| OK 	| OK 	|  	|
| With Advanced Control Map 	| OK 	| OK	| OK 	|  	|
| ***2. Shader Settings*** 	|  	|  	|  	|  	|
| Culling  	| OK 	| OK 	| OK 	|  	|
| Stencil  	| OK 	| OK 	| N.A. 	|  	|
| Stencil Value 	| OK 	| OK 	| N.A. 	|  	|
| Clipping 	| OK 	| OK 	| OK	|  	|
| Clipping Mask | OK 	|OK	| OK 	|  	|
| Invert  Clipping Mask 	| OK 	| OK 	| OK 	|  	|
| Use Base Map Alpha as Clipping Mask 	| OK 	|OK	| OK	|  	|
| ***3. Three Color Map and Control Map Settings*** 	|  	|  	|  	|  	|
| Base Map　| OK 	| OK	| OK 	|  	|
| 1st Shading Map	| OK 	| OK 	| OK	|  	|
| 2nd Shading Map	| OK 	| OK 	| OK 	|  	|
| Normal Map	| OK 	| OK 	| OK 	|  	|
| Shadow Control Maps	| OK 	| OK 	| OK 	|  	|
| ***4. Shading Steps and Feather Settings***	|  	|  	|  	|  	|
| Base Color Step	| OK 	| OK 	| OK 	|  	|
| Base Shading Feather| OK 	| OK 	| OK 	|  	|
| Shading Color Step 	| OK 	| OK 	| OK 	|  	|
| Point Light Step Offset	| OK 	| OK 	| OK 	|  	|
| Filter Point Light Highlights 	| OK 	| OK 	| OK 	|  	|
| ***5. Highlight Settings*** 	|  	|  	|  	|  	|
| Highlight Power | OK 	| OK 	| OK 	|  	|
| Specular Mode 	| OK 	| OK 	| OK 	|  	|
| Color Blending Mode 	| OK 	| OK 	| OK 	|  	|
| Highlight Blending on Shadows 	| OK 	| OK 	| OK 	|  	|
| Highlight Mask	| OK 	| OK 	| OK 	|  	|
| Highlight Mask Level	| OK 	| OK 	| OK 	|  	|
| ***6. Rim Light Settings*** 	|  	|  	|  	|  	|
| Rim Light Color	| OK 	| OK 	| OK 	|  	|
| Rim Light Level	| OK 	| OK 	| OK 	|  	|
| Adjust Rim Light Area 	| OK 	| OK 	| OK 	|  	|
| Inverted Light Direction Rim Light 	| OK 	| OK 	| OK 	|  	|
| Rim Light Mask	| OK 	| OK 	| OK 	|  	|
| ***7. Material Capture(MatCap) Settings*** 	|  	|  	|  	|  	|
| MatCap Map 	| OK 	| OK 	| OK 	|  	|
| MatCap Blur Level	| OK 	| OK 	| OK 	|  	|
| Color Blending Mode 	| OK 	| OK 	| OK 	|  	|
| Scale MatCap UV	| OK 	| OK 	| OK 	|  	|
| Rotate MatCap UV	| OK 	| OK 	| OK 	|  	|
| Stabilize Camera Rolling	| OK 	| OK 	| OK 	|  	|
| Normal Map	| OK 	| OK 	| OK 	|  	|
| Rotate Normal Map UV 	| OK 	| OK 	| OK 	|  	|
| MatCap Blending on Shadows 	| OK 	| OK 	| OK 	|  	|
| MatCap Camera Mode	| OK 	| OK 	| OK 	|  	|
| MatCap Mask	| OK 	| OK 	| OK 	|  	|
| MatCap Mask Level 	| OK 	| OK 	| OK 	|  	|
| Invert MatCap Mask	| OK 	| OK 	| OK 	|  	|
| ***8. Emission Settings*** 	|  	|  	|  	|  	|
| Emission Map 	| OK 	| OK 	| OK 	|  	|
| Use the alpha channel of Emissive Map as a Clipping mask 	| OK 	| OK 	| OK 	|  	|
| Emission Map Animation	| OK 	| OK 	| OK 	|  	|
| Base Speed (Time)	| OK 	| OK 	| OK 	|  	|
| Animation Mode	| OK 	| OK 	| OK 	|  	|
| Scroll U/X direction 	| OK 	| OK 	| OK 	|  	|
| Scroll V/Y direction	| OK 	| OK 	| OK 	|  	|
| Rotate around UV center 	| OK 	| OK 	| OK 	|  	|
| Ping-pong moves for base 	| OK 	| OK 	| OK 	|  	|
| Color Shifting with Time 	| OK 	| OK 	| OK 	|  	|
| Color Shifting with View Angle	| OK 	| OK 	| OK 	|  	|
| ***9. Angel Ring Projection Settings*** 	|  	|  	|  	|  	|
| Angel Ring	| OK 	| OK 	| OK 	|  	|
| Offset U/V	| OK 	| OK 	| OK 	|  	|
| Alpha Channel as Clipping Mask	| OK 	| OK 	| OK 	|  	|
| ***10. Scene Light Effectiveness Settings*** 	|  	|  	|  	|  	|
Activate/Deactive LightColor Effectiveness to each color:<br> Base Color,1st Shading Color, 2nd Shading Color, Highlight, Rim Light, Inverted Light Direction Rim Light, MatCap, Angel Ring, or Outline 	| OK 	| OK 	| OK 	|  	|
| ***11. Metaverse Settings*** 	|  	|  	|  	|  	|
| Metaverse Light 	| OK 	| OK 	| N.A. 	|  	|
| Metaverse Light Intensity	| OK 	| OK 	| N.A. 	|  	|
| Metaverse Light Direction	| OK 	| OK 	| N.A. 	|  	|
| ***12. Outline Settings*** 	|  	|  	|  	|  	|
| Outline Mode 	| OK 	| OK 	| OK 	|  	|
| Outline Width 	| OK 	| OK 	| OK 	|  	|
| Outline Color	| OK 	| OK 	| OK 	|  	|
| Blend Base Color to Outline	| OK 	| OK 	| OK 	|  	|
| Offset Outline with Camera Z-axis	| OK 	| OK 	| OK 	|  	|
| Camera Distance for Outline Width 	| OK 	| OK 	| OK 	|  	|
| Outline Color Map	| OK 	| OK 	| OK 	|  	|
| Rotate around UV center 	| OK 	| OK 	| OK 	|  	|
| Ping-pong moves for base 	| OK 	| OK 	| OK 	|  	|
| Color Shifting with Time 	| OK 	| OK 	| OK 	|  	|
| Color Shifting with View Angle	| OK 	| OK 	| OK 	|  	|
| ***13.Tessellation Settings*** 	|  	|  	|  	|  	|
| Tessellation Settings (Built-in) 	| DX11/Vulkan/Metal 	| N.A. 	| N.A. 	|  	|
| Tessellation Settings (HDRP) 	|N.A.  	|N.A.  	| DX11/Vulkan/Metal 	|  	|
| ***14. EV Adjustment*** 	|  	|  	|  	|  	|
| EV Adjustment in high intensity light scenes	 	| N.A	| N.A.	| OK	|  	|
| ***15. Render pipeline built-in ray-traced shadows*** 	|  	|  	|  	|  	|
| DXR shadow supported in render pipelines 	| N.A.	| N.A.	| OK	|  	|
| ***16. Box Light*** 	|  	|  	|  	|  	|
| Substitute for directional light 	| N.A.	| N.A.	| OK	| To get around the limitation that multiple directional lights can't cast shadows at the same time. |




