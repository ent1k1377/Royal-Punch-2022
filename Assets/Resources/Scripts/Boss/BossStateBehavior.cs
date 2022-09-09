using System.Collections.Generic;
using System.Linq;
using Resources.Scripts.Boss.States;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Resources.Scripts.Boss
{
    [RequireComponent(typeof(Animator))]
    public class BossStateBehavior : MonoBehaviour, IStationStateSwitcher
    {
        [Header("General Prefabs")]
        [SerializeField] private ParticleSystem _stripesEffect;
        
        [Header("Fist Attack")]
        [SerializeField] private float _fistAttackDistance;
        
        [Header("AOE Attack")]
        [SerializeField] private SpriteRenderer _aoeAttackZone;
        [SerializeField] private ParticleSystem _smokeExplosionEffectAoeAttack;
        [SerializeField] private Transform _positionStripesEffectAoeAttack;

        [Header("Directed Attack")]
        [SerializeField] private SpriteRenderer _directionalAttackZone;
        [SerializeField] private Renderer _directionalAttackZoneShader;
        [SerializeField] private ParticleSystem _smokeExplosionEffectDirectedAttack;
        [SerializeField] private Transform _positionStripesEffectDirectedAttack;
        [SerializeField, Range(1, 180)] private int _degreeAttackZone;
        [SerializeField] private Rotator _bossRotator;
        
        [Header("Player Pull")]
        [SerializeField] private Transform _positionStripesEffectPlayerPull;
        [SerializeField] private ParticleSystem _arrowsEffect;

        [Header("General Settings")]
        [SerializeField] private Player.Player _player;
        [SerializeField] private Vector2 _timeRangeBetweenStates;

        private Animator _animator;
        private bool _isSuperAttack;
        
        private float _timeBetweenStates;
        private float _currentTimeBetweenStates;

        private State _startState;
        private State _currentState;
        private List<State> _allActiveStates;
        private List<State> _superAttacks;
        
        private float GetDistanceToPlayer() => Vector3.Distance(transform.position, _player.transform.position);
        
        public bool IsArmsLength => GetDistanceToPlayer() <= _fistAttackDistance;
        
        private void StatesInitialize()
        {
            _allActiveStates = new List<State>
            {
                new IdleState(_animator, this),
                new TiredState(_animator, this),
                new FistAttackState(_animator,this),
                new DirectedAttackState(_animator,this, _directionalAttackZone, _directionalAttackZoneShader, 
                    _smokeExplosionEffectDirectedAttack, _stripesEffect, _positionStripesEffectDirectedAttack, 
                    _degreeAttackZone, _bossRotator),
                new AoeAttackState(_animator,this, _aoeAttackZone, _smokeExplosionEffectAoeAttack, 
                    _stripesEffect, _positionStripesEffectAoeAttack),
                new PlayerPullState(_animator, this, _arrowsEffect,_stripesEffect, 
                    _positionStripesEffectPlayerPull, this, _player),
                new SuperPunchState(_animator, this)
            };
            _superAttacks = new List<State> {_allActiveStates[5]};
            _currentState = _allActiveStates.FirstOrDefault(s => s is IdleState);
            _currentState.Enter();
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            StatesInitialize();
            _timeBetweenStates = Random.Range(_timeRangeBetweenStates.x, _timeRangeBetweenStates.y);
        }

        private void Update()
        {
            if (_isSuperAttack)
                return;
            _currentTimeBetweenStates += Time.deltaTime;
            
            if (IsArmsLength && _currentState is IdleState)
                SwitchState<FistAttackState>();
            else if (_currentTimeBetweenStates < _timeBetweenStates && IsArmsLength)
                SwitchState<IdleState>();
            else if (_currentTimeBetweenStates >= _timeBetweenStates && !IsArmsLength)
            {
                RandomChoiceSuperAttacks();
                _currentTimeBetweenStates = 0;
                _timeBetweenStates = Random.Range(_timeRangeBetweenStates.x, _timeRangeBetweenStates.y);
            }
        }

        private void RandomChoiceSuperAttacks()
        {
            _isSuperAttack = true;
            var randomIndex = Random.Range(0, _superAttacks.Count - 1);
            SwitchStatePlug(_superAttacks[randomIndex]);
        }

        public void SwitchState<T>() where T : State
        {
            var state = _allActiveStates.FirstOrDefault(s => s is T);
            if (state is IdleState)
                _isSuperAttack = false;
            SwitchStatePlug(state);
        }

        private void SwitchStatePlug(State state)
        {
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }
        
        private void AnimationEventHandler(int index)
        {
            _currentState.AnimationEventHandler(index);
        }
    }
}