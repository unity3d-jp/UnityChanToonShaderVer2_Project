using System;
using System.Collections.Generic;
using System.IO;
using Unity.FilmInternalUtilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityEditor.Rendering.Toon
{
    internal abstract class RenderPipelineConverterContainer
    {
        public enum InstalledStatus
        {
            NotInstalled,
            InstalledUnsupportedVersion,
            Installed
        };
        protected InstalledStatus m_sourceShaderInstalledStatus;
        protected int m_materialCount = 0;
        protected string[] m_materialGuids;
        internal int m_versionErrorCount = 0;

        internal static readonly string[] lineSeparators = new[] { "\r\n", "\r", "\n" };
        internal static readonly string[] targetSepeartors = new[] { ":", "," };
        internal static readonly string[] targetSepeartors2 = new[] { ":" };
        internal static readonly char[] wordSepeators = new[] { ' ', ',', ':', '\t' };
        protected List<string> m_ConvertingMaterialGuids = new List<string>();
        protected Dictionary<Material, string> m_Material2GUID_Dictionary = new Dictionary<Material, string>();
        protected Dictionary<string, UTSGUID> m_GuidToUTSID_Dictionary = new Dictionary<string, UTSGUID>();
        protected const string kIntegratedUTS3Name = "Toon";
        protected const string kIntegratedUTS3GUID = "be891319084e9d147b09d89e80ce60e0";

        protected const string kIntegratedTessllationUTS3Name = "Toon(Tessellation)";
        protected const string kIntegratedTessllationUTS3GUID = "e4468eb8a8320f7488ddbb0e591f9fbc";
        protected const string kShaderKeywordInMatrial = "  m_Shader:";

        protected static string packageFullPath
        {
            get; set;
        }
        protected const string utsVersionProp = "_utsVersion";
        protected void Error(string path)
        {
            Debug.LogErrorFormat("File: {0} is corrupted.", path);
        }

        protected bool IsTesselationShader(string materialPath)
        {
            var shaderID = GetShaderIDinMaterial(materialPath);
            foreach (var tessShaderGUID in UTS2ShaderInfo.tessShaders)
            {
                if (tessShaderGUID.m_Guid == shaderID)
                {
                    return true;
                }
            }
            if (shaderID == HdrpUTS3toIntegratedUTS3Converter.kOrgTessShaderGUID.m_Guid)
            {
                return true;
            }
            if (shaderID == BuiltinUTS3toIntegratedUTS3Converter.kOrgTessShaderGUID.m_Guid)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// The name of the container. This name shows up in the UI.
        /// </summary>
        public abstract string name { get; }

        /// <summary>
        /// The description of the container.
        /// It is shown in the UI. Describe the converters in this container.
        /// </summary>
        public abstract string info { get; }

        /// <summary>
        /// The priority of the container. The lower the number (can be negative), the earlier Unity executes the container, and the earlier it shows up in the converter container menu.
        /// </summary>
        public virtual int priority => 0;


        public abstract void SetupConverter();
        public abstract void Convert();
        public abstract void PostConverting();

        public abstract int CountErrors(bool addToScrollView);
        public abstract InstalledStatus CheckSourceShaderInstalled();

        public void Reset()
        {
            m_materialCount = 0;

            m_ConvertingMaterialGuids.Clear();
            m_versionErrorCount = 0;

            m_Material2GUID_Dictionary.Clear();
            m_GuidToUTSID_Dictionary.Clear();
            UTS3Converter.scrollView.Clear();

        }
        public void CommonSetup()
        {
            Reset();
            Debug.Assert(UTS3Converter.scrollView != null);
            if (m_materialGuids == null)
            {
                m_materialGuids = AssetDatabase.FindAssets("t:Material", null);
            }
            // CheckSourceShaderInstalled(); // Not necessary? 
        }

        protected string GetShaderIDinMaterial(string path)
        {
            string content = File.ReadAllText(path);
            string[] lines = content.Split(lineSeparators, StringSplitOptions.None);
            // always two spaces before m_Shader?
            var targetLine = Array.Find<string>(lines, line => line.StartsWith(kShaderKeywordInMatrial));
            if (targetLine == null)
            {
                return null;  // todo. prefab?
            }
            var shaderMetadata = targetLine.Split(targetSepeartors, StringSplitOptions.None);
            if (shaderMetadata == null)
            {
                return null;
            }
            if (shaderMetadata.Length < 4)
            {
                m_versionErrorCount++;
                Error(path);
                return null;
            }
            var shaderGUID = shaderMetadata[4];
            while (shaderGUID.StartsWith(" "))
            {
                shaderGUID = shaderGUID.TrimStart(' ');
            }
            return shaderGUID;
        }
        protected void SetupConverterCommon(UTSGUID srcShaderGUID, UTSGUID srcTessShaderGUID)
        {


            int materialCount = 0;

            for (int ii = 0; ii < m_materialGuids.Length; ii++)
            {
                var guid = m_materialGuids[ii];

                string path = AssetDatabase.GUIDToAssetPath(guid);
                Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
                var shaderName = material.shader.ToString();

                var shaderGUID = GetShaderIDinMaterial(path);
                var foundOldUTSGUID = FindSrcShader2GUID(shaderGUID, srcShaderGUID, srcTessShaderGUID);
                if (foundOldUTSGUID == null)
                {
                    continue;
                }

                m_ConvertingMaterialGuids.Add(guid);
                if (!m_Material2GUID_Dictionary.ContainsKey(material))
                {
                    m_Material2GUID_Dictionary.Add(material, shaderGUID);
                }
                if (!m_GuidToUTSID_Dictionary.ContainsKey(shaderGUID))
                {
                    m_GuidToUTSID_Dictionary.Add(shaderGUID, foundOldUTSGUID);
                }
                materialCount++;

                AddMaterialToScrollview(material);
            }
        }
        protected void CommonConvert()
        {
            foreach (var guid in m_ConvertingMaterialGuids)
            {
                int renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
                string content = File.ReadAllText(path);
                string[] lines = content.Split(lineSeparators, StringSplitOptions.None);
                var shderGUID = GetShaderIDinMaterial(path);

                // deal with m_CustomRenderQueue
                renderQueue = GetRenderQueue(path, lines);
                // deal with RenderType
                var renderType = GetRenderType(path, lines);
                material.shader = Shader.Find(IsTesselationShader(path) ? kIntegratedTessllationUTS3Name : kIntegratedUTS3Name);

                material.renderQueue = renderQueue;
                if (renderType != null)
                {
                    material.SetOverrideTag("RenderType", renderType);
                }
                UTS3GUI.UpdateVersionInMaterial(material);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        protected int GetRenderQueue( string path, string[] lines)
        {
            int renderQueue = -1;
            var targetLine = Array.Find<string>(lines, line => line.StartsWith("  m_CustomRenderQueue:"));
            if (targetLine == null)
            {
                return renderQueue; // todo. prefab?
            }
            var customRenderQueue = targetLine.Split(targetSepeartors, StringSplitOptions.None);
            if (customRenderQueue.Length < 2)
            {
                Error(path);
                return renderQueue; 
            }
            var queueNumber = customRenderQueue[1];
            while (queueNumber.StartsWith(" "))
            {
                queueNumber = queueNumber.TrimStart(' ');
            }
            renderQueue = int.Parse(queueNumber);
            return renderQueue; 
        }
        string GetRenderType(string path, string[] lines)
        {
            var targetLine = Array.Find<string>(lines, line => line.StartsWith("    RenderType:"));
            if (targetLine == null)
            {
                return null; ; // todo. prefab?
            }
            var renderType = targetLine.Split(targetSepeartors, StringSplitOptions.None);
            if (renderType.Length < 2)
            {
                Error(path);
                return null;
            }
            var targetRenderType = renderType[1];
            while (targetRenderType.StartsWith(" "))
            {
                targetRenderType = targetRenderType.TrimStart(' ');
            }
            return targetRenderType;
        }
        protected UTSGUID FindSrcShader2GUID(string strShaderGUID, UTSGUID srcShaderGUID, UTSGUID srcTessShaderGUID)
        {

            if (srcShaderGUID != null && srcShaderGUID.m_Guid == strShaderGUID)
            {
                return srcShaderGUID;
            }
            if (srcTessShaderGUID != null && srcTessShaderGUID.m_Guid == strShaderGUID)
            {
                return srcTessShaderGUID;
            }
            return null;
        }
        public void AddMaterialToScrollview(Material material)
        {
            Label item = new Label(material.name);
            UTS3Converter.scrollView.Add(item);
        }

        protected void SendAnalyticsEvent()
        {
            AnalyticsSender.SendEventInEditor(new ToonShaderAnalytics.ConvertEvent(GetType().Name));
        }
    }
}