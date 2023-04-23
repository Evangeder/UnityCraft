using UnityEngine;

namespace UnityCraft.Patterns.StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected BaseState currentState;

        private void Update()
        {
            if (currentState != null)
            {
                currentState.UpdateState();
            }
        }

        public void ChangeState(BaseState state)
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