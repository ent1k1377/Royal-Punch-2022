using UnityEngine;

namespace Resources.Scripts.Boss
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private float _speed;

        private void Rotate()
        {
            var direction = _target.transform.position - transform.position;
            var newRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, _speed * Time.deltaTime);
        }

        private void Update()
        {
            Rotate();
        }
    }
}