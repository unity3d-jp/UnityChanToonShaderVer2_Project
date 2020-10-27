using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityEditor.Rendering.HDRP.Toon
{

    internal class GameLightRecommendationWindow : EditorWindow
    {
        private const float WINDOWSIZE_W = 340.0f; 
        private const float WINDOWSIZE_H = 400.0f; 
        Material m_material;
        HDRPToonGUI m_gui;
        internal static void OpenWindow(HDRPToonGUI gui, Material material)
        {
            var window = GetWindow<GameLightRecommendationWindow>("Game Light Recommendation");
            window.m_material = material;
            window.m_gui = gui;

            window.maxSize = window.minSize = new Vector2(WINDOWSIZE_W, WINDOWSIZE_H);
        }

        private void OnGUI()
        {
            if (m_gui == null || m_material == null)
            {
                Close();
            }
            m_gui.GUI_GameLightRecommendation(m_material, this);
        }

    }
}