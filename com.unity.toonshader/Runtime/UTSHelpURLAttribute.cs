using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using UnityObject = UnityEngine.Object;
namespace Unity.Rendering.Toon
{
#if UNITY_2021_1_OR_NEWER
    internal class UTSHelpURLAttribute : HelpURLAttribute
    {
        internal const string fallbackVersion = "0.7";

        internal static string version
        {
            get
            {
                return fallbackVersion;
            }
        }
        const string url = "https://docs.unity3d.com/Packages/{0}@{1}/manual/{2}.html";

        internal UTSHelpURLAttribute(string pageName, string packageName = "com.unity.toonshader")
            : base(GetPageLink(packageName, pageName))
        {
        }

        internal static string GetPageLink(string packageName, string pageName) => string.Format(url, packageName, version, pageName);

    }
#else
    internal class UTSHelpURLAttribute : System.Attribute 
    {
        internal UTSHelpURLAttribute(string name)  
        {
        }
    }
#endif
}
