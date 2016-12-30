using UnityEngine;
using System.Collections;
using System;

namespace ExplorTheCampus
{
    public class Module : MonoBehaviour
    {
        [Tooltip("Anzahl an maximalen Credits (bei Note 1) für das Modul. Sollte zwischen 3 und 8 liegen.")]
        public int credits;
        private int id;
        private float mark;
        private float score;

        void Start()
        {
            this.id = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            GameManager.instance.MaxCredits += credits;
        }

        public float Mark
        {
            get
            {
                return mark;
            }
            set
            {
                if (value <= 5f && value >= 1f)
                {
                    mark = (float)Math.Round(value, 1);
                    calcScore();
                    Record record = new Record(id, mark, score);
                    ExplorTheCampus.GameManager.instance.AddRecord(record);
                }
            }
        }

        public float Score
        {
            get
            {
                return score;
            }
        }

        private void calcScore()
        {
            if (mark > 4f)
            {
                score = 0;
            }
            else if (mark <= 4f && mark > 1f)
            {
                score = (float)Math.Round(credits * (1 / mark), 1);
            }
            else
            {
                score = credits;
            }
        }

        void OnValidate()
        {
            Debug.Log("VKDMOWEC");
        }
    }
}