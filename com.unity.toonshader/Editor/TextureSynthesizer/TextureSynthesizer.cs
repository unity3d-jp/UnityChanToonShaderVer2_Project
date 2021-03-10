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
        static RenderTexture s_RenderTexture;
        static Material s_MaterialCombine3_1;
        static Material s_MaterialCombine4;
 
        internal static eSynthesizerMode SynthesizerMode
        {
            get;set;
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
            get;set;
        }
        internal static Texture Source1
        {
            get; set;
        }
        internal static Texture Source2
        {
            get; set;
        }
        internal static Texture Source3
        {
            get; set;
        }

        static internal void Init()
        {


            if (s_CommandBuffer == null )
            {
                s_CommandBuffer = new CommandBuffer();
                s_CommandBuffer.name = "UTS_TextureSynthesizer Command Buffer";
            }
        }
        internal static void Proc()
        {
            int resoX = 1024;
            int resoY = 1024;

            if (s_MaterialCombine3_1 == null)
            {
                s_MaterialCombine3_1 = new Material(Shader.Find("Hidden/UnityToonShader/Synth3_1")) { hideFlags = HideFlags.HideAndDontSave };
            }
            if (s_MaterialCombine4 == null)
            {
                s_MaterialCombine4 = new Material(Shader.Find("Hidden/UnityToonShader/Synth4")) { hideFlags = HideFlags.HideAndDontSave };
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
            Graphics.ExecuteCommandBuffer(s_CommandBuffer);

        }




    }
}