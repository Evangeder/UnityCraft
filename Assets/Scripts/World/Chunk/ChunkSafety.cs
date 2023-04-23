using System;

namespace UnityCraft.World.Chunk
{
    [Flags]
    public enum ChunkSafety
    {
        Empty = 0,
        Generating = 1,
        Generated = 2,
        Rendering = 4,
        Rendered = 8,
        Disposing = 16,
        Busy= 32
    }
}