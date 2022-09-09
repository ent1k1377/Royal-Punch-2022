using UnityEngine;

namespace Resources.Scripts.Boss.States
{
    public class SuperPunchState : State
    {
        private readonly int _superPunchAttackHash = Animator.StringToHash("Armature_BossSuper2");
        
        public SuperPunchState(Animator animator, IStationStateSwitcher stateSwitcher) : base(animator, stateSwitcher)
        {
        }

        public override void Enter()
        {
            Debug.Log("Вошел в состояние Super Punch");
            
            _animator.CrossFade(_superPunchAttackHash, 0.1f);
        }

        public override void Exit()
        {
            Debug.Log("Вышел из состояния Super Punch");
        }
        
        public override void AnimationEventHandler(int index)
        {
            switch (index)
            {
                case 1:
                    _stateSwitcher.SwitchState<TiredState>();
                    break;
            }
        }
    }
}