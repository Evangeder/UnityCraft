using System.IO;

namespace UnityCraft.Networking.Packets.Incoming.Packet
{
    public class LevelFinalize : IncomingPacket
    {
        public override ReadedPacket ReadPacket(BinaryReader reader)
        {
            return new ReadedPacket
            {
                X = reader.ReadInt16(),
                Y = reader.ReadInt16(),
                Z = reader.ReadInt16()
            };
        }
    }
}