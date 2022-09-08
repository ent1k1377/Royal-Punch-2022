using UnityEngine;

namespace Resources.Scripts.Boss.States
{
    public abstract class State
    {
        protected readonly Animator _animator;
        protected readonly IStationStateSwitcher _stateSwitcher;
        
        protected State(Animator animator, IStationStateSwitcher stateSwitcher)
        {
            _animator = animator;
            _stateSwitcher = stateSwitcher;
        }
        
        public abstract void Enter();
        public abstract void Exit();

        public virtual void AnimationEventHandler(int index)
        {
        }
    }
}