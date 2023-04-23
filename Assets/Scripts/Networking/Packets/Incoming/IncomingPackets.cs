namespace UnityCraft.Networking.Packets.Incoming
{
    public enum IncomingPackets
    {
        /// <summary>
        /// Response to a joining player. The user type indicates whether a player is an op (0x64) or not (0x00) which decides if the player can place water/lava. The protocol version is 0x07, unless you're using a client below 0.28.
        /// </summary>
        ServerIdentification = 0x00,

        /// <summary>
        /// Sent to clients periodically. The only way a client can disconnect at the moment is to force it closed, which does not let the server know. The ping packet is used to determine if the connection is still open.
        /// </summary>
        Ping = 0x01,

        /// <summary>
        /// Notifies the player of incoming level data.
        /// </summary>
        LevelInitialize = 0x02,

        /// <summary>
        /// Contains a chunk of the gzipped level data, to be concatenated with the rest. Chunk Data is up to 1024 bytes, padded with 0x00s if less.
        /// </summary>
        LevelDataChunk = 0x03,

        /// <summary>
        /// Sent after level data is complete and gives map dimensions. The y coordinate is how tall the map is.
        /// </summary>
        LevelFinalize = 0x04,

        /// <summary>
        /// Sent to indicate a block change by physics or by players. In the case of a player change, the server will also echo the block change back to the player who initiated it.
        /// </summary>
        SetBlock = 0x06,

        /// <summary>
        /// Sent to indicate where a new player is spawning in the world. This will set the player's spawn point.
        /// </summary>
        SpawnPlayer = 0x07,

        /// <summary>
        /// Sent with changes in player position and rotation. Used for sending initial position on the map, and teleportation. Some servers don't send relative packets, opting to only use this one.
        /// </summary>
        TeleportPlayer = 0x08,

        /// <summary>
        /// Sent with changes in player position and rotation. Sent when both position and orientation is changed at the same time.
        /// <br>Not required for server operation.</br>
        /// </summary>
        PositionOrientationUpdate = 0x09,

        /// <summary>
        /// Sent with changes in player position.
        /// <br>Not required for server operation.</br>
        /// </summary>
        PositionUpdate = 0x0a,

        /// <summary>
        /// Sent with changes in player rotation.
        /// <br>Not required for server operation.</br>
        /// </summary>
        OrientationUpdate = 0x0b,

        /// <summary>
        /// Sent to others when the player disconnects.
        /// </summary>
        DespawnPlayer = 0x0c,

        /// <summary>
        /// Messages sent by chat or from the console.
        /// <br><see href="https://wiki.vg/Chat#Old_system">Chat documentation</see></br>
        /// </summary>
        Message = 0x0d,

        /// <summary>
        /// Sent to a player when they're disconnected from the server.
        /// <br>1. "Cheat detected: Distance" - happens not only when setting tile too far away from the player (how far is maximum distance and how it is measured?), but also when player moves and then immediately builds.</br>
        /// <br>2. "Cheat detected: Tile type"</br>
        /// <br>3. "Cheat detected: Too much clicking!"</br>
        /// <br>4. "Cheat detected: Too much lag"</br>
        /// </summary>
        DisconnectPlayer = 0x0e,

        /// <summary>
        /// Sent when a player is opped/deopped. User type is 0x64 for op, 0x00 for normal user. This decides if the player can place water/lava/bedrock.
        /// </summary>
        UpdateUserType = 0x0f
    }
}