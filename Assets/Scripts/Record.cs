using UnityEngine;
using System.Collections;
using System;

namespace ExplorTheCampus
{
    /// <summary>
    /// The Record class allows to store all attributes of a played minigame.
    /// </summary>
    [System.Serializable]
    public class Record
    {

        private int gameId;
        private float score;
        private float mark;
        private DateTime timeStemp;
        private bool missed = false;

        public Record(int gameId, float mark, float score)
        {
            this.gameId = gameId;
            if (mark > 4f)
            {
                missed = true;
            }
            this.timeStemp = DateTime.Now;
            this.mark = mark;
            this.score = score;
        }

        public DateTime TimeStemp
        {
            get
            {
                return timeStemp;
            }
        }

        public float Mark
        {
            get
            {
                return mark;
            }
        }

        public bool Missed
        {
            get
            {
                return missed;
            }
        }

        public float Score
        {
            get
            {
                return score;
            }
        }

        public int GameId
        {
            get
            {
                return gameId;
            }
        }

    }

}