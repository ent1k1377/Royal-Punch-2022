using Resources.Scripts.Joystick;
using UnityEngine;

namespace Resources.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;
        [SerializeField] private JoystickHandler _joystick;
        [SerializeField] private Canvas _ui;

        public void StartGame()
        {
            _joystick.gameObject.SetActive(true);
            _ui.gameObject.SetActive(false);
        }
        
        
    }
}
