using System.IO;

namespace UnityCraft.Networking.Packets.Incoming.Packet
{
    public class DisconnectPlayer : IncomingPacket
    {
        public override ReadedPacket ReadPacket(BinaryReader reader)
        {
            return new ReadedPacket
            {
                DisconnectReason = reader.ReadString()
            };
        }
    }
}