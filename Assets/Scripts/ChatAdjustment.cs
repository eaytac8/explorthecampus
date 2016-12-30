using UnityEngine;
using System.Collections;

namespace ExplorTheCampus
{
    public class ChatAdjustment : MonoBehaviour
    {

        private RectTransform chat;

        // Use this for initialization
        void Start()
        {
            chat = gameObject.GetComponent(typeof(RectTransform)) as RectTransform;
            Vector2 rectSize = chat.sizeDelta;
            chat.sizeDelta = new Vector2(rectSize.x, Screen.height * 0.25f);
        }
    }
}