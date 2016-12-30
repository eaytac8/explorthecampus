using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace ExplorTheCampus
{

    /// <summary>
    /// This script is used to load different scenes depending on the current scene.
    /// </summary>
    public class SceneManager : MonoBehaviour
    {

        public static SceneManager instance = null;
        private int amountScenes;

        public ScreenFader screenFader;
        [HideInInspector]
        public string lastScene = null;

        void Awake()
        {
            amountScenes = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        /*
        public IEnumerator LoadScene(string sceneName)
        {
            this.LoadScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName).buildIndex);
        }
        */

        public IEnumerator LoadScene(int buildIndex)
        {
            string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            lastScene = currentScene;

            float fadeTime = screenFader.BeginFade(1);
            yield return new WaitForSeconds(fadeTime);
            if (currentScene == "Campus")
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Campus");
                GameManager.instance.ShowControl(true);
            }
        }

        public int GetSceneCount()
        {
            return amountScenes;
        }
    }

}