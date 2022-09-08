using DG.Tweening;
using UnityEngine;

namespace Resources.Scripts.Boss.States
{
    public class AoeAttackState : State
    {
        private readonly SpriteRenderer _attackZone;
        private readonly ParticleSystem _smokeExplosionEffect;
        private readonly ParticleSystem _stripesEffect;
        private readonly Transform _positionStripesEffect;
        
        private readonly int _aoeAttackHash = Animator.StringToHash("Armature_BossSuper5");
        private readonly float _firstPhaseTime = 1.5f;
        
        public AoeAttackState(Animator animator, IStationStateSwitcher stateSwitcher, SpriteRenderer attackZone, 
            ParticleSystem smokeExplosionEffect, ParticleSystem stripesEffect, 
            Transform positionStripesEffect) : base(animator, stateSwitcher)
        {
            _attackZone = attackZone;
            _smokeExplosionEffect = smokeExplosionEffect;
            _stripesEffect = stripesEffect;
            _positionStripesEffect = positionStripesEffect;
        }

        public override void Enter()
        {
            Debug.Log("Вошел в состояние AOE Attack");
            
            _animator.CrossFade(_aoeAttackHash, 0.15f);
            
            _attackZone.gameObject.SetActive(true);
            _stripesEffect.gameObject.SetActive(true);
            _stripesEffect.transform.SetParent(_positionStripesEffect.parent);
            _stripesEffect.transform.position = _positionStripesEffect.position;

            _attackZone.transform.DOScale(Vector3.one * 10, _firstPhaseTime).
                OnComplete(() => 
                {
                    _animator.speed = 1;
                    _stripesEffect.gameObject.SetActive(false);
                });
        }

        public override void Exit()
        {
            Debug.Log("Вышел из состояния AOE Attack");

            _attackZone.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            _attackZone.gameObject.SetActive(false);
            _stripesEffect.gameObject.SetActive(false);
            _smokeExplosionEffect.gameObject.SetActive(false);
        }

        public override void AnimationEventHandler(int index)
        {
            switch (index)
            {
                case 1:
                    _animator.speed = 0;
                    break;
                case 2:
                    _attackZone.gameObject.SetActive(false);
                    _smokeExplosionEffect.gameObject.SetActive(true);
                    break;
                case 3:
                    _stateSwitcher.SwitchState<TiredState>();
                    break;
            }
        }
    }
}