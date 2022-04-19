using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;


namespace UnityEditor.Rendering.Toon
{

    /// <summary>
    /// Collection to store <see cref="UTS3MaterialHeaderScopeItem"></see>
    /// </summary>
    public class UTS3MaterialHeaderScopeList
    {
        internal readonly uint m_DefaultExpandedState;
        internal readonly List<UTS3MaterialHeaderScopeItem> m_Items = new List<UTS3MaterialHeaderScopeItem>();

        /// <summary>
        /// Constructor that initializes it with the default expanded state for the internal scopes
        /// </summary>
        /// <param name="defaultExpandedState">By default, everything is expanded</param>
        public UTS3MaterialHeaderScopeList(uint defaultExpandedState = uint.MaxValue)
        {
            m_DefaultExpandedState = defaultExpandedState;
        }

        /// <summary>
        /// Registers a <see cref="MaterialHeaderScopeItem"/> into the list
        /// </summary>
        /// <param name="title"><see cref="GUIContent"/> The title of the scope</param>
        /// <param name="expandable">The mask identifying the scope</param>
        /// <param name="action">The action that will be drawn if the scope is expanded</param>
        /// <param name="workflowMode">UTS workflow mode </param>        /// 
        /// <param name="isTransparent">Flag transparent material header should be drawn</param>        /// 
        /// <param name="isTessellation">Flag Tessellation material header should be drawn</param>        /// 
        public void RegisterHeaderScope<TEnum>(GUIContent title, TEnum expandable, Action<Material> action, uint workflowMode, uint isTransparent, uint isTessellation )
            where TEnum : struct, IConvertible
        {
            m_Items.Add(new UTS3MaterialHeaderScopeItem()
            {
                headerTitle = title,
                expandable = Convert.ToUInt32(expandable),
                drawMaterialScope = action,
#if UNITY_2021_1_OR_NEWER
                url = UTS3DocumentationUtils.GetHelpURL<TEnum>(expandable),
#else
                url = string.Empty,
#endif
                workflowMode = workflowMode,
                transparentEnabled = isTransparent,
                tessellationEnabled = isTessellation
            }); 
        }

        /// <summary>
        /// Draws all the <see cref="MaterialHeaderScopeItem"/> with its information stored
        /// </summary>
        /// <param name="materialEditor"><see cref="MaterialEditor"/></param>
        /// <param name="material"><see cref="Material"/></param>
        public void DrawHeaders(MaterialEditor materialEditor, Material material)
        {
            if (material == null)
                throw new ArgumentNullException(nameof(material));

            if (materialEditor == null)
                throw new ArgumentNullException(nameof(materialEditor));

            foreach (var item in m_Items)
            {
                using (var header = new UTS3MaterialHeaderScope(
                    item.headerTitle,
                    item.expandable,
                    materialEditor,
                    defaultExpandedState: m_DefaultExpandedState,
                    documentationURL: item.url))
                {
                    if (!header.expanded)
                        continue;

                    item.drawMaterialScope(material);

                    EditorGUILayout.Space();
                }
            }
        }
    }

}
