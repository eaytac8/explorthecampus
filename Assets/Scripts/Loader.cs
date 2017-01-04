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
            Debug.Log(GameManager.instance);
            Debug.Log(SceneManager.instance);
            Debug.Log(SoundManager.instance);
            Debug.Log(SettingsManager.instance);
            if (GameManager.instance == null)
            {
                Instantiate(gameManager);
            }
            if (SceneManager.instance == null)
            {
                Instantiate(sceneManager);
            }
            if (SoundManager.instance == null)
            {
                Instantiate(soundManager);
            }
            if (SettingsManager.instance == null)
            {
                Instantiate(settingsManager);
            }
        }
    }
}