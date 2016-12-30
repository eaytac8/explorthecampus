using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ExplorTheCampus {

    public class PauseMenu : MonoBehaviour
    {

        public Text scoreText;
        public Text semesterTex;
        public Text attemtsText;

        void OnEnable()
        {
            //scoreText.text = GameManager.instance.gameData.creditsValue + "";
        }
    }

}
