using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
[AddComponentMenu("Effects/Post Effect Mask Renderer", -1)]
public class PostEffectMaskRenderer : MonoBehaviour {

    static class Uniforms {
        internal static readonly int _MainTex       = Shader.PropertyToID("_MainTex");
        internal static readonly int _Color         = Shader.PropertyToID("_Color");
        internal static readonly int _Extrude       = Shader.PropertyToID("_Extrude");
        internal static readonly int _CullMode      = Shader.PropertyToID("_CullMode");
        internal static readonly int _DepthComp     = Shader.PropertyToID("_DepthComp");
    }

    public struct ChildMesh {
        public MeshFilter meshFilter;
        public SkinnedMeshRenderer skinnedMeshFilter;
        public Transform transform;
    }

    public struct ChildRenderer {
        public Renderer renderer;
        public Mesh cachedMesh;

        public int submeshCount { get { return cachedMesh != null ? cachedMesh.subMeshCount : 1; } }
    }

    public enum Mode {
        UseMeshes,
        UseRenderers,
        UseCustomMesh
    }

    [SerializeField] private PostEffectMask m_mask;
    public Mode mode;
    [SerializeField] private bool m_includeChildren = true;

    public Mesh customMesh;
    public readonly List<ChildRenderer> childRenderers = new List<ChildRenderer>();
    public readonly List<ChildMesh> childMeshes = new List<ChildMesh>();

    // TODO: add option to create a unique material for this mask renderer to enable the options below (+depthTest & cullMode) when using renderers.

    [Header("Options (Only when using meshes)")]
    public bool overrideGlobalOptions = true;
    [Range(0, 1)] public float opacity = 1;
    public Vector3 scale = Vector3.one;
    public float extrude;
    [Tooltip("Only alpha channel is used")]
    public Texture texture;

    //public bool depthTest = true;
    //public CullMode cullMode = CullMode.Off;

    private PostEffectMask m_attachedMask;
    private MaterialPropertyBlock m_materialProps;
    private Transform m_transform;


    public PostEffectMask mask {
        get { return m_attachedMask; }
        set {
            m_mask = value;
            if (m_attachedMask != value) {
                if (m_attachedMask != null)
                    m_attachedMask.maskRenderers.Remove(this);
                m_attachedMask = value;
                if (m_attachedMask != null)
                    m_attachedMask.maskRenderers.Add(this);
            }
        }
    }

    public bool includeChildren {
        get { return m_includeChildren; }
        set {
            if (m_includeChildren != value) {
                m_includeChildren = value;
                UpdateChildren();
            }
        }
    }

    public new Transform transform { get { return m_transform == null ? m_transform = base.transform : m_transform; } }

    internal MaterialPropertyBlock materialProps {
        get {
            if (m_materialProps == null) m_materialProps = new MaterialPropertyBlock();

            //if (!overrideGlobalOptions) return null;
            
            m_materialProps.Clear();

            if (overrideGlobalOptions) {
                // when a property is added to the block, the corresponding global renderer option in PostEffectMask is overridden
                m_materialProps.SetColor(Uniforms._Color, new Color(1, 1, 1, opacity));
                m_materialProps.SetFloat(Uniforms._Extrude, extrude);
                if (texture != null)
                    m_materialProps.SetTexture(Uniforms._MainTex, texture);

                // these don't work through the material property block, would have to create a separate material for each object
                //var depthCompare = depthTest ? CompareFunction.LessEqual : CompareFunction.Always;
                //m_materialProps.SetFloat(Uniforms._DepthComp, (float)depthCompare);
                //m_materialProps.SetFloat(Uniforms._CullMode, (float)cullMode);
            }

            return m_materialProps;
        }
    }

    private void Reset() {
        m_mask = FindObjectOfType<PostEffectMask>();
        OnValidate();
    }

    private void Awake() {
        m_transform = GetComponent<Transform>();

        if (m_mask != null) {
            m_attachedMask = m_mask;
            m_attachedMask.maskRenderers.Add(this);
        }
    }

    private void OnEnable() {
        UpdateChildren();
    }

    private void OnDestroy() {
        if (m_attachedMask != null)
            m_attachedMask.maskRenderers.Remove(this);
        m_attachedMask = null;
    }

    private void OnValidate() {
        mask = m_mask;
        UpdateChildren();
    }

    private void OnTransformChildrenChanged() {
        if (mode != Mode.UseCustomMesh)
            UpdateChildren();
    }

    public void UpdateChildren() {
        UpdateChildRenderers();
        UpdateChildMeshes();
    }

    public void UpdateChildRenderers() {
        childRenderers.Clear();
        FindRenderers(childRenderers, transform, m_includeChildren);
    }

    public void UpdateChildMeshes() {
        childMeshes.Clear();
        FindMeshes(childMeshes, transform, m_includeChildren);
    }

    private static void FindRenderers(ICollection<ChildRenderer> renderers, Transform go, bool includeChildren) {
        Renderer r = go.GetComponent<Renderer>();
        if (r != null)
            renderers.Add(new ChildRenderer { renderer = r, cachedMesh = GetMesh(r) });
        if (includeChildren) {
            for (int i = 0; i < go.childCount; ++i) {
                var child = go.GetChild(i);
                if (child.GetComponent<PostEffectMaskRenderer>() == null) // skip child if it has a PostEffectMaskRenderer.
                    FindRenderers(renderers, child, includeChildren);
            }
        }
    }

    private static void FindMeshes(ICollection<ChildMesh> meshes, Transform go, bool includeChildren) {
        ChildMesh mesh;
        if (TryGetMesh(go, out mesh))
            meshes.Add(mesh);
        if (includeChildren) {
            for (int i = 0; i < go.childCount; ++i) {
                var child = go.GetChild(i);
                if (child.GetComponent<PostEffectMaskRenderer>() == null) // skip child if it has a PostEffectMaskRenderer.
                    FindMeshes(meshes, child, includeChildren);
            }
        }
    }

    private static bool TryGetMesh(Transform go, out ChildMesh mesh) {
        mesh = new ChildMesh();

        var mf = go.GetComponent<MeshFilter>();
        if (mf != null)
            mesh.meshFilter = mf;

        var smf = go.GetComponent<SkinnedMeshRenderer>();
        if (smf != null)
            mesh.skinnedMeshFilter = smf;

        if (mesh.meshFilter != null || mesh.skinnedMeshFilter != null) {
            mesh.transform = go;
            return true;
        }
        return false;
    }

    private static Mesh GetMesh(Renderer renderer) {
        if (renderer is MeshRenderer) {
            var mf = renderer.GetComponent<MeshFilter>();
            return mf != null ? mf.sharedMesh : null;
        }
        if (renderer is SkinnedMeshRenderer) {
            return ((SkinnedMeshRenderer)renderer).sharedMesh;
        }
        if (renderer is ParticleSystemRenderer) {
            return ((ParticleSystemRenderer)renderer).mesh;
        }
        return null;
    }

}
