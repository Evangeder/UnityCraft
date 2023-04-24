using UnityEngine;

namespace UnityCraft.UI.MainMenu.StateMachine
{
    using Patterns.StateMachine;
    using MainMenu;
    using States;

    public class MainMenuStateMachine : StateMachine
    {
        [field: SerializeField]
        public UIRoot UIRoot { get; private set; }

        private void Start()
        {
            ChangeState(new MenuState(UIRoot.MenuView) { Owner = this });
        }

        public override void ChangeState(BaseState state)
        {
            if (currentState != null)
            {
                currentState.DestroyState();
            }

            currentState = state;

            if (currentState != null)
            {
                currentState.Owner = this;
                currentState.PrepareState();
                currentState.UpdateState();
            }
        }
    }
}