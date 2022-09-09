using UnityEngine;

namespace Resources.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Boss.Boss _boss;
        [SerializeField] private float _fistAttackDistance;

        private Animator _animator;
        private readonly int _isArmsLengthName = Animator.StringToHash("IsArmsLength");
        
        private int _strength;
        private float GetDistanceToPlayer() => Vector3.Distance(_boss.transform.position, _player.transform.position);
        
        public bool IsArmsLength => GetDistanceToPlayer() <= _fistAttackDistance;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void Start()
        {
            _strength = _player.Strength;
        }

        private void Update()
        {
            _animator.SetBool(_isArmsLengthName, IsArmsLength);
        }
    }
}