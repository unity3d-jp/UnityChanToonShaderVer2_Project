using UnityEngine;
using UnityEditor;
using Unity.RaytracedHardShadow;

namespace Unity.RaytracedHardShadowEditor
{
    public class ExportToImageWindowUTS2_HDRP : EditorWindow
    {
        ShadowRaytracerUTS2_HDRP m_raytracer;
        static ShadowRaytracerUTS2_HDRP.ImageFormat m_format = ShadowRaytracerUTS2_HDRP.ImageFormat.PNG; // static to keep last selection
        string m_path;

        public static void Open(ShadowRaytracerUTS2_HDRP sr)
        {
            var window = EditorWindow.GetWindow<ExportToImageWindowUTS2_HDRP>();
            window.titleContent = new GUIContent("Export To Image");
            window.m_raytracer = sr;
            window.Show();
        }

        private void OnGUI()
        {
            if (m_raytracer == null)
            {
                Close();
                return;
            }

            m_format = (ShadowRaytracerUTS2_HDRP.ImageFormat)EditorGUILayout.EnumPopup("Format", m_format);
            if (GUILayout.Button("Export"))
            {
                string ext = "";
                switch(m_format)
                {
                    case ShadowRaytracerUTS2_HDRP.ImageFormat.PNG: ext = "png"; break;
                    case ShadowRaytracerUTS2_HDRP.ImageFormat.EXR: ext = "exr"; break;
#if UNITY_2018_3_OR_NEWER
                    case ShadowRaytracerUTS2_HDRP.ImageFormat.TGA: ext = "tga"; break;
#endif
                }

                string path = EditorUtility.SaveFilePanel("Path to export", "", m_raytracer.outputTexture.name + "." + ext, ext);
                m_raytracer.ExportToImage(path, m_format);
                SceneView.RepaintAll();
                Close();
            }
        }
    }
}

