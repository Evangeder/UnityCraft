using System.IO;
using System.Net.Sockets;

namespace UnityCraft.Networking.Packets.Outgoing.Packet
{
    public class PlayerIdentification : OutgoingPacket
    {
        public override void Send(TcpClient client, WrittenPacket packet)
        {
            using (var w = new BinaryWriter(client.GetStream()))
            {
                w.Write(packet.ProtocolVersion);
                w.Write(packet.Username);
                w.Write(packet.VerificationKey);
                w.Write(new byte());
                w.Flush();
            }
        }
    }
}