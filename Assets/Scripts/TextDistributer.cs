using UnityEngine;
using System.Collections;
using CnControls;

namespace ExplorTheCampus {

    public class TextDistributer : MonoBehaviour {

        public TextAsset textFile;
        public TextBoxManager textBoxManager;

        public int startFromLine;
        public int endAtLine;

        public bool startOnButtonPress; //You cannot walk
        public bool closeOnLeave; //You can walk
        public bool destroyWhenFinished;
        private bool waitForPress;

        void Start()
        {
            textBoxManager = FindObjectOfType<TextBoxManager>();
            if (startOnButtonPress)
            {
                closeOnLeave = false;
            }
            if (closeOnLeave)
            {
                startOnButtonPress = false;
            }
        }

        void Update()
        {
            if (waitForPress && CnInputManager.GetButtonUp("Jump"))
            {
                GameManager.instance.AllowPlayerMovement(false);
                startPrintingText();
                waitForPress = false;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Collision");
            if (other.tag == "Player")
            {
                if (startOnButtonPress)
                {
                    waitForPress = true;
                    return;
                }
                if (!startOnButtonPress && !closeOnLeave)
                {
                    GameManager.instance.AllowPlayerMovement(false);
                }
                startPrintingText();
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                waitForPress = false;

                if (closeOnLeave)
                {
                    textBoxManager.DisableTextBox();
                }
            }
        }

        public void startPrintingText()
        {
            textBoxManager.startFromLine = startFromLine;
            textBoxManager.endAtLine = endAtLine;
            textBoxManager.ReloadTextFile(textFile);
            textBoxManager.EnableTextBox();

            if (destroyWhenFinished)
            {
                Destroy(gameObject);
            }
        }
    }
}