using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Unity.UnityChanToonShader2.Tests {
    public class ShaderCompileTest
    {
        [Test]
        public void CompileAllToonShaders() {
            string[] guids = AssetDatabase.FindAssets("t:Shader", new[] {"Packages/com.unity.unitychantoonshader2/Runtime/Shader"});
            int numShaders = guids.Length;
            for (int i=0;i<numShaders;++i) {
                Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(AssetDatabase.GUIDToAssetPath(guids[i]));
                Assert.True(shader.isSupported);
                
            }


        }
    }
}
