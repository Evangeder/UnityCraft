using System.IO;

namespace UnityCraft.Networking.Packets.Incoming.Packet
{
    public class SpawnPlayer : IncomingPacket
    {
        public override ReadedPacket ReadPacket(BinaryReader reader)
        {
            return new ReadedPacket
            {
                PlayerId = reader.ReadSByte(),
                PlayerName = reader.ReadString(),
                Xf = ShortToFloat(reader.ReadInt16()),
                Yf = ShortToFloat(reader.ReadInt16()),
                Zf = ShortToFloat(reader.ReadInt16()),
                Yaw = reader.ReadByte(),
                Pitch = reader.ReadByte()
            };
        }
    }
}