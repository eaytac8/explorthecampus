using UnityEngine;
using System.Collections;

namespace ExplorTheCampus
{
    public class CameraMove : MonoBehaviour
    {
    
        void Update()
        {
            Transform cameraTransform = Camera.main.transform;
            Vector3 calcVector = new Vector3(Random.Range(0, 2), Random.Range(0, 2), -10);
            //cameraTransform.position += cameraTransform.right * 0.05f * Time.deltaTime;
            //cameraTransform.right
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, calcVector, 0.01f) + new Vector3(0, 0, -10);
        }
    }
}