using UnityEngine;
using System.Collections;
using CnControls;

namespace ExplorTheCampus
{
    public class Introduction : MonoBehaviour
    {

        public TextBoxManager tbm;
        public Animator cameraAnimator;
        public Animator semesterShowAnimator;
        public Animator creditsShowAnimator;
        public Animator joystickShowAnimator;
        public Animator youShowAnimator;

        public TextDistributer secondChat;
        public TextDistributer thirdChat;
        public TextDistributer fifthChat;
        public TextDistributer sixthChat;
        private int sequence = 0;
        private bool hasClicked = false;

        GameObject player;
        PlayerController playerController;
        Transform playerTransform;

        // Use this for initialization
        void Start()
        {
            GameManager.instance.ShowControl(true);
            GameObject player = GameObject.Find("Player");
            playerController = player.GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (CnInputManager.GetButtonUp("Jump") && !tbm.IsReading() && !hasClicked && sequence == 0)
            {
                sequence++;
                hasClicked = true;
            }
            if (sequence == 1)
            {
                sequence++;
                cameraAnimator.SetBool("zoom_out", true);
                hasClicked = false;
                secondChat.startPrintingText();
                GameManager.instance.AllowPlayerMovement(false);
            }
            if (CnInputManager.GetButtonUp("Jump") && !tbm.IsReading() && !hasClicked && sequence == 2)
            {
                sequence++;
            }
            if (sequence == 3)
            {
                sequence++;
                cameraAnimator.SetBool("zoom_out", false);
                hasClicked = false;
                StartCoroutine(WaitForSeconds(2));
                GameManager.instance.AllowPlayerMovement(false);
            }
            if (sequence == 5)
            {
                sequence++;
                thirdChat.startPrintingText();
                GameManager.instance.ShowControl(true);
                joystickShowAnimator.SetBool("ready", true);
            }
            if (sequence == 6 && tbm.printedTexts == 4)
            {
                sequence++;
            }
            if (sequence == 7)
            {
                semesterShowAnimator.SetBool("ready", true);
                joystickShowAnimator.SetBool("ready", false);
                GameManager.instance.AllowPlayerMovement(false);
                sequence++;
            }
            if (CnInputManager.GetButtonUp("Jump") && !tbm.IsReading() && !hasClicked && sequence == 8)
            {
                sequence++;
                GameManager.instance.AllowPlayerMovement(false);
            }
            if (sequence == 9)
            {
                semesterShowAnimator.SetBool("ready", false);
                creditsShowAnimator.SetBool("ready", true);
                fifthChat.startPrintingText();
                GameManager.instance.AllowPlayerMovement(false);
                sequence++;
            }
            if (CnInputManager.GetButtonUp("Jump") && !tbm.IsReading() && !hasClicked && sequence == 10)
            {
                sequence++;
            }
            if (sequence == 11)
            {
                creditsShowAnimator.SetBool("ready", false);
                sixthChat.startPrintingText();
                youShowAnimator.SetBool("show_you", true);
                GameManager.instance.AllowPlayerMovement(false);
                sequence++;
            }
            if (CnInputManager.GetButtonUp("Jump") && !tbm.IsReading() && !hasClicked && sequence == 12)
            {
                sequence++;
            }
            if (sequence == 13)
            {
                youShowAnimator.SetBool("show_you", false);
                GameManager.instance.SaveGameData();
                StartCoroutine(SceneManager.instance.LoadScene(2));
                GameManager.instance.AllowPlayerMovement(false);
            }
            Debug.Log("Sequence " + sequence);
        }

        private IEnumerator WaitForSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            sequence++;
        }

        private IEnumerator WaitForAnimation(Animation animation)
        {
            do
            {
                yield return null;
            } while (animation.isPlaying);
        }

        public AnimationClip GetAnimationClip(Animator animator, string name)
        {
            if (!animator) return null; // no animator

            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == name)
                {
                    return clip;
                }
            }
            return null; // no clip by that name
        }
    }
}