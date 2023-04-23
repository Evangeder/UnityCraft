namespace UnityCraft.Blocks
{
    /// <summary>
    /// Defines if the block should get rendered and/or update the physics near it.
    /// </summary>
    public enum BlockUpdateMode : byte
    {
        /// <summary>
        /// Default, initiates light recalculation and rendering after placement, aswell as physics.
        /// </summary>
        ForceUpdate = 0,

        /// <summary>
        /// Add blockupdate to the queue and render it later along with light recalculation and physics update.
        /// </summary>
        Queue = 1,

        /// <summary>
        /// Do not render the change, but update physics.
        /// </summary>
        Silent = 2,

        /// <summary>
        /// Do not initiate the light recalculation, rendering, nor physics update.
        /// </summary>
        None = 3
    }
}