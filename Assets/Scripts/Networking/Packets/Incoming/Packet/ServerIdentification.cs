using System.IO;

namespace UnityCraft.Networking.Packets.Incoming.Packet
{
    public class ServerIdentification : IncomingPacket
    {
        public override ReadedPacket ReadPacket(BinaryReader reader)
        {
            return new ReadedPacket
            {
                ProtocolVersion = reader.ReadByte(),
                ServerName = reader.ReadString(),
                ServerMOTD = reader.ReadString(),
                UserType = reader.ReadByte()
            };
        }
    }
}