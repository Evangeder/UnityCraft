namespace UnityCraft.Patterns.StateMachine
{
    public abstract class BaseState
    {
        /// <summary>
        /// Owner <see cref="StateMachine"/> which controlls this state.
        /// </summary>
        public StateMachine Owner;

        /// <summary>
        /// Spawns and/or shows hidden objects bound to this state.
        /// </summary>
        public virtual void PrepareState() { }

        /// <summary>
        /// <see cref="UpdateState"/> works just like Unity's Update() message.
        /// </summary>
        public virtual void UpdateState() { }

        /// <summary>
        /// Hides and/or destroys objects initated in <see cref="PrepareState"/>.
        /// </summary>
        public virtual void DestroyState() { }
    }
}