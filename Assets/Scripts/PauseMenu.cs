using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ExplorTheCampus {

    public class PauseMenu : MonoBehaviour
    {
        public Text scoreText;

        void OnEnable()
        {
            GameManager.instance.ShowControl(false);
            scoreText.text = GameManager.instance.Credits 
                + "\n" + GameManager.instance.Semester + "/" + GameManager.instance.maxAmountSemester
                + "\n" + GameManager.instance.Attempts;
        }
    }

}
