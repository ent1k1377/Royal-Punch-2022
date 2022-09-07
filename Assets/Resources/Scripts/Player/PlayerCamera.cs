using UnityEngine;

namespace Resources.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] public float _changeTime;
        
        private Animator _animator;

        private readonly int _toStartCameraHash = Animator.StringToHash("toStartCamera");
        private readonly int _toCameraInGameHash = Animator.StringToHash("toCameraInGame");
        private int _nextCameraPosition;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _nextCameraPosition = _toCameraInGameHash;
        }

        public float GetAnimationRunTime() => _animator.GetCurrentAnimatorStateInfo(0).length + _changeTime;

        public void ChangeTransform()
        {
            _animator.CrossFade(_nextCameraPosition, _changeTime);
            _nextCameraPosition = _nextCameraPosition == _toStartCameraHash ? _toCameraInGameHash : _toStartCameraHash;
        }
    }
}