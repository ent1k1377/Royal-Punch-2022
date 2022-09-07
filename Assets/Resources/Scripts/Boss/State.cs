using UnityEngine;

namespace Resources.Scripts.Boss
{
    public abstract class State
    {
        protected Animator _animator;
        
        public abstract void Enter();
        public abstract void Exit();

        public virtual void Update()
        {
        }
    }
}