using UniRx;
using UnityEngine;

namespace Resources.Scripts.Boss.States
{
    public class PlayerPullState : State
    {
        private readonly ParticleSystem _arrowsEffect;
        private readonly ParticleSystem _stripesEffect;
        private readonly Transform _positionStripesEffect;
        private readonly BossStateBehavior _bossStateBehavior;
        private readonly Player.Player _player;
        
        private readonly float _pullTime = 2.5f;
        private readonly float _pullSpeed = 2f;
        private readonly int _pullPlayerHash = Animator.StringToHash("Armature_BossSuper6");

        private readonly CompositeDisposable _disposable = new();
        
        public PlayerPullState(Animator animator, IStationStateSwitcher stateSwitcher, ParticleSystem arrowsEffect, 
            ParticleSystem stripesEffect, Transform positionStripesEffect, BossStateBehavior bossStateBehavior, 
            Player.Player player) : base(animator, stateSwitcher)
        {
            _arrowsEffect = arrowsEffect;
            _stripesEffect = stripesEffect;
            _positionStripesEffect = positionStripesEffect;
            _bossStateBehavior = bossStateBehavior;
            _player = player;
        }

        public override void Enter()
        {
            Debug.Log("Вошел в состояние Player Pull");

            _animator.CrossFade(_pullPlayerHash, 0.15f);
            _stripesEffect.gameObject.SetActive(true);
            _stripesEffect.transform.position = _positionStripesEffect.position;
            _arrowsEffect.gameObject.SetActive(true);

            var pastTime = 0f;
            
            Observable.EveryUpdate().Subscribe( _ =>
            {
                pastTime += Time.deltaTime;
                
                if (_bossStateBehavior.IsArmsLength)
                {
                    _disposable.Clear();
                    _stateSwitcher.SwitchState<SuperPunchState>();
                }
                if (pastTime >= _pullTime)
                {
                    _disposable.Clear();
                    _stateSwitcher.SwitchState<TiredState>();
                }
                _player.transform.position = Vector3.MoveTowards(_player.transform.position, 
                    _bossStateBehavior.transform.position, _pullSpeed * Time.deltaTime);
                
            }).AddTo(_disposable);
        }

        public override void Exit()
        {
            Debug.Log("Вышел из состояния Player Pull");
            
            _stripesEffect.gameObject.SetActive(false);
            _arrowsEffect.gameObject.SetActive(false);
        }
    }
}