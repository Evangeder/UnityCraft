namespace UnityCraft.UI.MainMenu.StateMachine.States
{
    using MainMenu.Views;
    using Networking.API;

    public class LoginState : MainMenuBaseState<LoginView>
    {
        public LoginState(LoginView view) : base(view) { }

        public override void PrepareState()
        {
            base.PrepareState();
            View.Return.onClick.AddListener(NavigateToMainMenu);
            View.Login.onClick.AddListener(Login);
            View.gameObject.SetActive(true);
        }

        public override void DestroyState()
        {
            base.DestroyState();
            View.Return.onClick.RemoveListener(NavigateToMainMenu);
            View.Login.onClick.RemoveListener(Login);
            View.gameObject.SetActive(false);
        }

        private void NavigateToMainMenu()
        {
            Owner.ChangeState(new MenuState(Owner.UIRoot.MenuView) { Owner = Owner });
        }

        private async void Login()
        {
            var success = await ClassicubeApi.Instance.Login(View.LoginField.text, View.PasswordField.text);
            View.LoginField.text = View.PasswordField.text = string.Empty;

            if (!success)
            {
                View.TextLoginFailed.text = "Login failed.";
                return;
            }

            Owner.ChangeState(new MultiplayerState(Owner.UIRoot.MultiplayerView) { Owner = Owner });
        }
    }
}