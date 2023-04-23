using System.IO;
using System.Net.Sockets;

namespace UnityCraft.Networking.Packets.Outgoing
{
    public abstract class OutgoingPacket
    {
        public abstract void Send(TcpClient client, WrittenPacket packet);

        protected float SByteToFloat(sbyte value)
        {
            return ((float)value) / 32;
        }

        protected sbyte FloatToSByte(float value)
        {
            return (sbyte)(value * 32);
        }

        protected float ShortToFloat(short value)
        {
            return ((float)value) / 32;
        }

        protected short FloatToShort(float value)
        {
            return (short)(value * 32);
        }
    }
}