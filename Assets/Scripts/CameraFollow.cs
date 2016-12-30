using UnityEngine;
using System.Collections;

/// <summary>
/// This script is used 
/// </summary>

namespace ExplorTheCampus {

    public class CameraFollow : MonoBehaviour {

        public Transform target;
        public float cameraSpeed = 0.1f;

        void Update() {
            if (target) {
                transform.position = Vector3.Lerp(transform.position, target.position, cameraSpeed) + new Vector3(0, 0, -10);
            }
        }

    }
}