namespace UnityCraft.Networking.Packets.Incoming
{
    public class ReadedPacket
    {
        public byte ProtocolVersion { get; set; }
        public string ServerName { get; set; }
        public string ServerMOTD { get; set; }
        public byte UserType { get; set; }
        public short ChunkLength { get; set; }
        public byte[] ChunkData { get; set; }
        public byte PercentComplete { get; set; }
        public short X { get; set; }
        public short Y { get; set; }
        public short Z { get; set; }
        public float Xf { get; set; }
        public float Yf { get; set; }
        public float Zf { get; set; }
        public byte BlockId { get; set; }
        public byte Yaw { get; set; }
        public byte Pitch { get; set; }
        public float ChangeX { get; set; }
        public float ChangeY { get; set; }
        public float ChangeZ { get; set; }
        public sbyte PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string Message { get; set; }
        public string DisconnectReason { get; set; }
    }
}