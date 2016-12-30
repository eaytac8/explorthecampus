using UnityEngine;
using System.Collections;

namespace ExplorTheCampus
{
    public class CollisionDetection : MonoBehaviour
    {

        //Detect collisions with other objects like bonus items
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Exit")
            {
                //StartCoroutine(SceneManager.instance.LoadScene(1));
                //SceneManager.instance.LoadScene(1);
                GameManager.instance.LoadRandomMinigame();
            }
        }
    }
}
