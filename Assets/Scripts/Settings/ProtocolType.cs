using System.Runtime.InteropServices;

namespace UnityCraft.Settings
{
    /// <summary>
    /// Packet protocol type used for compatibility.
    /// </summary>
    public enum ProtocolType
    {
        /// <summary>
        /// Original minecraft's packet protocol
        /// <br><see href="https://wiki.vg/Classic_Protocol">Documentation</see></br>
        /// </summary>
        Classic,

        /// <summary>
        /// Compatibility with ClassiCube
        /// <br><see href="https://wiki.vg/Classic_Protocol_Extension">Documentation</see></br>
        /// </summary>
        ClassicExtension
    }
}