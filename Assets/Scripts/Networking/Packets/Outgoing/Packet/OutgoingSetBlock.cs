using System.IO;
using System.Net.Sockets;

namespace UnityCraft.Networking.Packets.Outgoing.Packet
{
    public class OutgoingSetBlock : OutgoingPacket
    {
        public override void Send(TcpClient client, WrittenPacket packet)
        {
            using (var w = new BinaryWriter(client.GetStream()))
            {
                w.Write(packet.X);
                w.Write(packet.Y);
                w.Write(packet.Z);
                w.Write(packet.Mode);
                w.Write(packet.BlockType);
                w.Flush();
            }
        }
    }
}