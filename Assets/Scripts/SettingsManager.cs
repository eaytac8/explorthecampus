using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ExplorTheCampus
{
    public class SettingsManager : MonoBehaviour
    {
        private const string BACKGROUND_MUSIC_VOLUME = "backgroundMusicVolume";
        private const string EFFECTS_VOLUME = "effectsVolume";
        private const string TEXT_SPEED = "textSpeed";
        private const string SAVE_ON_QUIT = "saveOnQuit";
        private const string SETTINGS_INITIALIZED = "settingsInitialized";
 
        public Slider backgroundMusicVolumeSlider;
        public Slider effectsSlider;
        public Slider textSpeedSlider;
        public Toggle saveOnQuitToggle;

        public float defaultBackgroundMusicVolume = 1f;
        public float defaultEffectsVolume = 0.5f;
        public float defaultTypeSpeed = 0.1f;
        public bool defaultSetSaveOnQuit = true;

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

            Initialize();
        }

        public bool GetSaveOnQuit()
        {
            return PlayerPrefs.GetInt(SAVE_ON_QUIT) == 1 ? true : false;
        }

        public float GetBackgroundMusicVolume()
        {
            return PlayerPrefs.GetFloat(BACKGROUND_MUSIC_VOLUME);
        }

        public float GetEffectsVolume()
        {
            return PlayerPrefs.GetFloat(EFFECTS_VOLUME);
        }

        public float GetTextSpeed()
        {
            return PlayerPrefs.GetFloat(TEXT_SPEED);
        }

        public void SetBackgroundMusicVolume()
        {
            if (backgroundMusicVolumeSlider)
            {
                PlayerPrefs.SetFloat(BACKGROUND_MUSIC_VOLUME, backgroundMusicVolumeSlider.value);
            }
        }

        public void SetEffectsVolume()
        {   if (effectsSlider)
            {
                PlayerPrefs.SetFloat(EFFECTS_VOLUME, effectsSlider.value);
            }
        }

        public void SetTextSpeed()
        {
            if (textSpeedSlider)
            {
                PlayerPrefs.SetFloat(TEXT_SPEED, textSpeedSlider.value);
            }
        }

        public void SetSaveOnQuit()
        {
            if (saveOnQuitToggle)
            {
                PlayerPrefs.SetInt(SAVE_ON_QUIT, saveOnQuitToggle.isOn == true ? 1 : 0);
            }
        }

        public void OpenSettings(bool open)
        {
            Debug.Log(open);
            gameObject.transform.GetChild(0).gameObject.SetActive(open);
        }

        private void Initialize()
        {
            if (!IsInitialized())
            {
                backgroundMusicVolumeSlider.value = defaultBackgroundMusicVolume;
                effectsSlider.value = defaultEffectsVolume;
                textSpeedSlider.value = defaultTypeSpeed;
                saveOnQuitToggle.isOn = defaultSetSaveOnQuit;
                PlayerPrefs.SetInt(SETTINGS_INITIALIZED, 1);
            }
            else
            {
                backgroundMusicVolumeSlider.value = GetBackgroundMusicVolume();
                effectsSlider.value = GetEffectsVolume();
                textSpeedSlider.value = GetTextSpeed();
                saveOnQuitToggle.isOn = GetSaveOnQuit();
            }
        }

        private bool IsInitialized()
        {
            return PlayerPrefs.GetInt(SETTINGS_INITIALIZED) == 0 ? false : true;
        }

        public void ResetSettings()
        {
            PlayerPrefs.SetInt(SETTINGS_INITIALIZED, 0);
            Initialize();
        }

        private void ResetGame()
        {
            GameManager.instance.ResetGame();
        }


    }
}
