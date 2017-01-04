using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ExplorTheCampus
{
    public class EnterNameScript : MonoBehaviour
    {

        public TextBoxManager textBoxManager;
        private InputField input;

        void Start()
        {
            input = gameObject.GetComponent<InputField>();
            var se = new InputField.SubmitEvent();
            se.AddListener(SubmitName);
            input.onEndEdit = se;
            //or simply use the line below, 
            //input.onEndEdit.AddListener(SubmitName);  // This also works
        }

        private void SubmitName(string arg0)
        {
            if (arg0.Trim().Length >= 3)
            {
                GameManager.instance.UserName = arg0;
                textBoxManager.hasInputted = true;
                gameObject.SetActive(false);
            }
        }
    }
}