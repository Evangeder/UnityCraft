using UnityEngine;
using Unity.Mathematics;

namespace UnityCraft.World.Map
{
    using Blocks;
    using Chunk;

    public partial class Map : MonoBehaviour
    {
        public BlockMetadata GetBlock(int3 pos) => GetBlock(pos.x, pos.y, pos.z);
        public BlockMetadata GetBlock(int x, int y, int z)
        {
            if (GetChunk(x, y, z, out Chunk chunk))
            {
                BlockMetadata block = chunk.GetBlock(x - chunk.Position.x, y - chunk.Position.y, z - chunk.Position.z);
                return block;
            }
            else
            {
                return new BlockMetadata { ID = 0 };
            }
        }

        public void SetBlock(int3 pos, BlockMetadata blockMetadata, BlockUpdateMode UpdateMode = BlockUpdateMode.ForceUpdate)
            => SetBlock(pos.x, pos.y, pos.z, blockMetadata, UpdateMode);

        public void SetBlock(int x, int y, int z, BlockMetadata blockMetadata, BlockUpdateMode UpdateMode = BlockUpdateMode.ForceUpdate)
        {
            GetChunk(x, y, z, out Chunk chunk);

            if (chunk != null)
            {
                chunk.SetBlock(x - chunk.Position.x, y - chunk.Position.y, z - chunk.Position.z, blockMetadata, UpdateMode);
            }
        }
    }
}