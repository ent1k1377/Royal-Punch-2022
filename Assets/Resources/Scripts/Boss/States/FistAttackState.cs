using UnityEngine;

namespace Resources.Scripts.Boss.States
{
    public class FistAttackState : State
    {
        private readonly int _bossPunchHash = Animator.StringToHash("Armature_BossPunch");
        public FistAttackState(Animator animator)
        {
            _animator = animator;
        }

        public override void Enter()
        {
            _animator.CrossFade(_bossPunchHash, 0);
        }

        public override void Exit()
        {
            
        }
    }
}