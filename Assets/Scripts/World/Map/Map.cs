using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Unity.Mathematics;

using Random = System.Random;

namespace UnityCraft.World.Map
{
    using Chunk;
    using Blocks;
    using Generation;
    using Settings;
    using Unity.Collections;

    public partial class Map : MonoBehaviour
    {
        public Dictionary<int3, Chunk> Chunks;

        [field: Header("Prefabs")]
        [field: SerializeField]
        public Chunk ChunkPrefab { get; private set; }

        public string MapName = "World";

        private Random Random;
        private float2 Seed;

        public int3 Size;
        public int3 ChunkSize;
        public int SkylightSubtracted;

        public NativeArray<BlockMetadata> Blocks;
        public int[] Heightmap;

        public static Thread MainThread = Thread.CurrentThread;

        private bool IsPowerOfTwo(int x)
        {
            return (x & (x - 1)) == 0;
        }

        public void InitializeWorld(int height)
        {
            Random = new Random(Guid.NewGuid().GetHashCode());
            Seed = new float2(Random.Next());

            Chunks = new Dictionary<int3, Chunk>();

            if (IsPowerOfTwo(height) && height >= 16)
            {
                Debug.LogError("Map (chunk) height is not a multiply of 2.");

                // TODO: go back to main menu
                return;
            }

            ChunkSize = new int3(GameSettings.CHUNK_SIZE, GameSettings.DEFAULT_CHUNK_HEIGHT, GameSettings.CHUNK_SIZE);

            // TODO var mapgen and shit
        }

        public void InitializeWorldMultiplayer()
        {
            Chunks = new Dictionary<int3, Chunk>();
        }

        private void OnDestroy()
        {
            // TODO dispose natives
        }
    }

}