namespace UnityCraft.UI.MainMenu.StateMachine.States
{
    using Patterns.StateMachine;

    public abstract class MainMenuBaseState<T> : BaseState where T : BaseView
    {
        public MainMenuBaseState(T view)
        {
            View = view;
        }

        public new MainMenuStateMachine Owner;
        public readonly T View;
    }
}