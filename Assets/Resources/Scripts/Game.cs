using DG.Tweening;
using Resources.Scripts.Joystick;
using UnityEngine;

namespace Resources.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Player.PlayerCamera _playerCamera;
        [SerializeField] private JoystickHandler _joystick;
        [SerializeField] private Canvas _ui;

        public void StartGame()
        {
            _ui.gameObject.SetActive(false);
            _playerCamera.ChangeTransform();
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(_playerCamera.GetAnimationRunTime()).AppendCallback(() => _joystick.gameObject.SetActive(true));
        }
    }
}
