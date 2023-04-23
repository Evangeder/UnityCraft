using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace UnityCraft.Networking
{
    using Settings;
    using Packets.Incoming;
    using Packets.Incoming.Packet;
    using Packets.Outgoing;
    using Packets.Outgoing.Packet;

    public class Client
    {
        static byte CURRENT_PROTOCOL_VERSION = 0x07;
        
        private Queue<(OutgoingPackets type, WrittenPacket data)> packetsToSend;
        private ProtocolType protocolType;
        private bool userAuthentication = false;

        private TcpClient server;

        public Client(ProtocolType protocolType, bool userAuthentication)
        { 
            this.protocolType = protocolType;
            this.userAuthentication = userAuthentication;
        }

        public void RunNetworking(string ip, int port)
        {
            IPAddress ipAddress = Dns.GetHostEntry(ip).AddressList[0];

            server.Connect(ipAddress, port);
            var stream = server.GetStream();
            var reader = new BinaryReader(stream);

            packetsToSend.Enqueue((OutgoingPackets.PlayerIdentification, new WrittenPacket
            {
                ProtocolVersion = CURRENT_PROTOCOL_VERSION,
                Username = "",
                VerificationKey = ""
            }));

            while (server.Connected)
            {
                if (packetsToSend.Count > 0)
                {
                    SendPacket();
                }

                switch ((IncomingPackets)stream.ReadByte())
                {
                    case IncomingPackets.ServerIdentification:
                        {
                            var packet = new ServerIdentification().ReadPacket(reader);

                            
                        }
                        break;

                    case IncomingPackets.Ping:
                        {
                            // do nothing?
                        }
                        break;

                    case IncomingPackets.LevelInitialize:
                        {
                            // TODO: reroute this to show map loading canvas
                        }
                        break;

                    case IncomingPackets.LevelDataChunk:
                        {
                            var packet = new LevelDataChunk().ReadPacket(reader);

                        }
                        break;

                    case IncomingPackets.LevelFinalize:
                        {
                            var packet = new LevelFinalize().ReadPacket(reader);

                        }
                        break;

                    case IncomingPackets.SetBlock:
                        {
                            var packet = new Packets.Incoming.Packet.SetBlock().ReadPacket(reader);

                        }
                        break;

                    case IncomingPackets.SpawnPlayer:
                        {
                            var packet = new SpawnPlayer().ReadPacket(reader);

                        }
                        break;

                    case IncomingPackets.TeleportPlayer:
                        {
                            var packet = new TeleportPlayer().ReadPacket(reader);

                        }
                        break;

                    case IncomingPackets.PositionOrientationUpdate:
                        {
                            var packet = new PositionOrientationUpdate().ReadPacket(reader);

                        }
                        break;

                    case IncomingPackets.PositionUpdate:
                        {
                            var packet = new PositionUpdate().ReadPacket(reader);

                        }
                        break;

                    case IncomingPackets.OrientationUpdate:
                        {
                            var packet = new OrientationUpdate().ReadPacket(reader);

                        }
                        break;

                    case IncomingPackets.DespawnPlayer:
                        {
                            var packet = new DespawnPlayer().ReadPacket(reader);

                        }
                        break;

                    case IncomingPackets.Message:
                        {
                            var packet = new IncomingMessage().ReadPacket(reader);

                        }
                        break;

                    case IncomingPackets.DisconnectPlayer:
                        {
                            var packet = new DisconnectPlayer().ReadPacket(reader);

                        }
                        break;

                    case IncomingPackets.UpdateUserType:
                        {
                            var packet = new UpdateUserType().ReadPacket(reader);

                        }
                        break;
                }
            }

            reader.Dispose();
            stream.Dispose();
            server.Dispose();
        }

        private void SendPacket()
        {
            var packet = packetsToSend.Dequeue();

            switch (packet.type)
            {
                case OutgoingPackets.PlayerIdentification:
                    {
                        new PlayerIdentification().Send(server, packet.data);
                    }
                    break;

                case OutgoingPackets.SetBlock:
                    {
                        new OutgoingSetBlock().Send(server, packet.data);
                        
                    }
                    break;

                case OutgoingPackets.PositionOrientation:
                    {
                        new PositionOrientation().Send(server, packet.data);
                    }
                    break;

                case OutgoingPackets.Message:
                    {
                        new Message().Send(server, packet.data);
                    }
                    break;
            }
        }
    }
}