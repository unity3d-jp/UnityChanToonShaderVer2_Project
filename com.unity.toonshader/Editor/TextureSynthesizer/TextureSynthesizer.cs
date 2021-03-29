using UnityEngine;
using UnityEngine.Rendering;

namespace UnityEditor.Rendering.Toon
{
    internal class UTS_TextureSynthesizer 
    {
        internal enum eSynthesizerMode
        {
            Combine3_1,
            Combine4, 
        };

        const string strS0 = "Source0";
        const string strS1 = "Source1";
        const string strS2 = "Source2";
        const string strS3 = "Source3";
        static GameObject s_UTSController;
        static CommandBuffer s_CommandBuffer;
        static RenderTexture s_DebugRenderTexture;   // Debug.
    
        static Texture2D s_DebugTexture;
        static Material s_MaterialCombine3_1;
        static Material s_MaterialCombine4;

        static RenderTexture s_DummyRenderTexture;   // to test gpu sync.
        static Texture2D s_DummyTexture2D;
        internal static eSynthesizerMode SynthesizerMode
        {
            get; private set;
        }

        internal static Texture2D DebugTexture
        {
            get
            {
                return s_DebugTexture;
            }
        }
        internal static RenderTexture DebugRenderTexture
        {
            get
            {
                return s_DebugRenderTexture;
            }
        }
        internal static Texture Source0
        {
            get; private set;
        }
        internal static Texture Source1
        {
            get; private set;
        }
        internal static Texture Source2
        {
            get; private set;
        }
        internal static Texture Source3
        {
            get; private set;
        }

        internal static void SetMode(eSynthesizerMode mode, Texture s0, Texture s1, Texture s2 = null, Texture s3 = null )
        {
            SynthesizerMode = mode;
            Source0 = s0;
            Source1 = s1;
            Source2 = s2;
            Source3 = s3;
        }
        static internal void Init()
        {


            if (s_CommandBuffer == null )
            {
                s_CommandBuffer = new CommandBuffer();
                s_CommandBuffer.name = "UTS_TextureSynthesizer Command Buffer";
            }
        }

        static void GetTextureSize(out int outResoX, out int outResoY)
        {
            int resoX = 1024;
            int resoY = 1024;
            if (SynthesizerMode == eSynthesizerMode.Combine3_1)
            {
                if ( Source0 != null )
                {
                    resoX = Mathf.Max(resoX, Source0.width);
                    resoY = Mathf.Max(resoY, Source0.height);
                }
                if ( Source1 != null )
                {
                    resoX = Mathf.Max(resoX, Source1.width);
                    resoY = Mathf.Max(resoY, Source1.height);
                }
            }
            else
            {
                if (Source0 != null)
                {
                    resoX = Mathf.Max(resoX, Source0.width);
                    resoY = Mathf.Max(resoY, Source0.height);
                }
                if (Source1 != null)
                {
                    resoX = Mathf.Max(resoX, Source1.width);
                    resoY = Mathf.Max(resoY, Source1.height);
                }
                if (Source2 != null)
                {
                    resoX = Mathf.Max(resoX, Source2.width);
                    resoY = Mathf.Max(resoY, Source2.height);
                }
                if (Source3 != null)
                {
                    resoX = Mathf.Max(resoX, Source3.width);
                    resoY = Mathf.Max(resoY, Source3.height);
                }
            }
            outResoX = resoX;
            outResoY = resoY;
        }
        internal static void Proc(ref Texture outTexture)
        {
            const int kRenderTextureWidth = 2048;
            const int kRenderTetureHeight = 2048;
            int resoX;
            int resoY;
            GetTextureSize(out resoX, out resoY);
            if (s_MaterialCombine3_1 == null)
            {
                s_MaterialCombine3_1 = new Material(Shader.Find("Hidden/UnityToonShader/Synth3_1")) { hideFlags = HideFlags.HideAndDontSave };
            }
            if (s_MaterialCombine4 == null)
            {
                s_MaterialCombine4 = new Material(Shader.Find("Hidden/UnityToonShader/Synth4")) { hideFlags = HideFlags.HideAndDontSave };
            }
            if (outTexture == null || outTexture.width != resoX || outTexture.height != resoY)
            {
                if (outTexture != null)
                {
                    Object.DestroyImmediate(outTexture);
                }
                // outTexture = new Texture2D(resoX, resoY, TextureFormat.ARGB32, false, false);
                // only fixed size can be used.
                outTexture = new Texture2D(kRenderTextureWidth, kRenderTetureHeight, TextureFormat.ARGB32, false, false);
            }
            if (!outTexture.isReadable)
            {
                Debug.LogError("Unable to read Texture:" + outTexture);
            }
            Debug.Assert(outTexture != null);
            Debug.Assert(outTexture.isReadable);
            if (s_DebugRenderTexture == null)
            {
                s_DebugRenderTexture = new RenderTexture(kRenderTextureWidth, kRenderTetureHeight, 24, RenderTextureFormat.ARGB32);
                s_DebugRenderTexture.name = "UTS_TextureSynthesizer RenderTexture";
                s_DebugRenderTexture.Create();
            }

            if (s_DummyRenderTexture == null)
            {
                s_DummyRenderTexture = new RenderTexture(2, 2, 24, RenderTextureFormat.ARGB32);
                s_DummyRenderTexture.name = "Dummy Sync RenderTexture";
                s_DummyRenderTexture.Create();
            }
            if (s_DummyTexture2D == null)
            {
                s_DummyTexture2D = new Texture2D(s_DummyRenderTexture.width, s_DummyRenderTexture.height, TextureFormat.ARGB32, false);
            }
            Material material;


            if (SynthesizerMode == eSynthesizerMode.Combine3_1)
            {
                material = s_MaterialCombine3_1;
                material.SetTexture(strS0, Source0);
                material.SetTexture(strS1, Source1);
                material.SetTexture(strS2, null);
                material.SetTexture(strS3, null);
            }
            else
            {
                material = s_MaterialCombine4;
                material.SetTexture(strS0, Source0);
                material.SetTexture(strS1, Source1);
                material.SetTexture(strS2, Source2);
                material.SetTexture(strS3, Source3);
            }
#if false
//            int tempTextureIdentifier = Shader.PropertyToID("TmpTexture");
            s_CommandBuffer.Clear();

//            s_CommandBuffer.GetTemporaryRT(tempTextureIdentifier, resoX, resoY);

//            s_CommandBuffer.Blit(Source0, tempTextureIdentifier, material);
            s_CommandBuffer.Blit(Source0, s_DebugRenderTexture, material);

            if ( s_DebugRenderTexture.width != outTexture.width )
            {
                Debug.LogErrorFormat("s_DebugRenderTexture.width:{0} != outTexture.width:{1}", s_DebugRenderTexture.width, outTexture.width);
            }
            if (s_DebugRenderTexture.height != outTexture.height)
            {
                Debug.LogErrorFormat("s_DebugRenderTexture.height:{0} != outTexture.height:{1}", s_DebugRenderTexture.height, outTexture.height);
            }

//            s_CommandBuffer.CopyTexture(s_DebugRenderTexture, 0, outTexture,0);
            s_DebugTexture = outTexture as Texture2D;
//            s_CommandBuffer.ReleaseTemporaryRT(tempTextureIdentifier);
            Graphics.ExecuteCommandBuffer(s_CommandBuffer);
#else
            Graphics.Blit(Source0, s_DebugRenderTexture, material);
            Graphics.CopyTexture(s_DebugRenderTexture, outTexture);
#endif
            //            SyncGPU();
        }

        internal static void SyncGPU()
        {
            var store = RenderTexture.active;


            RenderTexture.active = s_DummyRenderTexture;


            s_DummyTexture2D.ReadPixels(new Rect(0, 0, s_DummyRenderTexture.width, s_DummyRenderTexture.height), 0, 0);
            s_DummyTexture2D.Apply();
            RenderTexture.active = store;
        }



    }
}