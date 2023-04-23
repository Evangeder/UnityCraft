namespace UnityCraft.Blocks
{
    /// <summary>
    /// This defines if the block is, for example: transparent, semitransparent, liquid or gives out light.
    /// </summary>
    [System.Flags]
    public enum BlockFlags : byte
    {
        /// <summary>
        /// Disable this if you want to get glass-like transparency.
        /// </summary>
        Solid = 1,

        /// <summary>
        /// This block becomes semi-transparent. Block faces will be rendered for every block.
        /// </summary>
        Foliage = 2,
        
        /// <summary>
        /// Player can swim in this block.
        /// </summary>
        Liquid = 4,

        /// <summary>
        /// Emit light.
        /// </summary>
        Torch = 8,

        /// <summary>
        /// Use custom physics, like falling sand.
        /// </summary>
        UsePhysics = 16,
    }
}