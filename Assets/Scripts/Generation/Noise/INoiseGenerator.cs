using Unity.Mathematics;

namespace UnityCraft.Generation.Noise
{
    public interface INoiseGenerator
    {
        float Noise(float x, float y);
        float Noise(float2 f2);
    }
}