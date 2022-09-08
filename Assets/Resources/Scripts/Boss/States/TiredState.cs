using UnityEngine;
using UniRx;

namespace Resources.Scripts.Boss.States
{
    public class TiredState : State
    {
        private CompositeDisposable _disposable = new ();
        private readonly int _restHash = Animator.StringToHash("Armature_BossTired");
        private readonly float _timeTired = 1.5f;
        
        public TiredState(Animator animator, IStationStateSwitcher stateSwitcher) : base(animator, stateSwitcher)
        {
        }

        public override void Enter()
        {
            Debug.Log("Вошел в состояние Tired");
            
            _animator.CrossFade(_restHash, 0.15f);

            var currentTimeTired = 0f;
            Observable.EveryUpdate().Subscribe(_ =>
            {
                currentTimeTired += Time.deltaTime;
                if (currentTimeTired >= _timeTired)
                {
                    _disposable.Clear();
                    _stateSwitcher.SwitchState<IdleState>();
                }
            }).AddTo(_disposable);
        }

        public override void Exit()
        {
            Debug.Log("Вышел из состояния Tired");
        }
    }
}