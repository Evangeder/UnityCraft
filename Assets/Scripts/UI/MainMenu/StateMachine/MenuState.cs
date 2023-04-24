namespace UnityCraft.UI.MainMenu.StateMachine.States
{
    using Shared.StateMachine.States;
    using MainMenu.Views;
    using UnityEngine;

    public class MenuState : MainMenuBaseState<MenuView>
    {
        public MenuState(MenuView view) : base(view) { }

        public override void PrepareState()
        {
            base.PrepareState();
            View.Singleplayer.onClick.AddListener(NavigateToSingleplayer);
            View.Multiplayer.onClick.AddListener(NavigateToMultiplayer);
            View.Settings.onClick.AddListener(NavigateToSettings);
            View.QuitButton.onClick.AddListener(ExitApplication);
            View.gameObject.SetActive(true);
        }

        public override void DestroyState()
        {
            base.DestroyState();
            View.Singleplayer.onClick.RemoveListener(NavigateToSingleplayer);
            View.Multiplayer.onClick.RemoveListener(NavigateToMultiplayer);
            View.Settings.onClick.RemoveListener(NavigateToSettings);
            View.QuitButton.onClick.RemoveListener(ExitApplication);
            View.gameObject.SetActive(false);
        }

        private void NavigateToSingleplayer()
        {
            //Owner.ChangeState(new SingleplayerState(Owner.UIRoot.SingleplayerView) { Owner = Owner });
        }

        private void NavigateToMultiplayer()
        {
            Owner.ChangeState(new MultiplayerState(Owner.UIRoot.MultiplayerView) { Owner = Owner });
        }

        private void NavigateToSettings()
        {
            //Owner.ChangeState(new SettingsState(Owner.UIRoot.SettingsView) { Owner = Owner });
        }

        private void ExitApplication()
        {
            Application.Quit();
        }
    }
}

