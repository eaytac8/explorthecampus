using UnityEngine;
using System.Collections;

namespace ExplorTheCampus
{
    /// <summary>
    /// This script is used to fade in and fout out scenes.
    /// </summary>
    public class ScreenFader : MonoBehaviour
    {
        public Texture2D fadeOutTexture;
        public float fadeSpeed = 1.5f;

        private int drawDepth = -1000;
        private float alpha = 1.0f;
        private int fadeDir = -1;
        private bool isFading = true;

        void OnGUI()
        {
            if (isFading)
            {
                alpha += fadeDir * fadeSpeed * Time.deltaTime;
                alpha = Mathf.Clamp01(alpha);

                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
                GUI.depth = drawDepth;
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
            }
        }

        void OnLevelWasLoaded()
        {
            BeginFade(-1);
        }

        /// <summary>
        /// Start fading.
        /// </summary>
        /// <param name="direction">1 to fade in. -1 to fade out.</param>
        /// <returns>The fade speed.</returns>
        public float BeginFade(int direction)
        {
            fadeDir = direction;
            return fadeSpeed;
        }
    }

}