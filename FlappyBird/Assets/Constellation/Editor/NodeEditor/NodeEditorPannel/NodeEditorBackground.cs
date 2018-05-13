using UnityEngine;

namespace ConstellationEditor {
    public class NodeEditorBackground {
        private Texture2D Background;
        private IGUI GUI;

        public NodeEditorBackground (IGUI gui, Texture2D background)
        {
            Background = background;
            GUI = gui;
        }

        public void DrawBackgroundGrid (float _width, float _height, float offsetX, float offsetY, Color tint) {
            if (Background != null) {
                //Background location based of current location allowing unlimited background
                //How many background are needed to fill the background
                var xCount = Mathf.Round (_width / Background.width) + 2;
                var yCount = Mathf.Round (_height / Background.height) + 2;
                //Current scroll offset for background
                var xOffset = Mathf.Round (offsetX / Background.width) - 1;
                var yOffset = Mathf.Round (offsetY / Background.height) - 1;
                var texRect = new Rect (0, 0, Background.width, Background.height);
               // if (isInstance && constellationScript.IsDifferentThanSource)
                GUI.SetColor (tint);
                for (var i = xOffset; i < xOffset + xCount; i++) {
                    for (var j = yOffset; j < yOffset + yCount; j++) {
                        texRect.x = i * Background.width;
                        texRect.y = j * Background.height;
                        GUI.DrawTexture (texRect, Background);
                    }
                }
                GUI.SetColor (Color.white);
            }
        }
    }
}