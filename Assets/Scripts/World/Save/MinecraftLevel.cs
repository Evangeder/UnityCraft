namespace UnityCraft.World.Save
{
    using Components;

    public class IndevSaveFile
    {
        public About About { get; set; }
        public Environment Environment { get; set; }
        public Map Map { get; set; }
        public Entities Entities { get; set; }
        public TileEntities TileEntities { get; set; }
    }
}