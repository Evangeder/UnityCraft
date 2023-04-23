using System.IO;

namespace UnityCraft.Networking.Packets.Incoming
{
    public abstract class IncomingPacket
    {
        public abstract ReadedPacket ReadPacket(BinaryReader reader);

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