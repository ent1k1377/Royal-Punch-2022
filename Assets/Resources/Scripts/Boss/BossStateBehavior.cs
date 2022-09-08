using System.Collections.Generic;
using System.Linq;
using Resources.Scripts.Boss.States;
using UnityEngine;
using Random = System.Random;

namespace Resources.Scripts.Boss
{
    [RequireComponent(typeof(Animator))]
    public class BossStateBehavior : MonoBehaviour, IStationStateSwitcher
    {
        [SerializeField] private Player.Player _player;
        [SerializeField, Range(0.1f, 10f)] private float _timeBetweenStates;
        
        [Header("Fist Attack")]
        [SerializeField] private float _fistAttackDistance;
        
        [Header("AOE Attack")]
        [SerializeField] private SpriteRenderer _redCircle;
        [SerializeField] private ParticleSystem _stripesEffect;
        [SerializeField] private ParticleSystem _smokeExplosionEffect;
        
        private Animator _animator;
        private bool _isReadyChangeState = true;
        private bool _isSuperAttack = false;
        private float _currentTimeBetweenStates;

        private State _startState;
        private State _currentState;
        private List<State> _allStates;
        private List<State> _superAttacks;

        private bool _isArmsLength => GetDistanceToPlayer() <= _fistAttackDistance;

        private float GetDistanceToPlayer() => Vector3.Distance(transform.position, _player.transform.position);
        
        private void StatesInitialize()
        {
            _allStates = new List<State>
            {
                new IdleState(_animator, this),
                new TiredState(_animator, this),
                new FistAttackState(_animator,this),
                new DirectedAttackState(_animator,this),
                new AoeAttackState(_animator,this, this, _redCircle, _stripesEffect, _smokeExplosionEffect),
            };
            _superAttacks = new List<State> {_allStates[4]};
            _currentState = _allStates.FirstOrDefault(s => s is IdleState);
            _currentState.Enter();
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            StatesInitialize();
        }

        public void AnimationEvent(int index)
        {
            _currentState.AnimationEvent(index);
        }
        
        private void Update()
        {
            Debug.Log($"{_isArmsLength}");
            if (_isSuperAttack)
                return;
            
            if (_isReadyChangeState)
                _currentTimeBetweenStates += Time.deltaTime;
            
            if (_isArmsLength && _currentState is IdleState)
                SwitchState<FistAttackState>();
            else if (_currentTimeBetweenStates >= _timeBetweenStates && !_isArmsLength)
                RandomChoiceSuperAttacks();
            else if (_currentTimeBetweenStates < _timeBetweenStates && _isArmsLength)
                SwitchState<IdleState>();
        }

        private void RandomChoiceSuperAttacks()
        {
            _isSuperAttack = true;
            var random = new Random();
            var randomIndex = random.Next(0, _superAttacks.Count);
            _currentState.Exit();
            _currentState = _superAttacks[randomIndex];
            _currentState.Enter();
        }

        public void SwitchState<T>() where T : State
        {
            _currentState.Exit();
            _currentState = _allStates.FirstOrDefault(s => s is T);
            _currentState.Enter();
        }
    }
}