public class WrittenPacket
{
    public byte ProtocolVersion { get; set; }
    public string Username { get; set; }
    public string VerificationKey { get; set; }
    public sbyte PlayerId { get; set; }
    public short X { get; set; }
    public short Y { get; set; }
    public short Z { get; set; }
    public float Xf { get; set; }
    public float Yf { get; set; }
    public float Zf { get; set; }
    public byte Mode { get; set; }
    public byte BlockType { get; set; }
    public byte Yaw { get; set; }
    public byte Pitch { get; set; }
    public string Message { get; set; }
}
