using UnityEngine;

namespace Resources.Scripts.Boss
{
    public abstract class State
    {
        protected readonly Animator _animator;
        
        protected State(Animator animator)
        {
            _animator = animator;
        }
        
        public abstract void Enter();
        public abstract void Exit();

        public virtual void Update()
        {
        }
    }
}