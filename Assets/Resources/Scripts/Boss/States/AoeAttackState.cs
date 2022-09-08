using DG.Tweening;
using UnityEngine;

namespace Resources.Scripts.Boss.States
{
    public class AoeAttackState : State
    {
        private BossStateBehavior _bossState;
        private SpriteRenderer _redCircle;
        private ParticleSystem _stripesEffect;
        private ParticleSystem _smokeExplosionEffect;
        
        private readonly int _aoeAttackHash = Animator.StringToHash("Armature_BossSuper5");
        private float _firstPhaseTime = 1.5f;
        
        public AoeAttackState(Animator animator, IStationStateSwitcher stateSwitcher, BossStateBehavior bossState, 
            SpriteRenderer redCircle, ParticleSystem stripesEffect, ParticleSystem smokeExplosionEffect) : base(animator, stateSwitcher)
        {
            _bossState = bossState;
            _redCircle = redCircle;
            _stripesEffect = stripesEffect;
            _smokeExplosionEffect = smokeExplosionEffect;
        }

        public override void Enter()
        {
            Debug.Log("Вошел в состояние AOE Attack");
            
            _animator.CrossFade(_aoeAttackHash, 0.15f);
            
            _redCircle.gameObject.SetActive(true);
            _stripesEffect.gameObject.SetActive(true);

            _redCircle.transform.DOScale(Vector3.one * 10, _firstPhaseTime).
                OnComplete(() => 
                {
                    _animator.speed = 1;
                    _stripesEffect.gameObject.SetActive(false);
                });
        }

        public override void Exit()
        {
            Debug.Log("Вышел из состояния AOE Attack");

            _redCircle.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            _redCircle.gameObject.SetActive(false);
            _stripesEffect.gameObject.SetActive(false);
            _smokeExplosionEffect.gameObject.SetActive(false);
        }

        public override void AnimationEvent(int index)
        {
            switch (index)
            {
                case 1:
                    _animator.speed = 0;
                    break;
                case 2:
                    _redCircle.gameObject.SetActive(false);
                    _smokeExplosionEffect.gameObject.SetActive(true);
                    break;
                case 3:
                    _stateSwitcher.SwitchState<TiredState>();
                    break;
            }
        }
    }
}