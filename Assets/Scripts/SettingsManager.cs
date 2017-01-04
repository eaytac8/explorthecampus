using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ExplorTheCampus
{
    public class SettingsManager : MonoBehaviour
    {

        public static SettingsManager instance = null;

        void Awake()
        {
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

        public bool GetSaveOnQuit()
        {
            return PlayerPrefs.GetInt("saveOnQuit") == 1 ? true : false;
        }

        public void SetBackgroundMusicVolume(GameObject target)
        {
            Slider slider = target.GetComponent<Slider>();
            PlayerPrefs.SetFloat("backgroundMusicVolume", slider.value);
        }

        public void SetEffectsVolume(GameObject target)
        {
            Slider slider = target.GetComponent<Slider>();
            PlayerPrefs.SetFloat("effectsVolume", slider.value);
        }

        public void SetTextSpeed(GameObject target)
        {
            Slider slider = target.GetComponent<Slider>();
            PlayerPrefs.SetFloat("textSpeed", slider.value);
        }

        public void SetSaveOnQuit(GameObject target)
        {
            Toggle toggle = target.GetComponent<Toggle>();
            PlayerPrefs.SetInt("saveOnQuit", toggle.isOn == true ? 1 : 0);
        }

    }
}
