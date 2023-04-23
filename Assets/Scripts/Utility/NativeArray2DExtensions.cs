using Unity.Collections;
using JacksonDunstan.NativeCollections;

namespace UnityCraft.Utility
{
    public static class NativeArray2DExtensions
    {
        /// <summary>
        /// Callable only inside of Job system. Do NOT call it outside of it.
        /// </summary>
        public static void CopyFrom(this NativeArray2D<int> target, int index, NativeArray<int> source)
        {
            if (target.Length0 > index)
            {
                for (int i = 0; i < source.Length; i++)
                {
                    target[index, i] = source[i];
                }
            }
        }

        /// <summary>
        /// Single use-case for Map Generator only.
        /// </summary>
        public static void RemoveAt(this NativeArray2D<int> array, int index)
        {
            for (int i = 0; i < array.Length1; i++)
            {
                array[index, i] = default;
            }
        }
    }
}