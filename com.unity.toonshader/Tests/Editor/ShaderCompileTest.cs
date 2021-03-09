using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.TestTools;

namespace Unity.Rendering.ToonShader.Tests {
    public class ShaderCompileTest
    {
        
        [Test]
        public void CompileLegacyToonShadersDefault() {
            string[] guids      = AssetDatabase.FindAssets("t:Shader", new[] { LEGACY_SHADERS_PATH});
            int      numShaders = guids.Length;
            Assert.Greater(numShaders,0);
            bool shaderHasError = false;
            for (int i=0;i<numShaders && !shaderHasError;++i) {
                string curAssetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(curAssetPath);
                AssetDatabase.ImportAsset(curAssetPath); //Recompile the shader to make sure there are no compile errors

                Assert.True(shader.isSupported);     
                shaderHasError = ShaderUtil.ShaderHasError(shader);
                Assert.False(shaderHasError);             
                
            }
        }

//---------------------------------------------------------------------------------------------------------------------
        [Test]        
        [UnityPlatform(RuntimePlatform.WindowsEditor)]
        public void CompileLegacyToonShadersWithRTHS() { //RaytracedHardShadow
            string[] guids      = AssetDatabase.FindAssets("t:Shader", new[] {LEGACY_SHADERS_PATH});
            int      numShaders = guids.Length;
            Assert.Greater(numShaders,0);

            List<Material> materials = new List<Material>();

            //Try to compile shader manually
            System.Type t = typeof(ShaderUtil);
            MethodInfo dynMethod = t.GetMethod("OpenCompiledShader", BindingFlags.NonPublic | BindingFlags.Static);
            Assert.NotNull(dynMethod);
            if (null == dynMethod)
                return;

            int defaultMask = (1 << System.Enum.GetNames(typeof(UnityEditor.Rendering.ShaderCompilerPlatform)).Length - 1);
            bool shaderHasError = false;

            for (int i=0;i<numShaders && !shaderHasError;++i) {
                string curAssetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(curAssetPath);
                AssetDatabase.ImportAsset(curAssetPath); //Recompile the shader to make sure there are no compile errors
                Assert.True(shader.isSupported);

                Material mat = new Material(shader);
                mat.EnableKeyword("UTS_USE_RAYTRACING_SHADOW");
                materials.Add(mat);
                const bool INCLUDE_ALL_VARIANTS = false;
                dynMethod.Invoke(null, new object[] { shader, 1, defaultMask, INCLUDE_ALL_VARIANTS});

                shaderHasError = ShaderUtil.ShaderHasError(shader);
                Assert.False(shaderHasError);             

            }

            Shader.WarmupAllShaders();
            int numMaterials = materials.Count;
            for (int i=0;i<numMaterials;++i) {
                Object.DestroyImmediate(materials[i]);
            }

        }
        
//---------------------------------------------------------------------------------------------------------------------

        private const string LEGACY_SHADERS_PATH = "Packages/com.unity.toonshader/Runtime/Legacy/Shaders";

    }
} //end namespace
