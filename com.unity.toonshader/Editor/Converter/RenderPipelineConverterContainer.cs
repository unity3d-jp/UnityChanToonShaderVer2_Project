using System;
using System.Collections.Generic;
using System.IO;
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

        protected readonly string[] lineSeparators = new[] { "\r\n", "\r", "\n" };
        protected readonly string[] targetSepeartors = new[] { ":", "," };
        protected readonly string[] targetSepeartors2 = new[] { ":" };

        protected List<Material> m_ConvertingMaterials = new List<Material>();
        protected Dictionary<Material, string> m_Material2GUID_Dictionary = new Dictionary<Material, string>();
        protected Dictionary<string, UTSGUID> m_GuidToUTSID_Dictionary = new Dictionary<string, UTSGUID>();

        protected const string kIntegratedUTS3GUID = "be891319084e9d147b09d89e80ce60e0";
        protected const string kIntegratedUTS3Name = "Toon";
        protected static string packageFullPath
        {
            get; set;
        }
        protected const string utsVersionProp = "_utsVersion";
        protected void Error(string path)
        {
            Debug.LogErrorFormat("File: {0} is corrupted.", path);
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
            m_ConvertingMaterials.Clear();

            m_versionErrorCount = 0;
            m_ConvertingMaterials.Clear();
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
        protected void SetupConverterCommon(UTSGUID srcShaderGUID, UTSGUID srcTessShaderGUID)
        {


            int materialCount = 0;

            for (int ii = 0; ii < m_materialGuids.Length; ii++)
            {
                var guid = m_materialGuids[ii];

                string path = AssetDatabase.GUIDToAssetPath(guid);
                Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
                var shaderName = material.shader.ToString();

                string content = File.ReadAllText(path);
                string[] lines = content.Split(lineSeparators, StringSplitOptions.None);
                // always two spaces before m_Shader?
                var targetLine = Array.Find<string>(lines, line => line.StartsWith("  m_Shader:"));
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
                var foundOldUTSGUID = FindSrcShader2GUID(shaderGUID, srcShaderGUID, srcTessShaderGUID);
                if (foundOldUTSGUID == null)
                {
                    continue;
                }

                m_ConvertingMaterials.Add(material);
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
            foreach (var material in m_ConvertingMaterials)
            {
                material.shader = Shader.Find(kIntegratedUTS3Name);
                UTS3GUI.UpdateVersionInMaterial(material);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
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


    }
}