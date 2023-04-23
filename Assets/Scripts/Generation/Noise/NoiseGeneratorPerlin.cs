using System;

namespace UnityCraft.Generation.Noise
{
    public class NoiseGeneratorPerlin : NoiseGenerator
    {
        private int[] permutations;

        public NoiseGeneratorPerlin() : this(new Random()) { }
        public NoiseGeneratorPerlin(Random random)
        {
            permutations = new int[512];

            for (int i = 0; i < 256; ++i)
                permutations[i] = i;

            for (int i = 0; i < 256; ++i)
            {
                int n = random.Next(256 - i) + i;
                int n2 = permutations[i];
                permutations[i] = permutations[n];
                permutations[n] = n2;
                permutations[i + 256] = permutations[i];
            }
        }

        private static double GenerateNoise(double n)
            => n * n * n * (n * (n * 6.0 - 15.0) + 10.0);

        private static double Lerp(double n, double n2, double n3)
            => n2 + n * (n3 - n2);

        private static double Grad(int n, double n2, double n3, double n4)
        {
            double n5 = ((n &= 0xF) < 8) ? n2 : n3;
            double n6 = (n < 4) ? n3 : ((n == 12 || n == 14) ? n2 : n4);
            return (((n & 0x1) == 0x0) ? n5 : (-n5)) + (((n & 0x2) == 0x0) ? n6 : (-n6));
        }

        public override double Noise(double n5, double n4)
        {
            int n6 = (int)Math.Floor(n5) & 0xFF;
            int n7 = (int)Math.Floor(n4) & 0xFF;
            int n8 = (int)Math.Floor(0.0) & 0xFF;
            n5 -= (int)Math.Floor(n5);
            n4 -= (int)Math.Floor(n4);
            double n3 = 0.0 - (int)Math.Floor(0.0);
            double noise1 = GenerateNoise(n5);
            double noise2 = GenerateNoise(n4);
            double noise3 = GenerateNoise(n3);
            int n9 = permutations[n6] + n7;
            int n10 = permutations[n9] + n8;
            n9 = permutations[n9 + 1] + n8;
            n6 = permutations[n6 + 1] + n7;
            n7 = permutations[n6] + n8;
            n6 = permutations[n6 + 1] + n8;
            return Lerp(noise3, Lerp(noise2,
                    Lerp(noise1,
                        Grad(permutations[n10], n5, n4, n3),
                        Grad(permutations[n7], n5 - 1.0, n4, n3)),
                    Lerp(noise1,
                        Grad(permutations[n9], n5, n4 - 1.0, n3),
                        Grad(permutations[n6], n5 - 1.0, n4 - 1.0, n3))),
                    Lerp(noise2,
                    Lerp(noise1,
                        Grad(permutations[n10 + 1], n5, n4, n3 - 1.0),
                        Grad(permutations[n7 + 1], n5 - 1.0, n4, n3 - 1.0)),
                    Lerp(noise1, Grad(permutations[n9 + 1], n5, n4 - 1.0, n3 - 1.0),
                    Grad(permutations[n6 + 1], n5 - 1.0, n4 - 1.0, n3 - 1.0))));
        }
    }
}