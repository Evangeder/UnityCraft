namespace UnityCraft.UI.MainMenu.StateMachine.States
{
    using MainMenu.Views;

    public class MultiplayerState : MainMenuBaseState<MultiplayerView>
    {
        public MultiplayerState(MultiplayerView view) : base(view) { }

        public override void PrepareState()
        {
            base.PrepareState();
            View.Return.onClick.AddListener(NavigateToMainMenu);
            View.gameObject.SetActive(true);
        }

        public override void DestroyState()
        {
            base.DestroyState();
            View.Return.onClick.RemoveListener(NavigateToMainMenu);
            View.gameObject.SetActive(false);
        }

        private void NavigateToMainMenu()
        {
            Owner.ChangeState(new MenuState(Owner.UIRoot.MenuView) { Owner = Owner });
        }
    }
}
