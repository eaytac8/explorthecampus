﻿using UnityEngine;
using System.Collections;


namespace ExplorTheCampus
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance = null;

        public AudioSource music;

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

        public void PlaySingle(AudioClip clip)
        {

        }
    }

}