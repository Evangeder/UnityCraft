using UnityEngine;
using Unity.Mathematics;

namespace UnityCraft.Blocks
{
    /// <summary>
    /// All the data required for block to be rendered, as well as physics and light data.
    /// <br>Do not confuse this with <see cref="BlockMetadata"/> which holds information about <u><b>single block</b></u>, not a <u><b>blocktype</b></u>.</br>
    /// </summary>
    public readonly struct BlockDefinition
    {
        /// <summary>
        /// Block ID, this is required to assign correct block to correct place in array.
        /// <br>If multiple blocks are assigned to one ID, only the first one will initialize.</br>
        /// </summary>
        public ushort ID { get; }

        /// <summary>
        /// This defines a package name. Default is UnityCraft.
        /// <br>Example of full block name: <i>UnityCraft::Cobblestone</i></br>
        /// </summary>
        public string Namespace { get; }

        /// <summary>
        /// Block name, such as Air, Grass, etc.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Texture data.
        /// </summary>
        public BlockTextures Textures { get; }

        /// <summary>
        /// Sounds of footsteps.
        /// </summary>
        public AudioClip[] FootstepSounds { get; }

        /// <summary>
        /// This defines if the block is, for example: transparent, semitransparent, liquid or gives out light.
        /// </summary>
        public byte Flags { get; }

        /// <summary>
        /// How long should player mine the block.
        /// </summary>
        public float Hardness { get; }

        /// <summary>
        /// How resistant is the block to TNT and creeper explosions.
        /// </summary>
        public float BlastResistance { get; }

        /// <summary>
        /// How frequently should the block update, measured in ticks.
        /// </summary>
        public float PhysicsTime { get; }

        /// <summary>
        /// How much light the block should pass through.
        /// </summary>
        public int LightOpacity { get; }

        /// <summary>
        /// Is the block luminescent and by how much.
        /// </summary>
        public int LightValue { get; }

        public BlockDefinition(ushort id, string @namespace, string name, int2[] textures, AudioClip[] footstepSounds, bool solid, bool foliage = false, bool liquid = false, bool torch = false, bool usePhysics = false, float physicsTime = 0f, float hardness = 0f, float blastResistance = 0f, int lightOpacity = 0, int lightValue = 255)
        {
            ID = id;
            Namespace = @namespace;
            Name = name;
            FootstepSounds = footstepSounds;

            BlockTextures blockTextures = new BlockTextures(
                textures[(int)BlockTexturePositions.Up],
                textures[(int)BlockTexturePositions.Down],
                textures[(int)BlockTexturePositions.North],
                textures[(int)BlockTexturePositions.South],
                textures[(int)BlockTexturePositions.East],
                textures[(int)BlockTexturePositions.West]);

            Flags = 0;
            Flags |= (byte)(solid ? BlockFlags.Solid : 0);
            Flags |= (byte)(foliage ? BlockFlags.Foliage : 0);
            Flags |= (byte)(liquid ? BlockFlags.Liquid : 0);
            Flags |= (byte)(torch ? BlockFlags.Torch : 0);
            Flags |= (byte)(usePhysics ? BlockFlags.UsePhysics : 0);

            Textures = blockTextures;
            PhysicsTime = physicsTime;
            Hardness = hardness;
            BlastResistance = blastResistance;
            LightOpacity = lightOpacity;
            LightValue = lightValue;
        }
    }
}