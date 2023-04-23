namespace UnityCraft.Networking.Packets.Outgoing
{
    public enum OutgoingPackets
    {
        /// <summary>
        /// Sent by a player joining a server with relevant information. The protocol version is 0x07, unless you're using a client below 0.28.
        /// </summary>
        PlayerIdentification = 0x00,

        /// <summary>
        /// Sent when a user changes a block. The mode field indicates whether a block was created (0x01) or destroyed (0x00).
        /// <br>Block type is always the type player is holding, even when destroying.</br>
        /// <br>Client assumes that this command packet always succeeds, and so draws the new block immediately. To disallow block creation, server must send back Set Block packet with the old block type.</br>
        /// </summary>
        SetBlock = 0x05,

        /// <summary>
        /// Sent frequently (even while not moving) by the player with the player's current location and orientation. Player ID is always -1 (255), referring to itself.
        /// </summary>
        PositionOrientation = 0x08,

        /// <summary>
        /// Contain chat messages sent by player. Player ID is always -1 (255), referring to itself.
        /// <br><see href="https://wiki.vg/Chat#Old_system">Chat documentation</see></br>
        /// </summary>
        Message = 0x0d
    }
}