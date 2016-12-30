using UnityEngine;
using UnityEngine.EventSystems;

namespace CnControls
{
    /// <summary>
    /// Simple button class
    /// Handles press, hold and release, just like a normal button
    /// </summary>
    public class SimpleButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        private RectTransform buttonTransform;

        private SimpleJoystick simpleJoystickScript;

        private Vector2 buttonAnchoredPos;
        private float ratioWidth;
        private float ratioAnchPosX;
        private float ratioAnchPosY;
        private int referenceScreenHeight;

        /// <summary>
        /// The name of the button
        /// </summary>
        public string ButtonName = "Jump";

        /// <summary>
        /// Utility object that is registered in the system
        /// </summary>
        private VirtualButton _virtualButton;
        
        /// <summary>
        /// It's pretty simple here
        /// When we enable, we register our button in the input system
        /// </summary>
        private void OnEnable()
        {
            _virtualButton = _virtualButton ?? new VirtualButton(ButtonName);
            CnInputManager.RegisterVirtualButton(_virtualButton);
        }

        /// <summary>
        /// When we disable, we unregister our button
        /// </summary>
        private void OnDisable()
        {
            CnInputManager.UnregisterVirtualButton(_virtualButton);
        }

        /// <summary>
        /// uGUI Event system stuff
        /// It's also utilised by the editor input helper
        /// </summary>
        /// <param name="eventData">Data of the passed event</param>
        public void OnPointerUp(PointerEventData eventData)
        {
            _virtualButton.Release();
        }

        /// <summary>
        /// uGUI Event system stuff
        /// It's also utilised by the editor input helper
        /// </summary>
        /// <param name="eventData">Data of the passed event</param>
        public void OnPointerDown(PointerEventData eventData)
        {
            _virtualButton.Press();
        }

        void Awake()
        {
            buttonTransform = gameObject.GetComponent(typeof(RectTransform)) as RectTransform;
            buttonAnchoredPos = buttonTransform.anchoredPosition;
            simpleJoystickScript = FindObjectOfType<SimpleJoystick>();
            referenceScreenHeight = simpleJoystickScript.referenceScreenHeight;

            Vector2 buttonTransformPos = buttonTransform.sizeDelta;
            ratioWidth = buttonTransformPos.x / referenceScreenHeight;
            ratioAnchPosX = buttonAnchoredPos.x / referenceScreenHeight;
            ratioAnchPosY = buttonAnchoredPos.y / referenceScreenHeight;
        }

        void Update()
        {

            int screenSize = Screen.height;

            if (Screen.orientation == ScreenOrientation.Portrait)
            {
                screenSize = Screen.width;
            }

            float sizeButton = screenSize * ratioWidth;
            float anchoredPosX = screenSize * ratioAnchPosX;
            float anchoredPosY = screenSize * ratioAnchPosY;

            buttonTransform.sizeDelta = new Vector2(sizeButton, sizeButton);
            buttonTransform.anchoredPosition = new Vector2(anchoredPosX, anchoredPosY);
        }
    }
}