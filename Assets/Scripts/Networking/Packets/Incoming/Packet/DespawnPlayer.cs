using System.IO;

namespace UnityCraft.Networking.Packets.Incoming.Packet
{
    public class DespawnPlayer : IncomingPacket
    {
        public override ReadedPacket ReadPacket(BinaryReader reader)
        {
            return new ReadedPacket
            {
                PlayerId = reader.ReadSByte()
            };
        }
    }
}