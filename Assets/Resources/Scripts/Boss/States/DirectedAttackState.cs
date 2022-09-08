using UnityEngine;

namespace Resources.Scripts.Boss.States
{
    public class DirectedAttackState : State
    {
        public DirectedAttackState(Animator animator, IStationStateSwitcher stateSwitcher) : base(animator, stateSwitcher)
        {
        }

        public override void Enter()
        {
            Debug.Log("Вошел в состояние Directed Attack");
        }

        public override void Exit()
        {
            Debug.Log("Вышел из состояния Directed Attack");
        }
    }
}