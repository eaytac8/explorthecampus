using UnityEngine;
using System.Collections;

namespace ExplorTheCampus
{
    /// <summary>
    /// This Script is used to initialize the players position on the map.
    /// This could be done after the player leaves a building or (re)starts the game.
    /// </summary>
    public class Initializer : MonoBehaviour
    {

        /// <summary>
        /// Position the player and the camera depending on the last scene.
        /// </summary>
        void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Transform playerTransform = player.GetComponent<Transform>();
            Animator playerAnimator = player.GetComponent<Animator>();
            playerTransform.position = GameManager.instance.PersistedPlayerPosition;
            playerAnimator.SetFloat("input_x", GameManager.instance.PersistedPlayerDirection[0]);
            playerAnimator.SetFloat("input_y", GameManager.instance.PersistedPlayerDirection[1]);
            
            if (SceneManager.instance.lastScene != null && SceneManager.instance.lastScene != "" && SceneManager.instance.lastScene != "Campus")
            {
                Vector3 lastPlayerPosition = GameManager.instance.LastPlayerPosition;
                float[] lastPlayerDirection = GameManager.instance.LastPlayerDirection;
                Vector3 newPlayerPosition = Vector3.down;
                float[] newPlayerDirection = new float[2];

                if (lastPlayerDirection[1] > 0) //Up -> Down
                {
                    newPlayerPosition = new Vector3(lastPlayerPosition.x, lastPlayerPosition.y - 0.01f, lastPlayerPosition.z);
                    newPlayerDirection[1] = -1;
                }
                else if (lastPlayerDirection[1] < 0) //Down -> Up
                {
                    newPlayerPosition = new Vector3(lastPlayerPosition.x, lastPlayerPosition.y + 0.01f, lastPlayerPosition.z);
                    newPlayerDirection[1] = 1;
                }
                else if (lastPlayerDirection[0] > 0) //Right -> Left
                {
                    newPlayerPosition = new Vector3(lastPlayerPosition.x - 0.01f, lastPlayerPosition.y, lastPlayerPosition.z);
                    newPlayerDirection[0] = -1;
                }   
                else //Left -> Right
                {
                    newPlayerPosition = new Vector3(lastPlayerPosition.x + 0.01f, lastPlayerPosition.y, lastPlayerPosition.z);
                    newPlayerDirection[0] = 1;
                }
                playerTransform.position = newPlayerPosition;
                playerAnimator.SetFloat("input_x", newPlayerDirection[0]);
                playerAnimator.SetFloat("input_y", newPlayerDirection[1]);
            }

            Camera.main.transform.position = playerTransform.position;
        }
    }

}