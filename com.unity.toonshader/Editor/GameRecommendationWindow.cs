using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityEditor.Rendering.Toon
{

    internal class GameRecommendationWindow : EditorWindow
    {
        private const float WINDOWSIZE_W = 340.0f; 
        private const float WINDOWSIZE_H = 420.0f; 
        Material m_material;
        UTS_GUIBase m_gui;
        internal static void OpenWindow(UTS_GUIBase gui, Material material)
        {
            var window = GetWindow<GameRecommendationWindow>("Game Recommendation");
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
            m_gui.GUI_GameRecommendation(m_material, this);
        }

    }
}