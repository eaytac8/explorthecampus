using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using CnControls;

namespace ExplorTheCampus
{

    /// <summary>
    /// This script is used to control the player.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {

        private Animator animator;
        private Rigidbody2D rb2D;
        private float inverseMoveTime;

        public float moveTime = 0.1f;
        public bool allowDiagonalMove = false;
        private bool allowedToMove = true;

        public AudioClip moveSound1;
        public AudioClip moveSound2;

        public bool AllowedToMove
        {
            get
            {
                return allowedToMove;
            }
            set
            {
                allowedToMove = value;
                if (value == false)
                {
                    animator.SetBool("is_walking", false);
                }
                
            }
        }

        void Start()
        {
            animator = GetComponent<Animator>();
            rb2D = GetComponent<Rigidbody2D>();
            rb2D.freezeRotation = true;
            inverseMoveTime = 1f / moveTime;
        }

        void Update()
        {
            if (!allowedToMove)
            {
                return;
            }

            float horizontal = 0.0f;
            float vertical = 0.0f;

#if UNITY_STANDALONE || UNITY_WEBPLAYER
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
#else
            horizontal = CnInputManager.GetAxisRaw("Horizontal");
            vertical = CnInputManager.GetAxisRaw("Vertical");
#endif
            if (allowDiagonalMove == false)
            {
                if (horizontal > 0.5 || horizontal < -0.5)
                {
                    vertical = 0.0f;
                }
                else
                {
                    horizontal = 0.0f;
                }
            }

            Vector2 start = transform.position;
            Vector2 movementVector = new Vector2(horizontal, vertical);
            Vector2 end = start + movementVector;

            if (movementVector != Vector2.zero)
            {
                animator.SetBool("is_walking", true);
                SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
                animator.SetFloat("input_x", movementVector.x);
                animator.SetFloat("input_y", movementVector.y);
            }
            else
            {
                animator.SetBool("is_walking", false);
            }

            Vector2 newPostion = Vector2.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPostion);
        }
    }
}