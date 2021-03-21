using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace UnityEditor.Rendering.Toon
{

    internal class TextureSynthesizerDebugWindow : EditorWindow
    {
        Texture SynthesizedTexture { get; set; }
        [MenuItem("Window/Rendering/Unity Toon Shader/Texture Synthesizer Window")]
        static void OpenWindow()
        {
            GetWindow<TextureSynthesizerDebugWindow>();
        }

        private void OnGUI()
        {
            var rect = position;
            rect.x = 0.0f;
            rect.y = 0.0f;

            var mode = UTS_TextureSynthesizer.SynthesizerMode;
            switch (mode)
            {
                case UTS_TextureSynthesizer.eSynthesizerMode.Combine3_1:
                    DrawSourceTexture(rect, 2);
                    break;
                case UTS_TextureSynthesizer.eSynthesizerMode.Combine4:
                    DrawSourceTexture(rect, 4);
                    break;
            }



        }

        void DrawSourceTexture(Rect rect, int cloumn)
        {
            var textureRect = rect;
            textureRect.width /= (float)cloumn;
            textureRect.height /= 2;
            for ( int ii = 0; ii < cloumn; ii++)
            {


                switch (ii)
                {
                    case 0:
                        if (UTS_TextureSynthesizer.Source0 != null)
                        {
                            Graphics.DrawTexture(textureRect, UTS_TextureSynthesizer.Source0);
                        }
                        break;
                    case 1:
                        if (UTS_TextureSynthesizer.Source1 != null)
                        {
                            Graphics.DrawTexture(textureRect, UTS_TextureSynthesizer.Source1);
                        }
                        break;
                    case 2:
                        if (UTS_TextureSynthesizer.Source2 != null)
                        {
                            Graphics.DrawTexture(textureRect, UTS_TextureSynthesizer.Source2);
                        }
                        break;
                    case 3:
                        if (UTS_TextureSynthesizer.Source3 != null)
                        {
                            Graphics.DrawTexture(textureRect, UTS_TextureSynthesizer.Source3);
                        }
                        break;
                }
                textureRect.x += rect.width / (float)cloumn;
            }
            textureRect.x = 0;
            textureRect.y = textureRect.height;
            textureRect.width = rect.width;


            SynthesizedTexture = UTS_TextureSynthesizer.DebugTexture;
            if (SynthesizedTexture != null)
            {
                Graphics.DrawTexture(textureRect, SynthesizedTexture);
            }
        }

        void Update()
        {
            Repaint();
        }

    }
}

