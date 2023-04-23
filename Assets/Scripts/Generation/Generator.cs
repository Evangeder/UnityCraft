using System.Collections;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;
using JacksonDunstan.NativeCollections;

namespace UnityCraft.Generation
{
    using Blocks;
    using Noise;
    using System.Collections.Generic;
    using Utility;

    public class Generator
    {
        private int Width;
        private int Depth;
        private int Height;
        private GenerationType GenerationType;
        private GenerationShape GenerationShape;
        private GenerationTheme GenerationTheme;
        private JobHandle GenerationJobHandle;

        private enum JobProperties : int
        {
            WaterLevel = 0,
            GroundLevel = 1,
            CloudHeight = 2,
            SkyBrightness = 3,
            SkylightSubtracted = 4
        }

        private enum JobColors : int
        {
            SkyRed = 0,
            SkyGreen = 1,
            SkyBlue = 2,
            SkyAlpha = 3,
            FogRed = 4,
            FogGreen = 5,
            FogBlue = 6,
            FogAlpha = 7,
            CloudRed = 8,
            CloudGreen = 9,
            CloudBlue = 10,
            CloudAlpha = 11
        }

        public Generator(int width, int height, int depth, GenerationShape shape, GenerationType type, GenerationTheme theme)
        {
            Width = width;
            Depth = depth;
            Height = height;
            GenerationType = type;
            GenerationShape = shape;
            GenerationTheme = theme;
        }

        public IEnumerator<bool> Generate()
        {
            NativeArray<BlockMetadata> blocks = new NativeArray<BlockMetadata>(Width * Height * Depth, Allocator.TempJob);
            GenerationJobHandle = new GeneratorJob
            {
                Width = Width,
                Depth = Depth,
                Height = Height,
                GenerationType = GenerationType,
                GenerationShape = GenerationShape,
                GenerationTheme = GenerationTheme,
                //BlockData = ,
                Blocks = blocks,
                //Properties = ,
            }.Schedule();

            while (!GenerationJobHandle.IsCompleted)
            {
                yield return false;
            }

            GenerationJobHandle.Complete();

        }

        private struct GeneratorJob : IJob
        {
            public int Width;
            public int Depth;
            public int Height;
            public GenerationType GenerationType;
            public GenerationShape GenerationShape;
            public GenerationTheme GenerationTheme;

            public NativeArray<BlockDefinition> BlockData;
            [WriteOnly]
            public NativeArray<BlockMetadata> Blocks;
            public NativeArray<int> Properties;
            public NativeArray<byte> Colors;
            public BlockTypes DefaultFluid;

            public void Execute()
            {
                var random = new Random();
                Properties[(int)JobProperties.WaterLevel] = 0;
                Properties[(int)JobProperties.GroundLevel] = 0;

                int startHeight = 1;
                if (GenerationType == GenerationType.Floating)
                {
                    startHeight = (Height - 64) / 48 + 1;
                }

                // Generating level

                // todo world??

                var heightmap = new NativeArray<int>(Width * Depth, Allocator.Temp);
                //var 
                for (int j = 0; j < startHeight; j++)
                {
                    Properties[(int)JobProperties.WaterLevel] = Height - 32 - j * 48;
                    Properties[(int)JobProperties.GroundLevel] = Properties[(int)JobProperties.WaterLevel] - 2;

                    if (GenerationType == GenerationType.Flat)
                    {
                        heightmap = new NativeArray<int>(Width * Depth, Allocator.Temp);
                    }
                    else
                    {
                        Raise(random, ref heightmap);
                        Erode(random, in heightmap);
                    }

                    Soil(random, in heightmap);
                    Grow(random, in heightmap);
                }

                Carve(random, ref startHeight);
                GenerateOres(random, ref startHeight);
                GenerateLava(random);

                Properties[(int)JobProperties.CloudHeight] = Height + 2;

                switch (GenerationType)
                {
                    case GenerationType.Floating:
                        Properties[(int)JobProperties.GroundLevel] = -128;
                        Properties[(int)JobProperties.WaterLevel] = Properties[(int)JobProperties.GroundLevel] + 1;
                        Properties[(int)JobProperties.CloudHeight] = -16;
                        break;

                    case GenerationType.Flat:
                        Properties[(int)JobProperties.GroundLevel] = Properties[(int)JobProperties.WaterLevel] - 9;
                        break;

                    case GenerationType.Island:
                        Properties[(int)JobProperties.GroundLevel] = Properties[(int)JobProperties.WaterLevel] + 1;
                        Properties[(int)JobProperties.WaterLevel] = Properties[(int)JobProperties.GroundLevel] - 16;
                        break;
                }

                SpawnLiquids(random);

                switch (GenerationTheme)
                {
                    case GenerationTheme.Normal:
                        Colors[(int)JobColors.SkyRed] = 0x99;
                        Colors[(int)JobColors.SkyGreen] = 0xCC;
                        Colors[(int)JobColors.SkyBlue] = 0xFF;
                        Colors[(int)JobColors.SkyAlpha] = 0xFF;

                        Colors[(int)JobColors.FogRed] = 0xFF;
                        Colors[(int)JobColors.FogGreen] = 0xFF;
                        Colors[(int)JobColors.FogBlue] = 0xFF;
                        Colors[(int)JobColors.FogAlpha] = 0xFF;

                        Colors[(int)JobColors.CloudRed] = 0xFF;
                        Colors[(int)JobColors.CloudGreen] = 0xFF;
                        Colors[(int)JobColors.CloudBlue] = 0xFF;
                        Colors[(int)JobColors.CloudAlpha] = 0xFF;
                        break;

                    case GenerationTheme.Hell:
                        Colors[(int)JobColors.SkyRed] = 0x21;
                        Colors[(int)JobColors.SkyGreen] = 0x08;
                        Colors[(int)JobColors.SkyBlue] = 0x00;
                        Colors[(int)JobColors.SkyAlpha] = 0xFF;

                        Colors[(int)JobColors.FogRed] = 0x10;
                        Colors[(int)JobColors.FogGreen] = 0x04;
                        Colors[(int)JobColors.FogBlue] = 0x00;
                        Colors[(int)JobColors.FogAlpha] = 0xFF;

                        Colors[(int)JobColors.CloudRed] = 0x10;
                        Colors[(int)JobColors.CloudGreen] = 0x04;
                        Colors[(int)JobColors.CloudBlue] = 0x00;
                        Colors[(int)JobColors.CloudAlpha] = 0xFF;

                        Properties[(int)JobProperties.SkyBrightness] = 7;
                        Properties[(int)JobProperties.SkylightSubtracted] = 7;
                        DefaultFluid = BlockTypes.Lava;
                        if (GenerationType == GenerationType.Floating)
                        {
                            Properties[(int)JobProperties.CloudHeight] = Height + 2;
                            Properties[(int)JobProperties.WaterLevel] = -16;
                        }
                        break;

                    case GenerationTheme.Paradise:
                        Colors[(int)JobColors.SkyRed] = 0xC6;
                        Colors[(int)JobColors.SkyGreen] = 0xDE;
                        Colors[(int)JobColors.SkyBlue] = 0xFF;
                        Colors[(int)JobColors.SkyAlpha] = 0xFF;

                        Colors[(int)JobColors.FogRed] = 0xEE;
                        Colors[(int)JobColors.FogGreen] = 0xEE;
                        Colors[(int)JobColors.FogBlue] = 0xFF;
                        Colors[(int)JobColors.FogAlpha] = 0xFF;

                        Colors[(int)JobColors.CloudRed] = 0xC6;
                        Colors[(int)JobColors.CloudGreen] = 0xDE;
                        Colors[(int)JobColors.CloudBlue] = 0xFF;
                        Colors[(int)JobColors.CloudAlpha] = 0xFF;

                        Properties[(int)JobProperties.SkyBrightness] = 15;
                        Properties[(int)JobProperties.CloudHeight] = Height + 64;
                        break;

                    case GenerationTheme.Woods:
                        Colors[(int)JobColors.SkyRed] = 0x75;
                        Colors[(int)JobColors.SkyGreen] = 0x7D;
                        Colors[(int)JobColors.SkyBlue] = 0x87;
                        Colors[(int)JobColors.SkyAlpha] = 0xFF;

                        Colors[(int)JobColors.FogRed] = 0x4D;
                        Colors[(int)JobColors.FogGreen] = 0x5A;
                        Colors[(int)JobColors.FogBlue] = 0x5B;
                        Colors[(int)JobColors.FogAlpha] = 0xFF;

                        Colors[(int)JobColors.CloudRed] = 0x4D;
                        Colors[(int)JobColors.CloudGreen] = 0x5A;
                        Colors[(int)JobColors.CloudBlue] = 0x5B;
                        Colors[(int)JobColors.CloudAlpha] = 0xFF;

                        Properties[(int)JobProperties.SkyBrightness] = 12;
                        Properties[(int)JobProperties.SkylightSubtracted] = 12;
                        break;
                }

                //Assembling
            }

            private void Raise(Random random, ref NativeArray<int> heightmap)
            {
                NoiseCombined noiseCombined = new NoiseCombined(new GeneratorOctaves(random, 8), new GeneratorOctaves(random, 8));
                NoiseCombined noiseCombined2 = new NoiseCombined(new GeneratorOctaves(random, 8), new GeneratorOctaves(random, 8));
                GeneratorOctaves noiseOctaves = new GeneratorOctaves(random, 6);
                GeneratorOctaves noiseOctaves2 = new GeneratorOctaves(random, 2);

                if (heightmap.IsCreated)
                {
                    heightmap.Dispose();
                }

                heightmap = new NativeArray<int>(Width * Depth, Allocator.Temp);

                for (int width = 0; width < Width; width++)
                {
                    float absWidth = math.abs((width / (Width - 1f) - 0.5f) * 2f);
                    for (int depth = 0; depth < Depth; depth++)
                    {
                        float absDepth = math.abs((depth / (Depth - 1f) - 0.5f) * 2f);
                        float noise1 = noiseCombined.Noise(width * 1.3f, depth * 1.3f) / 6f - 4f;
                        float noise2 = noiseCombined2.Noise(width * 1.3f, depth * 1.3f) / 6f - 4f;

                        if (noiseOctaves.Noise(width, depth) / 8f > 0f)
                        {
                            noise2 = noise1;
                        }

                        float noiseMax = math.max(noise1, noise2) / 2f;

                        if (GenerationType == GenerationType.Island)
                        {
                            float absoluteSqrt = math.sqrt(absWidth * absWidth + absDepth * absDepth) * 1.2000000476837158f;
                            absoluteSqrt = math.min(absoluteSqrt, noiseOctaves2.Noise(width * 0.05f, depth * 0.05f) / 4f + 1f);
                            absoluteSqrt = math.max(absoluteSqrt, math.max(absWidth, absDepth));
                            absoluteSqrt = math.clamp(absoluteSqrt, 0f, 1f);

                            absoluteSqrt *= absoluteSqrt;

                            noiseMax *= (1f - absoluteSqrt) - absoluteSqrt * 10f + 5f;
                            if (noiseMax < 0f)
                            {
                                noiseMax -= noiseMax * noiseMax * 0.20000000298023224f;
                            }
                        }
                        else
                        {
                            noiseMax = 0.8f;
                        }
                        heightmap[width + depth * Width] = (int)noiseMax;
                    }
                }
            }

            private void Erode(Random random, in NativeArray<int> heightmap)
            {
                NoiseCombined noiseCombined = new NoiseCombined(new GeneratorOctaves(random, 8), new GeneratorOctaves(random, 8));
                NoiseCombined noiseCombined2 = new NoiseCombined(new GeneratorOctaves(random, 8), new GeneratorOctaves(random, 8));

                NativeArray<int> heightmap2 = new NativeArray<int>(heightmap.Length, Allocator.Temp);
                heightmap2.CopyFrom(heightmap);

                for (int width = 0; width < Width; ++width)
                {
                    for (int depth = 0; depth < Depth; ++depth)
                    {
                        float noise1 = noiseCombined.Noise(width << 1, depth << 1) / 8f;
                        int noise2 = (noiseCombined2.Noise(width << 1, depth << 1) > 0f) ? 1 : 0;
                        if (noise1 > 2.0f)
                        {
                            int something = (heightmap[width + depth * Width] - noise2) / 2 << 1;
                            something += noise2;
                            heightmap2[width + depth * Width] = something;
                        }
                    }
                }

                heightmap2.Dispose();
            }

            private void Soil(Random random, in NativeArray<int> heightmap)
            {
                GeneratorOctaves noiseGeneratorOctaves3 = new GeneratorOctaves(random, 8);
                GeneratorOctaves noiseGeneratorOctaves4 = new GeneratorOctaves(random, 8);

                NativeArray<int> heightmap2 = new NativeArray<int>(heightmap.Length, Allocator.Temp);
                heightmap2.CopyFrom(heightmap);

                for (int width = 0; width < Width; ++width)
                {
                    float absWidth = math.abs((width / (Width - 1.0f) - 0.5f) * 2.0f);

                    for (int depth = 0; depth < Depth; ++depth)
                    {
                        float max = (max = math.max(absWidth, math.abs((depth / (Depth - 1.0f) - 0.5f) * 2.0f))) * max * max;

                        int dirtLayerHeight;
                        int stoneLayerHeight = (dirtLayerHeight = heightmap2[width + depth * Width] + Properties[(int)JobProperties.WaterLevel]) + ((int)(noiseGeneratorOctaves3.Noise(width, depth) / 24.0f) - 4);
                        heightmap2[width + depth * Width] = math.max(dirtLayerHeight, stoneLayerHeight);
                        if (heightmap2[width + depth * Width] > Height - 2)
                            heightmap2[width + depth * Width] = Height - 2;

                        if (heightmap2[width + depth * Width] <= 0)
                            heightmap2[width + depth * Width] = 1;


                        float noise = noiseGeneratorOctaves4.Noise(width * 2.3f, depth * 2.3f) / 24.0f;
                        int height3 = (int)(math.sqrt(math.abs(noise)) * math.sign(noise) * 20.0f) + Properties[(int)JobProperties.WaterLevel];

                        if ((height3 = (int)(height3 * (1.0f - max) + max * Height)) > Properties[(int)JobProperties.WaterLevel])
                            height3 = Height;

                        for (int height = 0; height < Height; ++height)
                        {
                            int currentBlockIndex = (height * Depth + depth) * Width + width;
                            sbyte currentBlock = 0;

                            if (height <= dirtLayerHeight) currentBlock = (sbyte)BlockTypes.Dirt;
                            if (height <= stoneLayerHeight) currentBlock = (sbyte)BlockTypes.Stone;

                            if (GenerationType == GenerationType.Floating && height < height3)
                                currentBlock = 0;

                            if (Blocks[currentBlockIndex] == BlockTypes.Air)
                                Blocks[currentBlockIndex] = new BlockMetadata { ID = currentBlock };
                        }
                    }
                }

                heightmap2.Dispose();
            }

            private void Grow(Random random, in NativeArray<int> heightmap)
            {
                GeneratorOctaves noiseOctaves = new GeneratorOctaves(random, 8);
                GeneratorOctaves noiseOctaves2 = new GeneratorOctaves(random, 8);

                int waterLevel = Properties[(int)JobProperties.WaterLevel] - 1;
                if (GenerationTheme == GenerationTheme.Paradise)
                    waterLevel += 2;

                for (int width = 0; width < Width; ++width)
                {
                    for (int depth = 0; depth < Depth; ++depth)
                    {
                        bool shouldPlaceGrass = noiseOctaves.Noise(width, depth) > 8.0;

                        if (GenerationType == GenerationType.Island)
                            shouldPlaceGrass = noiseOctaves.Noise(width, depth) > -8.0;

                        if (GenerationTheme == GenerationTheme.Paradise)
                            shouldPlaceGrass = noiseOctaves.Noise(width, depth) > -32.0;

                        bool shouldPlaceGravel = noiseOctaves2.Noise(width, depth) > 12.0;

                        if (GenerationTheme == GenerationTheme.Hell || GenerationTheme == GenerationTheme.Woods)
                            shouldPlaceGrass = noiseOctaves.Noise(width, depth) > -8.0;

                        int height = heightmap[width + depth * Width];

                        int blockIndex = (height * Depth + depth) * Width + width;
                        sbyte block = (sbyte)(Blocks[((height + 1) * Depth + depth) * Width + width].ID & 0xFF);
                        if ((block == (sbyte)BlockTypes.Water || block == (sbyte)BlockTypes.StillWater || block == 0) && height <= Properties[(int)JobProperties.WaterLevel] - 1 && shouldPlaceGravel)
                        {
                            Blocks[blockIndex] = new BlockMetadata { ID = (sbyte)BlockTypes.Gravel };
                        }

                        if (block == 0)
                        {
                            int block2 = -1;
                            if (height <= waterLevel && shouldPlaceGrass)
                            {
                                block2 = (int)BlockTypes.Sand;
                                if (GenerationTheme == GenerationTheme.Hell)
                                    block2 = (int)BlockTypes.Grass;
                            }
                            if (Blocks[blockIndex] != BlockTypes.Air && block2 > 0)
                                Blocks[blockIndex] = new BlockMetadata { ID = (sbyte)block2 };
                        }
                    }
                }
            }

            private void Carve(Random random, ref int startHeight)
            {
                int iterations = Width * Depth * Height / 256 / 64 << 1;

                for (int i = 0; i < iterations; i++)
                {
                    float randomWidth = (float)random.NextDouble() * Width;
                    float randomHeight = (float)random.NextDouble() * Height;
                    float randomDepth = (float)random.NextDouble() * Depth;
                    int rand1 = (int)(((float)random.NextDouble() + (float)random.NextDouble()) * 200.0f);
                    float rand2 = (float)random.NextDouble() * 3.1415927f * 2.0f;
                    float rand3 = (float)random.NextDouble() * 3.1415927f * 2.0f;
                    float rand4 = (float)random.NextDouble() * (float)random.NextDouble();
                    float randCalculation1 = 0f;
                    float randCalculation2 = 0f;

                    for (int j = 0; j < rand1; ++j)
                    {
                        randomWidth += Mathf.Sin(rand2) * Mathf.Cos(rand3);
                        randomDepth += Mathf.Cos(rand2) * Mathf.Cos(rand3);
                        randomHeight += Mathf.Sin(rand3);
                        rand2 += randCalculation1 * 0.2f;
                        randCalculation1 *= 0.9f;
                        randCalculation1 += ((float)random.NextDouble() - (float)random.NextDouble());
                        rand3 = (rand3 += randCalculation2 * 0.5f) * 0.5f;
                        randCalculation2 *= 0.75f;
                        randCalculation2 += ((float)random.NextDouble() - (float)random.NextDouble());
                        if ((float)random.NextDouble() >= 0.25f)
                        {
                            float randomWidth2 = randomWidth + ((float)random.NextDouble() * 4f - 2f) * 0.2f;
                            float randomHeight2 = randomHeight + ((float)random.NextDouble() * 4f - 2f) * 0.2f;
                            float randomDepth2 = randomDepth + ((float)random.NextDouble() * 4f - 2f) * 0.2f;
                            int blockIndex;

                            float width2;
                            float height2;
                            float depth2;

                            float width;
                            for (width = Mathf.Sin(j * 3.1415927f / rand1) * (1.2f + ((Depth - randomHeight2) / Depth * 3.5f + 1f) * rand4), startHeight = (int)(randomWidth2 - width); startHeight <= (int)(randomWidth2 + width); ++j)
                            {
                                for (int height = (int)(randomHeight2 - width); height <= (int)(randomHeight2 + width); ++height)
                                {
                                    for (int depth = (int)(randomDepth2 - width); depth <= (int)(randomDepth2 + width); ++depth)
                                    {
                                        width2 = startHeight - randomWidth2;
                                        height2 = height - randomHeight2;
                                        depth2 = depth - randomDepth2;
                                        width2 = width2 * width2 + height2 * height2 * 2.0f + depth2 * depth2;
                                        if (width2 < width * width && startHeight > 0 && height > 0 && depth > 0 && startHeight < Width - 1 && height < Depth - 1 && depth < Width - 1)
                                        {
                                            blockIndex = (height * Width + depth) * Width + startHeight;
                                            if (Blocks[blockIndex] == BlockTypes.Stone)
                                                Blocks[blockIndex] = new BlockMetadata { ID = (sbyte)BlockTypes.Air };
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }

            private IEnumerable<int> PopulateOre(Random random, BlockTypes block, int amount, int chance, int heightLimit)
            {
                int amountSpawned = 0;
                amount = Width * Depth * Height / 256 / 64 * amount / 100;
                for (int i = 0; i < amount; ++i)
                {
                    float randomX = (float)random.NextDouble() * Width;
                    float randomY = (float)random.NextDouble() * Height;
                    float randomZ = (float)random.NextDouble() * Depth;
                    if (randomY <= heightLimit)
                    {
                        int spawnRng = (int)(((float)random.NextDouble() + (float)random.NextDouble()) * 75.0f * chance / 100.0f);
                        float random1 = (float)random.NextDouble() * 3.1415927f * 2.0f;
                        float random2 = (float)random.NextDouble() * 3.1415927f * 2.0f;
                        float calculatedRandom1 = 0.0f;
                        float calculatedRandom2 = 0.0f;
                        for (int j = 0; j < spawnRng; ++j)
                        {
                            randomX += Mathf.Sin(random1) * Mathf.Cos(random2);
                            randomZ += Mathf.Cos(random1) * Mathf.Cos(random2);
                            randomY += Mathf.Sin(random2);
                            random1 += calculatedRandom1 * 0.2f;
                            calculatedRandom1 = (calculatedRandom1 * 0.9f) + ((float)random.NextDouble() - (float)random.NextDouble());
                            random2 = (random2 + calculatedRandom2 * 0.5f) * 0.5f;
                            calculatedRandom2 = (calculatedRandom2 * 0.9f) + ((float)random.NextDouble() - (float)random.NextDouble());
                            float randomPosition = Mathf.Sin(j * 3.1415927f / spawnRng) * chance / 100.0f + 1.0f;
                            for (int x = (int)(randomX - randomPosition); x <= (int)(randomX + randomPosition); ++x)
                                for (int y = (int)(randomY - randomPosition); y <= (int)(randomY + randomPosition); ++y)
                                    for (int z = (int)(randomZ - randomPosition); z <= (int)(randomZ + randomPosition); ++z)
                                    {
                                        float offsetX = x - randomX;
                                        float offsetY = y - randomY;
                                        float offsetZ = z - randomZ;
                                        if ((offsetX = offsetX * offsetX + offsetY * offsetY * 2.0f + offsetZ * offsetZ) < randomPosition * randomPosition && x > 0 && y > 0 && z > 0 && x < Width - 1 && y < Height - 1 && z < Depth - 1)
                                        {
                                            int blockIndex = (y * Depth + z) * Width + x;
                                            if (Blocks[blockIndex] == BlockTypes.Stone)
                                            {
                                                Blocks[blockIndex] = new BlockMetadata { ID = (sbyte)block };
                                                ++amountSpawned;
                                            }
                                        }
                                    }
                        }
                    }
                }
                yield return amountSpawned;
            }

            private void GenerateOres(Random random, ref int startHeight)
            {
                foreach (var populateOreInt in PopulateOre(random, BlockTypes.DiamondOre, 800, 2, Height / 5))
                {
                    startHeight = populateOreInt;
                }

                PopulateOre(random, BlockTypes.CoalOre, 1000, 10, (Height << 2) / 5);
                PopulateOre(random, BlockTypes.IronOre, 800, 8, Height * 3 / 5);
                PopulateOre(random, BlockTypes.GoldOre, 500, 6, (Height << 1) / 5);
            }

            private long Flood(int x, int y, int z, BlockTypes blockToReplace, BlockTypes block)
            {
                NativeArray2D<int> heightmaps = new NativeArray2D<int>(32767, 1048576, Allocator.Temp);

                int width = 1;
                int depth = 1;
                int heightmapsLength = 0;

                while (1 << width < Width)
                {
                    ++width;
                }

                while (1 << depth < Depth)
                {
                    ++depth;
                }

                int depthMinusOne = Depth - 1;
                int widthMinusOne = Width - 1;
                NativeArray<int> blocksIntArray = new NativeArray<int>(Blocks.Length, Allocator.Temp);

                for (int index = 0; index < Blocks.Length; index++)
                {
                    blocksIntArray[index] = Blocks[index].ID;
                }

                int i = 1;
                blocksIntArray[0] = ((y << depth) + z << width) + x;
                long value = 0L;
                x = Width * Depth;
                while (i > 0)
                {
                    y = blocksIntArray[--i];
                    if (i == 0 && heightmapsLength > 0)
                    {
                        heightmaps.CopyTo(heightmapsLength++, blocksIntArray);
                        heightmapsLength--;
                        heightmaps.RemoveAt(heightmapsLength - 1);
                        i = blocksIntArray.Length;
                    }

                    z = y >> width & depthMinusOne;
                    int height1 = y >> width + depth;
                    int height2 = y & widthMinusOne;
                    int iterations;

                    for (iterations = height2; iterations > 0 && Blocks[y - 1] == blockToReplace; --iterations, --y) { }
                    while (height2 < Width && Blocks[y + height2 - iterations] == blockToReplace)
                        ++height2;

                    if (block == BlockTypes.Invalid && (iterations == 0 || height2 == Width - 1 || height1 == 0 || height1 == Height - 1 || z == 0 || z == Depth - 1))
                        return -1L;

                    int lastState1 = 0;
                    int lastState2 = 0;
                    int lastState3 = 0;

                    value += height2 - iterations;
                    for (; iterations < height2; ++iterations)
                    {
                        Blocks[y] = new BlockMetadata { ID = (sbyte)block };
                        if (z > 0)
                        {
                            sbyte state = (sbyte)((Blocks[y - Width] == blockToReplace) ? 1 : 0);
                            if (state != 0 && lastState1 == 0)
                            {
                                if (i == blocksIntArray.Length)
                                {
                                    heightmaps.CopyTo(heightmapsLength++, blocksIntArray);
                                    blocksIntArray.Dispose();
                                    blocksIntArray = new NativeArray<int>(1048576, Allocator.Temp);
                                    i = 0;
                                }
                                blocksIntArray[i++] = y - Width;
                            }
                            lastState1 = state;
                        }
                        if (z < Depth - 1)
                        {
                            sbyte state = (sbyte)((Blocks[y + Width] == blockToReplace) ? 1 : 0);
                            if (state != 0 && lastState2 == 0)
                            {
                                if (i == blocksIntArray.Length)
                                {
                                    heightmaps.CopyTo(heightmapsLength++, blocksIntArray);
                                    blocksIntArray.Dispose();
                                    blocksIntArray = new NativeArray<int>(1048576, Allocator.Temp);
                                    i = 0;
                                }
                                blocksIntArray[i++] = y + Width;
                            }
                            lastState2 = state;
                        }
                        if (height1 > 0)
                        {
                            sbyte b2 = Blocks[y - x].ID;
                            if (((int)block == (int)BlockTypes.Lava || (int)block == (int)BlockTypes.StillLava) && (b2 == (int)BlockTypes.Water || b2 == (int)BlockTypes.StillWater))
                                Blocks[y - x] = new BlockMetadata { ID = (sbyte)BlockTypes.Stone };

                            sbyte state = (sbyte)((b2 == (sbyte)blockToReplace) ? 1 : 0);

                            if (state != 0 && lastState3 == 0)
                            {
                                if (i == blocksIntArray.Length)
                                {
                                    heightmaps.CopyTo(heightmapsLength++, blocksIntArray);
                                    blocksIntArray.Dispose();
                                    blocksIntArray = new NativeArray<int>(1048576, Allocator.Temp);
                                    i = 0;
                                }
                                blocksIntArray[i++] = y - x;
                            }
                            lastState3 = state;
                        }
                        ++y;
                    }
                }
                heightmaps.Dispose();
                return value;
            }

            private void GenerateLava(Random random)
            {
                int n = Width * Depth * Height / 2000;
                for (int i = 0; i < n; ++i)
                {
                    int nextInt = random.NextInt(Width);

                    int min = Mathf.Min(
                        Mathf.Min(
                            random.NextInt(Properties[(int)JobProperties.GroundLevel]),
                            random.NextInt(Properties[(int)JobProperties.GroundLevel])),
                        Mathf.Min(
                            random.NextInt(Properties[(int)JobProperties.GroundLevel]),
                            random.NextInt(Properties[(int)JobProperties.GroundLevel])));

                    int nextInt2 = random.NextInt(Depth);
                    if (Blocks[(min * Depth + nextInt2) * Width + nextInt] == BlockTypes.Air)
                    {
                        long flood = Flood(nextInt, min, nextInt2, BlockTypes.Air, BlockTypes.Invalid);
                        if (flood > 0L && flood < 640L)
                            Flood(nextInt, min, nextInt2, BlockTypes.Invalid, BlockTypes.StillLava);
                        else
                            Flood(nextInt, min, nextInt2, BlockTypes.Invalid, BlockTypes.Air);
                    }
                }
            }

            private void SpawnLiquids(Random random)
            {
                BlockTypes block = GenerationTheme == GenerationTheme.Hell ? BlockTypes.StillLava : BlockTypes.StillWater;

                int iterations = Width * Depth * Height / 1000;
                for (int i = 0; i < iterations; ++i)
                {
                    int x = random.NextInt(Width);
                    int y = random.NextInt(Height);
                    int z = random.NextInt(Depth);
                    if (Blocks[(y * Depth + z) * Width + x] == BlockTypes.Air)
                    {
                        long flood;
                        if ((flood = Flood(x, y, z, 0, BlockTypes.Invalid)) > 0L && flood < 640L)
                            Flood(x, y, z, BlockTypes.Invalid, block);
                        else
                            Flood(x, y, z, BlockTypes.Invalid, BlockTypes.Air);
                    }
                }
            }

            private void Assemble(Random random)
            {
                int z1;
                int y1;
                int blockId;

                for (int height = 0; height < Width; height++)
                {
                    for (z1 = 0; z1 < Depth; z1++)
                    {
                        for (y1 = 0; y1 < Height; y1++)
                        {
                            blockId = 0;
                            if (y1 <= 1 && y1 < Properties[(int)JobProperties.GroundLevel] - 1 && Blocks[((y1 + 1) * Depth + z1) * Width + height] == BlockTypes.Air)
                                blockId = (int)BlockTypes.StillLava;
                            else if (y1 < Properties[(int)JobProperties.GroundLevel] - 1)
                                blockId = (int)BlockTypes.Bedrock;
                            else if (y1 < Properties[(int)JobProperties.GroundLevel])
                            {
                                if (Properties[(int)JobProperties.GroundLevel] > Properties[(int)JobProperties.WaterLevel] && DefaultFluid == BlockTypes.Water)
                                    blockId = (int)BlockTypes.Grass;
                                else
                                    blockId = (int)BlockTypes.Dirt;
                            }
                            else if (y1 < Properties[(int)JobProperties.WaterLevel])
                                blockId = (int)DefaultFluid;
                            Blocks[(y1 * Depth + z1) * Width + height] = new BlockMetadata { ID = (sbyte)blockId };
                            if (y1 == 1 && height != 0 && z1 != 0 && height != Width - 1 && z1 != Depth - 1)
                                y1 = Height - 2;
                        }
                    }
                }

                var heightMap = new NativeArray<int>(Width * Depth, Allocator.Temp);
                for (int i = 0; i < Width * Depth; i++)
                {
                    heightMap[i] = Height;
                }

                // TODO
                // Original: MinecraftIndev.RawWorld.Generate()

                NativeArray<sbyte> dataByteArray = new NativeArray<sbyte>(Blocks.Length, Allocator.Temp);
                NativeArray<int> heightmap = new NativeArray<int>(Width * Depth, Allocator.Temp);
                for (int i = 0; i < heightmap.Length; i++)
                {
                    heightmap[i] = Height;
                }
                
                //Lighting = new RawWorldLighting(this);
                var skylight = Properties[(int)JobProperties.SkylightSubtracted];
                int y;
                BlockMetadata block;
                for (int length = 0; length < Width; ++length)
                {
                    for (int z = 0; z < Depth; ++z)
                    {
                        for (y = Height - 1; y > 0 && BlockData[GetBlock(length, y, z).ID].LightOpacity == 0; --y)
                        { 

                        }

                        heightmap[length + z * Width] = y + 1;

                        for (y = 0; y < Height; ++y)
                        {
                            z1 = (y * Depth + z) * Width + length;
                            y1 = heightmap[length + z * Width];
                            y1 = ((y >= y1) ? skylight : 0);
                            block = Blocks[z1];
                            if (y1 < BlockData[block.ID].LightValue)
                                y1 = BlockData[block.ID].LightValue;

                            dataByteArray[z1] = (sbyte)((dataByteArray[z1] & 0xF0) + y1);
                        }
                    }
                }
                //Lighting.EnqueueUpdate(0, 0, 0, this.width, this.height, this.length);
            }

            public BlockMetadata GetBlock(int x, int y, int z)
            {
                x = Mathf.Clamp(x, 0, Width - 1);
                y = Mathf.Clamp(y, 0, Height - 1);
                z = Mathf.Clamp(z, 0, Depth - 1);

                return Blocks[GetAddress(x, y, z)];
            }

            public int GetAddress(int x, int y, int z) => (y * Depth + z) * Width + x;

        }
    }
}