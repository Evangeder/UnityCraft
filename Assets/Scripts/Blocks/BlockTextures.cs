using Unity.Mathematics;

namespace UnityCraft.Blocks
{
    public readonly struct BlockTextures
    {
        /// <summary>
        /// Top texture.
        /// </summary>
        public int2 Up { get; }

        /// <summary>
        /// Bottom texture.
        /// </summary>
        public int2 Down { get; }

        /// <summary>
        /// Northern texture.
        /// </summary>
        public int2 North { get; }

        /// <summary>
        /// Southern texture.
        /// </summary>
        public int2 South { get; }

        /// <summary>
        /// Eastern texture.
        /// </summary>
        public int2 East { get; }

        /// <summary>
        /// Western texture.
        /// </summary>
        public int2 West { get; }

        public BlockTextures(int2 up, int2 down, int2 north, int2 south, int2 east, int2 west)
        {
            Up = up;
            Down = down;
            North = north;
            South = south;
            East = east;
            West = west;
        }
    }
}