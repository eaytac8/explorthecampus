using UnityEngine;
using System.Collections;

namespace ExplorTheCampus
{
    public class Menu : MonoBehaviour
    {
        void OnEnable()
        {
            GameManager.instance.ShowControl(false);
            GameManager.instance.AllowPlayerMovement(false);
        }

        void OnDisable()
        {
            GameManager.instance.ShowControl(true);
            GameManager.instance.AllowPlayerMovement(true);
        }
    }
}
