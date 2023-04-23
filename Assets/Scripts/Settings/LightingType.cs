using System.Runtime.InteropServices;

namespace UnityCraft.Settings
{
    public enum LightingType
    {
        /// <summary>
        /// Use classic minecraft's shading, which has only two states: Lit and Unlit.
        /// </summary>
        Classic,

        /// <summary>
        /// Use minecraft indev's lighting
        /// </summary>
        Indev,

        /// <summary>
        /// Use unity's internal shader to cast shadows.
        /// </summary>
        Realistic
    }
}