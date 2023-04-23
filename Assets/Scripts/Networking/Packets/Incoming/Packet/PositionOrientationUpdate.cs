using System.IO;

namespace UnityCraft.Networking.Packets.Incoming.Packet
{
    public class PositionOrientationUpdate : IncomingPacket
    {
        public override ReadedPacket ReadPacket(BinaryReader reader)
        {
            return new ReadedPacket
            {
                PlayerId = reader.ReadSByte(),
                ChangeX = SByteToFloat(reader.ReadSByte()),
                ChangeY = SByteToFloat(reader.ReadSByte()),
                ChangeZ = SByteToFloat(reader.ReadSByte()),
                Yaw = reader.ReadByte(),
                Pitch = reader.ReadByte()
            };
        }
    }
}