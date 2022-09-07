using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Resources.Scripts.Joystick
{
    public abstract class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _joystickBackground;
        [SerializeField] private Image _joystick;
        [SerializeField] private Image _joystickArea;

        private Vector2 _joystickBackgroundStartPosition;

        protected Vector2 InputVector;

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, 
                    eventData.position, null, out var joystickPosition))
            {
                var sizeDelta = _joystickBackground.rectTransform.sizeDelta;
                joystickPosition.x = joystickPosition.x * 2 / sizeDelta.x;
                joystickPosition.y = joystickPosition.y * 2 / sizeDelta.y;

                InputVector = new Vector2(joystickPosition.x, joystickPosition.y);
                InputVector = InputVector.magnitude > 1 ? InputVector.normalized : InputVector;

                var delta = _joystickBackground.rectTransform.sizeDelta;
                _joystick.rectTransform.anchoredPosition = new Vector2(InputVector.x * (delta.x / 2), 
                                                                        InputVector.y * (delta.y / 2));
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ChangeState(true);
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArea.rectTransform, 
                    eventData.position, null, out var joystickBackgroundPosition))
            {
                _joystickBackground.rectTransform.anchoredPosition = joystickBackgroundPosition;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ChangeState(false);
            InputVector = Vector2.zero;
            _joystick.rectTransform.anchoredPosition = Vector2.zero;
        }

        private void ChangeState(bool flag)
        {
            _joystickBackground.gameObject.SetActive(flag);
        }
    }
}
