namespace UnityCraft.World.Save.Components
{
    public class Map
    {
        public short Width { get; set; }
        public short Length { get; set; }
        public short Height { get; set; }
        public short[] Spawn { get; set; }
        public byte[] Blocks { get; set; }
        public byte[] Data { get; set; }
    }
}