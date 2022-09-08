namespace Resources.Scripts.Boss.States
{
    public interface IStationStateSwitcher
    {
        public void SwitchState<T>() where T : State;
    }
}