using UnityEngine;
using Unity.Mathematics;

namespace UnityCraft.World.Map
{
    using Chunk;

    public partial class Map : MonoBehaviour
    {
        public Chunk CreateChunk(int x, int y, int z)
            => CreateChunk(new int3(x, y, z));

        public Chunk CreateChunk(int3 position)
        {
            if (Chunks.ContainsKey(position) || GetChunk(position, out _))
            {
                return null;
            }

            Chunk chunk = Instantiate(ChunkPrefab, new Vector3(0, 0, 0), Quaternion.Euler(Vector3.zero));
            chunk.transform.parent = gameObject.transform;
            chunk.transform.name = $"Chunk ({position})";
            chunk.transform.position = new Vector3(position.x, position.y, position.z);
            chunk.Position = position;

            Chunks.TryAdd(position, chunk);

            return chunk;
        }

        public void DestroyChunk(int x, int y, int z)
            => DestroyChunk(new int3(x, y, z));

        public void DestroyChunk(int3 position)
        {
            if (!Chunks.Remove(position, out Chunk chunk))
            {
                return;
            }

            chunk.Dispose();
        }

        public bool GetChunk(int3 position, out Chunk chunk)
            => GetChunk(position.x, position.y, position.z, out chunk);

        public bool GetChunk(int x, int y, int z, out Chunk chunk)
        {
            int3 position = new int3();

            position.x = Mathf.FloorToInt(x / (float)ChunkSize.x) * ChunkSize.x;
            position.y = Mathf.FloorToInt(y / (float)ChunkSize.y) * ChunkSize.y;
            position.z = Mathf.FloorToInt(z / (float)ChunkSize.z) * ChunkSize.z;

            return Chunks.TryGetValue(position, out chunk);
        }
    }
}