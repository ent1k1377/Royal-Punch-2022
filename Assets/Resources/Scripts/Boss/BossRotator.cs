using UnityEngine;

namespace Resources.Scripts.Boss
{
    public class BossRotator : MonoBehaviour
    {
        [SerializeField] private Player.Player _target;
        [SerializeField] private float _speed;

        private void Rotate()
        {
            var direction = _target.transform.position - transform.position;
            var newRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, _speed * Time.deltaTime);
        }

        private void Update()
        {
            Rotate();
        }
    }
}