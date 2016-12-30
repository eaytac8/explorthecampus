using UnityEngine;
using System.Collections;

namespace ExplorTheCampus
{
    public class Loader : MonoBehaviour
    {

        public GameObject gameManager;  //Not impemented yet
        public GameObject soundManager; //Not implemented yet
        public GameObject sceneManager;

        void Awake()
        {

            if (SceneManager.instance == null)
            {
                Instantiate(sceneManager);
            }
            if (SoundManager.instance == null)
            {
                Instantiate(soundManager);
            }
        }
    }
}