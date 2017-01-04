using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CnControls;

namespace ExplorTheCampus { 

    public class TextBoxManager : MonoBehaviour {

        public GameObject textBox;
        public Text textToPrint;
        public Image arrow;
        public string[] textLines;

        public int startFromLine;
        public int endAtLine;
        public bool isActive;
        private bool isTyping = false;
        private bool cancelTyping = false;
        public float typeSpeed = 0.1f;

        private GameObject[] inputs;
        [HideInInspector]
        public bool hasInputted = true;
        [HideInInspector]
        public bool readNextLine = true;

        void Awake()
        {
            inputs = GameObject.FindGameObjectsWithTag("Input");
            foreach (GameObject input in inputs)
            {
                input.SetActive(false);
            }
        } 

        void Start()
        {
            arrow.enabled = false;

            if (isActive)
            {
                EnableTextBox();
            }
            else
            {
                DisableTextBox();
            }
        }

        void Update()
        {

            if (!isActive || !hasInputted || !readNextLine)
            {
                return;
            }

            if (CnInputManager.GetButtonUp("Jump"))
            {
                if (!isTyping)
                {
                    startFromLine++;

                    if (startFromLine == endAtLine)
                    {
                        arrow.enabled = false;
                    }

                    if (startFromLine > endAtLine)
                    {
                        DisableTextBox();
                    } else
                    {
                        StartCoroutine(TextScroll(textLines[startFromLine]));
                    }
                } else if (isTyping && !cancelTyping)
                {
                    cancelTyping = true;
                }
            }

            
        }

        private IEnumerator TextScroll(string textLine)
        {
            isTyping = true;
            cancelTyping = false;
            int currentLetter = 0;
            textToPrint.text = "";

            while(isTyping && !cancelTyping && currentLetter < textLine.Length - 1)
            {
                if (textLine[currentLetter] == '[')
                {
                    textToPrint.text = "";
                    break;
                } else
                {
                    textToPrint.text += textLine[currentLetter];
                }
                currentLetter++;
                yield return new WaitForSeconds(typeSpeed);
            }
            if (textLine.StartsWith("["))
            {
                textToPrint.text = "";
                hasInputted = false;
                string inputFieldName = textLine.Substring(1, textLine.Length - 3);
                foreach (GameObject input in inputs)
                {
                    Debug.Log(inputFieldName);
                    if (input.name == inputFieldName)
                    {
                        input.SetActive(true);
                        input.GetComponent<InputField>().Select();
                    };
                }
            } else
            {
                textToPrint.text = textLine;
            }
            Debug.Log("Goes here");
            isTyping = false;
            cancelTyping = false;
        }

        public void DisableTextBox()
        {
            textBox.SetActive(false);
            isActive = false;
            GameManager.instance.AllowPlayerMovement(true);
        }

        public void EnableTextBox()
        {
            textBox.SetActive(true);
            isActive = true;
            if (!isTyping)
            {
                StartCoroutine(TextScroll(textLines[startFromLine]));
            }
            if (arrow != null)
            {
                arrow.enabled = true;
            }
        }

        public void ReloadTextFile(TextAsset theText)
        {
            if (theText != null)
            {
                textLines = new string[1];
                textLines = (theText.text.Split('\n'));
                if (endAtLine == 0)
                {
                    endAtLine = textLines.Length - 1;
                }
            }
        }
    }
}