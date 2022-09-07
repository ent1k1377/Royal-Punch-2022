using Resources.Scripts.Joystick;
using UnityEngine;

namespace Resources.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private JoystickHandler _joystick;
        [SerializeField] private int _speed;
        
        private void Move(Vector3 direction)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }

        private void Update()
        {
            Move(_joystick.GetDirection());
        }
    }
}
