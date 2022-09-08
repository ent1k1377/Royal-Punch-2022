using DG.Tweening;
using UnityEngine;

namespace Resources.Scripts.Boss.States
{
    public class DirectedAttackState : State
    {
        private readonly SpriteRenderer _attackZone;
        private readonly Renderer _attackZoneShader;
        private readonly ParticleSystem _smokeExplosionEffect;
        private readonly ParticleSystem _stripesEffect;
        private readonly Transform _positionStripesEffect;
        private readonly int _degreeAttackZone;
        private readonly Rotator _bossRotator;
        
        private readonly int _directedAttackHash = Animator.StringToHash("Armature_BossSuper3");
        private readonly int _degreeFieldName = Shader.PropertyToID("Field Of View (grad)");
        private readonly float _firstPhaseTime = 1f;

        public DirectedAttackState(Animator animator, IStationStateSwitcher stateSwitcher, 
            SpriteRenderer attackZone, Renderer attackZoneShader, 
            ParticleSystem smokeExplosionEffect, ParticleSystem stripesEffect, 
            Transform positionStripesEffect, int degreeAttackZone, Rotator bossRotator) : base(animator, stateSwitcher)
        {
            _attackZone = attackZone;
            _attackZoneShader = attackZoneShader;
            _smokeExplosionEffect = smokeExplosionEffect;
            _stripesEffect = stripesEffect;
            _positionStripesEffect = positionStripesEffect;
            _degreeAttackZone = degreeAttackZone;
            _bossRotator = bossRotator;
        }

        public override void Enter()
        {
            Debug.Log("Вошел в состояние Directed Attack");
            
            _animator.CrossFade(_directedAttackHash, 0.15f);
            _bossRotator.enabled = false;
            
            _stripesEffect.gameObject.SetActive(true);
            _stripesEffect.transform.SetParent(_positionStripesEffect.parent);
            _stripesEffect.transform.position = _positionStripesEffect.position;
            
            _attackZoneShader.material.SetFloat(_degreeFieldName, _degreeAttackZone);
        }

        public override void AnimationEventHandler(int index)
        {
            switch (index)
            {
                case 1:
                    _animator.speed = 0;
                    
                    var sequence = DOTween.Sequence();
                    sequence.AppendInterval(0.3f).AppendCallback(() =>
                        {
                            _attackZone.gameObject.SetActive(true);
                            _attackZone.transform.DOScale(Vector3.one * 6, _firstPhaseTime).
                                OnComplete(() => 
                                {
                                    _animator.speed = 1;
                                    _stripesEffect.gameObject.SetActive(false);
                                });
                        });
                    break;
                case 2:
                    _smokeExplosionEffect.gameObject.SetActive(true);
                    _attackZone.gameObject.SetActive(false);
                    break;
                case 3:
                    _stateSwitcher.SwitchState<TiredState>();
                    break;
            }
        }
        
        public override void Exit()
        {
            Debug.Log("Вышел из состояния Directed Attack");
            
            _bossRotator.enabled = true;
            _stripesEffect.gameObject.SetActive(false);
            _attackZone.transform.localScale = Vector3.one * 0.1f;
            _attackZone.gameObject.SetActive(false);
        }
    }
}