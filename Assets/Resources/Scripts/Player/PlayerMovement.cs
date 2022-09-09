using Resources.Scripts.Joystick;
using UnityEngine;

namespace Resources.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private JoystickHandler _joystick;
        [SerializeField] private int _speed;

        private Animator _animator;
        
        private readonly int _runHorizontal = Animator.StringToHash("RunHorizontal");
        private readonly int _runVertical = Animator.StringToHash("RunVertical");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Move(Vector3 direction)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        private void ChangeParametersAnimation(Vector3 direction)
        {
            _animator.SetFloat(_runHorizontal, -direction.x);
            _animator.SetFloat(_runVertical, direction.z);
        }

        private void Update()
        {
            Move(_joystick.GetDirection());
            ChangeParametersAnimation(_joystick.GetDirection());
        }
    }
}
