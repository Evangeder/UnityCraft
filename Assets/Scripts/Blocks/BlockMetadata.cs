using System;

namespace UnityCraft.Blocks
{
    /// <summary>
    /// Per-Block metadata information. Contains info about block ID, rotation, texture variant, light, etc.
    /// </summary>
    [Serializable]
    public struct BlockMetadata
    {
        /// <summary>
        /// Block ID. This uses <see cref="BlockDefinition"/>'s id to lookup all required block informations to render and work.
        /// </summary>
        public sbyte ID { get; set; }

        /// <summary>
        /// Block rotation and variant data, such as wool color or furnace orientation.
        /// </summary>
        public sbyte Metadata { get; set; }

        /// <summary>
        /// How lit is this block.
        /// </summary>
        public sbyte Light { get; set; }

        #region Constructors

        public BlockMetadata(sbyte id, sbyte metadata = 0, sbyte light = 0)
        {
            ID = id;
            Metadata = metadata;
            Light = light;
        }

        public BlockMetadata(BlockMetadata T)
        {
            this = T;
        }

        #endregion

        #region Operator overrides

        public static bool operator ==(BlockMetadata operand1, BlockMetadata operand2)
            => operand1.ID == operand2.ID;

        public static bool operator !=(BlockMetadata operand1, BlockMetadata operand2)
           => !(operand1.ID == operand2.ID);

        public static bool operator ==(BlockMetadata operand1, sbyte operand2)
            => operand1.ID == operand2;

        public static bool operator !=(BlockMetadata operand1, sbyte operand2)
            => operand1.ID != operand2;

        public static bool operator ==(BlockMetadata operand1, BlockTypes operand2)
            => operand1.ID == (sbyte)operand2;

        public static bool operator !=(BlockMetadata operand1, BlockTypes operand2)
            => operand1.ID != (sbyte)operand2;

        public static bool operator ==(sbyte operand1, BlockMetadata operand2)
            => operand1 == operand2.ID;

        public static bool operator !=(sbyte operand1, BlockMetadata operand2)
            => operand1 != operand2.ID;

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(BlockMetadata))
                return this == (BlockMetadata)obj;
            else
                return false;
        }

        public override int GetHashCode()
            => base.GetHashCode();

       

        public static BlockMetadata EmptyPhysicsTrigger()
            => new BlockMetadata
                {
                    ID = 0,
                };

        #endregion
    }
}