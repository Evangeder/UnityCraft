using Unity.Mathematics;

namespace UnityCraft.Generation.Noise
{
    public struct NoiseCombined : INoiseGenerator
    {
        private INoiseGenerator _gen1;
        private INoiseGenerator _gen2;

        public NoiseCombined(INoiseGenerator gen1, INoiseGenerator gen2)
        {
            _gen1 = gen1;
            _gen2 = gen2;
        }

        public float Noise(float x, float y) => Noise(new float2(x, y));
        public float Noise(float2 f2)
            => _gen1.Noise(new float2(f2.x + _gen2.Noise(f2), f2.y));
    }
}