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

        
        static GameObject s_UTSController;
        static CommandBuffer s_CommandBuffer;
        static RenderTexture s_RenderTexture;   // Debug.
        static Material s_MaterialCombine3_1;
        static Material s_MaterialCombine4;
 
        internal static eSynthesizerMode SynthesizerMode
        {
            get; private set;
        }

        internal static RenderTexture RenderTexture
        {
            get
            {
                return s_RenderTexture;
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
            if (outTexture == null || outTexture.width != resoX || outTexture.height != resoY )
            {
                outTexture = new Texture2D(resoX, resoY, TextureFormat.ARGB32, false, false);
            }
            if (s_RenderTexture == null)
            {
                s_RenderTexture = new RenderTexture(resoX, resoY, 24, RenderTextureFormat.ARGB32);
                s_RenderTexture.name = "UTS_TextureSynthesizer RenderTexture";
                s_RenderTexture.Create();
            }
            Material material;
            if (SynthesizerMode == eSynthesizerMode.Combine3_1)
            {
                material = s_MaterialCombine3_1;
                material.SetTexture("Source0", Source0);
                material.SetTexture("Source1", Source1);
            }
            else
            {
                material = s_MaterialCombine4;
                material.SetTexture("Source0", Source0);
                material.SetTexture("Source1", Source1);
                material.SetTexture("Source2", Source2);
                material.SetTexture("Source3", Source3);
            }

 
            s_CommandBuffer.Clear();


            //            s_CommandBuffer.DrawMesh(s_Mesh, Matrix4x4.identity, material , 0);
            s_CommandBuffer.Blit(Source0, s_RenderTexture, material);
            s_CommandBuffer.Blit(Source0, outTexture, material);
            Graphics.ExecuteCommandBuffer(s_CommandBuffer);

        }




    }
}