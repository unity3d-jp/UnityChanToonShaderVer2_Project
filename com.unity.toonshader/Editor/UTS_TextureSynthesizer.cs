using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
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
            if (s_RenderTexture == null)
            {
                s_RenderTexture = new RenderTexture(resoX, resoY, 24, RenderTextureFormat.ARGB32);
                s_RenderTexture.name = "UTS_TextureSynthesizer RenderTexture";
                s_RenderTexture.Create();
            }
            Matrix4x4 worldToCameraMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(2f / resoX, 2f / resoY, 1f));
            Matrix4x4 projectionMatrix = Matrix4x4.identity;
            Color col = Color.red;
            s_CommandBuffer.Clear();
            s_CommandBuffer.SetRenderTarget(s_RenderTexture);
            s_CommandBuffer.SetViewProjectionMatrices(worldToCameraMatrix, projectionMatrix);
            s_CommandBuffer.ClearRenderTarget(true, true, col);

            Graphics.ExecuteCommandBuffer(s_CommandBuffer);

        }





    }
}