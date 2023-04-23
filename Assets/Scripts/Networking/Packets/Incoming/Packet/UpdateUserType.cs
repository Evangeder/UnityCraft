using System.IO;

namespace UnityCraft.Networking.Packets.Incoming.Packet
{
    public class UpdateUserType : IncomingPacket
    {
        public override ReadedPacket ReadPacket(BinaryReader reader)
        {
            return new ReadedPacket
            {
                UserType = reader.ReadByte()
            };
        }
    }
}