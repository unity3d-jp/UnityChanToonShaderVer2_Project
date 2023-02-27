using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
namespace UnityEditor.Rendering.Toon
{
    internal sealed class BuiltInUTS2toIntegratedConverter : RenderPipelineConverterContainer
    {
        internal UTS3GUI.CullingMode m_cullingMode;




        public override string name => "Unity-chan Toon Shader 2";
        public override string info => "This tool converts project materials from Unity-chan Toon Shader to Unity Toon Shader " + UTS3GUI.versionString;
        public override int priority => -9000;

        public override void SetupConverter() {



            int materialCount = 0;

            for (int ii = 0; ii < m_materialGuids.Length; ii++)
            {
                var guid = m_materialGuids[ii];

                string path = AssetDatabase.GUIDToAssetPath(guid);
                Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
                if ( material == null )
                {
                    continue; // the material is deleted.
                }
                var shaderName = material.shader.ToString();
#if false
                if (!shaderName.StartsWith("Hidden/InternalErrorShader"))
                {
                    continue;
                }
#endif
                string content = File.ReadAllText(path);
                string[] lines = content.Split(lineSeparators, StringSplitOptions.None);
                // always two spaces before m_Shader?
                var targetLine = Array.Find<string>(lines, line => line.StartsWith(kShaderKeywordInMatrial));
                if (targetLine == null)
                {
                    continue; // todo. prefab?
                }
                var shaderMetadata = targetLine.Split(targetSepeartors, StringSplitOptions.None);
                if (shaderMetadata.Length < 4)
                {
                    Error(path);
                    continue;
                }
                var shaderGUID = shaderMetadata[4];
                while (shaderGUID.StartsWith(" "))
                {
                    shaderGUID = shaderGUID.TrimStart(' ');
                }
                var foundUTS2GUID = FindUTS2GUID(shaderGUID);
                if (foundUTS2GUID == null)
                {
                    continue;
                }

                var targetLine2 = Array.Find<string>(lines, line => line.StartsWith("    - _utsVersion"));
                if (targetLine2 == null)
                {
                    Error(path);
                    continue;
                }
                string[] lines2 = targetLine2.Split(targetSepeartors2, StringSplitOptions.None);
                if (lines2 == null || lines2.Length < 2)
                {
                    Error(path);
                    m_versionErrorCount++;
                    continue;
                }
                var utsVersionString = lines2[1];
                while (utsVersionString.StartsWith(" "))
                {
                    utsVersionString = utsVersionString.TrimStart(' ');
                }
                float utsVersion = float.Parse(utsVersionString);
                if (utsVersion < 2.07f)
                {
                    m_versionErrorCount++;
                    continue;
                }
                m_ConvertingMaterialGuids.Add(guid);

                if (!m_Material2GUID_Dictionary.ContainsKey(material))
                {
                    m_Material2GUID_Dictionary.Add(material, shaderGUID);
                }
                if (!m_GuidToUTSID_Dictionary.ContainsKey(shaderGUID))
                {
                    m_GuidToUTSID_Dictionary.Add(shaderGUID, foundUTS2GUID);
                }
                materialCount++;

                AddMaterialToScrollview(material);
            }

        }
        public override void Convert() 
        {
            ConvertBuiltInUTS2Materials(m_materialGuids);
            SendAnalyticsEvent();
        }
        public override void PostConverting() { }


        const string legacyShaderPrefix = "UnityChanToonShader/";

        bool CheckUTS2VersionError()
        {
            
            int materialCount = 0;

            for (int ii = 0; ii < m_materialGuids.Length; ii++)
            {
                var guid = m_materialGuids[ii];


                string path = AssetDatabase.GUIDToAssetPath(guid);
                Material material = AssetDatabase.LoadAssetAtPath<Material>(path);

                var shaderName = material.shader.ToString();
                if (!shaderName.StartsWith(legacyShaderPrefix))
                {
                    continue;

                }

                if (material.HasProperty(utsVersionProp))
                {
                    float utsVersion = material.GetFloat(utsVersionProp);
                    if (utsVersion < 2.07)
                    {
                        m_versionErrorCount++;
                        continue;
                    }
                }
                else
                {
                    m_versionErrorCount++;
                    continue;
                }
                materialCount++;
            }
            m_materialCount = materialCount;
            if (m_versionErrorCount > 0)
            {
                return true;
            }
            return false;
        }

        bool CheckUTS2isInstalled()
        {
            var shaders = AssetDatabase.FindAssets("t:Shader", new string[] { "Assets" });
            foreach (var guid in shaders)
            {
                foreach (var shader in UTS2ShaderInfo.stdShaders)
                {
                    if (guid == shader.m_Guid)
                    {
                        /*
                                            var filename = AssetDatabase.GUIDToAssetPath(guid);

                                            if (!filename.EndsWith(kLegacyShaderFileName + kShaderFileNameExtention))
                                            {
                                                return true;
                                            }
                        */
                        return true;

                    }
                }
                foreach (var shader in UTS2ShaderInfo.tessShaders)
                {
                    if (guid == shader.m_Guid)
                    {
                        /*
                                            var filename = AssetDatabase.GUIDToAssetPath(guid);

                                            if (!filename.EndsWith(kLegacyShaderFileName + kShaderFileNameExtention))
                                            {
                                                return true;
                                            }
                        */
                        return true;

                    }
                }
            }
            return false;
        }
        UTSGUID FindUTS2GUID(string guid)
        {
            //            var ret = Array.Find<UTSGUID>(UTS2ShaderInfo.stdShaders, element => element.m_Guid == guid);
#if false
            foreach (var utsGuid in UTS2ShaderInfo.stdShaders)
            {
                if (utsGuid.m_Guid == guid)
                {
                    return utsGuid;
                }
            }
            foreach (var utsGuid in UTS2ShaderInfo.tessShaders)
            {
                if (utsGuid.m_Guid == guid)
                {
                    return utsGuid;
                }
            }
#else
            foreach (var utsGuid in UTS2Table.tables)
            {
                if (utsGuid.m_Guid == guid)
                {
                    return utsGuid;
                }
            }
            
#endif
            return null;
        }


        public override int CountErrors(bool addToScrollView) 
        {
            Debug.Assert(UTS3Converter.scrollView != null);

            m_versionErrorCount = 0;

            for (int ii = 0; ii < m_materialGuids.Length; ii++)
            {

                var guid = m_materialGuids[ii];

                string path = AssetDatabase.GUIDToAssetPath(guid);
                if ( path == null )
                {
                    continue; // the material is deleted.
                }
                Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
                if ( material == null )
                {
                    continue;
                }
                var shaderName = material.shader.ToString();
#if false
                if (!shaderName.StartsWith("Hidden/InternalErrorShader"))
                {
                    continue;
                }
#endif
                string content = File.ReadAllText(path);
                string[] lines = content.Split(lineSeparators, StringSplitOptions.None);
                // always two spaces before m_Shader?
                var targetLine = Array.Find<string>(lines, line => line.StartsWith(kShaderKeywordInMatrial));
                if (targetLine == null)
                {
                    continue; // todo. prefab?
                }
                var shaderMetadata = targetLine.Split(targetSepeartors, StringSplitOptions.None);
                if (shaderMetadata == null)
                {
                    continue;
                }
                if (shaderMetadata.Length < 4)
                {
                    m_versionErrorCount++;
                    Error(path);
                    continue;
                }
                var shaderGUID = shaderMetadata[4];
                while (shaderGUID.StartsWith(" "))
                {
                    shaderGUID = shaderGUID.TrimStart(' ');
                }
                var foundUTS2GUID = FindUTS2GUID(shaderGUID);
                if (foundUTS2GUID == null)
                {
                    continue;       // Not Unity-chan Toon Shader Ver 2.
                }
                var targetLine1 = Array.Find<string>(lines, line => line.StartsWith("    - _isUnityToonshader"));
                if (targetLine1 != null)
                {
                    continue;      // Not Unity-chan Toon Shader Ver 2.
                }
                var targetLine2 = Array.Find<string>(lines, line => line.StartsWith("    - _utsVersion"));
                if (targetLine2 == null)
                {
                    m_versionErrorCount++;
                    if (addToScrollView)
                        AddMaterialToScrollview(material);
                    continue;
                }
                string[] lines2 = targetLine2.Split(targetSepeartors2, StringSplitOptions.None);
                if (lines2 == null || lines2.Length < 2)
                {
                    m_versionErrorCount++;
                    if (addToScrollView)
                        AddMaterialToScrollview(material);
                    continue;
                }
                var utsVersionString = lines2[1];
                while (utsVersionString.StartsWith(" "))
                {
                    utsVersionString = utsVersionString.TrimStart(' ');
                }
                float utsVersion = float.Parse(utsVersionString);
                if (utsVersion < 2.07f)
                {
                    m_versionErrorCount++;
                    if (addToScrollView)
                        AddMaterialToScrollview(material);
                    continue;
                }

            }
            return m_versionErrorCount;
        }

        private static string GetPackageFullPath()
        {
            const string kUtsPackageName = "com.unity.toonshader";
            // Check for potential UPM package
            string packagePath = Path.GetFullPath("Packages/" + kUtsPackageName);
            if (Directory.Exists(packagePath))
            {
                return packagePath;
            }
            return null;
        }
#if false
        void RestoreShaderGUID(UTS3GUI.RenderPipeline renderPipeline)
        {
            var packagePath = packageFullPath;
            const string kGuid = "guid: ";
            AssetDatabase.StartAssetEditing();
            if (renderPipeline == UTS3GUI.RenderPipeline.Legacy)
            {
                var filePath = packagePath + "/Runtime/Legacy/Shaders/" + kLegacyShaderFileName + kShaderFileNameExtention + ".meta";
                string content = File.ReadAllText(filePath);
                string[] lines = content.Split(lineSeparators, StringSplitOptions.None);
                string oldGuid = null;
                foreach (var line in lines)
                {
                    if (line.Contains(kGuid))
                    {
                        var splitted = line.Split(targetSepeartors2, StringSplitOptions.None);
                        oldGuid = splitted[1];
                        while (oldGuid.StartsWith(" "))
                        {
                            oldGuid = oldGuid.TrimStart(' ');
                        }
                        break;
                    }
                }
                content = content.Replace(kGuid + oldGuid, kGuid + stdShaders[0].m_Guid);
                using (FileStream fs = new FileStream(filePath, FileMode.Open)) { using (TextWriter tw = new StreamWriter(fs, Encoding.UTF8, 1024, true)) { tw.Write(content); } fs.SetLength(fs.Position); }
            }
            else if (renderPipeline == UTS3GUI.RenderPipeline.Universal)
            {

            }
            AssetDatabase.StopAssetEditing();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif
        void ConvertBuiltInUTS2Materials( string[] guids)
        {
            foreach (var guid in m_ConvertingMaterialGuids)
            {

                string path = AssetDatabase.GUIDToAssetPath(guid);
                Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
                string content = File.ReadAllText(path);
                string[] lines = content.Split(lineSeparators, StringSplitOptions.None);
                int renderQueueInMaterial = GetRenderQueue(path, lines);
                var shaderID = GetShaderIDinMaterial(path);
                material.shader = Shader.Find(IsTesselationShader(path) ? kIntegratedTessllationUTS3Name :  kIntegratedUTS3Name);
                var shaderGUID = m_Material2GUID_Dictionary[material];
                var UTS2Info = m_GuidToUTSID_Dictionary[shaderGUID] as UTS2INFO;

                UTS3GUI.UTS_TransparentMode transparencyEnabled = (UTS2Info.m_renderQueue == UTS2RenderQueue.Transparent) ? UTS3GUI.UTS_TransparentMode.On : UTS3GUI.UTS_TransparentMode.Off;




                int stencilNo_Setting = UTS3GUI.MaterialGetInt(material, UTS3GUI.ShaderPropStencilNo);

                var renderType = UTS2Info.m_renderType;
                var renderQueueInShader = UTS2Info.m_renderQueue;
                material.SetOverrideTag(UTS2INFO.RENDERTYPE, renderType);
                UTS3GUI.UTS_Mode technique = (UTS3GUI.UTS_Mode)UTS3GUI.MaterialGetInt(material, UTS3GUI.ShaderPropUtsTechniqe);

                switch (technique)
                {
                    case UTS3GUI.UTS_Mode.ThreeColorToon:
                        material.DisableKeyword(UTS3GUI.ShaderDefineSHADINGGRADEMAP);
                        break;
                    case UTS3GUI.UTS_Mode.ShadingGradeMap:
                        material.EnableKeyword(UTS3GUI.ShaderDefineSHADINGGRADEMAP);
                        break;
                }



                if (transparencyEnabled == UTS3GUI.UTS_TransparentMode.On)
                {
                    UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropTransparentEnabled, 1);
                    UTS3GUI.SetupOverDrawTransparentObject(material);
                }
                else
                {
                    UTS3GUI.SetupOutline(material);
                }
                SetCullingMode(material);
                int autoRenderQueue = renderQueueInMaterial == -1 ? 1:0;
                SetAutoRenderQueue(material, autoRenderQueue);

                SetTranparent(material, transparencyEnabled);

                BasicLookdevs(material);
                // Should be kept as it is.
                // SetGameRecommendation(material);
                var clippingMode = UTS2Info.clippingMode;
                ApplyClippingMode(material, clippingMode);
                ApplyStencilMode(material, UTS2Info.m_stencilMode);
                ApplyAngelRing(material);
                ApplyMatCapMode(material);

                if (renderQueueInMaterial == -1)
                {
                    switch (renderQueueInShader)
                    {
                        case UTS2RenderQueue.None:
                            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
                            break;
                        case UTS2RenderQueue.AlphaTest:
                            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                            break;
                        case UTS2RenderQueue.AlphaTestMinus1:
                            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest - 1;
                            break;
                        case UTS2RenderQueue.Transparent:
                            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                            break;
                    }
                }
                else
                {
                    material.renderQueue = renderQueueInMaterial;
                }

            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }

        void SetCullingMode(Material material)
        {
            const string _CullMode = "_CullMode";
            int _CullMode_Setting = UTS3GUI.MaterialGetInt(material, _CullMode);
            //Convert it to Enum format and store it in the offlineMode variable.
            if ((int)UTS3GUI.CullingMode.Off == _CullMode_Setting)
            {
                m_cullingMode = UTS3GUI.CullingMode.Off;
            }
            else if ((int)UTS3GUI.CullingMode.Frontface == _CullMode_Setting)
            {
                m_cullingMode = UTS3GUI.CullingMode.Frontface;
            }
            else
            {
                m_cullingMode = UTS3GUI.CullingMode.Backface;
            }
            //If the value changes, write to the material.
            if (_CullMode_Setting != (int)m_cullingMode)
            {
                switch (m_cullingMode)
                {
                    case UTS3GUI.CullingMode.Off:
                        UTS3GUI.MaterialSetInt(material, _CullMode, 0);
                        break;
                    case UTS3GUI.CullingMode.Frontface:
                        UTS3GUI.MaterialSetInt(material, _CullMode, 1);
                        break;
                    default:
                        UTS3GUI.MaterialSetInt(material, _CullMode, 2);
                        break;
                }

            }
        }
        void SetAutoRenderQueue(Material material, int autoRenderQueue)
        {
            UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropAutoRenderQueue, autoRenderQueue);
        }

        void SetTranparent(Material material, UTS3GUI.UTS_TransparentMode transperentSetting)
        {
            const string _ZWriteMode = "_ZWriteMode";
            const string _ZOverDrawMode = "_ZOverDrawMode";


            if (transperentSetting == UTS3GUI.UTS_TransparentMode.On)
            {
                if (UTS3GUI.MaterialGetInt(material, UTS3GUI.ShaderPropUtsTechniqe) == (int)UTS3GUI.UTS_Mode.ThreeColorToon)
                {
                    UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropClippingMode, (int)UTS3GUI.UTS_ClippingMode.TransClippingMode);
                }
                else
                {
                    // ShadingGradeMap
                    UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropClippingMode, (int)UTS3GUI.UTS_TransClippingMode.On);
                }
                UTS3GUI.MaterialSetInt(material, _ZWriteMode, 0);
                material.SetFloat(_ZOverDrawMode, 1);
            }
            else
            {
                UTS3GUI.MaterialSetInt(material, _ZWriteMode, 1);
                material.SetFloat(_ZOverDrawMode, 0);
            }

        }

        void BasicLookdevs(Material material)
        {
            if (material.HasProperty(UTS3GUI.ShaderPropUtsTechniqe))//ThreeColorToon or ShadingGradeMap
            {
                if (UTS3GUI.MaterialGetInt(material, UTS3GUI.ShaderPropUtsTechniqe) == (int)UTS3GUI.UTS_Mode.ThreeColorToon)   //DWF
                {

                    //Sharing variables with ShadingGradeMap method.

                    material.SetFloat(UTS3GUI.ShaderProp1st_ShadeColor_Step, material.GetFloat(UTS3GUI.ShaderPropBaseColor_Step));
                    material.SetFloat(UTS3GUI.ShaderProp1st_ShadeColor_Feather, material.GetFloat(UTS3GUI.ShaderPropBaseShade_Feather));
                    material.SetFloat(UTS3GUI.ShaderProp2nd_ShadeColor_Step, material.GetFloat(UTS3GUI.ShaderPropShadeColor_Step));
                    material.SetFloat(UTS3GUI.ShaderProp2nd_ShadeColor_Feather, material.GetFloat(UTS3GUI.ShaderProp1st2nd_Shades_Feather));
                }
                else if (UTS3GUI.MaterialGetInt(material, UTS3GUI.ShaderPropUtsTechniqe) == (int)UTS3GUI.UTS_Mode.ShadingGradeMap)
                {    //SGM

                    //Share variables with DoubleWithFeather method.
                    material.SetFloat(UTS3GUI.ShaderPropBaseColor_Step, material.GetFloat(UTS3GUI.ShaderProp1st_ShadeColor_Step));
                    material.SetFloat(UTS3GUI.ShaderPropBaseShade_Feather, material.GetFloat(UTS3GUI.ShaderProp1st_ShadeColor_Feather));
                    material.SetFloat(UTS3GUI.ShaderPropShadeColor_Step, material.GetFloat(UTS3GUI.ShaderProp2nd_ShadeColor_Step));
                    material.SetFloat(UTS3GUI.ShaderProp1st2nd_Shades_Feather, material.GetFloat(UTS3GUI.ShaderProp2nd_ShadeColor_Feather));
                }
                else
                {
                    // OutlineObj.
                    return;
                }
            }
            EditorGUILayout.Space();
        }
        private bool IsShadingGrademap(Material material)
        {
            return UTS3GUI.MaterialGetInt(material, UTS3GUI.ShaderPropUtsTechniqe) == (int)UTS3GUI.UTS_Mode.ShadingGradeMap;
        }

 
        void ApplyMatCapMode(Material material)
        {
            if (UTS3GUI.MaterialGetInt(material, UTS3GUI.ShaderPropClippingMode) == 0)
            {
                if (material.GetFloat(UTS3GUI.ShaderPropMatCap) == 1)
                    material.EnableKeyword(UTS3GUI.ShaderPropMatCap);
                else
                    material.DisableKeyword(UTS3GUI.ShaderPropMatCap);
            }
            else
            {
                material.DisableKeyword(UTS3GUI.ShaderPropMatCap);
            }
        }

        void ApplyAngelRing(Material material)
        {
            int angelRingEnabled = UTS3GUI.MaterialGetInt(material, UTS3GUI.ShaderPropAngelRing);
            if (angelRingEnabled == 0)
            {
                material.DisableKeyword(UTS3GUI.ShaderDefineANGELRING_ON);
                material.EnableKeyword(UTS3GUI.ShaderDefineANGELRING_OFF);
            }
            else
            {
                material.EnableKeyword(UTS3GUI.ShaderDefineANGELRING_ON);
                material.DisableKeyword(UTS3GUI.ShaderDefineANGELRING_OFF);

            }
        }

        void ApplyStencilMode(Material material, UTS3GUI.UTS_StencilMode mode)
        {
            UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropStencilMode,(int)mode);

            switch (mode)
            {
                case UTS3GUI.UTS_StencilMode.Off:
                    //    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilNo,0);
                    UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropStencilComp, (int)UTS3GUI.StencilCompFunction.Disabled);
                    UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropStencilOpPass, (int)UTS3GUI.StencilOperation.Keep);
                    UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropStencilOpFail, (int)UTS3GUI.StencilOperation.Keep);
                    break;
                case UTS3GUI.UTS_StencilMode.StencilMask:
                    //    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilNo,0);
                    UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropStencilComp, (int)UTS3GUI.StencilCompFunction.Always);
                    UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropStencilOpPass, (int)UTS3GUI.StencilOperation.Replace);
                    UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropStencilOpFail, (int)UTS3GUI.StencilOperation.Replace);
                    break;
                case UTS3GUI.UTS_StencilMode.StencilOut:
                    //    UTS3GUI.MaterialSetInt(material,UTS3GUI.ShaderPropStencilNo,0);
                    UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropStencilComp, (int)UTS3GUI.StencilCompFunction.NotEqual);
                    UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropStencilOpPass, (int)UTS3GUI.StencilOperation.Keep);
                    UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropStencilOpFail, (int)UTS3GUI.StencilOperation.Keep);

                    break;
            }



        }
        void ApplyClippingMode(Material material,int clippingMode)
        {

            if (!IsShadingGrademap(material))
            {


                material.DisableKeyword(UTS3GUI.ShaderDefineIS_TRANSCLIPPING_OFF);
                material.DisableKeyword(UTS3GUI.ShaderDefineIS_TRANSCLIPPING_ON);

                switch (clippingMode)
                {
                    case 0:
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_OFF);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_MODE);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_TRANSMODE);
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_OUTLINE_CLIPPING_NO);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_OUTLINE_CLIPPING_YES);
                        break;
                    case 1:
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_OFF);
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_MODE);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_TRANSMODE);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_OUTLINE_CLIPPING_NO);
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_OUTLINE_CLIPPING_YES);
                        break;
                    case 2:
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_OFF);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_MODE);
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_TRANSMODE);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_OUTLINE_CLIPPING_NO);
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_OUTLINE_CLIPPING_YES);
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
            }
            else
            {


                material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_OFF);
                material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_MODE);
                material.DisableKeyword(UTS3GUI.ShaderDefineIS_CLIPPING_TRANSMODE);
                switch (clippingMode)
                {
                    case 0:
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_TRANSCLIPPING_OFF);
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_TRANSCLIPPING_ON);
                        break;
                    case 1:
                        material.DisableKeyword(UTS3GUI.ShaderDefineIS_TRANSCLIPPING_OFF);
                        material.EnableKeyword(UTS3GUI.ShaderDefineIS_TRANSCLIPPING_ON);
                        break;
                    default:
                        Debug.Assert(false);
                        break;

                }

            }
            UTS3GUI.MaterialSetInt(material, UTS3GUI.ShaderPropClippingMode, clippingMode);
        }





        void SetGameRecommendation(Material material)
        {


            material.SetFloat(UTS3GUI.ShaderPropIsLightColor_Base, 1);
            material.SetFloat(UTS3GUI.ShaderPropIs_LightColor_1st_Shade, 1);
            material.SetFloat(UTS3GUI.ShaderPropIs_LightColor_2nd_Shade, 1);
            material.SetFloat(UTS3GUI.ShaderPropIs_LightColor_HighColor, 1);
            material.SetFloat(UTS3GUI.ShaderPropIs_LightColor_RimLight, 1);
            material.SetFloat(UTS3GUI.ShaderPropIs_LightColor_Ap_RimLight, 1);
            material.SetFloat(UTS3GUI.ShaderPropIs_LightColor_MatCap, 1);
            if (material.HasProperty(UTS3GUI.ShaderPropAngelRing))
            {//When AngelRing is available
                material.SetFloat(UTS3GUI.ShaderPropIs_LightColor_AR, 1);
            }
            if (material.HasProperty(UTS3GUI.ShaderPropOutline))//OUTLINEÇ™Ç†ÇÈèÍçá.
            {
                material.SetFloat(UTS3GUI.ShaderPropIs_LightColor_Outline, 1);
            }
            material.SetFloat(UTS3GUI.ShaderPropSetSystemShadowsToBase, 1);
            material.SetFloat(UTS3GUI.ShaderPropIsFilterHiCutPointLightColor, 1);
            material.SetFloat(UTS3GUI.ShaderPropCameraRolling_Stabilizer, 1);
            material.SetFloat(UTS3GUI.ShaderPropIs_Ortho, 0);
            material.SetFloat(UTS3GUI.ShaderPropGI_Intensity, 0);
            material.SetFloat(UTS3GUI.ShaderPropUnlit_Intensity, 1);
            material.SetFloat(UTS3GUI.ShaderPropIs_Filter_LightColor, 1);
        }
        public override InstalledStatus CheckSourceShaderInstalled() { return InstalledStatus.NotInstalled; }
    }
}