namespace UnityCraft.World.Save.Components
{
    public class Entities
    {
        public string id { get; set; }
        public float[] Pos { get; set; }
        public float[] Rotation { get; set; }
        public float[] Motion { get; set; }
        public float FallDistance { get; set; }
        public short Health { get; set; }
        public short AttackTime { get; set; }
        public short HurtTime { get; set; }
        public short DeathTime { get; set; }
        public short Air { get; set; }
        public short Fire { get; set; }
        public int Score { get; set; }
        public Item Inventory { get; set; }
    }
}