using System.IO;
using System.Net.Sockets;

namespace UnityCraft.Networking.Packets.Outgoing.Packet
{
    public class PositionOrientation : OutgoingPacket
    {
        public override void Send(TcpClient client, WrittenPacket packet)
        {
            using (var w = new BinaryWriter(client.GetStream()))
            {
                w.Write(packet.PlayerId);
                w.Write(FloatToShort(packet.Xf));
                w.Write(FloatToShort(packet.Yf));
                w.Write(FloatToShort(packet.Zf));
                w.Write(packet.Yaw);
                w.Write(packet.Pitch);
                w.Flush();
            }
        }
    }
}