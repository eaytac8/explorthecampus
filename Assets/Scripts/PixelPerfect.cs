using UnityEngine;
using System.Collections;

namespace ExplorTheCampus
{

    /// <summary>
    /// This script is used enable pixel perfect.
    /// </summary>
    public class PixelPerfect : MonoBehaviour
    {

        public float tileScaleFactor = 1f;
        private float ratio;

        private Camera cam;

        void Start()
        {
            cam = GetComponent<Camera>();
            ratio = 0.5f / tileScaleFactor;
        }

        void Update()
        {
            cam.orthographicSize = (Screen.height * 0.01f) * ratio;
        }
    }

}
