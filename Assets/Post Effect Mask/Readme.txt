Post Effect Mask

Mask any camera post effects. Draws an alpha mask and uses alpha blending to blend the processed image on top of the unprocessed image.

Add a PostEffectMask -component to a camera after the post effect scripts you want to mask.
Add a PostEffectMaskRenderer -component to the objects you want to include in the mask.


Post Effect Mask parameters:
  - Opacity:                Control the opacity of the processed picture in the mask.
  - Invert:                 Invert the mask.
  - Blur:                   Level of blur applied on the mask.
  - Capture Mode:           At which point in rendering is the unprocessed image captured.
  
    - Before Opaque Effects:
      - Before effects that are applied at CommandBuffer event BeforeImageEffectsOpaque or later (e.g. Ambient Occlusion in the Post Processing Stack)
      - NOTE: this mode will cause all transparent objects to be masked out
    
    - Before Effects:
      - Before effects that are applied at CommandBuffer event BeforeImageEffects or later (e.g. majority of post effects, Motion Blur, Bloom in the Post Processing Stack)
    
  - Full Screen Texture:    Add the alpha channel of a texture to the mask.
  
  - Renderers enabled:      Toggle the renderers attached to this mask.
  - Depth test:             Is depth testing done on objects rendered to this mask.
  - Cull mode:              Hide front, back, of no face of the rendered triangles. (Off means that a quad will be visible from both sides)


Post Effect Mask Renderer parameters: (for individual objects)
  - Mask:               Which mask is this object rendered to.
  - Mode:               Choose what to render to the mask.
  
    - Use Meshes:
      - Find MeshFilters from the gameobject the script is attached to and draw their meshes to the mask.
      
    - Use Renderers:
      - Find Renderers from the gameobject the script is attached to and draw them to the mask.
      - IMPORTANT! "Override Global Options" does not work with this mode!
      
    - Use Custom Mesh:
      - Draw only the mesh set as the "Custom Mesh".
      
  - Include Children:   Find all MeshFilters or Renderers in the transform hierarchy under this gameobject.
  
  
  Options: (These can be set globally for the mask or overridden by individual objects)
  
  - Opacity:            Opacity used when drawing this object to the mask.
  - Scale:              Scale the vertices of this object when drawing to the mask.
  - Extrude:            Move the vertices along their normals by this amount.
  - Texture:            Texture applied to the mesh when drawing. Only the alpha channel is used!
    

Additional notes:
- To fix z-fighting problems, adjust the Scale and/or Extrude parameters.
- If a PostEffectMaskRenderer is in a child of another that has includeChildren, it will override it's parent.
- The children are not updated every frame, you can call PostEffectMaskRenderer.UpdateChildren() or disable and enable the script.
- The effect also works in the Scene View. (enable Image Effects through the Toggle Skybox button)