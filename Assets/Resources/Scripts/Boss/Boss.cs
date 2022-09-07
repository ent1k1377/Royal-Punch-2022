using Resources.Scripts.Boss.States;
using UnityEngine;
using Random = System.Random;

namespace Resources.Scripts.Boss
{
    [RequireComponent(typeof(Animator))]
    public class Boss : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;
        
        [Header("Fist Attack")]
        [SerializeField] private float _fistAttackDistance;
        
        [Header("AOE Attack")]
        [SerializeField] private SpriteRenderer _redCircle;
        [SerializeField] private ParticleSystem _stripesEffect;
        
        private Animator _animator;
        private StateMachine _stateMachine;

        private State _idle;
        private State _fistAttack;
        private State _directedAttack;
        private State _aoeAttack;
        private State[] _superAttacks;

        private bool _isArmsLength => GetDistanceToPlayer() <= _fistAttackDistance;

        private float GetDistanceToPlayer() => Vector3.Distance(transform.position, _player.transform.position);
        
        private void InitializeAllState()
        {
            _idle = new IdleState(_animator);
            _fistAttack = new FistAttackState(_animator);
            _directedAttack = new DirectedAttackState();
            _aoeAttack = new AoeAttackState(_animator, _redCircle, _stripesEffect);
            _superAttacks = new[] {_directedAttack, _aoeAttack};
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            InitializeAllState();
        }

        private void Start()
        {
            _stateMachine = new StateMachine();
            _stateMachine.Initialize(_idle);
        }

        private void Update()
        {
            Debug.Log($"{GetDistanceToPlayer()} || {_stateMachine.CurrentState}");
            if (_isArmsLength && _stateMachine.CurrentState is IdleState)
                _stateMachine.ChangeState(_fistAttack);
            if (!_isArmsLength && _stateMachine.CurrentState is IdleState)
                _stateMachine.ChangeState(RandomChoiceSuperAttacks());
        }

        private State RandomChoiceSuperAttacks()
        {
            var random = new Random();
            var randomIndex = random.Next(0, _superAttacks.Length);
            return _superAttacks[randomIndex];
        }
    }
}