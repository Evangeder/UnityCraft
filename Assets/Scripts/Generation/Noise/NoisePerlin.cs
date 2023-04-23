using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace UnityCraft.Generation.Noise
{
    public struct NoisePerlin : INoiseGenerator
    {
        private int[] permutations;

        public NoisePerlin(Random random)
        {
            permutations = new int[512];

            for (int i = 0; i < 256; ++i)
                permutations[i] = i;

            for (int i = 0; i < 256; ++i)
            {
                int j = random.NextInt(256 - i) + i;
                int k = permutations[i];
                permutations[i] = permutations[j];
                permutations[j] = k;
                permutations[i + 256] = permutations[i];
            }
        }

        private static float GenerateNoise(float n)
            => n * n * n * (n * (n * 6f - 15f) + 10f);

        private static float Lerp(float n, float n2, float n3)
            => n2 + n * (n3 - n2);

        private static float Grad(int n, float n2, float n3, float n4)
        {
            float n5 = ((n &= 0xF) < 8) ? n2 : n3;
            float n6 = (n < 4) ? n3 : ((n == 12 || n == 14) ? n2 : n4);
            return (((n & 0x1) == 0x0) ? n5 : (-n5)) + (((n & 0x2) == 0x0) ? n6 : (-n6));
        }

        public float Noise(float x, float y) => Noise(new float2(x, y));
        public float Noise(float2 f2)
        {
            int n6 = (int)Mathf.Floor(f2.x) & 0xFF;
            int n7 = (int)Mathf.Floor(f2.y) & 0xFF;
            int n8 = (int)Mathf.Floor(0f) & 0xFF;
            f2.x -= (int)Mathf.Floor(f2.x);
            f2.y -= (int)Mathf.Floor(f2.y);
            float n3 = 0f - (int)Mathf.Floor(0f);
            float noise1 = GenerateNoise(f2.x);
            float noise2 = GenerateNoise(f2.y);
            float noise3 = GenerateNoise(n3);
            int n9 = permutations[n6] + n7;
            int n10 = permutations[n9] + n8;
            n9 = permutations[n9 + 1] + n8;
            n6 = permutations[n6 + 1] + n7;
            n7 = permutations[n6] + n8;
            n6 = permutations[n6 + 1] + n8;

            return Lerp(noise3,
                    Lerp(noise2,
                        Lerp(noise1,
                            Grad(permutations[n10], f2.x, f2.y, n3),
                            Grad(permutations[n7], f2.x - 1f, f2.y, n3)),
                        Lerp(noise1,
                            Grad(permutations[n9], f2.x, f2.y - 1f, n3),
                            Grad(permutations[n6], f2.x - 1f, f2.y - 1f, n3))),
                    Lerp(noise2,
                        Lerp(noise1,
                            Grad(permutations[n10 + 1], f2.x, f2.y, n3 - 1f),
                            Grad(permutations[n7 + 1], f2.x - 1f, f2.y, n3 - 1f)),
                        Lerp(noise1,
                            Grad(permutations[n9 + 1], f2.x, f2.y - 1f, n3 - 1f),
                            Grad(permutations[n6 + 1], f2.x - 1f, f2.y - 1f, n3 - 1f))));
        }
    }
}