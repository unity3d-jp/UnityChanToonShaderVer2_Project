using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

using UnityObject = UnityEngine.Object;

#if UNITY_5_4_OR_NEWER
[ImageEffectAllowedInSceneView]
#endif
[RequireComponent(typeof(Camera)), ExecuteInEditMode]
[AddComponentMenu("Effects/Post Effect Mask", -1)]
public class PostEffectMask : MonoBehaviour, ISerializationCallbackReceiver {

    static class Uniforms {
        internal static readonly int _MainTex       = Shader.PropertyToID("_MainTex");
        internal static readonly int _Color         = Shader.PropertyToID("_Color");
        internal static readonly int _ColorMask     = Shader.PropertyToID("_ColorMask");
        internal static readonly int _CullMode      = Shader.PropertyToID("_CullMode");
        internal static readonly int _DepthComp     = Shader.PropertyToID("_DepthComp");
        internal static readonly int _SrcFactor     = Shader.PropertyToID("_SrcFactor");
        internal static readonly int _DstFactor     = Shader.PropertyToID("_DstFactor");

        internal static readonly int _BlurSize      = Shader.PropertyToID("_BlurSize");
        internal static readonly int _BlurTempRT    = Shader.PropertyToID("_BlurTempRT");
        internal static readonly int _Extrude       = Shader.PropertyToID("_Extrude");
    }

    public enum MaskCaptureMode {
        BeforeOpaqueEffects,
        BeforeEffects
    }
    
    [Range(0, 1)] public float opacity = 0;
    public bool invert;
    [Range(0,20)] public float blur = 0;
    public MaskCaptureMode captureMode;
    public Texture fullScreenTexture;

    [Serializable]
    public class RendererOptions {
        [Range(0, 1)] public float opacity = 1;
        public Vector3 scale = Vector3.one;
        public float extrude;
        [Tooltip("Only alpha channel is used")]
        public Texture texture;

        public bool depthTest = true;
        public CullMode cullMode = CullMode.Off;
    }

    [Header("Global Renderer options")]
    public bool renderersEnabled = true;
    public RendererOptions globalRendererOptions = new RendererOptions();

    // using HashSet to prevent multiple entries of same object
    public HashSet<PostEffectMaskRenderer> maskRenderers = new HashSet<PostEffectMaskRenderer>();

#if UNITY_EDITOR
    // serialized renderer list to make this effect work in scene camera
    [SerializeField, HideInInspector] private List<PostEffectMaskRenderer> serializedRenderers;
#endif

    private Material m_alphaClearMaterial;
    private Material m_alphaBlendMaterial;
    private Material m_alphaBlurMaterial;
    private Material m_alphaWriteMaterial;

    private Camera m_camera;
    private CameraEvent m_attachedEvent;
    private CommandBuffer m_beforePostFX;
    private RenderTexture m_unprocessed;

    private void Awake() {
        m_camera = GetComponent<Camera>();
    }

    private void OnEnable() {
        CreateResources();
        m_attachedEvent = GetCBEvent();
        AddAsFirstCommandBuffer(m_camera, m_attachedEvent, m_beforePostFX);
    }

    private void OnDisable() {
        if (m_beforePostFX != null)
            m_camera.RemoveCommandBuffer(m_attachedEvent, m_beforePostFX);
        DisposeResources();
    }

    private CameraEvent GetCBEvent() {
        return captureMode == MaskCaptureMode.BeforeOpaqueEffects ? CameraEvent.BeforeImageEffectsOpaque : CameraEvent.BeforeImageEffects;
    }

    private void CreateResources() {
        // create the command buffer (commands are added in OnPreRender)
        m_beforePostFX = new CommandBuffer() { name = "PostEffectMask" };

        // create the materials
        var alphaShader = Shader.Find("Unlit/Alpha");
        if (alphaShader == null) {
            enabled = false;
            throw new NullReferenceException("Cannot find Unlit/Alpha shader.");
        }

        m_alphaClearMaterial = new Material(alphaShader) { hideFlags = HideFlags.DontSave };
        // write only to alpha channel
        m_alphaClearMaterial.SetInt(Uniforms._ColorMask, (int)ColorWriteMask.Alpha); 

        m_alphaWriteMaterial = new Material(alphaShader) { hideFlags = HideFlags.DontSave };
        // write only to alpha channel
        m_alphaWriteMaterial.SetInt(Uniforms._ColorMask, (int)ColorWriteMask.Alpha); 
        // normal alpha blending to make the alpha textures work correctly
        m_alphaWriteMaterial.SetInt(Uniforms._SrcFactor, (int)BlendMode.SrcAlpha);
        m_alphaWriteMaterial.SetInt(Uniforms._DstFactor, (int)BlendMode.OneMinusSrcAlpha);

        m_alphaBlendMaterial = new Material(alphaShader) { hideFlags = HideFlags.DontSave }; 
        // blend mode is set in OnRenderImage

        var blurShader = Shader.Find("Hidden/Post FX/AlphaBlur");
        if (blurShader == null) {
            enabled = false;
            throw new NullReferenceException("Cannot find AlphaBlur shader.");
        }

        m_alphaBlurMaterial = new Material(blurShader) { hideFlags = HideFlags.DontSave };
    }

    private void DisposeResources() {
        Destroy(m_alphaClearMaterial);
        Destroy(m_alphaWriteMaterial);
        Destroy(m_alphaBlendMaterial);
        Destroy(m_alphaBlurMaterial);

        if (m_unprocessed != null)
            RenderTexture.ReleaseTemporary(m_unprocessed);
        m_unprocessed = null;

        if (m_beforePostFX != null)
            m_beforePostFX.Dispose();
        m_beforePostFX = null;
    }

    private void OnPreCull() {
        // update and prepare resources
        if (m_unprocessed != null)
            RenderTexture.ReleaseTemporary(m_unprocessed);
        m_unprocessed = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
        m_unprocessed.name = "PostEffectMask_unprocessed";

        m_alphaClearMaterial.SetColor(Uniforms._Color, new Color(1, 1, 1, opacity));

        // set global renderer options to m_alphaWriteMaterial
        m_alphaWriteMaterial.SetColor(Uniforms._Color, new Color(1, 1, 1, globalRendererOptions.opacity));
        m_alphaWriteMaterial.SetFloat(Uniforms._Extrude, globalRendererOptions.extrude);
        m_alphaWriteMaterial.SetTexture(Uniforms._MainTex, globalRendererOptions.texture);

        var depthCompare = globalRendererOptions.depthTest ? CompareFunction.LessEqual : CompareFunction.Always;
        m_alphaWriteMaterial.SetInt(Uniforms._DepthComp, (int)depthCompare);
        m_alphaWriteMaterial.SetInt(Uniforms._CullMode, (int)globalRendererOptions.cullMode);

        // replace the command buffer when necessary
        var evt = GetCBEvent();
        if (evt != m_attachedEvent) {
            m_camera.RemoveCommandBuffer(m_attachedEvent, m_beforePostFX);
            m_attachedEvent = evt;
            AddAsFirstCommandBuffer(m_camera, m_attachedEvent, m_beforePostFX);
        }
    }

    private void OnPreRender() {
        // set the command buffer to copy the unprocessed image and draw the mask before post effects

        // clear old commands
        m_beforePostFX.Clear();

        // clear the alpha channel
        m_beforePostFX.Blit(null, BuiltinRenderTextureType.CurrentActive, m_alphaClearMaterial);

        // draw the mask
        if (fullScreenTexture != null) {
            m_beforePostFX.Blit(fullScreenTexture, BuiltinRenderTextureType.CurrentActive, m_alphaWriteMaterial);
        }
        if (renderersEnabled) {
            foreach (var mr in maskRenderers) {
                if (mr == null || !mr.isActiveAndEnabled) continue;

                switch (mr.mode) {
                    case PostEffectMaskRenderer.Mode.UseMeshes:
                        {
                            var scaleMat = Matrix4x4.Scale(mr.overrideGlobalOptions ? mr.scale : globalRendererOptions.scale);
                            var materialProps = mr.materialProps;
                            foreach (var cm in mr.childMeshes) {
                                var mat = cm.transform.localToWorldMatrix * scaleMat;
                                if (cm.meshFilter != null) {
                                    var mesh = cm.meshFilter.sharedMesh;
                                    if (mesh != null) {
                                        for (int i = 0; i < mesh.subMeshCount; ++i) {
                                            m_beforePostFX.DrawMesh(mesh, mat, m_alphaWriteMaterial, i, -1, materialProps);
                                        }
                                    }
                                }
                                if (cm.skinnedMeshFilter != null) {
                                    var mesh = cm.skinnedMeshFilter.sharedMesh;
                                    if (mesh != null) {
                                        for (int i = 0; i < mesh.subMeshCount; ++i) {
                                            m_beforePostFX.DrawMesh(mesh, mat, m_alphaWriteMaterial, i, -1, materialProps);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case PostEffectMaskRenderer.Mode.UseRenderers:
                        {
                            foreach (var cr in mr.childRenderers) {
                                if (cr.renderer.enabled) {
                                    for (int i = 0; i < cr.submeshCount; ++i) {
                                        m_beforePostFX.DrawRenderer(cr.renderer, m_alphaWriteMaterial, i);
                                    }
                                }
                            }
                        }
                        break;
                    case PostEffectMaskRenderer.Mode.UseCustomMesh:
                        {
                            var mesh = mr.customMesh;
                            if (mesh != null) {
                                var mat = mr.transform.localToWorldMatrix * Matrix4x4.Scale(mr.overrideGlobalOptions ? mr.scale : globalRendererOptions.scale);
                                var materialProps = mr.materialProps;
                                for (int i = 0; i < mesh.subMeshCount; ++i) {
                                    m_beforePostFX.DrawMesh(mesh, mat, m_alphaWriteMaterial, i, -1, materialProps);
                                }
                            }
                        }
                        break;
                }
            }
        }

        // copy to m_unprocessed
        m_beforePostFX.Blit(BuiltinRenderTextureType.CurrentActive, m_unprocessed);

        // apply blur to the mask
        if (blur > 1.0E-6f) {
            m_beforePostFX.GetTemporaryRT(Uniforms._BlurTempRT, m_unprocessed.width, m_unprocessed.height, 0);

            m_alphaBlurMaterial.SetFloat(Uniforms._BlurSize, blur);
            m_beforePostFX.Blit(m_unprocessed, Uniforms._BlurTempRT, m_alphaBlurMaterial, 0); // horizontal blur
            m_beforePostFX.Blit(Uniforms._BlurTempRT, m_unprocessed, m_alphaBlurMaterial, 1); // vertical blur

            m_beforePostFX.ReleaseTemporaryRT(Uniforms._BlurTempRT);
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        // blend processed(source) with the unprocessed using it's alpha channel (the mask)
        var srcFactor = !invert ? BlendMode.DstAlpha : BlendMode.OneMinusDstAlpha;
        var dstFactor = !invert ? BlendMode.OneMinusDstAlpha : BlendMode.DstAlpha;
        m_alphaBlendMaterial.SetInt(Uniforms._SrcFactor, (int)srcFactor);
        m_alphaBlendMaterial.SetInt(Uniforms._DstFactor, (int)dstFactor);
        Graphics.Blit(source, m_unprocessed, m_alphaBlendMaterial);

        // render to destination
        source = m_unprocessed;
        Graphics.Blit(source, destination);

        RenderTexture.ReleaseTemporary(m_unprocessed);
        m_unprocessed = null;
    }

    public void OnBeforeSerialize() {
    #if UNITY_EDITOR
        serializedRenderers = new List<PostEffectMaskRenderer>(maskRenderers);
    #endif
    }

    public void OnAfterDeserialize() {
    #if UNITY_EDITOR
        maskRenderers = new HashSet<PostEffectMaskRenderer>(serializedRenderers);
    #endif
    }

    #region Utility functions
    private static void AddAsFirstCommandBuffer(Camera cam, CameraEvent evt, CommandBuffer buffer) {
        var buffers = cam.GetCommandBuffers(evt);
        cam.RemoveCommandBuffers(evt);
        cam.AddCommandBuffer(evt, buffer);
        foreach (var b in buffers) {
            cam.AddCommandBuffer(evt, b);
        }
    }

    private static new void Destroy(UnityObject obj) {
        if (obj != null) {
#if UNITY_EDITOR
            if (Application.isPlaying)
                UnityObject.Destroy(obj);
            else
                UnityObject.DestroyImmediate(obj);
#else
                UnityObject.Destroy(obj);
#endif
        }
    }
    #endregion
}
