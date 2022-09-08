using UnityEngine;

namespace Resources.Scripts.Boss.States
{
    public class IdleState : State
    {
        private readonly int _idleHash = Animator.StringToHash("Armature_Idle2");
        
        public IdleState(Animator animator, IStationStateSwitcher stateSwitcher) : base(animator, stateSwitcher)
        {
        }

        public override void Enter()
        {
            Debug.Log("Вошел в состояние Idle");
            _animator.CrossFade(_idleHash, 0.1f);
        }

        public override void Exit()
        {
            Debug.Log("Вышел из состояния Idle");
        }
    }
}