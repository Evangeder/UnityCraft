namespace UnityCraft.Generation.Noise
{
    public class NoiseGeneratorCombined : NoiseGenerator
    {
        private NoiseGenerator noiseGenerator;
        private NoiseGenerator noiseGenerator2;

        public NoiseGeneratorCombined(NoiseGenerator noiseGenerator, NoiseGenerator noiseGenerator2)
        {
            this.noiseGenerator = noiseGenerator;
            this.noiseGenerator2 = noiseGenerator2;
        }

        public override double Noise(double n, double n2)
            => noiseGenerator.Noise(n + noiseGenerator2.Noise(n, n2), n2);
    }
}