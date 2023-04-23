using System;

namespace UnityCraft.Generation.Noise
{
    public class NoiseGeneratorOctaves : NoiseGenerator
    {
        private NoiseGeneratorPerlin[] generatorCollection;
        private int octaves;

        public NoiseGeneratorOctaves(Random random, int octaves)
        {
            this.octaves = octaves;
            generatorCollection = new NoiseGeneratorPerlin[octaves];
            for (int i = 0; i < octaves; ++i)
                generatorCollection[i] = new NoiseGeneratorPerlin(random);
        }

        public override double Noise(double n, double n2)
        {
            double n3 = 0.0;
            double n4 = 1.0;
            for (int i = 0; i < octaves; ++i)
            {
                n3 += generatorCollection[i].Noise(n / n4, n2 / n4) * n4;
                n4 *= 2.0;
            }
            return n3;
        }
    }
}