namespace UnityCraft.UI.MainMenu.StateMachine.States
{
    using MainMenu.Views;

    public class SingleplayerState : MainMenuBaseState<SingleplayerView>
    {
        public SingleplayerState(SingleplayerView view) : base(view) { }

        public override void PrepareState()
        {
            base.PrepareState();
            View.Return.onClick.AddListener(NavigateToMainMenu);
            View.World1.onClick.AddListener(LoadOrCreateWorld1);
            View.World2.onClick.AddListener(LoadOrCreateWorld2);
            View.World3.onClick.AddListener(LoadOrCreateWorld3);
            View.World4.onClick.AddListener(LoadOrCreateWorld4);
            View.World5.onClick.AddListener(LoadOrCreateWorld5);
            View.gameObject.SetActive(true);
        }

        public override void DestroyState()
        {
            base.DestroyState();
            View.Return.onClick.RemoveListener(NavigateToMainMenu);
            View.World1.onClick.RemoveListener(LoadOrCreateWorld1);
            View.World2.onClick.RemoveListener(LoadOrCreateWorld2);
            View.World3.onClick.RemoveListener(LoadOrCreateWorld3);
            View.World4.onClick.RemoveListener(LoadOrCreateWorld4);
            View.World5.onClick.RemoveListener(LoadOrCreateWorld5);
            View.gameObject.SetActive(false);
        }

        private void LoadOrCreateWorld(int world)
        {
            // world manager ?? 
        }

        private void LoadOrCreateWorld1()
        {
            LoadOrCreateWorld(1);
        }

        private void LoadOrCreateWorld2()
        {
            LoadOrCreateWorld(2);
        }

        private void LoadOrCreateWorld3()
        {
            LoadOrCreateWorld(3);
        }

        private void LoadOrCreateWorld4()
        {
            LoadOrCreateWorld(4);
        }

        private void LoadOrCreateWorld5()
        {
            LoadOrCreateWorld(5);
        }

        private void NavigateToMainMenu()
        {
            Owner.ChangeState(new MenuState(Owner.UIRoot.MenuView) { Owner = Owner });
        }
    }
}

