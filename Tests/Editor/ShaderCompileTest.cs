using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Unity.UnityChanToonShader2.Tests {
    public class ShaderCompileTest
    {
        
        [Test]
        public void CompileAllToonShaders() {
            m_shaderCompileError = false;
            string[] guids = AssetDatabase.FindAssets("t:Shader", new[] {"Packages/com.unity.unitychantoonshader2/Runtime/Shader"});
            Application.logMessageReceived+= ShaderCompileErrorChecker;
            int numShaders = guids.Length;

            for (int i=0;i<numShaders && !m_shaderCompileError;++i) {
                string curAssetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(curAssetPath);

                AssetDatabase.ImportAsset(curAssetPath); //Recompile the shader to make sure there are no compile errors
                Assert.True(shader.isSupported);             
                
            }

            Application.logMessageReceived-= ShaderCompileErrorChecker;

            Assert.False(m_shaderCompileError);
        }

//---------------------------------------------------------------------------------------------------------------------

         static void ShaderCompileErrorChecker(string message, string stackTrace, LogType logType)
         {
             // if we receive a Debug.LogError we can assume that compilation failed
             if (logType == LogType.Error)
                 m_shaderCompileError  = true;
         }

//---------------------------------------------------------------------------------------------------------------------

         static bool m_shaderCompileError = false;
    }
}
