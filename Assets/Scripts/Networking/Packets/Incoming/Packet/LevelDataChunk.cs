using System.IO;

namespace UnityCraft.Networking.Packets.Incoming.Packet
{
    public class LevelDataChunk : IncomingPacket
    {
        public override ReadedPacket ReadPacket(BinaryReader reader)
        {
            short length = reader.ReadInt16();
            return new ReadedPacket
            {
                ChunkLength = length,
                ChunkData = reader.ReadBytes(length),
                PercentComplete = reader.ReadByte()
            };
        }
    }
}