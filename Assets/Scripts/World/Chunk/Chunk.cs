using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace UnityCraft.World.Chunk
{
    using Map;
    using Blocks;

    public class Chunk : MonoBehaviour
    {
        // world declaration goes here
        [HideInInspector]
        public int3 Position;
        public NativeArray<BlockMetadata> Blocks;
        public Map Map;
        public ChunkSafety ChunkSafety;

        private void Awake()
            => Blocks = new NativeArray<BlockMetadata>(Map.ChunkSize.x * Map.ChunkSize.y * Map.ChunkSize.z, Allocator.Persistent);

        private static bool InRange(int index, int size)
            => index >= 0 && index < size;

        public BlockMetadata GetBlock(int ArrayPosition)
            => Blocks[ArrayPosition];

        public BlockMetadata GetBlock(int x, int y, int z)
        {
            if (InRange(x, Map.ChunkSize.x) && InRange(y, Map.ChunkSize.y) && InRange(z, Map.ChunkSize.z))
            {
                return Blocks[x + y * Map.ChunkSize.x + z * Map.ChunkSize.x * Map.ChunkSize.y];
            }
            return Map.GetBlock(Position.x + x, Position.y + y, Position.z + z);
        }

        public void SetBlock((int ArrayPosition, BlockMetadata Metadata) block, BlockUpdateMode updateMode = BlockUpdateMode.ForceUpdate)
            => SetBlock(block.ArrayPosition, block.Metadata, updateMode);

        public void SetBlock(int arrayPosition, BlockMetadata metadata, BlockUpdateMode updateMode = BlockUpdateMode.ForceUpdate)
        {
            int x = arrayPosition % Map.ChunkSize.x;
            int y = (arrayPosition / Map.ChunkSize.x) % Map.ChunkSize.y;
            int z = (arrayPosition / (Map.ChunkSize.x * Map.ChunkSize.y)) % Map.ChunkSize.z;
            SetBlock(x, y, z, metadata, updateMode);
        }

        public void SetBlock(int x, int y, int z, BlockMetadata metadata, BlockUpdateMode updateMode = BlockUpdateMode.ForceUpdate)
        {
            // TODO
        }

        public void UpdateChunk()
        {
            // TODO
        }

        public void Dispose()
        {
            Blocks.Dispose();
            Destroy(gameObject);
        }
    }
}