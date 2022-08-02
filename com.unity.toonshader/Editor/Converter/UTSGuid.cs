using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using System.Linq;
using System.IO;

namespace UnityEditor.Rendering.Toon
{
    internal class UTSGUID
    {
        public UTSGUID(string guid, string shaderName)
        {
            m_ShaderName = shaderName;
            m_Guid = guid;
        }
        internal string m_ShaderName;
        internal string m_Guid;
    }

}