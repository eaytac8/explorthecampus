using UnityEngine;
using System.Collections;

namespace ExplorTheCampus
{
    public class Loader : MonoBehaviour
    {

        public GameObject gameManager;
        public GameObject soundManager;
        public GameObject sceneManager;
        public GameObject settingsManager;
        
        void Awake()
        {
            if (SceneManager.instance == null)
            {
                Instantiate(sceneManager);
            }
            if (SettingsManager.instance == null)
            {
                Instantiate(settingsManager);
            }
            if (SoundManager.instance == null)
            {
                Instantiate(soundManager);
            }
            if (GameManager.instance == null)
            {
                Instantiate(gameManager);
            }
        }
    }
}