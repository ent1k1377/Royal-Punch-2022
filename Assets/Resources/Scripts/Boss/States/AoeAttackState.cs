using UnityEngine;

namespace Resources.Scripts.Boss.States
{
    public class AoeAttackState : State
    {
        private SpriteRenderer _redCircle;
        private ParticleSystem _stripesEffect;
        private readonly int _aoeAttackHash = Animator.StringToHash("Armature_BossSuper5");
        
        public AoeAttackState(Animator animator, SpriteRenderer redCircle, ParticleSystem stripesEffect)
        {
            _animator = animator;
            _redCircle = redCircle;
            _stripesEffect = stripesEffect;
        }

        public override void Enter()
        {
            _animator.CrossFade(_aoeAttackHash, 0);
        }

        public override void Exit()
        {
            
        }
    }
}