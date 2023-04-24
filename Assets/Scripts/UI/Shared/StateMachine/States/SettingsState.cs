namespace UnityCraft.UI.Shared.StateMachine.States
{
    using MainMenu.StateMachine.States;
    using Views;

    public class SettingsState : MainMenuBaseState<SettingsView>
    {
        public SettingsState(SettingsView view) : base(view) { }

        public override void PrepareState()
        {
            base.PrepareState();
            //View.x.onClick.AddListener(NavigateToSingleplayer);
            View.gameObject.SetActive(true);
        }

        public override void DestroyState()
        {
            base.DestroyState();
            //View.x.onClick.RemoveListener(NavigateToSingleplayer);
            View.gameObject.SetActive(false);
        }
    }
}