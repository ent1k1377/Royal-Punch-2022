using UnityEngine;

namespace Resources.Scripts.Boss.States
{
    public class IdleState : State
    {
        private readonly int _idleHash = Animator.StringToHash("Armature_Idle2");
        
        public IdleState(Animator animator) : base(animator)
        {
        }

        public override void Enter()
        {
            _animator.CrossFade(_idleHash, 0);
        }

        public override void Exit()
        {
            
        }
    }
}