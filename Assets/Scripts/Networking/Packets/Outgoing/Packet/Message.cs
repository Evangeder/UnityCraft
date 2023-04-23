using System.IO;
using System.Net.Sockets;

namespace UnityCraft.Networking.Packets.Outgoing.Packet
{
    public class Message : OutgoingPacket
    {
        public override void Send(TcpClient client, WrittenPacket packet)
        {
            using (var w = new BinaryWriter(client.GetStream()))
            {
                w.Write(packet.PlayerId);
                w.Write(packet.Message);
                w.Flush();
            }
        }
    }
}