namespace UnityCraft.World.Save.Components
{
    public class Environment
    {
        public short TimeOfDay { get; set; }
        public byte SkyBrightness { get; set; }
        public int SkyColor { get; set; }
        public int FogColor { get; set; }
        public int CloudColor { get; set; }
        public short CloudHeight { get; set; }
        public byte SurroundingGroundType { get; set; }
        public short SurroundingGroundHeight { get; set; }
        public byte SurroundingWaterType { get; set; }
        public short SurroundingWaterHeight { get; set; }
    }
}