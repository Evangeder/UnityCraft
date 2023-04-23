namespace UnityCraft.Utility
{
    /// <summary>
    /// Blittable bool type for Native Containers (required to build with IL2CPP).
    /// </summary>
    [System.Serializable]
    public struct boolean
    {
        /// <summary>
        /// Either 1 when true, or 0 when false.
        /// </summary>
        public byte Value;

        public boolean(bool value)
            => Value = (byte)(value ? 1 : 0);

        public static implicit operator bool(boolean value)
            => value.Value == 1;

        public static implicit operator boolean(bool value)
            => new boolean(value);

        public override string ToString()
            => Value == 1 ? "true" : "false";
    }
}