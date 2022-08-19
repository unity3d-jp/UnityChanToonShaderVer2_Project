
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.TestTools;

namespace Unity.Rendering.ToonShader.Tests {
    internal class ShaderCompileTest
    {
//        [MenuItem("Tests/Unity Toon Shader Compile Test", false, 51)]

        internal static void CompileLegacyToonShadersDefault() {
            string[] guids      = AssetDatabase.FindAssets("t:Shader", new[] { LEGACY_SHADERS_PATH});
            int      numShaders = guids.Length;
            Debug.Assert(numShaders > 0);
            bool shaderHasError = false;
            for (int i=0;i<numShaders && !shaderHasError;++i) {
                string curAssetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(curAssetPath);
                AssetDatabase.ImportAsset(curAssetPath); //Recompile the shader to make sure there are no compile errors

                Debug.Assert(shader.isSupported);     
                shaderHasError = ShaderUtil.ShaderHasError(shader);
                Debug.Assert(shaderHasError == false);
            }
        }



        private const string LEGACY_SHADERS_PATH = "Packages/com.unity.toonshader/Runtime/Integrated/Shaders";

    }
} //end namespace