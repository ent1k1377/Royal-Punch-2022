using UnityEngine;

namespace Resources.Scripts.Boss.States
{
    public class FistAttackState : State
    {
        private readonly int _bossPunchHash = Animator.StringToHash("Armature_BossPunch");
        
        public FistAttackState(Animator animator, IStationStateSwitcher stateSwitcher) : base(animator, stateSwitcher)
        {
        }

        public override void Enter()
        {
            Debug.Log("Вошел в состояние Fist Attack");
            _animator.CrossFade(_bossPunchHash, 0);
        }

        public override void Exit()
        {
            Debug.Log("Вышел из состояния Fist Attack");
        }
    }
}