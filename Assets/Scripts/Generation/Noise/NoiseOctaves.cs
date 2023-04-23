using Unity.Mathematics;

namespace UnityCraft.Generation.Noise
{
    public struct GeneratorOctaves : INoiseGenerator
    {
        private NoisePerlin[] collection;
        private int octaves;

        public GeneratorOctaves(Random random, int octaves)
        {
            this.octaves = octaves;
            collection = new NoisePerlin[octaves];
            for (int i = 0; i < octaves; ++i)
                collection[i] = new NoisePerlin(random);
        }

        public float Noise(float x, float y) => Noise(new float2(x, y));
        public float Noise(float2 f2)
        {
            float result = 0.0f;
            float divisor = 1.0f;
            for (int i = 0; i < octaves; ++i)
            {
                result += collection[i].Noise(f2 / divisor) * divisor;
                divisor *= 2.0f;
            }
            return result;
        }
    }
}