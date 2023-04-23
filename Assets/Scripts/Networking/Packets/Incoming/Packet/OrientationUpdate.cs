using System.IO;

namespace UnityCraft.Networking.Packets.Incoming.Packet
{
    public class OrientationUpdate : IncomingPacket
    {
        public override ReadedPacket ReadPacket(BinaryReader reader)
        {
            return new ReadedPacket
            {
                PlayerId = reader.ReadSByte(),
                Yaw = reader.ReadByte(),
                Pitch = reader.ReadByte()
            };
        }
    }
}